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
    public partial class FrmModifyStudent : Form
    {
        public FrmModifyStudent()
        {
            InitializeComponent();
        }
        public FrmModifyStudent(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }
        public string guid = null;
        private Form parentForm;
        private DBHelper helper = new DBHelper();
        private string strCon = @"server=DEEP-20161031WY;database=MySchool;uid=sa;password=123;";
        private void FrmModifyStudent_Load(object sender, EventArgs e)
        {
            string strSQL = "sp_classQuery  ";
            using (IDataReader reader = helper.ExecuteReader(strSQL, CommandType.StoredProcedure))
            {
                while (reader.Read())
                {
                    string gradename = reader.GetString(reader.GetOrdinal("ClassName"));
                    this.cboClassName.Items.Add(gradename);
                }
                reader.Close();
            }
            if (guid != null)
            {
                strSQL = "sp_studentQuery ";
            }
            using (IDataReader reader = helper.ExecuteReader(strSQL, CommandType.StoredProcedure, new SqlParameter("@StudentGuid", guid)))
            {
                if (reader.Read())
                {
                    this.txtStudentNo.Text = reader.GetString(reader.GetOrdinal("StudentNO"));
                    this.txtStudentName.Text = reader.GetString(reader.GetOrdinal("StudentName"));
                    if (reader.GetString(reader.GetOrdinal("Sex")) == "女")
                    {
                        this.radioButton2.Checked = true;
                    }
                    else
                    {
                        this.radioButton1.Checked = true;
                    }
                    this.cboUserState.Text = reader.GetString(reader.GetOrdinal("UserStateId")) == "1" ? "在线" : "离线";
                    this.txtAddress.Text = reader.GetString(reader.GetOrdinal("Address"));
                    this.cboClassName.Text = reader.GetString(reader.GetOrdinal("ClassName")); ;
                }
                reader.Close();
            }
        }
        }
}
