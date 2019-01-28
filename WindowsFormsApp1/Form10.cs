using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Storage;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        public static String name, email, ID, tot = Form9.go, uni;
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
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

        IFirebaseClient client;
        public Form10()
        {
            InitializeComponent();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetTaskAsync("Order/" + ID);
            var json = response.Body;

            var dict = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            var AllKeys = dict.Keys.ToArray();
            var AllValues = dict.Values.ToArray();
            String num = AllKeys.Length.ToString();
            uni = ID + "___" + num;

            var order = new Order
            {
                Id = uni,
                Adress = textBox1.Text.ToString(),
                Total = Convert.ToInt32(tot),
                contact = textBox2.Text.ToString(),
                Payment = checkedListBox1.SelectedItem.ToString(),
                Track = "Confirmed"
            };

            SetResponse response2 = await client.SetTaskAsync("Order/" + ID + "/" + uni, order);
            try
            {
                Order result = response2.ResultAs<Order>();
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                checkedListBox1.SelectedItem = null;
                FirebaseResponse response3 = await client.GetTaskAsync("Cart/" + ID);
                var json3 = response3.Body;
                var dict3 = JsonConvert.DeserializeObject<Dictionary<string, Cart>>(json3);
                var AllKeys3 = dict3.Keys.ToArray();
                var AllValues3 = dict3.Values.ToArray();
                for (int i = 1; i < AllKeys3.Length; i++)
                {
                    var cart2 = new Cart
                    {
                        productname = AllValues3[i].productname,
                        ptype = AllValues3[i].ptype,
                        amount = AllValues3[i].amount,
                        total = AllValues3[i].total
                    };
                    SetResponse response4 = await client.SetTaskAsync("OrderDetail/" + uni + "/" + AllValues3[i].productname, cart2);
                    Cart result4 = response4.ResultAs<Cart>();
                    DeleteResponse response5 = await client.DeleteTaskAsync("Cart/" + ID + "/" + AllValues3[i].productname);
                }
                MessageBox.Show("Order confirmed!", "Order", MessageBoxButtons.OK);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Sorry! Order can not be confirmed!", "Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);
            name = Form2.setName.ToString();
            email = Form2.setEmail.ToString();
            label8.Text = name.ToString();
            label7.Text = email.ToString();
            StringBuilder id = new StringBuilder(email);
            for (int i = 0; i < email.Length; i++)
            {
                if (email[i] == '.')
                {
                    id[i] = '_';
                }
            }
            ID = id.ToString();
        }
    }
}
