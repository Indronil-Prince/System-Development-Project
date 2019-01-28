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
    public partial class Form7 : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;
        public static String x = "", id, productname, amount, total, unit, name;

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormB fB = new FormB();
            fB.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form9 fB = new Form9();
            fB.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FormB fb = new FormB();
            fb.Show();
        }

        public static MemoryStream ms;

        public Form7()
        {
            InitializeComponent();
        }

        private async void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex.Equals(6))
            {
                name = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

                FirebaseResponse response2 = await client.DeleteTaskAsync("Cart/" + Form2.ID.ToString() + "/" + name);
                MessageBox.Show("Product removed from cart!", "Cart", MessageBoxButtons.OK);
                this.Hide();
                Form7 f7 = new Form7();
                f7.Show();
            }
        }

        private async void Form7_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);
            
            label8.Text = Form2.setName.ToString();
            label7.Text = Form2.setEmail.ToString();
            id = Form2.ID.ToString();

            FirebaseResponse response = await client.GetTaskAsync("Cart/" + id);
            var json = response.Body;

            var dict = JsonConvert.DeserializeObject<Dictionary<string, Cart>>(json);
            var AllKeys = dict.Keys.ToArray();
            var AllValues = dict.Values.ToArray();

            if (AllKeys.Length > 1)
            {
                for (int i = 1; i < AllKeys.Length; i++)
                {
                    DataGridViewRow row1 = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                    row1.Height = 100;
                    row1.Cells[0].Value = AllValues[i].productname;
                    row1.Cells[1].Value = AllValues[i].ptype;
                    row1.Cells[4].Value = AllValues[i].amount;
                    row1.Cells[5].Value = AllValues[i].total;
                    if (AllValues[i].amount != 0)
                    { row1.Cells[3].Value = (AllValues[i].total) / (AllValues[i].amount); }
                    else { row1.Cells[2].Value = 0; }
                    row1.Cells[6].Value = "Remove";

                    FirebaseResponse response2 = await client.GetTaskAsync("Product/" + AllValues[i].ptype + "/" + AllValues[i].productname + "/image");
                    var img = response2.Body;
                    img = img.Replace("\"", "");

                    WebRequest request = WebRequest.Create(img);
                    WebResponse response3 = request.GetResponse();
                    Stream responseStream = response3.GetResponseStream();
                    Bitmap bitmap2 = new Bitmap(responseStream);
                    row1.Cells[2].Value = bitmap2;
                    dataGridView2.Rows.Add(row1);
                    label2.Visible = false;
                }
            }
            else
            {
                dataGridView2.Visible = false;
                label2.Visible = true;
                label2.Text = "Your cart is empty!";
                button3.Visible = false;
            }
            dataGridView2.AllowUserToAddRows = false;
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
