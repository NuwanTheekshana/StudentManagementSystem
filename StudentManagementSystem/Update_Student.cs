using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StudentManagementSystem
{
    public partial class Update_Student_form : Form
    {
        Functions Con;
        int student_id;
        string student_fname, student_lname, student_email;

        public Update_Student_form(int id, string firstName, string lastName, string email)
        {
            InitializeComponent();
            Con = new Functions();
            student_id = id;
            student_fname = firstName;
            student_lname = lastName;
            student_email = email;
        }
        private void update_student_btn_Click(object sender, EventArgs e)
        {
            int id;
            string fname, lname, email;

            id = Convert.ToInt32(student_id_txt.Text);
            fname = fname_txt.Text;
            lname = lname_txt.Text;
            email = email_txt.Text;

            try
            {

                string Query = "Update Students set first_name = '{0}', last_name = '{1}', email = '{2}' where student_id = '{3}'";
                Query = string.Format(Query, fname, lname, email, id);
                Con.SetData(Query);
                MessageBox.Show("Student details update successfully..!");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void Update_Student_Load(object sender, EventArgs e)
        {
            student_id_txt.Text = student_id.ToString();
            fname_txt.Text = student_fname;
            lname_txt.Text = student_lname;
            email_txt.Text = student_email;
        }
    }
}
