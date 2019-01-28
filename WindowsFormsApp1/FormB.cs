using FireSharp.Config;
using FireSharp.Interfaces;
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
    public partial class FormB : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;
        public static String setType = "";
        public FormB()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button3.Text = "Cart";
            setType = button5.Text;
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            setType = button6.Text;
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void FormB_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);

            label8.Text = Form2.setName.ToString();
            label7.Text = Form2.setEmail.ToString();
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
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setType = button7.Text;
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            setType = button8.Text;
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FormB fB = new FormB();
            fB.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
