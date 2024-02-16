using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace SoftDEC
{
    internal static class Program
    {
        public static bool IsElevated
        {
            get
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        [STAThread]
        static void Main(string[] args)
        {
            //new HTMLNetworkHandler();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!IsElevated)
            {
                var result = MessageBox.Show("SoftDEC requires elevation to work properly.\nDo you want to try requesting elevation?",
                    "SoftDEC",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Process self = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = Process.GetCurrentProcess().MainModule.FileName,
                            Verb = "runas"
                        }
                    };
                    try
                    {
                        self.Start();
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        switch (ex.HResult)
                        {
                            case -2147467259:
                                MessageBox.Show($"You can relaunch SoftDEC at anytime, but elevation is a must.",
                                "SoftDEC",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                                Environment.Exit(5);
                                return;
                            default:
                                MessageBox.Show($"Something went wrong. {ex.HResult}\n{ex.Message}",
                                "SoftDEC",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                                Environment.Exit(ex.HResult);
                                return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"You can relaunch SoftDEC at anytime, but elevation is a must.",
                                "SoftDEC",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    Environment.Exit(5);
                    return;
                }
            }
            // Specifying a long title over 15 characters or the viewable limit will not appear at all.
            // Keep titles short when possible, but similar. Here's some examples. All titles are uppercased.
            //
            // Do: Severe Thunderstorm
            // Don't: Severe Thunderstorm Warning
            //
            // Do: Biohazard Warning
            // Don't: Hazardous Materials Warning
            //
            // You will want to adjust the software you use. Try to prevent relaying certain codes,
            // such as, Network Message Notification, Transmitter Backup On, etc.
            CaptureForm cf = new CaptureForm("", "");
            cf.Show();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Application.DoEvents();
            }
            Bitmap bitmap = new Bitmap(1280, 720);
            cf.DrawToBitmap(bitmap, new Rectangle(0, 0, 1280, 720));
            bitmap.Save("bitmap.bmp");
        }
    }

    public class HTMLNetworkHandler
    {
        public HTMLNetworkHandler()
        {
            MessageBox.Show("Test");
        }
    }
}
