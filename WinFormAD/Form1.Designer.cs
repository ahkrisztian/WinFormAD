namespace WinFormAD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            searchButton = new Button();
            resultTextbox = new TextBox();
            nameTextbox = new TextBox();
            passwordTextbox = new TextBox();
            searchTextbox = new TextBox();
            serverTextbox = new TextBox();
            connectToADButton = new Button();
            connectedCheckBox = new CheckBox();
            searchGroupBox = new GroupBox();
            userInfoButton = new Button();
            updateNaverExpCheckBox = new CheckBox();
            updateNExtLoginCheckBox1 = new CheckBox();
            disconnectADButton = new Button();
            passwordGroupBox = new GroupBox();
            OrganizationalUnits = new ListBox();
            ouUsersGroupBox = new GroupBox();
            ouUsersListBox = new ListBox();
            setNewPasswordGroupBox = new GroupBox();
            passWordLastSetLabel = new Label();
            setNewPWButton = new Button();
            label1 = new Label();
            newPWLabel = new Label();
            confirmPasswordTextBox = new TextBox();
            newPasswordTextBox = new TextBox();
            ouGroupBox = new GroupBox();
            searchGroupBox.SuspendLayout();
            passwordGroupBox.SuspendLayout();
            ouUsersGroupBox.SuspendLayout();
            setNewPasswordGroupBox.SuspendLayout();
            ouGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // searchButton
            // 
            searchButton.Location = new Point(259, 51);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(92, 21);
            searchButton.TabIndex = 0;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // resultTextbox
            // 
            resultTextbox.Location = new Point(98, 78);
            resultTextbox.Name = "resultTextbox";
            resultTextbox.Size = new Size(425, 23);
            resultTextbox.TabIndex = 1;
            // 
            // nameTextbox
            // 
            nameTextbox.Location = new Point(8, 41);
            nameTextbox.Name = "nameTextbox";
            nameTextbox.Size = new Size(425, 23);
            nameTextbox.TabIndex = 2;
            // 
            // passwordTextbox
            // 
            passwordTextbox.Location = new Point(8, 70);
            passwordTextbox.Name = "passwordTextbox";
            passwordTextbox.PasswordChar = 'X';
            passwordTextbox.Size = new Size(425, 23);
            passwordTextbox.TabIndex = 3;
            // 
            // searchTextbox
            // 
            searchTextbox.Location = new Point(98, 22);
            searchTextbox.Name = "searchTextbox";
            searchTextbox.Size = new Size(425, 23);
            searchTextbox.TabIndex = 4;
            // 
            // serverTextbox
            // 
            serverTextbox.Location = new Point(8, 12);
            serverTextbox.Name = "serverTextbox";
            serverTextbox.Size = new Size(425, 23);
            serverTextbox.TabIndex = 5;
            // 
            // connectToADButton
            // 
            connectToADButton.Location = new Point(439, 12);
            connectToADButton.Name = "connectToADButton";
            connectToADButton.Size = new Size(75, 82);
            connectToADButton.TabIndex = 6;
            connectToADButton.Text = "Connect to AD";
            connectToADButton.UseVisualStyleBackColor = true;
            connectToADButton.Click += connectToADButton_Click;
            // 
            // connectedCheckBox
            // 
            connectedCheckBox.AutoSize = true;
            connectedCheckBox.Enabled = false;
            connectedCheckBox.Location = new Point(211, 99);
            connectedCheckBox.Name = "connectedCheckBox";
            connectedCheckBox.Size = new Size(117, 19);
            connectedCheckBox.TabIndex = 7;
            connectedCheckBox.Text = "Connected to AD";
            connectedCheckBox.UseVisualStyleBackColor = true;
            connectedCheckBox.CheckedChanged += connectedCheckBox_CheckedChanged_1;
            // 
            // searchGroupBox
            // 
            searchGroupBox.Controls.Add(userInfoButton);
            searchGroupBox.Controls.Add(searchTextbox);
            searchGroupBox.Controls.Add(searchButton);
            searchGroupBox.Controls.Add(resultTextbox);
            searchGroupBox.Location = new Point(8, 141);
            searchGroupBox.Name = "searchGroupBox";
            searchGroupBox.Size = new Size(596, 158);
            searchGroupBox.TabIndex = 8;
            searchGroupBox.TabStop = false;
            searchGroupBox.Text = "Search User";
            searchGroupBox.Visible = false;
            // 
            // userInfoButton
            // 
            userInfoButton.Location = new Point(259, 107);
            userInfoButton.Name = "userInfoButton";
            userInfoButton.Size = new Size(92, 23);
            userInfoButton.TabIndex = 17;
            userInfoButton.Text = "User Info";
            userInfoButton.UseVisualStyleBackColor = true;
            userInfoButton.Click += userInfoButton_Click;
            // 
            // updateNaverExpCheckBox
            // 
            updateNaverExpCheckBox.AutoSize = true;
            updateNaverExpCheckBox.Location = new Point(76, 22);
            updateNaverExpCheckBox.Name = "updateNaverExpCheckBox";
            updateNaverExpCheckBox.Size = new Size(191, 19);
            updateNaverExpCheckBox.TabIndex = 9;
            updateNaverExpCheckBox.Text = "Update Password Never Expires";
            updateNaverExpCheckBox.UseVisualStyleBackColor = true;
            updateNaverExpCheckBox.CheckedChanged += updateNaverExpCheckBox_CheckedChanged;
            // 
            // updateNExtLoginCheckBox1
            // 
            updateNExtLoginCheckBox1.AutoSize = true;
            updateNExtLoginCheckBox1.Location = new Point(338, 22);
            updateNExtLoginCheckBox1.Name = "updateNExtLoginCheckBox1";
            updateNExtLoginCheckBox1.Size = new Size(216, 19);
            updateNExtLoginCheckBox1.TabIndex = 10;
            updateNExtLoginCheckBox1.Text = "Update Password Next Login Renew";
            updateNExtLoginCheckBox1.UseVisualStyleBackColor = true;
            updateNExtLoginCheckBox1.CheckedChanged += updateNExtLoginCheckBox1_CheckedChanged;
            // 
            // disconnectADButton
            // 
            disconnectADButton.Location = new Point(439, 13);
            disconnectADButton.Name = "disconnectADButton";
            disconnectADButton.Size = new Size(75, 81);
            disconnectADButton.TabIndex = 11;
            disconnectADButton.Text = "Disconnect to AD";
            disconnectADButton.UseVisualStyleBackColor = true;
            disconnectADButton.Visible = false;
            disconnectADButton.Click += disconnectADButton_Click;
            // 
            // passwordGroupBox
            // 
            passwordGroupBox.Controls.Add(updateNaverExpCheckBox);
            passwordGroupBox.Controls.Add(updateNExtLoginCheckBox1);
            passwordGroupBox.Location = new Point(8, 419);
            passwordGroupBox.Name = "passwordGroupBox";
            passwordGroupBox.Size = new Size(596, 55);
            passwordGroupBox.TabIndex = 12;
            passwordGroupBox.TabStop = false;
            passwordGroupBox.Text = "Edit Password Credentials";
            passwordGroupBox.Visible = false;
            // 
            // OrganizationalUnits
            // 
            OrganizationalUnits.FormattingEnabled = true;
            OrganizationalUnits.ItemHeight = 15;
            OrganizationalUnits.Location = new Point(6, 15);
            OrganizationalUnits.Name = "OrganizationalUnits";
            OrganizationalUnits.Size = new Size(141, 79);
            OrganizationalUnits.TabIndex = 13;
            OrganizationalUnits.Visible = false;
            OrganizationalUnits.SelectedIndexChanged += OrganizationalUnits_SelectedIndexChanged;
            // 
            // ouUsersGroupBox
            // 
            ouUsersGroupBox.Controls.Add(ouUsersListBox);
            ouUsersGroupBox.Location = new Point(630, 141);
            ouUsersGroupBox.Name = "ouUsersGroupBox";
            ouUsersGroupBox.Size = new Size(159, 229);
            ouUsersGroupBox.TabIndex = 14;
            ouUsersGroupBox.TabStop = false;
            ouUsersGroupBox.Text = "Users of OU";
            ouUsersGroupBox.Visible = false;
            // 
            // ouUsersListBox
            // 
            ouUsersListBox.FormattingEnabled = true;
            ouUsersListBox.ItemHeight = 15;
            ouUsersListBox.Location = new Point(6, 22);
            ouUsersListBox.Name = "ouUsersListBox";
            ouUsersListBox.Size = new Size(147, 199);
            ouUsersListBox.TabIndex = 0;
            ouUsersListBox.SelectedIndexChanged += ouUsersListBox_SelectedIndexChanged;
            // 
            // setNewPasswordGroupBox
            // 
            setNewPasswordGroupBox.Controls.Add(passWordLastSetLabel);
            setNewPasswordGroupBox.Controls.Add(setNewPWButton);
            setNewPasswordGroupBox.Controls.Add(label1);
            setNewPasswordGroupBox.Controls.Add(newPWLabel);
            setNewPasswordGroupBox.Controls.Add(confirmPasswordTextBox);
            setNewPasswordGroupBox.Controls.Add(newPasswordTextBox);
            setNewPasswordGroupBox.Location = new Point(8, 317);
            setNewPasswordGroupBox.Name = "setNewPasswordGroupBox";
            setNewPasswordGroupBox.Size = new Size(596, 85);
            setNewPasswordGroupBox.TabIndex = 15;
            setNewPasswordGroupBox.TabStop = false;
            setNewPasswordGroupBox.Text = "Set a new password";
            setNewPasswordGroupBox.Visible = false;
            // 
            // passWordLastSetLabel
            // 
            passWordLastSetLabel.AutoSize = true;
            passWordLastSetLabel.Location = new Point(149, 19);
            passWordLastSetLabel.Name = "passWordLastSetLabel";
            passWordLastSetLabel.Size = new Size(81, 15);
            passWordLastSetLabel.TabIndex = 18;
            passWordLastSetLabel.Text = "passwrodDate";
            passWordLastSetLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // setNewPWButton
            // 
            setNewPWButton.Location = new Point(531, 56);
            setNewPWButton.Name = "setNewPWButton";
            setNewPWButton.Size = new Size(55, 23);
            setNewPWButton.TabIndex = 17;
            setNewPWButton.Text = "Set";
            setNewPWButton.UseVisualStyleBackColor = true;
            setNewPWButton.Visible = false;
            setNewPWButton.Click += setNewPWButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(255, 59);
            label1.Name = "label1";
            label1.Size = new Size(110, 15);
            label1.TabIndex = 3;
            label1.Text = "Confirm password :";
            // 
            // newPWLabel
            // 
            newPWLabel.AutoSize = true;
            newPWLabel.Location = new Point(6, 59);
            newPWLabel.Name = "newPWLabel";
            newPWLabel.Size = new Size(87, 15);
            newPWLabel.TabIndex = 2;
            newPWLabel.Text = "New password:";
            // 
            // confirmPasswordTextBox
            // 
            confirmPasswordTextBox.Location = new Point(369, 56);
            confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            confirmPasswordTextBox.PasswordChar = 'X';
            confirmPasswordTextBox.Size = new Size(150, 23);
            confirmPasswordTextBox.TabIndex = 1;
            confirmPasswordTextBox.TextChanged += confirmPasswordTextBox_TextChanged;
            // 
            // newPasswordTextBox
            // 
            newPasswordTextBox.Location = new Point(99, 56);
            newPasswordTextBox.Name = "newPasswordTextBox";
            newPasswordTextBox.PasswordChar = 'X';
            newPasswordTextBox.Size = new Size(150, 23);
            newPasswordTextBox.TabIndex = 0;
            newPasswordTextBox.TextChanged += newPasswordTextBox_TextChanged;
            // 
            // ouGroupBox
            // 
            ouGroupBox.Controls.Add(OrganizationalUnits);
            ouGroupBox.Location = new Point(630, 12);
            ouGroupBox.Name = "ouGroupBox";
            ouGroupBox.Size = new Size(153, 100);
            ouGroupBox.TabIndex = 16;
            ouGroupBox.TabStop = false;
            ouGroupBox.Text = "Organizational Units";
            ouGroupBox.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 486);
            Controls.Add(ouGroupBox);
            Controls.Add(setNewPasswordGroupBox);
            Controls.Add(ouUsersGroupBox);
            Controls.Add(passwordGroupBox);
            Controls.Add(disconnectADButton);
            Controls.Add(searchGroupBox);
            Controls.Add(connectedCheckBox);
            Controls.Add(connectToADButton);
            Controls.Add(serverTextbox);
            Controls.Add(passwordTextbox);
            Controls.Add(nameTextbox);
            Name = "Form1";
            Text = "Form1";
            searchGroupBox.ResumeLayout(false);
            searchGroupBox.PerformLayout();
            passwordGroupBox.ResumeLayout(false);
            passwordGroupBox.PerformLayout();
            ouUsersGroupBox.ResumeLayout(false);
            setNewPasswordGroupBox.ResumeLayout(false);
            setNewPasswordGroupBox.PerformLayout();
            ouGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button searchButton;
        private TextBox resultTextbox;
        private TextBox nameTextbox;
        private TextBox passwordTextbox;
        private TextBox searchTextbox;
        private TextBox serverTextbox;
        private Button connectToADButton;
        private CheckBox connectedCheckBox;
        private GroupBox searchGroupBox;
        private CheckBox updateNaverExpCheckBox;
        private CheckBox updateNExtLoginCheckBox1;
        private Button disconnectADButton;
        private GroupBox passwordGroupBox;
        private ListBox OrganizationalUnits;
        private GroupBox ouUsersGroupBox;
        private ListBox ouUsersListBox;
        private GroupBox setNewPasswordGroupBox;
        private Label label1;
        private Label newPWLabel;
        private TextBox confirmPasswordTextBox;
        private TextBox newPasswordTextBox;
        private GroupBox ouGroupBox;
        private Button setNewPWButton;
        private Button userInfoButton;
        private Label passWordLastSetLabel;
    }
}