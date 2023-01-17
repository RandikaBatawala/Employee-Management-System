using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EmpManagementSystem
{
    public partial class PL_EditEmp : Form
    {
        char Gender;
        char EduLevel;
        string nic;
        string serno = PL_AllEmp.SetValueForText1;

        public PL_EditEmp()
        {
            InitializeComponent();
        }

        private void PL_EditEmp_Load(object sender, EventArgs e)
        {
            
            string constr = @"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT emp_service_no, " +
                    "                                          emp_report_name, " +
                    "                                          emp_address, emp_gender, " +
                    "                                          emp_telephone_no, " +
                    "                                          emp_educational_level, " +
                    "                                          emp_nic_no, emp_Birth_date " +
                                                       "FROM dbo_emp_details " +
                                                       "WHERE emp_service_no = '" + serno + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        textBox1.Text = sdr["emp_service_no"].ToString();
                        textBox2.Text = sdr["emp_report_name"].ToString();
                        textBox3.Text = sdr["emp_address"].ToString();

                        if (char.Parse((string)sdr["emp_gender"]) == 'F')
                        {
                            radioButton1.Checked = true;
                        }
                        else if (char.Parse((string)sdr["emp_gender"]) == 'M')
                        {
                            radioButton2.Checked = true;
                        }

                        textBox4.Text = sdr["emp_telephone_no"].ToString();

                        if (char.Parse((string)sdr["emp_educational_level"]) == 'O')
                        {
                            comboBox1.Text = "After O/L";
                        }
                        else if (char.Parse((string)sdr["emp_educational_level"]) == 'A')
                        {
                            comboBox1.Text = "After A/L";
                        }
                        else if (char.Parse((string)sdr["emp_educational_level"]) == 'G')
                        {
                            comboBox1.Text = "Graduated";
                        }
                        else if (char.Parse((string)sdr["emp_educational_level"]) == 'N')
                        {
                            comboBox1.Text = "Other";
                        }
                        textBox5.Text = sdr["emp_nic_no"].ToString();
                        dateTimePicker1.Text = sdr["emp_Birth_date"].ToString();
                    }
                    con.Close();
                }
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gender = 'M'; //Male
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gender = 'F'; //Female
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


        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");

            try { 
                con.Open();

                String strDateFormat = "yyyy-MM-dd";//change accordingly if format is something different
                DateTime date = DateTime.ParseExact(dateTimePicker1.Text, strDateFormat, CultureInfo.InvariantCulture);

                SqlCommand cmd = new SqlCommand("UPDATE dbo_emp_details " +
                                                "SET  emp_report_name = '"+ textBox2.Text + "', " +
                                                "     emp_address = '" + textBox3.Text + "', " +
                                                "     emp_gender = '" + Gender + "', " +
                                                "     emp_telephone_no = '" + textBox4.Text + "', " +
                                                "     emp_educational_level = '" + EduLevel + "', " +
                                                "     emp_nic_no = '" + nic + "', " +
                                                "     emp_Birth_date = '" + date + "' " +
                                                "WHERE emp_service_no= '"+ serno + "'", con);
                cmd.ExecuteNonQuery();                                                                                                                      
                if (MessageBox.Show("Data Updated Successfully.") == DialogResult.OK)
                {
                    this.Hide();
                    PL_AllEmp allEmp = new PL_AllEmp();
                    allEmp.Show();
                }

            }
            
            catch
            {
                System.Windows.Forms.MessageBox.Show("An error occurred.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PL_AllEmp allEmp = new PL_AllEmp();
            allEmp.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
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

        private void textBox3_Validating(object sender, CancelEventArgs e)
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

        private void textBox5_Validating(object sender, CancelEventArgs e)
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string str = textBox5.Text;
            int length = textBox5.TextLength;

            Regex oldv = new Regex(@"^\d{9}[v]$");
            Regex oldV = new Regex(@"^\d{9}[V]$");

            Regex New = new Regex(@"^[0-9]{12}$");

            if (length == 10)
            {
                if (oldv.IsMatch(str) || oldV.IsMatch(str))
                {
                    nic = str;
                }
                else
                {
                    errorProvider1.SetError(textBox5, "NIC should be contain V or v!");
                }

            }
            else if (length == 12)
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
            if (textBox5.TextLength < 9 && textBox5.TextLength >= 0)
            {
                if (!char.IsDigit(ch) && ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
                {
                    e.Handled = true;
                }
            }

            else if (textBox5.TextLength > 11)
            {
                if (ch != Convert.ToChar(Keys.Back) && ch != Convert.ToChar(Keys.Delete))
                {
                    e.Handled = true;
                }
            }
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

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (textBox4.TextLength < 10)
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
