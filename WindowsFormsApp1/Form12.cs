using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form12 : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;
        public static String x = "", id, productname, amount, total, unit, name, go, uni=Form11.uni;
        public static int gtot = 0, vat = 0, ship = 100, gt = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f10 = new Form10();
            f10.Show();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormB fB = new FormB();
            fB.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.Show();
        }

        public static MemoryStream ms;

        public Form12()
        {
            InitializeComponent();
        }

        private async void Form12_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);

            gtot = 0;
            label8.Text = Form2.setName.ToString();
            label7.Text = Form2.setEmail.ToString();
            id = Form2.ID.ToString();
            label2.Text = uni;
            String own = "";
            for (int i = 0; i < uni.Length; i++)
            {
                if (uni[i] == '_' && uni[i + 1] == '_' && uni[i + 2] == '_')
                {
                    break;
                }
                else own = own + uni[i];
            }

            FirebaseResponse response = await client.GetTaskAsync("OrderDetail/" + uni);
            var json = response.Body;

            var dict = JsonConvert.DeserializeObject<Dictionary<string, Cart>>(json);
            var AllKeys = dict.Keys.ToArray();
            var AllValues = dict.Values.ToArray();

                for (int i = 0; i < AllKeys.Length; i++)
                {
                    DataGridViewRow row1 = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                    row1.Height = 100;
                    row1.Cells[0].Value = AllValues[i].productname;
                    row1.Cells[1].Value = AllValues[i].ptype;
                    row1.Cells[4].Value = AllValues[i].amount;
                    row1.Cells[5].Value = AllValues[i].total;
                    gtot = gtot + AllValues[i].total;
                    if (AllValues[i].amount != 0)
                    { row1.Cells[3].Value = (AllValues[i].total) / (AllValues[i].amount); }
                    else { row1.Cells[2].Value = 0; }

                    FirebaseResponse response2 = await client.GetTaskAsync("Product/" + AllValues[i].ptype + "/" + AllValues[i].productname + "/image");
                    var img = response2.Body;
                    img = img.Replace("\"", "");

                    WebRequest request = WebRequest.Create(img);
                    WebResponse response3 = request.GetResponse();
                    Stream responseStream = response3.GetResponseStream();
                    Bitmap bitmap2 = new Bitmap(responseStream);
                    row1.Cells[2].Value = bitmap2;
                    dataGridView2.Rows.Add(row1);
                }

            dataGridView2.AllowUserToAddRows = false;

            //FirebaseResponse response4 = await client.GetTaskAsync("Order/" + own + "/" + uni);
           // Order result = response.ResultAs<Order>();

            //var img = response2.Body;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label7.Text = String.Empty;
            label8.Text = String.Empty;
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
            MessageBox.Show("Logged out successfully!");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormB fb = new FormB();
            fb.Show();
        }
    }
}

