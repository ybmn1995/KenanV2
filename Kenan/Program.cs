using System;
using System.Windows.Forms;
using DotNetEnv;
using Kenan.View;
using static System.Windows.Forms.DataFormats;

namespace Kenan24Launcher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Env.Load(); // Load from .env
            ApplicationConfiguration.Initialize(); // ✅ Only for .NET 6+
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogInForm());
        }
    }
}