using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 抽卡
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class resp
        {
            public List<img> data { get; set; }
        }

        public class img
        {
            public string link { get; set; }
        }

        private resp GetImages(string albumHash, string clientId)
        {
            resp result = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.imgur.com/3/album/0PxBYNN/images");

                WebHeaderCollection myWebHeaderCollection = request.Headers;
                myWebHeaderCollection.Add(albumHash, "Client-ID " + clientId);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string json = readStream.ReadToEnd();
                result = JsonConvert.DeserializeObject<resp>(json);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                throw;
            }
            return result;
        }

        private Image Getimagefromurl(string url)
        {
            Image result = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                result = Image.FromStream(receiveStream);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                throw;
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var m = GetImages("Authorization", "4c73ffd69840e6e");
            Random girl = new Random();
            if (m == null)
            {
                return;
            }
            pb1.Image = Getimagefromurl(m.data[girl.Next(0,77)].link);
        }
    }
}
