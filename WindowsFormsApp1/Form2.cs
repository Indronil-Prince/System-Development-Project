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
    public partial class Form2 : Form
    {
        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = "p5KLAkMuWdH8vLiYGMndXtJS1mEIQfgGgWtcQWDb",
            BasePath = "https://indronil-9dcac.firebaseio.com/"
        };
        IFirebaseClient client;
        String Email, Password;
        public static String setName = "", setEmail = "", ID;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(firebaseConfig);
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            Email = textBox_Email.Text.ToString();
            Password = textBox_Password.Text.ToString();
            if (Email == "" || Password == "")
            {
                MessageBox.Show("Fill up all the fields!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StringBuilder Id = new StringBuilder(Email);
                for (int i = 0; i < Email.Length; i++)
                {
                    if (Email[i] == '.')
                    {
                        Id[i] = '_';
                    }
                }
                ID = Id.ToString();
                var user = new User
                {
                    email = textBox_Email.Text.ToString(),
                    password = textBox_Password.Text.ToString()
                };

                FirebaseResponse response = await client.GetTaskAsync("User/" + ID);

                try
                {
                    User result = response.ResultAs<User>();

                    if (result.password == Password)
                    {
                        textBox_Email.Text = String.Empty;
                        textBox_Password.Text = String.Empty;
                        setName = result.username;
                        setEmail = result.email;
                        this.Hide();
                        Form3 f3 = new Form3();
                        f3.Show();
                        MessageBox.Show("Log in successful!", "Log In", MessageBoxButtons.OK);
                    }
                    else
                    {
                        textBox_Password.Text = String.Empty;
                        MessageBox.Show("Password incorrect!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Sorry! User not found", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
         }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}

