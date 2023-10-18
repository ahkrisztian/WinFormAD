using Microsoft.Extensions.Configuration;
using Serilog;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WindiwsFormAdModels.OUModel;
using WindiwsFormAdModels.UserModels;
using WinFormDataAccess;
using WinFormDataAccess.Querys;

namespace WinFormAD
{
    public partial class MainForm : Form
    {
        private IEditUserPassword userPassword;

        bool allstasuesarechekedNever;
        bool allstasuesarechekedNext;

        private CancellationTokenSource cancelTokenSource;
        public UserAD user { get; set; }
        public string userName { get; set; }

        private List<OrganizationUnitModel> ouresults = new List<OrganizationUnitModel>();
        private bool clearTextBoxPw { get; set; } = false;

        private readonly IDataAccessAD dataAccessAD;
        private readonly IConfiguration configuration;
        private readonly ISearchUserAD searchUserAD;
        private readonly IEditUserPassword editUserPassword;
        private readonly ISearchOU searchOU;

        public MainForm(IDataAccessAD dataAccessAD, IConfiguration configuration,
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
                cancelTokenSource = new CancellationTokenSource();
                CancellationToken token = cancelTokenSource.Token;

                user = await searchUserAD.QueryUserAD(searchTextbox.Text, token);

                if (user is not null)
                {
                    resultTextbox.Text = user.DisplayName;

                    checkPasswordStatus();

                    passwordGroupBox.Visible = true;
                    setNewPasswordGroupBox.Visible = true;

                    passWordLastSetLabel.Text = user.PassWordLastChanged;
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
                cancelTokenSource = new CancellationTokenSource();
                CancellationToken token = cancelTokenSource.Token;

                dataAccessAD.SetThePassword(passwordTextbox.Text, serverTextbox.Text, nameTextbox.Text);

                DirectoryEntry connection = await dataAccessAD.ConnectToAD(token);

                cancelButton.Visible = true;

                if (connection.NativeGuid is not null && !cancelTokenSource.Token.IsCancellationRequested)
                {
                    connectedCheckBox.Checked = true;
                    passwordTextbox.Enabled = false;
                    serverTextbox.Enabled = false;
                    nameTextbox.Enabled = false;
                    connectedCheckBox.Enabled = false;

                    connectToADButton.Visible = false;
                    disconnectADButton.Visible = true;


                    ouresults = await searchOU.SearchOrganizationalUnits(token);

                    if (ouresults.Count > 0)
                    {
                        OrganizationalUnits.Items.Clear();

                        var ouNames = ouresults.Select(ou => ou.OUName).ToArray();

                        OrganizationalUnits.Items.AddRange(ouNames);
                        ouGroupBox.Visible = true;
                        OrganizationalUnits.Visible = true;
                    }

                    dataAccessAD.DisconnectAD(connection);
                    cancelButton.Visible = false;
                }
                else if (cancelTokenSource.Token.IsCancellationRequested)
                {
                    cancelTokenSource.Dispose();
                    MessageBox.Show("Cancelled");
                }
                else
                {
                    Log.Warning("Connection Error by Connect to AD");
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

                cancelTokenSource = new CancellationTokenSource();
                CancellationToken token = cancelTokenSource.Token;

                bool resultNeverExpires = await userPassword.CheckPasswordNeverExpires(searchTextbox.Text, token);
                bool resultNextLogin = await userPassword.CheckPasswordMustBeChangeNextLogin(searchTextbox.Text, token);

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
                    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                    CancellationToken token = cancelTokenSource.Token;


                    string result = await userPassword.SetUserPasswordNextLogon(searchTextbox.Text, updateNExtLoginCheckBox1.Checked, token);

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
                    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                    CancellationToken token = cancelTokenSource.Token;

                    string result = await userPassword.SetUserPasswordNeverExpires(searchTextbox.Text, updateNaverExpCheckBox.Checked, token);

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
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private async void OrganizationalUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            ouUsersListBox.Items.Clear();

            OrganizationUnitModel ou = ouresults[OrganizationalUnits.SelectedIndex];

            var ouResult = await searchOU.SearchMembersOfOrganizationalUnits(ou, token);

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
            if (!clearTextBoxPw)
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
            else
            {
                confirmPasswordTextBox.Clear();
            }

        }

        private async void setNewPWButton_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            string result = await editUserPassword.SetNewPassword(searchTextbox.Text, confirmPasswordTextBox.Text, token);

            MessageBox.Show(result);

            passwordGroupBox.Visible = false;
            setNewPasswordGroupBox.Visible = false;

            searchButton.PerformClick();

            clearTextBoxPw = true;
            confirmPasswordTextBox.Clear();
            newPasswordTextBox.Clear();

        }

        private void newPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!clearTextBoxPw)
            {
                passwordGroupBox.Visible = false;
            }
            else
            {
                newPasswordTextBox.Clear();
                clearTextBoxPw = false;
            }

        }

        private void userInfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(user.ToString());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelTokenSource.Cancel();
        }

        private void closeLabel_Click(object sender, EventArgs e)
        {

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            string message = "Do you want to close the application?";
            string title = "Exit";

            DialogResult result = MessageBox.Show(message, title, buttons);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void closeLabel_MouseEnter(object sender, EventArgs e)
        {
            closeLabel.Font = new Font(closeLabel.Font, FontStyle.Bold);
            closeLabel.ForeColor = Color.WhiteSmoke;
            closeLabel.Cursor = Cursors.Hand;
        }

        private void closeLabel_MouseLeave(object sender, EventArgs e)
        {
            closeLabel.Font = new Font(closeLabel.Font, FontStyle.Bold);
            closeLabel.ForeColor = SystemColors.ControlText;
        }

        private void minimizeLabel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void minimizeLabel_MouseEnter(object sender, EventArgs e)
        {
            minimizeLabel.Font = new Font(minimizeLabel.Font, FontStyle.Bold);
            minimizeLabel.ForeColor = Color.WhiteSmoke;
            minimizeLabel.Cursor = Cursors.Hand;
        }

        private void minimizeLabel_MouseLeave(object sender, EventArgs e)
        {
            minimizeLabel.Font = new Font(minimizeLabel.Font, FontStyle.Bold);
            minimizeLabel.ForeColor = Color.Black;

        }
    }

}