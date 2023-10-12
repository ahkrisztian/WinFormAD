using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using WinFormDataAccess;
using WinFormDataAccess.Querys;

namespace WinFormAD
{
    public partial class Form1 : Form
    {
        private DirectoryEntry direntry;
        private IEditUserPassword userPassword;

        bool allstasuesarechekedNever;
        bool allstasuesarechekedNext;

        private readonly IPrincipialContextDataAccess principialContextDataAccess;
        private readonly IDataAccessAD dataAccessAD;
        private readonly ISearchUserAD searchUserAD;
        private readonly IEditUserPassword editUserPassword;
        private readonly ISearchOU searchOU;

        public Form1(IDataAccessAD dataAccessAD,
                     ISearchUserAD searchUserAD,
                     IEditUserPassword editUserPassword, ISearchOU searchOU)
        {
            this.dataAccessAD = dataAccessAD;
            this.searchUserAD = searchUserAD;
            this.editUserPassword = editUserPassword;
            this.searchOU = searchOU;
            InitializeComponent();

            updateNaverExpCheckBox.Click -= updateNaverExpCheckBox_CheckedChanged;
            updateNExtLoginCheckBox1.Click -= updateNExtLoginCheckBox1_CheckedChanged;

        }

        public void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string result = searchUserAD.QueryUserAD(direntry, searchTextbox.Text);

                if (result != "User does not exists")
                {
                    resultTextbox.Text = result;

                    checkPasswordStatus();

                    passwordGroupBox.Visible = true;

                }
                else
                {
                    resultTextbox.Text = "No result";
                    passwordGroupBox.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void connectToADButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = dataAccessAD.ConnectToAD(passwordTextbox.Text);

                if (result.NativeGuid is not null)
                {
                    direntry = result;

                    connectedCheckBox.Checked = true;
                    passwordTextbox.Enabled = false;
                    serverTextbox.Enabled = false;
                    nameTextbox.Enabled = false;
                    connectedCheckBox.Enabled = false;

                    connectToADButton.Visible = false;
                    disconnectADButton.Visible = true;

                    List<string> ouresults = searchOU.SearchOrganizationalUnits(passwordTextbox.Text);

                    if (ouresults.Count > 0)
                    {
                        OrganizationalUnits.Items.Clear();
                        OrganizationalUnits.Items.AddRange(ouresults.ToArray());
                        OrganizationalUnits.Visible = true;
                    }


                }
                else
                {
                    MessageBox.Show("Connection Error DirectoryEntry");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                searchGroupBox.Visible = false;
                connectedCheckBox.Checked = false;
            }
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

        private void checkPasswordStatus()
        {
            allstasuesarechekedNever = true;
            allstasuesarechekedNext = true;

            if (editUserPassword is not null)
            {
                userPassword = editUserPassword;

                bool resultNeverExpires = userPassword.CheckPasswordNeverExpires(direntry, searchTextbox.Text);
                bool resultNextLogin = userPassword.CheckPasswordMustBeChangeNextLogin(direntry, searchTextbox.Text);

                if (resultNeverExpires)
                {
                    updateNaverExpCheckBox.Checked = resultNeverExpires;
                    updateNExtLoginCheckBox1.Enabled = false;
                    updateNaverExpCheckBox.Enabled = true;
                }
                else if (resultNextLogin)
                {
                    updateNExtLoginCheckBox1.Checked = resultNextLogin;
                    updateNaverExpCheckBox.Enabled = false;
                    updateNExtLoginCheckBox1.Enabled = true;
                }
                else
                {
                    updateNaverExpCheckBox.Enabled = true;
                    updateNExtLoginCheckBox1.Enabled = true;

                    updateNaverExpCheckBox.Checked = resultNeverExpires;
                    updateNExtLoginCheckBox1.Checked = resultNextLogin;
                }
                allstasuesarechekedNever = false;
                allstasuesarechekedNext = false;

            }
            else
            {
                MessageBox.Show("Connection Error PrincipalContext");
            }
        }

        private void updateNExtLoginCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!allstasuesarechekedNext)
            {
                if (!updateNaverExpCheckBox.Checked)
                {
                    string result = userPassword.SetUserPasswordNextLogon(direntry, searchTextbox.Text, updateNExtLoginCheckBox1.Checked);

                    MessageBox.Show(result);

                    checkPasswordStatus();
                }
                else
                {
                    updateNaverExpCheckBox.Enabled = false;
                    MessageBox.Show("The value of User password never expires musst be false");

                    checkPasswordStatus();
                }
            }
            else
            {
                allstasuesarechekedNext = false;
            }
        }

        private void updateNaverExpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!allstasuesarechekedNever)
            {
                if (!updateNExtLoginCheckBox1.Checked)
                {
                    string result = userPassword.SetUserPasswordNeverExpires(direntry, searchTextbox.Text, updateNaverExpCheckBox.Checked);

                    MessageBox.Show(result);
                    checkPasswordStatus();
                }
                else
                {
                    updateNExtLoginCheckBox1.Enabled = false;
                    MessageBox.Show("The value of User musst change password at next logon musst be false");
                    checkPasswordStatus();
                }
            }
            else
            {
                allstasuesarechekedNever = false;
            }
        }

        private void disconnectADButton_Click(object sender, EventArgs e)
        {
            using (direntry)
            {
                direntry.Dispose();
                MessageBox.Show("Disconnected");
            }

            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void OrganizationalUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            ouUsersListBox.Items.Clear();

            string ou = OrganizationalUnits.SelectedItem.ToString();

            List<string> ouResult = searchOU.SearchMembersOfOrganizationalUnits(passwordTextbox.Text, ou);

            if (ouResult.Count > 0)
            {
                ouUsersListBox.Items.AddRange(ouResult.ToArray());
                ouUsersGroupBox.Visible = true;
            }
        }

        private void ouUsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user = ouUsersListBox.SelectedItem.ToString();

            searchTextbox.Text = user;

            if (serverTextbox.Text != "")
            {
                searchButton.PerformClick();
            }
        }
    }
}