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
            updateNaverExpCheckBox = new CheckBox();
            updateNExtLoginCheckBox1 = new CheckBox();
            disconnectADButton = new Button();
            passwordGroupBox = new GroupBox();
            OrganizationalUnits = new ListBox();
            ouUsersGroupBox = new GroupBox();
            ouUsersListBox = new ListBox();
            searchGroupBox.SuspendLayout();
            passwordGroupBox.SuspendLayout();
            ouUsersGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // searchButton
            // 
            searchButton.Location = new Point(200, 70);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(92, 21);
            searchButton.TabIndex = 0;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // resultTextbox
            // 
            resultTextbox.Location = new Point(41, 122);
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
            nameTextbox.Text = "SERVER2022\\Administrator";
            // 
            // passwordTextbox
            // 
            passwordTextbox.Location = new Point(8, 70);
            passwordTextbox.Name = "passwordTextbox";
            passwordTextbox.Size = new Size(425, 23);
            passwordTextbox.TabIndex = 3;
            // 
            // searchTextbox
            // 
            searchTextbox.Location = new Point(41, 35);
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
            serverTextbox.Text = "LDAP://192.168.78.75";
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
            searchGroupBox.Controls.Add(searchTextbox);
            searchGroupBox.Controls.Add(searchButton);
            searchGroupBox.Controls.Add(resultTextbox);
            searchGroupBox.Location = new Point(7, 169);
            searchGroupBox.Name = "searchGroupBox";
            searchGroupBox.Size = new Size(509, 158);
            searchGroupBox.TabIndex = 8;
            searchGroupBox.TabStop = false;
            searchGroupBox.Text = "Search User";
            searchGroupBox.Visible = false;
            // 
            // updateNaverExpCheckBox
            // 
            updateNaverExpCheckBox.AutoSize = true;
            updateNaverExpCheckBox.Location = new Point(24, 22);
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
            updateNExtLoginCheckBox1.Location = new Point(286, 22);
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
            passwordGroupBox.Location = new Point(7, 343);
            passwordGroupBox.Name = "passwordGroupBox";
            passwordGroupBox.Size = new Size(509, 55);
            passwordGroupBox.TabIndex = 12;
            passwordGroupBox.TabStop = false;
            passwordGroupBox.Text = "Edit Password Credentials";
            passwordGroupBox.Visible = false;
            // 
            // OrganizationalUnits
            // 
            OrganizationalUnits.FormattingEnabled = true;
            OrganizationalUnits.ItemHeight = 15;
            OrganizationalUnits.Location = new Point(668, 12);
            OrganizationalUnits.Name = "OrganizationalUnits";
            OrganizationalUnits.Size = new Size(120, 79);
            OrganizationalUnits.TabIndex = 13;
            OrganizationalUnits.Visible = false;
            OrganizationalUnits.SelectedIndexChanged += OrganizationalUnits_SelectedIndexChanged;
            // 
            // ouUsersGroupBox
            // 
            ouUsersGroupBox.Controls.Add(ouUsersListBox);
            ouUsersGroupBox.Location = new Point(549, 169);
            ouUsersGroupBox.Name = "ouUsersGroupBox";
            ouUsersGroupBox.Size = new Size(239, 229);
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
            ouUsersListBox.Size = new Size(227, 199);
            ouUsersListBox.TabIndex = 0;
            ouUsersListBox.SelectedIndexChanged += ouUsersListBox_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ouUsersGroupBox);
            Controls.Add(OrganizationalUnits);
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
    }
}