using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SoftDEC_Message_Creator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SendAlertButton_Click(object sender, EventArgs e)
        {
            new Thread(() => SendMessage()).Start();
        }

        private void SendMessage()
        {
            var requestData = new Dictionary<string, object>
            {
                { "API-Key", "key" },
                // emergency (yellow on dark red)
                // warning (white on red)
                // watch (white on orange)
                // advisory (black on yellow)
                // statement (black on light blue)
                // test (black on white)
                { "AlertType", textBox5.Text },
                { "Title", textBox1.Text },
                { "Description", textBox2.Text },
                { "ImageURL", textBox3.Text },
                { "AudioURL", textBox4.Text },
            };

            string json = JsonConvert.SerializeObject(requestData);

            string url = "http://localhost:6262/relay";

            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    SystemSounds.Beep.Play();
                }
                else
                {
                    MessageBox.Show($"{response.Content.ReadAsStringAsync().Result}");
                }
            }
        }
    }
}
