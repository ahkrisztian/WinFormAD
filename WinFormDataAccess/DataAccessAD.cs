using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;

namespace WinFormDataAccess
{
    public class DataAccessAD : IDataAccessAD
    {
        public async Task<DirectoryEntry> ConnectToAD(string path, string username, string password)
        {
            try
            {
                DirectoryEntry ldapConnection = new DirectoryEntry(path, username, password);

                ldapConnection.AuthenticationType = AuthenticationTypes.Secure;


                if (ldapConnection is not null)
                {
                    return ldapConnection;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
