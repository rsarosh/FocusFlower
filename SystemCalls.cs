using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{
    class SystemCalls
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        /// <summary>
        /// Get the Focused App Name
        /// </summary>
        /// <returns>AppName else null</returns>
        static public string GetAppName()
        {
            string appName = "";
            string s = GetActiveWindowTitle();
            if (String.IsNullOrEmpty(s)) return null;

            int pos = s.LastIndexOf('\\');
            if (pos > 0 && pos < s.Length)
            {
                appName = s.Substring(pos + 1);
                if (String.IsNullOrEmpty(appName)) return null;
            }
            else
            {
                appName = s;
            }

            pos = appName.LastIndexOf('-');
            if (pos > 0 && pos < s.Length)
            {
                appName = appName.Substring(pos + 1);
                if (String.IsNullOrEmpty(appName)) return null;
            }

            if (String.IsNullOrEmpty(appName)) return null;

            appName = appName.Trim();
            return appName;

        }
        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    }
}
