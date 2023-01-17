using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace EmpManagementSystem
{
    public partial class PL_AllEmp : Form
    {
        public static string SetValueForText1 = "";
  

        public PL_AllEmp()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void PL_AllEmp_Load(object sender, EventArgs e)
        {
            // changeHeader();
            FillDGV();
            SetRowNumber();
            dataGridView1.Columns[1].HeaderText = "Service Number";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Address";
            dataGridView1.Columns[4].HeaderText = "Gender";
            dataGridView1.Columns[5].HeaderText = "Telephone";
            dataGridView1.Columns[6].HeaderText = "Education Level";
            dataGridView1.Columns[7].HeaderText = "NIC";
            dataGridView1.Columns[8].HeaderText = "Birth Day";

            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.Name = "Edit Details";
            btn1.Text = "Edit";
            btn1.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn1);
            btn1 = new DataGridViewButtonColumn();

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            btn2.Name = "Delete Details";
            btn2.Text = "Delete";
            btn2.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn2);
            btn2 = new DataGridViewButtonColumn();

            //convert F into female and M into Male in Gender Feild
            
            
        }



        private void SetRowNumber()
        {
            if (dataGridView1.Rows.Count > 1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                   
                        //= (i + 1);
                }
            }
        }



        private void FillDGV()
        {
            /*string connetionString = @"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123";
            SqlConnection cnn = new SqlConnection(connetionString);
            cnn.Open();
           // MessageBox.Show("Connection Open  !");
            string query = "Select * from dbo_emp_details";
            SqlDataAdapter da= new SqlDataAdapter(query, cnn);
            DataTable dt =  new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cnn.Close();*/


            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");
            SqlCommand cmd = new SqlCommand("SELECT emp_service_no, " +
                "                                   emp_report_name, " +
                "                                   emp_address, " +
                "                                   IIF(emp_gender='F','Female','Male' )," +
                "                                   emp_telephone_no, " +
                "                                   CASE WHEN emp_educational_level IN ('O') THEN ('After O/L') " +
                "                                        WHEN emp_educational_level IN('A') THEN('After A/L') " +
                "                                        WHEN emp_educational_level IN('G') THEN('Graduate')" +
                "                                        ELSE('Other') END  , " +
                "                                   emp_nic_no," +
                "                                   emp_Birth_date " +
                                            "FROM dbo.dbo_emp_details", con);
            
            DataTable dt = new DataTable();
            
            con.Open();
            
            SqlDataReader sdr = cmd.ExecuteReader();
              //string gender = sdr[3].ToString();

            /* (char.Parse((string)sdr["emp_gender"]) == 'F') 
            {
                dt.Load(sdr["emp_gender"] = "Female");
            }*/
            dt.Load(sdr);
            cmd.ExecuteNonQuery();
            con.Close();

            dataGridView1.DataSource = dt;

            /*using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                dt.Load(sdr);
                cmd.ExecuteNonQuery();
                dataGridView1.DataSource = dt;
            }
            con.Close();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PL_AddEmp addEmp = new PL_AddEmp();
            addEmp.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // int gg = e.ColumnIndex;
            //Type tp = dataGridView1.CurrentRow.Cells[1].Value.GetType();

            if (e.ColumnIndex== 9 )
            {
                string id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                SetValueForText1=id;
                this.Hide();
                PL_EditEmp EditEmp = new PL_EditEmp();
                EditEmp.Show();
            } 
            else if (e.ColumnIndex == 10)
            {
                if(MessageBox.Show("Are you sure to DELETE this employee?" , "Caption", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Exclamation)== DialogResult.Yes)
                {
                    
                    string id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                   
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo_emp_details " +
                                                    "WHERE emp_service_no= '"+id+"'",con);
                    cmd.ExecuteNonQuery();
                    if(MessageBox.Show("Data Deleted Successfully.") == DialogResult.OK)
                    {
                        this.Hide();
                    PL_AllEmp allEmp = new PL_AllEmp();
                    allEmp.Show();
                    }
                    con.Close();

                }
                
            }

            else if (e.ColumnIndex == 1 )
            {
                Type tp = dataGridView1.CurrentRow.Cells[3].Value.GetType();
                if (tp.Equals(typeof(decimal)))
                {
                    string id = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    SetValueForText1 = id;
                    this.Hide();
                    PL_EditEmp EditEmp = new PL_EditEmp();
                    EditEmp.Show();
                }
               
            }
            else if(e.ColumnIndex == 2 )
            {
                Type tp = dataGridView1.CurrentRow.Cells[3].Value.GetType();
                if (tp.Equals(typeof(decimal)))
                {
                    if (MessageBox.Show("Are you sure to DELETE this employee?", "Caption", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {

                        string id = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM dbo_emp_details " +
                                                        "WHERE emp_service_no= '" + id + "'", con);
                        cmd.ExecuteNonQuery();
                        if (MessageBox.Show("Data Deleted Successfully.") == DialogResult.OK)
                        {
                            this.Hide();
                            PL_AllEmp allEmp = new PL_AllEmp();
                            allEmp.Show();
                        }
                        con.Close();

                    }
                }
            }

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex+1 );
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");

            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT emp_service_no," +
                "                                             emp_report_name, " +
                "                                             emp_address, " +
                "                                             IIF(emp_gender='F','Female','Male' ), " +
                "                                             emp_telephone_no, " +
                "                                             CASE WHEN emp_educational_level IN ('O') THEN ('After O/L') " +
                "                                                  WHEN emp_educational_level IN('A') THEN('After A/L') " +
                "                                                  WHEN emp_educational_level IN('G') THEN('Graduate')" +
                "                                                  ELSE('Other') END  , " +
                "                                             emp_nic_no, " +
                "                                             emp_Birth_date " +
                                                      "FROM dbo.dbo_emp_details " +
                                                      "WHERE emp_service_no LIKE '" + textBox1.Text + "%' OR " +
                                                            "emp_report_name LIKE '" + textBox1.Text + "%' OR  " +
                                                            "emp_address LIKE '" + textBox1.Text + "%' OR " +
                                                            "emp_telephone_no LIKE '" + textBox1.Text + "%'OR " +
                                                            "emp_nic_no LIKE '" + textBox1.Text + "%'OR " +
                                                            "emp_Birth_date LIKE '" + textBox1.Text + "%'", con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
       

            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }


    }
}
