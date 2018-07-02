using EF_DB_MARKET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DB_Proj.SubPages
{
    /// <summary>
    /// Interaction logic for BackupPage.xaml
    /// </summary>
    public partial class BackupPage : System.Windows.Controls.UserControl
    {
        public BackupPage()
        {
            InitializeComponent();
        }

        //选择还原文件
        private void btOpenfile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "All files (*.*)|*.*|Excel Files (*.sql)|*.sql"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                RestoreRoute.Text = openFileDialog.FileName;
            }
        }
       //选择备份地址
        private void btSavefile_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            SaveRoute.Text = m_Dir;
        }


        //开始还原
        private void btRestore_Click(object sender, RoutedEventArgs e)
        {
            if (RestoreRoute.Text == null||RestoreRoute.Text=="")
                return;
            ///杀死原来所有的数据库连接进程
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=master;User ID=sa;pwd =blacklist4000";
            conn.Open();
            string sql = "SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" +
                          "db_Market" + "'";
            SqlCommand cmd1 = new SqlCommand(sql, conn);
            SqlDataReader dr;
            ArrayList list = new ArrayList();
            try
            {
                dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr.GetInt16(0));
                }
                dr.Close();
            }
            catch (SqlException eee)
            {
                System.Windows.MessageBox.Show(eee.ToString());
            }
            finally
            {
                conn.Close();
            }

            for (int i = 0; i < list.Count; i++)
            {
                conn.Open();
                cmd1 = new SqlCommand(string.Format("KILL {0}", list[i].ToString()), conn);
                cmd1.ExecuteNonQuery();
                conn.Close();
                //System.Windows.MessageBox.Show("系统已经清除的数据库线程： " + list[i].ToString() + "\r\n正在还原数据库！");
            }
            //这里一定要是master数据库，而不能是要还原的数据库，因为这样便变成了有其它进程
            //占用了数据库。
            string constr = @"Data Source=.;Initial Catalog=master;User ID=sa;pwd =your password";
            string database = "db_Market";
            string path = RestoreRoute.Text;
            string BACKUP = String.Format("RESTORE DATABASE {0} FROM DISK = '{1}' WITH REPLACE", database, path);
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(BACKUP, con);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("还原成功！");
               
            }
            catch (SqlException ee)
            {
                //throw(ee);
                //MessageBox.Show("还原失败");
                System.Windows.MessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
           
        
    }
       
        //开始备份
        private void btBackup_Click(object sender, RoutedEventArgs e)
        {
            if (SaveRoute.Text == null || SaveRoute.Text == "")
                return;
            string connectionString = "server=.;database=db_Market;uid=sa;pwd=your password here";
            SqlConnection conn = new SqlConnection(connectionString);
            //还原的数据库MyDataBase
            //string sql = "BACKUP DATABASE " + "db_Market" + " TO DISK = '" + "E:\\Code_Proj\\SQL_Server_exercise\\baefde" + ".bak' ";
            DateTime d=new DateTime();
            TimeSpan ts = d.ToUniversalTime() - new DateTime(1970, 1, 1);
            string sql = "BACKUP DATABASE " + "db_Market" + " TO DISK = '" + SaveRoute.Text + ts.TotalMilliseconds.ToString() +"_DB_MARKET.bak' ";
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                comm.ExecuteNonQuery();
                System.Windows.MessageBox.Show("已备份至 : " + SaveRoute.Text);
            }
            catch (Exception err)
            {
                string str = err.Message;
                conn.Close();
                System.Windows.MessageBox.Show("备份失败"+err);
            }
            conn.Close();//关闭数据库连接

        }
    }
}
