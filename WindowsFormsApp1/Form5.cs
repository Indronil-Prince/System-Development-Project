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
    public partial class Form5 : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;
        DataTable dt = new DataTable();
        public static String x= "", name, Type=FormB.setType.ToString(), type, price, available, image;
        public static MemoryStream ms;

        public Form5()
        {
            InitializeComponent();
            this.Text = FormB.setType.ToString();
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex.Equals(5))
            {
                name = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                type = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                price = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                available = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();

                Bitmap img = (Bitmap)dataGridView2.Rows[e.RowIndex].Cells[4].Value;
                ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                Form6 f6 = new Form6();
                f6.Show();
            }
        }

        private async void Form5_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);

            label8.Text = Form2.setName.ToString();
            label7.Text = Form2.setEmail.ToString();
            String Type = FormB.setType.ToString();

            FirebaseResponse response = await client.GetTaskAsync("Product/"+Type);
            var json = response.Body;
         
            var dict = JsonConvert.DeserializeObject<Dictionary<string, Product>>(json);
            var AllKeys = dict.Keys.ToArray();
            var AllValues = dict.Values.ToArray();

            for (int i=0; i<AllKeys.Length; i++)
            {
                DataGridViewRow row1 = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                row1.Height = 100;
                row1.Cells[0].Value = AllValues[i].productname;
                row1.Cells[1].Value = AllValues[i].ptype;
                row1.Cells[2].Value = AllValues[i].price;
                row1.Cells[3].Value = AllValues[i].available;
                row1.Cells[5].Value = "View details";

                WebRequest request = WebRequest.Create(AllValues[i].image);
                WebResponse response2 = request.GetResponse();
                Stream responseStream = response2.GetResponseStream();
                Bitmap bitmap2 = new Bitmap(responseStream);
                row1.Cells[4].Value = bitmap2;
                dataGridView2.Rows.Add(row1);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
