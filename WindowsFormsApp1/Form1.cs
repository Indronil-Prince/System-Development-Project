using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;
        String Password, ConPassword;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Password = textBox_Password.Text.ToString();
            ConPassword = textBox_ConPassword.Text.ToString();
            if((textBox_Name.Text.ToString()=="")||(textBox_Email.Text.ToString()=="") ||(Password=="")||(ConPassword==""))
            {
                MessageBox.Show("Fill up all the fields!", "Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Password == ConPassword)
            {
                var user = new User
                {
                    username = textBox_Name.Text.ToString(),
                    email = textBox_Email.Text.ToString(),
                    password = textBox_Password.Text.ToString()
                };
                var cart = new Cart
                {
                    productname = "Null",
                    amount = 0,
                    total = 0
                };

                String email = textBox_Email.Text.ToString();
                StringBuilder Id = new StringBuilder(email);
                for (int i = 0; i < email.Length; i++)
                {
                    if (email[i] == '.')
                    {
                        Id[i] = '_';
                    }
                }
                String ID = Id.ToString();

                SetResponse response = await client.SetTaskAsync("User/"+ID, user);
                User result = response.ResultAs<User>();
                SetResponse response2 = await client.SetTaskAsync("Cart/"+ID+"/0Null", cart);
                Form7 result2 = response.ResultAs<Form7>();

                textBox_Name.Text = String.Empty;
                textBox_Email.Text = String.Empty;
                textBox_Password.Text = String.Empty;
                textBox_ConPassword.Text = String.Empty;
                MessageBox.Show("Registration successful!");
                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();
            }
            else
            {
                textBox_Password.Text = String.Empty;
                textBox_ConPassword.Text = String.Empty;
                MessageBox.Show("Both passwords should match!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
