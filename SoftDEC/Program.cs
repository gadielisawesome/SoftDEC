using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static SoftDEC.ClientExceptions;

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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region Elevation Detection
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
            #endregion

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

            HTMLNetworkHandler network = new HTMLNetworkHandler();
            if (!network.TestCapture())
            {
                var result = MessageBox.Show("SoftDEC does not function properly beyond 100% (96) DPI scaling.\nDo you want to disable DPI scaling for SoftDEC only?",
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
                            Verb = "runas",
                            Environment = { { "__COMPAT_LAYER", "DPIUNAWARE" } }
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
                                MessageBox.Show($"You can relaunch SoftDEC at anytime, but 100% (96) DPI scaling is a must.",
                                "SoftDEC",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                                Environment.Exit(1);
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
                    MessageBox.Show($"You can relaunch SoftDEC at anytime, but 100% (96) DPI scaling is a must.",
                                "SoftDEC",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    Environment.Exit(1);
                    return;
                }
            }

            network.Run();

            //CaptureForm cf = new CaptureForm(args[0], args[1]);
            //cf.Show();
            //while (!cf.ConfigurationFinished)
            //{
            //    Application.DoEvents();
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(500);
            //    Application.DoEvents();
            //}
            //Bitmap bitmap = new Bitmap(1280, 720);
            //cf.DrawToBitmap(bitmap, new Rectangle(0, 0, 1280, 720));
            //cf.Dispose();
            //bitmap.Save("bitmap.bmp");
            //Console.WriteLine("Saved file 'bitmap.bmp' in the working directory.");
        }
    }

    public class HTMLNetworkHandler
    {
        private readonly Thread thread;

        public HTMLNetworkHandler()
        {
            thread = new Thread(() => Start());
        }

        public void Run()
        {
            thread.Start();
        }

        private static void Start()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:6262/");
            listener.Start();
            Console.WriteLine("Listening for POST requests on http://localhost:6262/");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                switch (request.HttpMethod)
                {
                    //case "GET":
                    //    Console.WriteLine("GET");
                    //    switch (request.Url.PathAndQuery)
                    //    {
                    //        case "/":
                    //            response.StatusCode = (int)HttpStatusCode.OK;
                    //            try
                    //            {
                    //                byte[] LocalHTMLDocument = File.ReadAllBytes("index.html");
                    //                response.OutputStream.Write(LocalHTMLDocument, 0, LocalHTMLDocument.Length);
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                byte[] LocalHTMLDocument = Encoding.UTF8.GetBytes($"<h1>{ex.Message}</h1>");
                    //                response.OutputStream.Write(LocalHTMLDocument, 0, LocalHTMLDocument.Length);
                    //            }
                    //            finally
                    //            {
                    //                response.OutputStream.Close();
                    //            }
                    //            break;
                    //    }
                    //    break;
                    case "POST":
                        Console.WriteLine("A request is being processed.");
                        switch (request.Url.PathAndQuery)
                        {
                            // All requests always start with a slash
                            case "/relay":
                                using (Stream body = request.InputStream)
                                {
                                    using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                                    {
                                        string requestBody = reader.ReadToEnd();
                                        var requestData = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

                                        try
                                        {
                                            Console.WriteLine("Grabbing API-Key");
                                            string key = requestData["API-Key"];
                                            try
                                            {
                                                if (key != File.ReadAllText("key.txt").Trim())
                                                {
                                                    Console.WriteLine("Invalid API-Key");
                                                    throw new AuthenticationFailedException("The API-Key field is incorrect.");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                if (ex is FileNotFoundException)
                                                {
                                                    Console.BackgroundColor = ConsoleColor.White;
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("Caution, the file 'key.txt' does not exist.");
                                                    Console.WriteLine("All keys will be accepted unless you create this file.");
                                                    Console.WriteLine("Please do so immediately to protect your server from attackers.");
                                                    Console.ResetColor();
                                                }
                                            }

                                            Console.WriteLine("Grabbing AlertType field.");
                                            string alertType = requestData["AlertType"].Trim();
                                            Console.WriteLine("Grabbing Title field.");
                                            string title = requestData["Title"].Trim();
                                            Console.WriteLine("Grabbing Description field.");
                                            string description = requestData["Description"].Trim();
                                            Console.WriteLine("Grabbing ImageURL field.");
                                            string imageURL = requestData["ImageURL"].Trim();
                                            Console.WriteLine("Grabbing AudioURL field.");
                                            string audioURL = requestData["AudioURL"].Trim();

                                            //try
                                            //{
                                            //    Color.FromName(((string)requestData["BackColor"]).Trim());
                                            //    Color.FromName(((string)requestData["ForeColor"]).Trim());
                                            //}
                                            //catch (Exception)
                                            //{
                                            //    Console.WriteLine("A value error occurred. Skipping.");
                                            //}

                                            Console.WriteLine("Rendering visuals.");
                                            Bitmap bitmap = CreateCapture(alertType, title, description, imageURL);
                                            Console.WriteLine("Saving render.");
                                            bitmap.Save("bitmap.bmp");
                                            Console.WriteLine("Saved render image 'bitmap.bmp' in the working directory.");

                                            try
                                            {
                                                using (var client = new WebClient())
                                                {
                                                    client.Headers.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 6.0; " + "Windows NT 5.2; .NET CLR 1.0.3705;) " + "SoftDEC");
                                                    client.DownloadFile(audioURL, "audio.wav");
                                                }
                                                Console.WriteLine("Saved file 'audio.wav' in the working directory.");
                                            }
                                            catch (Exception)
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Could not download 'audio.wav'. Audio will be silent.");
                                                    File.WriteAllBytes("audio.wav", Properties.Resources.silence);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Could not modify 'audio.wav'. Audio may be corrupt.");
                                                }
                                            }

                                            Thread.Sleep(500);

                                            try
                                            {
                                                File.WriteAllText("issued.txt", DateTime.Now.ToString("s"));
                                            }
                                            catch (Exception ex)
                                            {
                                                throw new Exception($"Message failed. {ex.HResult}");
                                            }

                                            response.StatusCode = (int)HttpStatusCode.OK;
                                            JsonConvert.SerializeObject(response);
                                            byte[] SuccessText = Encoding.UTF8.GetBytes("Message queued.");
                                            response.OutputStream.Write(SuccessText, 0, SuccessText.Length);
                                            response.OutputStream.Close();
                                            Thread.Sleep(100);
                                        }
                                        catch (Exception ex)
                                        {
                                            byte[] ErrorText;
                                            switch (ex)
                                            {
                                                case AuthenticationFailedException _:
                                                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                                    ErrorText = Encoding.UTF8.GetBytes(ex.Message);
                                                    response.OutputStream.Write(ErrorText, 0, ErrorText.Length);
                                                    break;
                                                case KeyNotFoundException _:
                                                    break;
                                                default:
                                                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                                    ErrorText = Encoding.UTF8.GetBytes(ex.Message);
                                                    response.OutputStream.Write(ErrorText, 0, ErrorText.Length);
                                                    break;
                                            }
                                            response.OutputStream.Close();
                                        }
                                        finally
                                        {
                                            response.Close();
                                        }
                                    }
                                }
                                break;
                            default:
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                byte[] buffer = Encoding.UTF8.GetBytes("Resource Not Found");
                                response.OutputStream.Write(buffer, 0, buffer.Length);
                                response.OutputStream.Close();
                                break;
                        }
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        response.OutputStream.Close();
                        continue;
                }
            }
        }

        private static Bitmap CreateCapture(string alertType, string title, string description, string imageURL)
        {
            CaptureForm cf;

            switch (alertType.ToLower())
            {
                // emergency (yellow on dark red)
                // warning (white on red)
                // watch (white on orange)
                // advisory (black on yellow)
                // statement (black on light blue)
                // test (black on white)

                case "emergency":
                    cf = new CaptureForm(title, description, imageURL,
                        188, 0, 0, 255, 255, 0);
                    break;
                case "warning":
                    cf = new CaptureForm(title, description, imageURL,
                        255, 0, 0, 255, 255, 255);
                    break;
                case "watch":
                    cf = new CaptureForm(title, description, imageURL,
                        255, 127, 0, 255, 255, 255);
                    break;
                case "advisory":
                    cf = new CaptureForm(title, description, imageURL,
                        255, 255, 0, 0, 0, 0);
                    break;
                case "statement":
                    cf = new CaptureForm(title, description, imageURL,
                        50, 50, 255, 0, 0, 0);
                    break;
                case "test":
                    cf = new CaptureForm(title, description, imageURL,
                        255, 255, 255, 0, 0, 0);
                    break;
                default:
                    cf = new CaptureForm(title, description, imageURL,
                        50, 50, 255, 0, 0, 0);
                    break;
            }

            //CaptureForm cf = new CaptureForm(title, description, imageURL,
            //    backValueR, backValueG, backValueB, foreValueR, foreValueG, foreValueB);

            cf.Show();
            while (!cf.ConfigurationFinished)
            {
                Application.DoEvents();
            }
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Application.DoEvents();
            }
            Bitmap bitmap = new Bitmap(1280, 720);
            cf.DrawToBitmap(bitmap, new Rectangle(0, 0, 1280, 720));
            cf.Dispose();
            return bitmap;
        }

        public bool TestCapture()
        {
            CaptureForm cf = new CaptureForm("TEST CAPTURE", "This is a test capture produced on startup of SoftDEC.\nThis is not saved to your device, or relayed in any way.\n\nPlease wait while SoftDEC finishes self-testing.")
            {
                //CaptureForm cf = new CaptureForm(title, description, imageURL, audioURL,
                //    backValueR, backValueG, backValueB, foreValueR, foreValueG, foreValueB);

                Text = "SoftDEC Temporary Test Capture Window"
            };
            cf.Show();
            while (!cf.ConfigurationFinished)
            {
                Application.DoEvents();
            }
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Application.DoEvents();
            }

            int DPI = cf.DeviceDpi;
            cf.Dispose();

            switch (DPI)
            {
                case 96:
                    return true;
                default:
                    return false;
            }
        }
    }

    public class ClientExceptions
    {
        /// <summary>
        /// Thrown when the API cannot authenticate successfully with the client.
        /// </summary>
        public class AuthenticationFailedException : Exception
        {
            public AuthenticationFailedException(string message) : base(message) { }
        }
    }
}
