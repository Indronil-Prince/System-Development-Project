using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form11 : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;
        DataTable dt = new DataTable();
        public static String x = "", name, id = Form2.ID.ToString(), email, uni;

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8();
            f8.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 f11 = new Form11();
            f11.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex.Equals(3))
            {
                uni = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                
                this.Hide();
                Form12 f12 = new Form12();
                f12.Show();
            }
        }

        async private void Form11_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);
            name = Form2.setName.ToString();
            email = Form2.setEmail.ToString();
            label8.Text = name.ToString();
            label7.Text = email.ToString();
            button1.Text = "Products";

            FirebaseResponse response = await client.GetTaskAsync("Order/"+id+"/");
            var json = response.Body;

            var dict = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            var AllKeys = dict.Keys.ToArray();
            var AllValues = dict.Values.ToArray();

            for (int i = 1; i < AllKeys.Length; i++)
            {
                DataGridViewRow row1 = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                row1.Height = 25;
                row1.Cells[0].Value = id;
                row1.Cells[1].Value = AllValues[i].Id;
                row1.Cells[2].Value = AllValues[i].Track;
                row1.Cells[3].Value = "Order Details";
                dataGridView2.Rows.Add(row1);
            }
            dataGridView2.AllowUserToAddRows = false;
        }

        public Form11()
        {
            InitializeComponent();
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
    }
}