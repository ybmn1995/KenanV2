// LoginForm.Designer.cs - Full UI Based on Screenshot

namespace Kenan.View
{
    partial class LogInForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtMachineId;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.PictureBox bgLeft;
        private System.Windows.Forms.Label lblMachineId;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.Button btnTogglePassword;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.txtMachineId = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.bgLeft = new System.Windows.Forms.PictureBox();
            this.lblMachineId = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblServerIp = new System.Windows.Forms.Label();
            this.btnTogglePassword = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.bgLeft)).BeginInit();

            // Main Form
            this.ClientSize = new System.Drawing.Size(1000, 750);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Text = "Kenan24 Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.Black;

            // bgLeft Image
            byte[] imageBytes = Properties.Resources.KenanLeftBanner; // KenanLeftBanner is byte[]
            using (var ms = new MemoryStream(imageBytes))
            {
                this.bgLeft.Image = Image.FromStream(ms);
            }
            this.bgLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            this.bgLeft.Location = new System.Drawing.Point(0, 0);
            this.bgLeft.Size = new System.Drawing.Size(500, 750);

            // loginPanel
            this.loginPanel.BackColor = System.Drawing.Color.FromArgb(20, 20, 25);
            this.loginPanel.Location = new System.Drawing.Point(500, 0);
            this.loginPanel.Size = new System.Drawing.Size(500, 750);

            // Labels and Icons
            this.lblMachineId.Text = "Machine ID";
            this.lblMachineId.ForeColor = System.Drawing.Color.White;
            this.lblMachineId.Location = new System.Drawing.Point(60, 180);

            this.lblEmail.Text = "Email";
            this.lblEmail.ForeColor = System.Drawing.Color.White;
            this.lblEmail.Location = new System.Drawing.Point(60, 250);

            this.lblPassword.Text = "Password";
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(60, 320);

            this.lblServerIp.Text = "Server IP";
            this.lblServerIp.ForeColor = System.Drawing.Color.White;
            this.lblServerIp.Location = new System.Drawing.Point(60, 390);

            // TextBoxes
            this.txtMachineId.Location = new System.Drawing.Point(60, 200);
            this.txtMachineId.Text = Managers.MachineInfoHelper.GetMachineIdFormatted();
            this.txtMachineId.Size = new System.Drawing.Size(380, 30);

            this.txtEmail.Location = new System.Drawing.Point(60, 270);
            this.txtEmail.Size = new System.Drawing.Size(380, 30);

            this.txtPassword.Location = new System.Drawing.Point(60, 340);
            this.txtPassword.Size = new System.Drawing.Size(340, 30);
            this.txtPassword.UseSystemPasswordChar = true;

            // Toggle Password Button
            this.btnTogglePassword.Text = "👁";
            this.btnTogglePassword.Location = new System.Drawing.Point(410, 340);
            this.btnTogglePassword.Size = new System.Drawing.Size(30, 30);
            this.btnTogglePassword.Click += new System.EventHandler(this.TogglePasswordEye_Click);

            this.txtServerIp.Location = new System.Drawing.Point(60, 410);
            this.txtServerIp.Size = new System.Drawing.Size(380, 30);

            // Remember Checkbox
            this.chkRemember.Text = "Remember";
            this.chkRemember.ForeColor = System.Drawing.Color.White;
            this.chkRemember.Location = new System.Drawing.Point(60, 460);

            // Sign In Button
            this.btnSignIn.Text = "Sign in";
            this.btnSignIn.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSignIn.ForeColor = System.Drawing.Color.White;
            this.btnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignIn.Location = new System.Drawing.Point(60, 500);
            this.btnSignIn.Size = new System.Drawing.Size(380, 45);
            this.btnSignIn.Click += new System.EventHandler(this.BtnSignIn_Click);

            // Add controls
            this.loginPanel.Controls.Add(this.lblMachineId);
            this.loginPanel.Controls.Add(this.txtMachineId);
            this.loginPanel.Controls.Add(this.lblEmail);
            this.loginPanel.Controls.Add(this.txtEmail);
            this.loginPanel.Controls.Add(this.lblPassword);
            this.loginPanel.Controls.Add(this.txtPassword);
            this.loginPanel.Controls.Add(this.btnTogglePassword);
            this.loginPanel.Controls.Add(this.lblServerIp);
            this.loginPanel.Controls.Add(this.txtServerIp);
            this.loginPanel.Controls.Add(this.chkRemember);
            this.loginPanel.Controls.Add(this.btnSignIn);

            // Add to form
            this.Controls.Add(this.bgLeft);
            this.Controls.Add(this.loginPanel);

            ((System.ComponentModel.ISupportInitialize)(this.bgLeft)).EndInit();
            this.ResumeLayout(false);
        }
    }
}