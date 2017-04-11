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
    public partial class FrmSelectStudent : Form
    {
        public FrmSelectStudent()
        {
            InitializeComponent();
        }

        private string listItems = null;

        private DBHelper helper = new DBHelper();


        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.lvwShow.Items.Clear();
            string str = "sp_SelectAll";
            string className1 = cboClass.Text;
            if (cboClass.Text == "查询所有")
            {
                className1 = "";
            }
            string sex1 = cboSex.Text;
            if (cboSex.Text == "查询所有")
            {
                sex1 = "";
            }
            string stuName = txtName.Text;
            if (txtName.Text == "查询所有")
            {
                stuName = "";
            }

            using (IDataReader reader = helper.ExecuteReader(str, CommandType.StoredProcedure, new SqlParameter("@Sex", sex1),
                new SqlParameter("@ClassName", className1), new SqlParameter("@StudentName", stuName)))
            {
                Task.Run(new Action(() =>
                {
                    System.Threading.Thread.Sleep(1000);
                }));
                while (reader.Read())
                {
                    string stuNO = reader.GetString(reader.GetOrdinal("StudentNO"));
                    string loginId = reader.GetString(reader.GetOrdinal("LoginId"));
                    int userStateId = reader.GetInt32(reader.GetOrdinal("UserStateId"));
                    string userState = userStateId == 1 ? "在线" : "离线";
                    string className = reader.GetString(reader.GetOrdinal("ClassName"));
                    string studentName = reader.GetString(reader.GetOrdinal("StudentName"));
                    string address = reader.GetString(reader.GetOrdinal("Address"));
                    string sex = reader.GetString(reader.GetOrdinal("Sex"));
                    string stuGuid = reader.GetString(reader.GetOrdinal("StudentGuid"));

                    ListViewItem list = new ListViewItem(loginId);
                    list.SubItems.Add(userState);
                    list.SubItems.Add(className);
                    list.SubItems.Add(stuNO);
                    list.SubItems.Add(studentName);
                    list.SubItems.Add(sex);
                    list.SubItems.Add(address);
                    list.Tag = stuGuid;
                    this.lvwShow.Items.Add(list);
                }
                reader.Close();
               
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddStudnet frm = new FrmAddStudnet();

            frm.ShowDialog();
            this.btnSelect.PerformClick();
        }

        private void tsmiModify_Click(object sender, EventArgs e)
        {
            if (this.lvwShow.SelectedItems.Count > 0)
            {
                listItems = lvwShow.SelectedItems[0].Tag.ToString();
                if (listItems != null)
                {
                    FrmModifyStudent frm = new FrmModifyStudent();
                    frm.ShowDialog();
                }
                this.btnSelect.PerformClick();
            }
            else
            {
                MessageBox.Show("没有选择信息");
            }
        }

        private void tsmiDel_Click(object sender, EventArgs e)
        {
            if (this.lvwShow.SelectedItems.Count > 0)
            {
                listItems = lvwShow.SelectedItems[0].Tag.ToString();
                DialogResult dr = MessageBox.Show("是否确定删除？", "删除", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    FrmDeleteStudnet frm = new FrmDeleteStudnet();
                    frm.ShowDialog();
                }
                this.btnSelect.PerformClick();
            }
            else
            {
                MessageBox.Show("没有选择信息");
            }
        }

        private void tsmiModifyPwd_Click(object sender, EventArgs e)
        {
         
        }

        private void FrmSelectStudent_Load(object sender, EventArgs e)
        {
            string strSql = "sp_selectClassName";
            using (IDataReader reader = helper.ExecuteReader(strSql, CommandType.StoredProcedure))
            {
                while (reader.Read())
                {
                    object obj = reader.GetString(reader.GetOrdinal("ClassName"));
                    cboClass.Items.Add(obj);
                }
                reader.Close();
            }
        }
    }
}
