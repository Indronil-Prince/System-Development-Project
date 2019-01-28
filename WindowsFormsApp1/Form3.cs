using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        String name, email;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            name = Form2.setName.ToString();
            email = Form2.setEmail.ToString();
            label8.Text = name.ToString();
            label7.Text = email.ToString();
            label2.Text = name.ToString();

            if ((name.Equals("ADMIN")) && (email.Equals("admin@shopit.com")))
            {
                button1.Text = "Products";
                button3.Text = "Users";
                splitContainer1.Panel1.BackgroundImage = Properties.Resources.Untitled2;
            }
            else
            {
                button1.Text = "Shop Here";
                button3.Text = "Cart";
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (button3.Text == "Cart")
            {
                Form7 f7 = new Form7();
                f7.Show();
            }
            else if (button3.Text == "Users")
            {
                Form8 f8 = new Form8();
                f8.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 fB = new Form11();
            fB.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (button1.Text == "Shop Here")
            {
                FormB fB = new FormB();
                fB.Show();
            }
            else if (button1.Text == "Products")
            {
                Form4 f4 = new Form4();
                f4.Show();
            }
        }
    }
}
