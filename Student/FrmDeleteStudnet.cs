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

namespace Student
{
    public partial class FrmDeleteStudnet : Form
    {
        public FrmDeleteStudnet()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        public FrmDeleteStudnet(string f)
        {
            this.f = f;
            InitializeComponent();
        }
        private string f;
        private string StudentNo;
        //private DBMySchool db = new DBMySchool();
        private string strCon = @"server=.\SQL2014;database=MySchool;uid=sa;password=123;";
        
        private void btnCertain_Click(object sender, EventArgs e)
        {
            StudentNo = this.lblStuNo.Text;

            string strSQL = "Delete from Student where StudentNo=@StudentNo";

            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@StudentNo", StudentNo);
                con.Open();
                int obj = cmd.ExecuteNonQuery();
                con.Close();
                if (obj > 0)
                {
                    MessageBox.Show("操作成功！");
                    Close();
                }
                else
                {
                    MessageBox.Show("操作失败！");
                }
            }
        }

        private void FrmDelete_Load(object sender, EventArgs e)
        {
            lblStuNo.Text = f;

            string strSQL = "Select * from Student where StudentNo=@StudentNo";

            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@StudentNo", f);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lblStuNo.Text = reader.GetString(reader.GetOrdinal("StudentNo"));
                    lblLogin.Text = reader.GetString(reader.GetOrdinal("LoginId"));
                    lblLoginPwd.Text = reader.GetString(reader.GetOrdinal("LoginPwd"));
                    lblStateId1.Text = reader.GetInt32(reader.GetOrdinal("UserStateId")) == 1 ? ("在线") : ("离线");
                    lblClass1.Text = reader.GetString(reader.GetOrdinal("ClassGuid"));
                    lblName.Text = reader.GetString(reader.GetOrdinal("StudentName"));
                    lblSex.Text = reader.GetString(reader.GetOrdinal("Sex"));
                    lblAdress.Text = reader.GetString(reader.GetOrdinal("Address"));
                }
                reader.Close();
                con.Close();
                
            }
        }
    }
}
        
