using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace testforms
{
    internal class MainForm : Form
    {
        // Properties.
        private Label label;
        private TextBox myName;
        private Button btn;
        private ListBox rtb;

        public MainForm()
        {
            InitializeComponent();
        }

        async Task LoadData()
        {
        }

        private void InitializeComponent()
        {
            // Call the function to render the objects.
            Text = "Windows Forms app";

            this.Size = new Size(300, 350);
            label = new Label { Text = "Your name: ", Location = new Point(10, 35) };
            myName = new TextBox { Location = new Point(10, 60), Width = 1000 };
            btn = new Button { Text = "Submit", Location = new Point(10, 100) };
            rtb = new ListBox { Text = "Submit", Location = new Point(10, 150), Width = 1000, Height = 500};
            btn.Click += Btn_Click; // Handle the event.

            // Attach these objects to the graphics window.
            this.Controls.Add(label);
            this.Controls.Add(myName);
            Controls.Add(btn);
            Controls.Add(rtb);
        }

        // Handler
        async void Btn_Click(object sender, EventArgs e)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            for (; month > 0; month--)
            {
                for (; day > 0; day--)
                {
                    await LoadData(year, month, day);
                }
            }

            year--;
            for (; year > 2015; year--)
            {
                for (month = 12; month > 0; month--)
                {
                    for (day = 31; day > 0; day--)
                    {
                        await LoadData(year, month, day);
                    }
                }
            }
        }

        private async Task LoadData(int year, int month, int day)
        {
            string password = "";
            string card = "";
            
            rtb.Items.Add($"{day}.{month}.{year}");

            var data = $"<oper>cmt</oper><wait>60</wait><payment><prop name=\"sd\" value=\"{day}.{month}.{year}\"/><prop name=\"ed\" value=\"{day}.{month}.{year}\"/><prop name=\"card\" value=\"{card}\"/></payment>";
            var md5 = MD5.Create();
            var sha1 = SHA1.Create();
            
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            var secret = data + password;
            var bytes = Encoding.UTF8.GetBytes(secret);
            var md5h = md5.ComputeHash(bytes);
            var md5hs = ByteArrayToString(md5h);
            var sha1h = sha1.ComputeHash(Encoding.UTF8.GetBytes(md5hs));
            var signature = ByteArrayToString(sha1h);

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            
            
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.privatbank.ua/p24api/rest_fiz"),
                Content = new StringContent($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><request version=\"1.0\"><merchant><id>154628</id><signature>{signature}</signature></merchant><data>{data}</data></request>",
                    Encoding.Default, "application/xml"),
            };
            var res = await client.SendAsync(request);
            
            var xml = await res.Content.ReadAsStringAsync();
            myName.Text = xml;
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            string xpath = "response/data/info/statements/statement";
            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                // Вытянуть данные из хмл
                rtb.Items.Add($"{day:d2}.{month:d2}.{year}) [{childrenNode.Attributes["cardamount"].Value}] {childrenNode.Attributes["description"].Value}");
            }
            
            rtb.Items.Remove($"{day}.{month}.{year}");
        }
        
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}