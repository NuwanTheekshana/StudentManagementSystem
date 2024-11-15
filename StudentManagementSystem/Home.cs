using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Home : Form
    {
        private Encryption Pass;
        private Functions Con;

        int user_id;
        string user_email, username, role;
        public Home(int user_id, string user_email, string username, string role)
        {
            InitializeComponent();
            Pass = new Encryption();
            Con = new Functions();

            user_id_lbl.Text = Login.user_id+"";
            user_email_lbl.Text = Login.user_email;
            username_lbl.Text = Login.username;
            userrole_lbl.Text = Login.role;
        }

        private void admin_add_btn_Click_1(object sender, EventArgs e)
        {

        }

        private void AdminListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (UserListGrid.Columns[e.ColumnIndex].HeaderText == "Delete")
            {

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this user", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int id;
                    id = Convert.ToInt32(UserListGrid.Rows[e.RowIndex].Cells["Id"].Value);

                    try
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string Query = "Delete Users where user_id = '{0}'";
                        Query = string.Format(Query, id);
                        Con.SetData(Query);

                        MessageBox.Show("User delete successfully..!");
                        Show_user_list();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.grade_data_vmTableAdapter.Fill(this.student_dbDataSet6.grade_data_vm);
            this.enrollment_data2_vmTableAdapter.Fill(this.student_dbDataSet4.enrollment_data2_vm);
            this.enrollment_data_vmTableAdapter.Fill(this.student_dbDataSet3.enrollment_data_vm);
            this.coursesTableAdapter.Fill(this.student_dbDataSet2.Courses);
            this.studentsTableAdapter.Fill(this.student_dbDataSet1.Students);
            this.usersTableAdapter.Fill(this.student_dbDataSet.Users);

            string user_id = user_id_lbl.Text;

            string userrole = userrole_lbl.Text;

            if (userrole == "Student")
            {
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    if (tab.Text != "User Profile" && tab.Text != "Grades" && tab.Text != "Home")
                    {
                        tabControl1.TabPages.Remove(tab);
                    }
                }
            }
            else if (userrole == "Teacher")
            {
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    if (tab.Text != "User Profile" && tab.Text != "Grades" && tab.Text != "Home" && tab.Text != "Attendance")
                    {
                        tabControl1.TabPages.Remove(tab);
                    }
                }
            }
            else
            {
                ShowAllTabs();
            }


        }


        private void ShowAllTabs()
        {
            TabPage attendanceTab = new TabPage("Attendance");
            TabPage gradesTab = new TabPage("Grades");
            TabPage studentRegistrationTab = new TabPage("Student Registration");
            TabPage coursesTab = new TabPage("Courses");
            TabPage homeTab = new TabPage("Home");

            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add(homeTab);
            tabControl1.TabPages.Add(attendanceTab);
            tabControl1.TabPages.Add(gradesTab);
            tabControl1.TabPages.Add(studentRegistrationTab);
            tabControl1.TabPages.Add(coursesTab);
        }

        private void admin_add_btn_Click(object sender, EventArgs e)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            try
            {
                if (string.IsNullOrWhiteSpace(fname_txt.Text) ||
                    string.IsNullOrWhiteSpace(lname_txt.Text) ||
                    string.IsNullOrWhiteSpace(email_txt.Text) ||
                    string.IsNullOrWhiteSpace(password_txt.Text) ||
                    string.IsNullOrWhiteSpace(role_txt.Text))
                {
                    MessageBox.Show("Missing Data..!");
                    return;
                }

                if (!Regex.IsMatch(email_txt.Text, emailPattern))
                {
                    MessageBox.Show("Invalid email format. Please enter a valid email..!");
                    return;
                }

                string fname = fname_txt.Text;
                string lname = lname_txt.Text;
                string username = fname + "." + lname;
                string email = email_txt.Text;
                string password = password_txt.Text;
                string role = role_txt.Text;
                string encryptedPassword = Pass.HashPassword(password);


                string Query = "insert into Users(username, password_hash, role, email) values('{0}', '{1}', '{2}', '{3}')";
                Query = string.Format(Query, username, encryptedPassword, role, email);
                Con.SetData(Query);

                MessageBox.Show("User details added successfully..!");

                Show_user_list();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void ClearInputFields()
        {
            fname_txt.Text = "";
            lname_txt.Text = "";
            email_txt.Text = "";
            password_txt.Text = "";
            role_txt.Text = "";
        }




        private void Show_user_list()
        {
            string Query = "select * from Users";
            UserListGrid.DataSource = Con.GetData(Query);
        }

        private void Show_student_list()
        {
            string Query = "select * from Students";
            studnet_data.DataSource = Con.GetData(Query);
        }

        private void Show_course_list()
        {
            string Query = "select * from Courses";
            course_dataGridView1.DataSource = Con.GetData(Query);
        }

        private void Show_grade_list()
        {
            string Query = "select * from Grades";
            dataGridView2.DataSource = Con.GetData(Query);
        }
        

        private void add_student_Click(object sender, EventArgs e)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            try
            {
                if (string.IsNullOrWhiteSpace(fname_text.Text) ||
                    string.IsNullOrWhiteSpace(lname_text.Text) ||
                    string.IsNullOrWhiteSpace(email_text.Text) ||
                    string.IsNullOrWhiteSpace(dob_txt.Text) ||
                    string.IsNullOrWhiteSpace(gender_txt.Text) ||
                    string.IsNullOrWhiteSpace(phone_txt.Text) ||
                    string.IsNullOrWhiteSpace(address_txt.Text) ||
                    string.IsNullOrWhiteSpace(enrollment_date_txt.Text))
                {
                    MessageBox.Show("Missing Data..!");
                    return;
                }

                if (!Regex.IsMatch(email_text.Text, emailPattern))
                {
                    MessageBox.Show("Invalid email format. Please enter a valid email..!");
                    return;
                }

                string fname = fname_text.Text;
                string lname = lname_text.Text;
                string email = email_text.Text;
                string dob = dob_txt.Value.ToString("yyyy-MM-dd");
                string gender = gender_txt.Text;
                string phone = phone_txt.Text;
                string address = address_txt.Text;
                string enrollment_date = enrollment_date_txt.Value.ToString("yyyy-MM-dd");


                string Query = "insert into Students(first_name, last_name, dob, gender, email, phone, address, enrollment_date) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')";
                Query = string.Format(Query, fname, lname, dob, gender, email, phone, address, enrollment_date);
                Con.SetData(Query);

                MessageBox.Show("Student details added successfully..!");

                Show_student_list();

                fname_text.Text = "";
                lname_text.Text = "";
                email_text.Text = "";
                dob_txt.Text = "";
                gender_txt.Text = "";
                phone_txt.Text = "";
                address_txt.Text = "";
                enrollment_date_txt.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void studnet_data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (studnet_data.Columns[e.ColumnIndex].HeaderText == "Update")
            {
                int id;
                string firstName, lastName, email;

                id = Convert.ToInt32(studnet_data.Rows[e.RowIndex].Cells["student_id"].Value);
                firstName = Convert.ToString(studnet_data.Rows[e.RowIndex].Cells["first_name"].Value);
                lastName = Convert.ToString(studnet_data.Rows[e.RowIndex].Cells["last_name"].Value);
                email = Convert.ToString(studnet_data.Rows[e.RowIndex].Cells["email"].Value);

                Update_Student_form student_Update_Form = new Update_Student_form(id, firstName, lastName, email);
                student_Update_Form.ShowDialog();
            }

            if (studnet_data.Columns[e.ColumnIndex].HeaderText == "Delete")
            {

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this student", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int id;
                    id = Convert.ToInt32(studnet_data.Rows[e.RowIndex].Cells["student_id"].Value);

                    try
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string Query = "Delete Students where student_id = '{0}'";
                        Query = string.Format(Query, id);
                        Con.SetData(Query);

                        MessageBox.Show("Student delete successfully..!");
                        Show_student_list();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Show_student_list();
        }

        private void add_course_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(course_name_txt.Text) ||
                    string.IsNullOrWhiteSpace(course_desc_txt.Text))
                {
                    MessageBox.Show("Missing Data..!");
                    return;
                }

                string course_name = course_name_txt.Text;
                string course_desc = course_desc_txt.Text;


                string Query = "insert into Courses(course_name, course_description) values('{0}', '{1}')";
                Query = string.Format(Query, course_name, course_desc);
                Con.SetData(Query);

                MessageBox.Show("Course details added successfully..!");

                Show_course_list();

                course_name_txt.Text = "";
                course_desc_txt.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void course_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (course_dataGridView1.Columns[e.ColumnIndex].HeaderText == "Delete")
            {

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this course?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int id;
                    id = Convert.ToInt32(course_dataGridView1.Rows[e.RowIndex].Cells["Course_Id"].Value);

                    try
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string Query = "Delete Courses where course_id = '{0}'";
                        Query = string.Format(Query, id);
                        Con.SetData(Query);

                        MessageBox.Show("Course delete successfully..!");
                        Show_course_list();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void add_entrollment_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(student_combo.SelectedValue?.ToString()) ||
                    string.IsNullOrWhiteSpace(course_combo.SelectedValue?.ToString()) ||
                    string.IsNullOrWhiteSpace(enrollement_datepicker.Text))
                {
                    MessageBox.Show("Missing Data..!");
                    return;
                }

                int student = int.Parse(student_combo.SelectedValue.ToString());
                int course = int.Parse(course_combo.SelectedValue.ToString());
                string enrollement_date = enrollement_datepicker.Value.ToString("yyyy-MM-dd");


                string Query = "insert into Enrollments(student_id, course_id, enrollment_date, status) values('{0}', '{1}', '{2}', '{3}')";
                Query = string.Format(Query, student, course, enrollement_date, "Active");
                Con.SetData(Query);

                MessageBox.Show("Course details added successfully..!");

                Show_course_list();

                course_name_txt.Text = "";
                course_desc_txt.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (enrollment_datagrid.Columns[e.ColumnIndex].HeaderText == "Delete")
            {

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this enrollment?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int id;
                    id = Convert.ToInt32(enrollment_datagrid.Rows[e.RowIndex].Cells["enrollment_id"].Value);

                    try
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string Query = "Delete Enrollments where enrollment_id = '{0}'";
                        Query = string.Format(Query, id);
                        Con.SetData(Query);

                        MessageBox.Show("Enrollment delete successfully..!");
                        Show_course_list();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Present")
            {
                    int student_id;
                    int course_id;
                    student_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["attendance_student_id"].Value);
                    course_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["attendance_course_id"].Value);
                    string attendance_date = attendance_date_picker.Value.ToString("yyyy-MM-dd");

                    try
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string Query = "insert into Attendance(student_id, course_id, attendance_date, status) values('{0}', '{1}', '{2}', '{3}')";
                        Query = string.Format(Query, student_id, course_id, attendance_date, "Present");
                        Con.SetData(Query);

                        MessageBox.Show("Addendance update successfully..!");
                        Show_course_list();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }

            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Absent")
            {
                    int student_id;
                    int course_id;
                    student_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["attendance_student_id"].Value);
                    course_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["attendance_course_id"].Value);
                    string attendance_date = attendance_date_picker.Value.ToString("yyyy-MM-dd");

                    try
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string Query = "insert into Attendance(student_id, course_id, attendance_date, status) values('{0}', '{1}', '{2}', '{3}')";
                        Query = string.Format(Query, student_id, course_id, attendance_date, "Absent");
                        Con.SetData(Query);

                        MessageBox.Show("Addendance update successfully..!");
                        Show_course_list();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }
        }

        private void logout_btn_Click_1(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void add_grades_Click_1(object sender, EventArgs e)
        {
            int student = int.Parse(grade_studenet_combo.SelectedValue.ToString());
            int course = int.Parse(grade_course_combo.SelectedValue.ToString());
            string grade = grade_combo.Text;
            string remark = remark_txt.Text;
            
            try
            {
                DateTime currentDateTime = DateTime.Now;
                string Query = "insert into Grades(student_id, course_id, grade, remarks) values('{0}', '{1}', '{2}', '{3}')";
                Query = string.Format(Query, student, course, grade, remark);
                Con.SetData(Query);

                MessageBox.Show("Grade update successfully..!");
                Show_grade_list();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Delete")
            {

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this grade?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    int id;
                    id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["grade_id"].Value);

                    try
                    {
                        DateTime currentDateTime = DateTime.Now;
                        string Query = "Delete Grades where grade_id = '{0}'";
                        Query = string.Format(Query, id);
                        Con.SetData(Query);

                        MessageBox.Show("Grade delete successfully..!");
                        Show_grade_list();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
