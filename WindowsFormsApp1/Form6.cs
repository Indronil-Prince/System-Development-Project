using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        public static MemoryStream Mem;
        public static int avl, num;
        IFirebaseClient client;
        public Form6()
        {
            InitializeComponent();
            this.Text = Form5.name.ToString();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);
            Mem = Form5.ms;
            
            label1.Text = Form5.name.ToString();
            label2.Text = Form5.type.ToString();
            label3.Text = Form5.price.ToString();
            label4.Text = Form5.available.ToString();
            pictureBox1.Image = Image.FromStream(Mem);

            avl = Convert.ToInt32(label4.Text.ToString());
            num = Convert.ToInt32(textBox1.Text.ToString());
        }   

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (num < avl)
            {
                num = num + 1;
                textBox1.Text = num.ToString();
            }
        }

        private async void button5_Click_1(object sender, EventArgs e)
        {
            String id, name = label1.Text.ToString();
            int amnt = Convert.ToInt32(textBox1.Text.ToString());
            id = Form2.ID;
            var cart = new Cart
            {
                productname = name,
                ptype = label2.Text.ToString(),
                amount = amnt,
                total = (Convert.ToInt32(label3.Text.ToString())) * amnt
            };
            int c = avl - 1;
            label4.Text = c.ToString();

            SetResponse response = await client.SetTaskAsync("Cart/" + id + "/" + name, cart);
            try
            {
                Form7 result = response.ResultAs<Form7>();
                MessageBox.Show("Product added to your cart!", "Cart", MessageBoxButtons.OK);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Sorry! Product can not be added to your cart!", "Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (num > 1)
            {
                num = num - 1;
                textBox1.Text = num.ToString();
            }
        }

    }
}
