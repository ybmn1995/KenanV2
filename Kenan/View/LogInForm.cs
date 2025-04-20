using System;
using System.Windows.Forms;

namespace Kenan.View
{
    public partial class LogInForm : Form
    {
        private bool passwordVisible = false;

        public LogInForm()
        {
            InitializeComponent();
        }

        private void TogglePasswordEye_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            txtPassword.UseSystemPasswordChar = !passwordVisible;
        }

        private async void BtnSignIn_Click(object sender, EventArgs e)
        {
            var url = Environment.GetEnvironmentVariable("KENAN_API_URL") ??
          "https://run.mocky.io/v3/942b0d49-e323-43b1-b7da-9caf1c0f774b"; // fallback URL

            var controller = new Kenan.Controller.ApiController(url);

            string response = await controller.SendLoginAsync(
                txtEmail.Text,
                txtMachineId.Text,
                Environment.OSVersion.ToString(),
                txtPassword.Text,
                txtServerIp.Text
            );

            MessageBox.Show("Response:\n" + response, "API Response");
        }

    }
}
