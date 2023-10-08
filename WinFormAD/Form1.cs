using System.DirectoryServices;
using WinFormDataAccess;

namespace WinFormAD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        public void button1_Click(object sender, EventArgs e)
        {

            try
            {
                DirectoryEntry ldapConnection = new DirectoryEntry(serverTextbox.Text, nameTextbox.Text, passwordTextbox.Text);

                //DirectoryEntry deUser = new DirectoryEntry($"LDAP://cn={serverTextbox.Text},cn=Users,dc={serverTextbox.Text},dc=de");

                if (ldapConnection != null)
                {
                    ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

                    DirectorySearcher search = new DirectorySearcher(ldapConnection);

                    search.Filter = "(cn=" + searchTextbox.Text + ")";

                    SearchResult result = search.FindOne();

                    if (result is not null)
                    {
                        string displayName = result.Properties["displayName"][0].ToString();

                        resultTextbox.Text = displayName;
                    }
                    else
                    {
                        resultTextbox.Text = "User does not exists";
                    }
                }
            }
            catch (Exception)
            {
                resultTextbox.Text = "Error";
            }


        }

        private async void connectToADButton_Click(object sender, EventArgs e)
        {
            IDataAccessAD dataAccessAD = new DataAccessAD();

            var result = await dataAccessAD.ConnectToAD(serverTextbox.Text, nameTextbox.Text, passwordTextbox.Text);

            if (result is not null)
            {
                connectedCheckBox.Checked = true;
            }

            MessageBox.Show("Error");
        }

        private void connectedCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (connectedCheckBox.Checked)
            {
                searchGroupBox.Visible = true;
            }
            else
            {
                searchGroupBox.Visible = false;
            }
        }
    }
}