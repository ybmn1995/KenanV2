using System;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace Kenan.Managers
{
    public static class MachineInfoHelper
    {
        public static string GetMachineIdFormatted()
        {
            string uuid = GetHardwareUuid();
            string mac = GetMacAddress();
            string osInfo = $"{Environment.OSVersion}-{Environment.Version}";

            string combined = $"{uuid}-{mac}-{osInfo}";
            string hash = ComputeSha1(combined);
            return FormatHash(hash);
        }

        private static string GetHardwareUuid()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct");
                foreach (var item in searcher.Get())
                {
                    return item["UUID"]?.ToString() ?? "unknown";
                }
            }
            catch { return "unknown"; }

            return "unknown";
        }

        private static string GetMacAddress()
        {
            try
            {
                return NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic =>
                        nic.OperationalStatus == OperationalStatus.Up &&
                        nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        !nic.Description.ToLower().Contains("virtual"))
                    .Select(nic => nic.GetPhysicalAddress().ToString())
                    .FirstOrDefault() ?? "00:00:00:00";
            }
            catch { return "00:00:00:00"; }
        }

        private static string ComputeSha1(string input)
        {
            using var sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static string FormatHash(string hex)
        {
            string shortHex = hex.Substring(0, 12);
            return $"{shortHex.Substring(0, 4)}-{shortHex.Substring(4, 4)}-{shortHex.Substring(8, 4)}";
        }
    }
}
