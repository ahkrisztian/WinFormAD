using System;
using System.DirectoryServices;
using System.Globalization;
using System.Runtime.Versioning;
using System.Threading;

namespace WinFormDataAccess.Querys;

[SupportedOSPlatform("windows")]
public class EditUserPassword : IEditUserPassword
{
    private readonly IDataAccessAD dataAccessAD;

    public EditUserPassword(IDataAccessAD dataAccessAD)
    {
        this.dataAccessAD = dataAccessAD;
    }
    //
    public async Task<bool> CheckPasswordNeverExpires(string queryusername, CancellationToken cancellationToken)
    {
        using (DirectoryEntry direntry = await dataAccessAD.ConnectToAD(cancellationToken))
        {
            try
            {
                DirectoryEntry userDistinguishedName = await userDistingushedName(queryusername, cancellationToken);

                if (userDistinguishedName != null)
                {
                    int userAccountControl = (int)userDistinguishedName.Properties["userAccountControl"].Value;

                    // Check if the "Password Never Expires" flag is set
                    bool passwordNeverExpires = (userAccountControl & 0x10000) == 0x10000;

                    return passwordNeverExpires;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
            
    }

    public async Task<bool> CheckPasswordMustBeChangeNextLogin(string queryusername, CancellationToken cancellationToken)
    {
        using (DirectoryEntry direntry = await dataAccessAD.ConnectToAD(cancellationToken))
        {

            try
            {
                DirectoryEntry userDistinguishedName = await userDistingushedName(queryusername, cancellationToken);

                if (userDistinguishedName != null)
                {
                    long result = ReturnPasswordLastSetObject(userDistinguishedName);

                    if (result == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else { return false; }

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);

            }
        }

    }

    public async Task<string> SetUserPasswordNextLogon(string queryusername, bool checkedCheckbox, CancellationToken cancellationToken)
    {
        using (DirectoryEntry direntry = await dataAccessAD.ConnectToAD(cancellationToken))
        {
            try
            {
                DirectoryEntry userDistinguishedName = await userDistingushedName(queryusername, cancellationToken);

                if (userDistinguishedName is not null)
                {

                    if (checkedCheckbox)
                    {
                        userDistinguishedName.Properties["pwdLastSet"].Value = 0;

                        userDistinguishedName.CommitChanges();

                        return "User must change at password next logon: True";
                    }
                    else
                    {
                        userDistinguishedName.Properties["pwdLastSet"].Value = -1;

                        userDistinguishedName.CommitChanges();

                        return "User must change at password next logon: False";
                    }
                }
                else
                {
                    return "No result";
                }
            }
            catch (Exception)
            {
                return "No result";
                throw;
            }
        }
            
    }

    public async Task<string> SetUserPasswordNeverExpires(string queryusername, bool checkedCheckbox, CancellationToken cancellationToken)
    {
        using (DirectoryEntry direntry = await dataAccessAD.ConnectToAD(cancellationToken))
        {
            try
            {
                DirectoryEntry userDistinguishedName = await userDistingushedName(queryusername, cancellationToken);

                if (userDistinguishedName is not null)
                {
                    int userAccountControl = (int)userDistinguishedName.Properties["userAccountControl"].Value;

                    if (checkedCheckbox)
                    {
                        const int PASSWORD_NEVER_EXPIRES = 0x10000;
                        userAccountControl = userAccountControl | PASSWORD_NEVER_EXPIRES;

                        // Update the userAccountControl attribute
                        userDistinguishedName.Properties["userAccountControl"].Value = userAccountControl;

                        // Commit the changes to Active Directory
                        userDistinguishedName.CommitChanges();

                        if (await CheckPasswordNeverExpires(queryusername, cancellationToken))
                        {
                            return "User password never expires set to: True";
                        }
                        else
                        {
                            return "Error";
                        }


                    }
                    else
                    {
                        // Set the "Password Never Expires" flag by removing the 0x10000 value
                        userAccountControl = userAccountControl & ~0x10000;

                        // Update the userAccountControl attribute
                        userDistinguishedName.Properties["userAccountControl"].Value = userAccountControl;

                        // Commit the changes to Active Directory
                        userDistinguishedName.CommitChanges();

                        if (!await CheckPasswordNeverExpires(queryusername, cancellationToken))
                        {
                            return "User password never expires set to: False";
                        }
                        else
                        {
                            return "Error";
                        }

                    }
                }
                else
                {
                    return "No result";
                }
            }
            catch (Exception)
            {
                return "No result";
                throw;
            }
        }
        
    }

    public string UserPasswordLastSetDateTime(DirectoryEntry user)
    {
        try
        {
            if (user != null)
            {
                long pwLastSetDate = ReturnPasswordLastSetObject(user);

                //if the last password set datetime = {1601. 01. 01. 0:00:00}
                //the admin set the new password and the user must change it
                //if the user change the password at the next logon the datetime will be a normal datetime like { 2023. 10. 15. 8:46:17 }
                DateTime pwdLastSetDateTime = DateTime.FromFileTime(pwLastSetDate);

                DateTime reference = DateTime.ParseExact("1601. 01. 01. 1:00:00", "yyyy. MM. dd. H:mm:ss", CultureInfo.InvariantCulture);

                if (pwdLastSetDateTime != reference)
                {
                    return $"User password updated at: {pwdLastSetDateTime}";
                }

                return "User password updated by Admin and waiting to set by a User";
            }
            else
            {
                return "No result";
            }
        }
        catch (Exception)
        {
            return "No result";
        }

            
    }

    public async Task<string> SetNewPassword(string username, string newPassword, CancellationToken cancellationToken)
    {
        using (DirectoryEntry direntry = await dataAccessAD.ConnectToAD(cancellationToken))
        {
            try
            {
                DirectorySearcher searcher = new DirectorySearcher(direntry);
                searcher.Filter = $"(&(objectClass=user)(sAMAccountName={username}))";

                SearchResult result = searcher.FindOne();

                if (result != null)
                {
                    DirectoryEntry user = result.GetDirectoryEntry();

                    if (user != null)
                    {
                        user.Invoke("SetPassword", new object[] { newPassword });
                        user.Properties["pwdLastSet"].Value = 0;
                        user.CommitChanges();

                        return "User password updated by Admin and waiting to set by a User";
                    }
                    else
                    {
                        return "No result";
                    }
                }
                else
                {
                    return "No result";
                }
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }
    }

    private async Task<DirectoryEntry> userDistingushedName(string queryusername, CancellationToken cancellationToken)
    {
        using (DirectoryEntry direntry = await dataAccessAD.ConnectToAD(cancellationToken))
        {
            try
            {
                DirectorySearcher search = new DirectorySearcher(direntry);

                search.Filter = $"(samaccountname={queryusername})";

                SearchResult? result = search.FindOne();

                if (result is not null)
                {

                    string userDistinguishedName = result.Properties["distinguishedName"][0].ToString();

                    search.Filter = "(distinguishedName=" + userDistinguishedName + ")";

                    var distinguishedresult = search.FindOne();

                    DirectoryEntry userEntry = distinguishedresult.GetDirectoryEntry();

                    if (userEntry is not null)
                    {
                        return userEntry;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

            
    }

    private long ReturnPasswordLastSetObject(DirectoryEntry userDistinguishedName)
    {
        long output = 0;

        try
        {
            object pwdLastSet = userDistinguishedName.Properties["pwdLastSet"].Value;

            if (pwdLastSet != null)
            {
                var comType = pwdLastSet.GetType();

                int highPart = (int)comType.InvokeMember("HighPart", System.Reflection.BindingFlags.GetProperty, null, pwdLastSet, null);
                int lowPart = (int)comType.InvokeMember("LowPart", System.Reflection.BindingFlags.GetProperty, null, pwdLastSet, null);

                long pwdLastSetresult = ((long)highPart << 32) | (uint)lowPart;

                output = pwdLastSetresult;

                return output;
            }
            else { return output; }
        }
        catch (Exception)
        {
            return output;
        }
    }
}
