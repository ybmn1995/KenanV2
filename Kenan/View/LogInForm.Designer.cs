using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Kenan.View
{
    partial class LogInForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel loginPanel;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtMachineId;
        private TextBox txtServerIp;
        private CheckBox chkRemember;
        private Button btnSignIn;
        private PictureBox bgLeft;
        private Label lblMachineId;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblServerIp;
        private Button btnTogglePassword;

        private void InitializeComponent()
        {
            loginPanel = new Panel();
            lblMachineId = new Label();
            txtMachineId = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnTogglePassword = new Button();
            lblServerIp = new Label();
            txtServerIp = new TextBox();
            chkRemember = new CheckBox();
            btnSignIn = new Button();
            bgLeft = new PictureBox();
            loginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bgLeft).BeginInit();
            SuspendLayout();
            // 
            // loginPanel
            // 
            loginPanel.BackColor = Color.FromArgb(20, 20, 25);
            loginPanel.Controls.Add(lblMachineId);
            loginPanel.Controls.Add(txtMachineId);
            loginPanel.Controls.Add(lblEmail);
            loginPanel.Controls.Add(txtEmail);
            loginPanel.Controls.Add(lblPassword);
            loginPanel.Controls.Add(txtPassword);
            loginPanel.Controls.Add(btnTogglePassword);
            loginPanel.Controls.Add(lblServerIp);
            loginPanel.Controls.Add(txtServerIp);
            loginPanel.Controls.Add(chkRemember);
            loginPanel.Controls.Add(btnSignIn);
            loginPanel.Location = new Point(500, 0);
            loginPanel.Name = "loginPanel";
            loginPanel.Size = new Size(500, 750);
            loginPanel.TabIndex = 1;
            // 
            // lblMachineId
            // 
            lblMachineId.Font = new Font("Segoe UI", 9F);
            lblMachineId.ForeColor = Color.White;
            lblMachineId.Location = new Point(59, 345);
            lblMachineId.Name = "lblMachineId";
            lblMachineId.Size = new Size(100, 23);
            lblMachineId.TabIndex = 0;
            lblMachineId.Text = "Machine ID";
            // 
            // txtMachineId
            // 
            txtMachineId.BorderStyle = BorderStyle.FixedSingle;
            txtMachineId.Font = new Font("Segoe UI", 9F);
            txtMachineId.Location = new Point(59, 371);
            txtMachineId.Name = "txtMachineId";
            txtMachineId.Size = new Size(380, 27);
            txtMachineId.TabIndex = 1;
            txtMachineId.Text = "ff55-8f8d-ca93";
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Segoe UI", 9F);
            lblEmail.ForeColor = Color.White;
            lblEmail.Location = new Point(59, 415);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(59, 441);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(380, 27);
            txtEmail.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.Font = new Font("Segoe UI", 9F);
            lblPassword.ForeColor = Color.White;
            lblPassword.Location = new Point(59, 485);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 9F);
            txtPassword.Location = new Point(59, 511);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(340, 27);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnTogglePassword
            // 
            btnTogglePassword.Font = new Font("Segoe UI", 9F);
            btnTogglePassword.Location = new Point(409, 511);
            btnTogglePassword.Name = "btnTogglePassword";
            btnTogglePassword.Size = new Size(30, 27);
            btnTogglePassword.TabIndex = 6;
            btnTogglePassword.Text = "👁";
            btnTogglePassword.UseVisualStyleBackColor = true;
            btnTogglePassword.Click += TogglePasswordEye_Click;
            // 
            // lblServerIp
            // 
            lblServerIp.Font = new Font("Segoe UI", 9F);
            lblServerIp.ForeColor = Color.White;
            lblServerIp.Location = new Point(59, 555);
            lblServerIp.Name = "lblServerIp";
            lblServerIp.Size = new Size(100, 23);
            lblServerIp.TabIndex = 7;
            lblServerIp.Text = "Server IP";
            // 
            // txtServerIp
            // 
            txtServerIp.Font = new Font("Segoe UI", 9F);
            txtServerIp.Location = new Point(59, 581);
            txtServerIp.Name = "txtServerIp";
            txtServerIp.Size = new Size(380, 27);
            txtServerIp.TabIndex = 8;
            // 
            // chkRemember
            // 
            chkRemember.Font = new Font("Segoe UI", 9F);
            chkRemember.ForeColor = Color.White;
            chkRemember.Location = new Point(59, 631);
            chkRemember.Name = "chkRemember";
            chkRemember.Size = new Size(104, 24);
            chkRemember.TabIndex = 9;
            chkRemember.Text = "Remember";
            // 
            // btnSignIn
            // 
            btnSignIn.BackColor = Color.DeepSkyBlue;
            btnSignIn.FlatStyle = FlatStyle.Flat;
            btnSignIn.Font = new Font("Segoe UI", 9F);
            btnSignIn.ForeColor = Color.White;
            btnSignIn.Location = new Point(59, 671);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(380, 40);
            btnSignIn.TabIndex = 10;
            btnSignIn.Text = "Sign in";
            btnSignIn.UseVisualStyleBackColor = false;
            btnSignIn.Click += BtnSignIn_Click;
            // 
            // bgLeft
            // 
            bgLeft.Location = new Point(0, 0);
            bgLeft.Name = "bgLeft";
            bgLeft.Size = new Size(494, 750);
            bgLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            bgLeft.TabIndex = 0;
            bgLeft.TabStop = false;
            bgLeft.Click += bgLeft_Click;
            // 
            // LogInForm
            // 
            BackColor = Color.Black;
            ClientSize = new Size(1000, 750);
            Controls.Add(bgLeft);
            Controls.Add(loginPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "LogInForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kenan24 Launcher";
            Load += LogInForm_Load;
            loginPanel.ResumeLayout(false);
            loginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bgLeft).EndInit();
            ResumeLayout(false);
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            byte[] imageBytes = Properties.Resources.KenanLeftBanner;
            using (var ms = new MemoryStream(imageBytes))
            {
                this.bgLeft.Image = Image.FromStream(ms);
            }
        }
    }
}
