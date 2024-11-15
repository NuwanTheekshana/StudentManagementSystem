using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StudentManagementSystem
{
    public partial class Login : Form
    {
        Functions Con;
        Encryption Pass;
        public Login()
        {
            InitializeComponent();
            Con = new Functions();
            Pass = new Encryption();
        }

        public static int user_id;
        public static string user_email = "";
        public static string username = "";
        public static string role = "";

        private void login_btn_Click(object sender, EventArgs e)
        {
            //Home home = new Home();
            //home.Show();
            //this.Hide();
            string email = email_txt.Text;
            string password = password_txt.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            string Query = "select * from Users where email = '{0}'";
            Query = string.Format(Query, email);
            DataTable dt = Con.GetData(Query);

            if (dt.Rows.Count > 0)
            {
                string hashedPasswordFromDatabase = dt.Rows[0]["password_hash"].ToString();

                if (Pass.VerifyPassword(password, hashedPasswordFromDatabase))
                {
                    user_id = Convert.ToInt32(dt.Rows[0]["user_id"].ToString());
                    user_email = dt.Rows[0]["email"].ToString();
                    username = dt.Rows[0]["username"].ToString();
                    role = dt.Rows[0]["role"].ToString();
                    Home home = new Home(user_id, user_email, username, role);
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect email or password. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Incorrect email or password. Please try again..!");
                email_txt.Text = "";
                password_txt.Text = "";
            }
        }
    
    }
}
