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
            searchGroupBox.SuspendLayout();
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
            searchButton.Click += button1_Click;
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
            connectToADButton.Location = new Point(439, 11);
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
            connectedCheckBox.Location = new Point(341, 139);
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
            searchGroupBox.Location = new Point(137, 209);
            searchGroupBox.Name = "searchGroupBox";
            searchGroupBox.Size = new Size(509, 158);
            searchGroupBox.TabIndex = 8;
            searchGroupBox.TabStop = false;
            searchGroupBox.Text = "Search User";
            searchGroupBox.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}