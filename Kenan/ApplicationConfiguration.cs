using System;
using System.Windows.Forms;

namespace Kenan
{
    internal static class ApplicationConfiguration
    {
        public static void Initialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}
