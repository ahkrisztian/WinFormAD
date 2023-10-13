using System;
using System.DirectoryServices;
using System.Runtime.Versioning;

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
    public bool CheckPasswordNeverExpires(string queryusername)
    {
        using (DirectoryEntry direntry = dataAccessAD.ConnectToAD())
        {
            try
            {
                DirectoryEntry userDistinguishedName = userDistingushedName(queryusername);

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

    public bool CheckPasswordMustBeChangeNextLogin(string queryusername)
    {
        using (DirectoryEntry direntry = dataAccessAD.ConnectToAD())
        {

            try
            {
                DirectoryEntry userDistinguishedName = userDistingushedName(queryusername);

                // Get the current value of pwdLastSet
                if (userDistinguishedName != null)
                {
                    object pwdLastSet = userDistinguishedName.Properties["pwdLastSet"].Value;

                    if (pwdLastSet != null)
                    {
                        // Use reflection to access the values
                        var comType = pwdLastSet.GetType();

                        int highPart = (int)comType.InvokeMember("HighPart", System.Reflection.BindingFlags.GetProperty, null, pwdLastSet, null);
                        int lowPart = (int)comType.InvokeMember("LowPart", System.Reflection.BindingFlags.GetProperty, null, pwdLastSet, null);

                        long pwdLastSetresult = ((long)highPart << 32) | (uint)lowPart;

                        // Check if pwdLastSet is 0, which means the user must change their password at next login
                        if (pwdLastSetresult == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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

    public string SetUserPasswordNextLogon(string queryusername, bool checkedCheckbox)
    {
        using (DirectoryEntry direntry = dataAccessAD.ConnectToAD())
        {
            try
            {
                DirectoryEntry userDistinguishedName = userDistingushedName(queryusername);

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

    public string SetUserPasswordNeverExpires(string queryusername, bool checkedCheckbox)
    {
        using (DirectoryEntry direntry = dataAccessAD.ConnectToAD())
        {
            try
            {
                DirectoryEntry userDistinguishedName = userDistingushedName(queryusername);

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

                        if (CheckPasswordNeverExpires(queryusername))
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

                        if (!CheckPasswordNeverExpires(queryusername))
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

    public string SetNewPassword(string username, string newPassword)
    {
        using (DirectoryEntry direntry = dataAccessAD.ConnectToAD())
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
                        return "User password updated";
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

    private DirectoryEntry userDistingushedName(string queryusername)
    {
        using (DirectoryEntry direntry = dataAccessAD.ConnectToAD())
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


}
