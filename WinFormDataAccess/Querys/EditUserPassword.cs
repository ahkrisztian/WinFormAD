using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Runtime.Versioning;

namespace WinFormDataAccess.Querys;

[SupportedOSPlatform("windows")]
public class EditUserPassword : IEditUserPassword
{
    //
    public bool CheckPasswordNeverExpires(DirectoryEntry direntry, string queryusername)
    {
        try
        {
            DirectoryEntry userDistinguishedName = userDistingushedName(direntry, queryusername);

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

    public bool CheckPasswordMustBeChangeNextLogin(DirectoryEntry direntry, string queryusername)
    {
        try
        {
            DirectoryEntry userDistinguishedName = userDistingushedName(direntry, queryusername);

            // Get the current value of pwdLastSet
            object pwdLastSet = userDistinguishedName.Properties["pwdLastSet"].Value;


            // Check if pwdLastSet is 0, which means the user must change their password at next login
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
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);

        }
    }

    public string SetUserPasswordNextLogon(DirectoryEntry direntry, string queryusername, bool checkedCheckbox)
    {
        try
        {
            DirectoryEntry userDistinguishedName = userDistingushedName(direntry, queryusername);

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

            throw;
        }
    }

    public string SetUserPasswordNeverExpires(DirectoryEntry direntry, string queryusername, bool checkedCheckbox)
    {
        try
        {
            DirectoryEntry userDistinguishedName = userDistingushedName(direntry, queryusername);

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

                    if (CheckPasswordNeverExpires(direntry, queryusername))
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

                    if (!CheckPasswordNeverExpires(direntry, queryusername))
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

            throw;
        }
    }

    private DirectoryEntry userDistingushedName(DirectoryEntry direntry, string queryusername)
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

                if(userEntry is not null)
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

            throw;
        }
    }
}
