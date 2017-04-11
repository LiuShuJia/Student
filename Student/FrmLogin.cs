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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private DBHelper helper = new DBHelper();
        private void btnEnter_Click(object sender, EventArgs e)
        {
            string userName = this.txtUserName.Text.Trim ();
            string key = this.txtKey.Text.Trim();
            string strSQL = @"select log";
            int row = Convert.ToInt32(helper.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@StudentNo", userName)));
         
        }
    }
}
