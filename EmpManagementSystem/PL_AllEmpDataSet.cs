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

namespace EmpManagementSystem
{
    public partial class PL_AllEmpDataSet : Form
    {
        public PL_AllEmpDataSet()
        {
            InitializeComponent();
        }

        private void PL_AllEmpDataSet_Load(object sender, EventArgs e)
        {
            GetEmpDetails();
        }

        private void GetEmpDetails()
        {
            /*SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");         
            SqlCommand cmd = new SqlCommand("SELECT emp_service_no, emp_report_name, emp_address, emp_gender, emp_telephone_no, emp_educational_level, emp_nic_no, emp_Birth_date FROM dbo.dbo_emp_details", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr= cmd.ExecuteReader();
            dt.Load(sdr);
            cmd.ExecuteNonQuery();
            con.Close();

            dataGridView1.DataSource= dt;   */

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SSTT7DL;Initial Catalog=ReactTestDB;User ID=user;Password=user123");
            SqlCommand cmd = new SqlCommand("SELECT emp_service_no, emp_report_name, emp_address, emp_gender, emp_telephone_no, emp_educational_level, emp_nic_no, emp_Birth_date FROM dbo.dbo_emp_details", con);
            DataSet ds = new DataSet();
            con.Open();
            SqlDataReader sdr= cmd.ExecuteReader();

            /*foreach (DataRow dr in ds.)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    MessageBox.Show(dr[dc.ColumnName].ToString());
                }
            }*/
        }
    }
}
