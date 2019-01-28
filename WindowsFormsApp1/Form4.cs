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

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        String name, type, price, avail, uname, uemail;
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);
            uname = Form2.setName.ToString();
            uemail = Form2.setEmail.ToString();
            label8.Text = uname.ToString();
            label7.Text = uemail.ToString();

            if ((uname.Equals("ADMIN")) && (uemail.Equals("admin@shopit.com")))
            {
                splitContainer1.Panel1.BackgroundImage = Properties.Resources.Untitled2;
            }
            else
            {
                button1.Text = "Shop Here";
                button3.Text = "Cart";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                label7.Text = String.Empty;
                label8.Text = String.Empty;
                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();
                MessageBox.Show("Logged out successfully!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
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
            Form8 f8 = new Form8();
            f8.Show();
        }

        String imgLocation = "";
        private void button2_Click_1(object sender, EventArgs e)
        {
try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "JPG files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    imgLocation = dialog.FileName;
                    pictureBox1.ImageLocation = imgLocation;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("An Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
 private async void button3_Click_1(object sender, EventArgs e)
        {
if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(imgLocation))
            {
                MessageBox.Show("All the fields are required");
            }
            else
            {
                name = textBox1.Text.ToString().Trim();
                type = textBox2.Text.ToString().Trim();
                price = textBox3.Text.ToString().Trim();
                Int32 Price = Convert.ToInt32(price);
                avail = textBox4.Text.ToString().Trim();
                Int32 Avail = Convert.ToInt32(avail);
                var stream = File.Open(@imgLocation, FileMode.Open);
                var task = new FirebaseStorage("indronil-9dcac.appspot.com")
                    .Child("Images")
                    .Child("Products")
                    .Child(type)
                    .Child(name)
                    .PutAsync(stream);
                var downloadUrl = await task;

                var user = new Product
                {
                    productname = name,
                    ptype = type,
                    price = Price,
                    available = Avail,
                    image = downloadUrl
                };
                SetResponse response = await client.SetTaskAsync("Product/" + type +"/" + name, user);
                Product result = response.ResultAs<Product>();

                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                textBox4.Text = String.Empty;
                pictureBox1.ImageLocation = null;
                MessageBox.Show("Product added!", "New Product");
            }
        }
    }
}
