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

        public Form1(IDataAccessAD dataAccessAD, 
                     ISearchUserAD searchUserAD, 
                     IEditUserPassword editUserPassword)
        {
            this.dataAccessAD = dataAccessAD;
            this.searchUserAD = searchUserAD;
            this.editUserPassword = editUserPassword;

            InitializeComponent();

            updateNaverExpCheckBox.Click -= updateNaverExpCheckBox_CheckedChanged;
            updateNExtLoginCheckBox1.Click -= updateNExtLoginCheckBox1_CheckedChanged;
         
        }

        public void searchButton_Click(object sender, EventArgs e)
        {
            string result = searchUserAD.QueryUserAD(direntry, searchTextbox.Text);

            if (result != null)
            {
                resultTextbox.Text = result;

                checkPasswordStatus();

            }
            else
            {
                resultTextbox.Text = "No result";
            }
        }

        private void connectToADButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = dataAccessAD.ConnectToAD(passwordTextbox.Text);

                if (result is not null)
                {
                    direntry = result;
                    connectedCheckBox.Checked = true;
                }
                else
                {
                    MessageBox.Show("Connection Error DirectoryEntry");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
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
                else if(resultNextLogin) 
                {
                    updateNExtLoginCheckBox1.Checked = resultNextLogin;
                    updateNaverExpCheckBox.Enabled = false;
                    updateNExtLoginCheckBox1.Enabled = true;
                }
                else
                {
                    updateNaverExpCheckBox.Enabled = true;
                    updateNExtLoginCheckBox1.Enabled = true;
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
    }
}