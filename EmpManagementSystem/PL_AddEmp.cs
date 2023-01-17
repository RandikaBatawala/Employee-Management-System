using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmpManagementSystem
{
    public partial class PL_AddEmp : Form
    {
        char Gender;
        char EduLevel;
        string nic;
        

        public PL_AddEmp()
        {
            InitializeComponent();
        }

        private void PL_AddEmp_Load(object sender, EventArgs e)
        {
            /*SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT emp_service_no" +
                                            "FROM dbo_emp_details" +
                                            "WHERE emp_service_no=(SELECT MAX(emp_service_no) " +
                                                                   "FROM dbo_emp_details)", con))
            {

            }
            
            textBox1.Text= */

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
            {
                e.Handled = true;
            }
            else if (textBox1.TextLength > 6)
            {
                //char ch = e.KeyChar;
                if (ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
                {
                    e.Handled = true;
                }
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gender = 'M'; //Male
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gender = 'F'; //Female
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "After O/L")
            {
                EduLevel = 'O'; //O/L
            }
            else if (comboBox1.Text == "After A/L")
            {
                EduLevel = 'A'; //a/L
            }
            else if (comboBox1.Text == "Graduated")
            {
                EduLevel = 'G'; //Graduated
            }
            else if (comboBox1.Text == "Other")
            {
                EduLevel = 'N'; //Other
            }
        }

        private void serno_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "Service Number should not be left blank!");
            }
            else if (textBox1.TextLength > 8)
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "Service Number should be contain 8 numbers or less.");
            } 
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, "");
            }
        }

        private void comboBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void name_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox2, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox2, "");
            }
        }

        private void add_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "Address should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox3, "");
            }
        }

        private void nic_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                e.Cancel = true;
                textBox5.Focus();
                errorProvider1.SetError(textBox5, "NIC should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox5, "");
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");

            try
            {

               
                    if (nic != null)
                    {
                        con.Open();

                        String strDateFormat = "yyyy-MM-dd";//change accordingly if format is something different
                        DateTime date = DateTime.ParseExact(dateTimePicker1.Text, strDateFormat, CultureInfo.InvariantCulture);

                        SqlCommand cmd = new SqlCommand("INSERT INTO dbo_emp_details(emp_service_no, " +
                            "                                                        emp_report_name, " +
                            "                                                        emp_address, " +
                            "                                                        emp_gender, " +
                            "                                                        emp_telephone_no, " +
                            "                                                        emp_educational_level, " +
                            "                                                        emp_nic_no, " +
                            "                                                        emp_Birth_date) " +
                            "                                   VALUES('" + textBox1.Text + "', " +
                            "                                          '" + textBox2.Text + "'," +
                            "                                          '" + textBox3.Text + "'," +
                            "                                          '" + Gender + "', " +
                            "                                          '" + textBox4.Text + "', " +
                            "                                          '" + EduLevel + "', " +
                            "                                          '" + nic + "', " +
                            "                                           '" + date + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        if (MessageBox.Show("Data Inserted Successfully.") == DialogResult.OK)
                        {
                            this.Hide();
                            PL_AllEmp allEmp = new PL_AllEmp();
                            allEmp.Show();
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(textBox5, "Please input a valid NIC.");
                    }
                
            }
            
            catch
            {
                System.Windows.Forms.MessageBox.Show("Service Number Already Exists!");
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string str = textBox5.Text;
            int length = textBox5.TextLength;

            Regex oldv = new Regex(@"^\d{9}[v]$");
            Regex oldV = new Regex(@"^\d{9}[V]$");
          
            Regex New = new Regex(@"^[0-9]{12}$");

            if (length==10 )
            {
                if(oldv.IsMatch(str) || oldV.IsMatch(str))
                {
                    nic = str;
                }
                else
                {
                    errorProvider1.SetError(textBox5, "NIC should be contain V or v!");    
                }

            }
            else if(length==12 )
            {
                if (New.IsMatch(str))
                {
                    nic = str;
                }
                else
                {
                    errorProvider1.SetError(textBox5, "New NIC should be contain only 12 digits!");
                }
            }
            
            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {   
            char ch = e.KeyChar;
            if(textBox5.TextLength<9 && textBox5.TextLength>=0)
            {
                if (!char.IsDigit(ch) && ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
                {
                    e.Handled = true;
                }
            }

            /*else if (textBox5.TextLength == 11)
            {
                if (!char.IsDigit(ch) && ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
                {
                    e.Handled = true;
                }
            }*/
            
            else if (textBox5.TextLength >11)
            {
                if ( ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
                {
                    e.Handled = true;
                }
            }

            /*else if (textBox5.TextLength == 9)
            {
                if (ch != 'v' || ch != 'V')
                {
                    errorProvider1.SetError(textBox5, "NIC should be contain V or v!");
                }
            }*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PL_AllEmp allEmp = new PL_AllEmp();
            allEmp.Show();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
            {
                e.Handled = true;
            }
            else if (textBox4.TextLength > 9)
            {
                //char ch1 = e.KeyChar;
                if (ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (textBox4.TextLength<10)
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider1.SetError(textBox4, "Telephone Number should be a valid number!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox4, "");
            }
        }
    }
}