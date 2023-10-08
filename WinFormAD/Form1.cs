using System.DirectoryServices;
using WinFormDataAccess;
using WinFormDataAccess.Querys;

namespace WinFormAD
{
    public partial class Form1 : Form
    {
        private DirectoryEntry direntry;
        public Form1()
        {
            InitializeComponent();

        }

        public async void button1_Click(object sender, EventArgs e)
        {
            ISearchUserAD search = new SearchUserAD();

            string result = await search.QueryUserAD(direntry, searchTextbox.Text);

            if(result != null)
            {
                resultTextbox.Text = result;
            }
            else
            {
                resultTextbox.Text = "No result";
            }
        }

        private async void connectToADButton_Click(object sender, EventArgs e)
        {
            IDataAccessAD dataAccess = new DataAccessAD();

            var result = await dataAccess.ConnectToAD("LDAP://192.168.178.75", "SERVER2022\\Administrator", passwordTextbox.Text);

            if (result is not null)
            {
                connectedCheckBox.Checked = true;
            }
            else
            {
                MessageBox.Show("Connection Error");
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
    }
}