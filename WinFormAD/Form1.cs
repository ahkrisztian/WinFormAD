using Microsoft.Extensions.Configuration;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using WindiwsFormAdModels.UserModels;
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

        public UserAD user {  get; set; }
        public string userName { get; set; }

        private readonly IDataAccessAD dataAccessAD;
        private readonly IConfiguration configuration;
        private readonly ISearchUserAD searchUserAD;
        private readonly IEditUserPassword editUserPassword;
        private readonly ISearchOU searchOU;

        public Form1(IDataAccessAD dataAccessAD, IConfiguration configuration,
                     ISearchUserAD searchUserAD,
                     IEditUserPassword editUserPassword, ISearchOU searchOU)
        {
            this.dataAccessAD = dataAccessAD;
            this.configuration = configuration;
            this.searchUserAD = searchUserAD;
            this.editUserPassword = editUserPassword;
            this.searchOU = searchOU;

            InitializeComponent();

            updateNaverExpCheckBox.Click -= updateNaverExpCheckBox_CheckedChanged;
            updateNExtLoginCheckBox1.Click -= updateNExtLoginCheckBox1_CheckedChanged;

            serverTextbox.Text = configuration["ActiveDirectory:Server"];
            nameTextbox.Text = configuration["ActiveDirectory:Username"];


        }

        public async void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                user = await searchUserAD.QueryUserAD(searchTextbox.Text);

                if (user is not null)
                {
                    resultTextbox.Text = user.DisplayName;

                    checkPasswordStatus();

                    passwordGroupBox.Visible = true;
                    setNewPasswordGroupBox.Visible = true;

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

        private async void connectToADButton_Click(object sender, EventArgs e)
        {
            try
            {
                dataAccessAD.SetThePassword(passwordTextbox.Text, serverTextbox.Text, nameTextbox.Text);
                var result = await dataAccessAD.ConnectToAD();

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


                    List<string> ouresults = await searchOU.SearchOrganizationalUnits();

                    if (ouresults.Count > 0)
                    {
                        OrganizationalUnits.Items.Clear();
                        OrganizationalUnits.Items.AddRange(ouresults.ToArray());
                        ouGroupBox.Visible = true;
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

        private async void checkPasswordStatus()
        {
            allstasuesarechekedNever = true;
            allstasuesarechekedNext = true;

            if (editUserPassword is not null)
            {
                userPassword = editUserPassword;

                bool resultNeverExpires = await userPassword.CheckPasswordNeverExpires(searchTextbox.Text);
                bool resultNextLogin = await userPassword.CheckPasswordMustBeChangeNextLogin(searchTextbox.Text);

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

        private async void updateNExtLoginCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!allstasuesarechekedNext)
            {
                if (!updateNaverExpCheckBox.Checked)
                {
                    string result = await userPassword.SetUserPasswordNextLogon(searchTextbox.Text, updateNExtLoginCheckBox1.Checked);

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

        private async void updateNaverExpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!allstasuesarechekedNever)
            {
                if (!updateNExtLoginCheckBox1.Checked)
                {
                    string result = await userPassword.SetUserPasswordNeverExpires(searchTextbox.Text, updateNaverExpCheckBox.Checked);

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

        private async void OrganizationalUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            ouUsersListBox.Items.Clear();

            string ou = OrganizationalUnits.SelectedItem.ToString();

            var ouResult = await searchOU.SearchMembersOfOrganizationalUnits(ou);

            if (ouResult.Count > 0)
            {
                ouUsersListBox.Items.AddRange(ouResult.ToArray());
                ouUsersGroupBox.Visible = true;
            }
        }

        private void ouUsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            userName = ouUsersListBox.SelectedItem.ToString();

            searchTextbox.Text = userName;

            if (serverTextbox.Text != "")
            {
                searchButton.PerformClick();
            }
        }

        private void confirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (newPasswordTextBox.Text == confirmPasswordTextBox.Text)
            {
                if (confirmPasswordTextBox.Text.Length >= 6
                    && Regex.IsMatch(confirmPasswordTextBox.Text, "[a-z]")
                    && Regex.IsMatch(confirmPasswordTextBox.Text, "[A-Z]")
                    && Regex.IsMatch(confirmPasswordTextBox.Text, "[0-9]")
                    && Regex.IsMatch(confirmPasswordTextBox.Text, "[!@#$%^&*()]"))
                {
                    setNewPWButton.Visible = true;

                }
                else
                {
                    MessageBox.Show("Password must contians: ");
                }

            }
        }

        private async void setNewPWButton_Click(object sender, EventArgs e)
        {
            string result = await editUserPassword.SetNewPassword(searchTextbox.Text, confirmPasswordTextBox.Text);

            MessageBox.Show(result);

            passwordGroupBox.Visible = false;
            setNewPasswordGroupBox.Visible = false;

            searchButton.PerformClick();

        }

        private void newPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordGroupBox.Visible = false;
        }

        private void userInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(user.ToString());
        }
    }
}