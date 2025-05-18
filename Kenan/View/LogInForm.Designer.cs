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
            this.Text = "Kenan24 Launcher";
            this.ClientSize = new Size(1000, 750);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;

            bgLeft = new PictureBox
            {
                Location = new Point(0, 0),
                Size = new Size(500, 750),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            bgLeft.Click += bgLeft_Click;
            this.Controls.Add(bgLeft);

            loginPanel = new Panel
            {
                BackColor = Color.FromArgb(20, 20, 25),
                Location = new Point(500, 0),
                Size = new Size(500, 750)
            };
            this.Controls.Add(loginPanel);

            Font fieldFont = new Font("Segoe UI", 10F);
            Size textBoxSize = new Size(380, 30);
            Point origin = new Point(59, 200);
            int spacing = 55;

            lblMachineId = CreateLabel("Machine ID", origin.X, origin.Y);
            txtMachineId = CreateTextBox("ff55-8f8d-ca93", origin.X, origin.Y + 25);
            loginPanel.Controls.AddRange(new Control[] { lblMachineId, txtMachineId });

            lblEmail = CreateLabel("Email", origin.X, origin.Y + spacing);
            txtEmail = CreateTextBox("", origin.X, origin.Y + spacing + 25);
            loginPanel.Controls.AddRange(new Control[] { lblEmail, txtEmail });

            lblPassword = CreateLabel("Password", origin.X, origin.Y + spacing * 2);
            txtPassword = CreateTextBox("", origin.X, origin.Y + spacing * 2 + 25);
            txtPassword.UseSystemPasswordChar = true;
            loginPanel.Controls.AddRange(new Control[] { lblPassword, txtPassword });

            btnTogglePassword = new Button
            {
                Location = new Point(origin.X + 350, origin.Y + spacing * 2 + 25),
                Size = new Size(30, 30),
                Text = "👁",
                FlatStyle = FlatStyle.Flat
            };
            btnTogglePassword.Click += TogglePasswordEye_Click;
            loginPanel.Controls.Add(btnTogglePassword);

            lblServerIp = CreateLabel("Server IP", origin.X, origin.Y + spacing * 3);
            txtServerIp = CreateTextBox("", origin.X, origin.Y + spacing * 3 + 25);
            loginPanel.Controls.AddRange(new Control[] { lblServerIp, txtServerIp });

            chkRemember = new CheckBox
            {
                Text = "Remember",
                Font = fieldFont,
                ForeColor = Color.White,
                Location = new Point(origin.X, origin.Y + spacing * 4 + 10),
                AutoSize = true
            };
            loginPanel.Controls.Add(chkRemember);

            btnSignIn = new Button
            {
                Text = "Sign in",
                Font = fieldFont,
                Size = new Size(380, 40),
                Location = new Point(origin.X, origin.Y + spacing * 5),
                BackColor = Color.DeepSkyBlue,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };
            btnSignIn.Click += BtnSignIn_Click;
            loginPanel.Controls.Add(btnSignIn);

            this.Load += LogInForm_Load;
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                Location = new Point(x, y),
                Size = new Size(100, 23)
            };
        }

        private TextBox CreateTextBox(string defaultText, int x, int y)
        {
            return new TextBox
            {
                Text = defaultText,
                Font = new Font("Segoe UI", 10F),
                Size = new Size(380, 30),
                Location = new Point(x, y),
                BorderStyle = BorderStyle.FixedSingle
            };
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
