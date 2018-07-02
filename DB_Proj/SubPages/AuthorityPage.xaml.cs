using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DB_Proj.SubPages
{
    /// <summary>
    /// Interaction logic for AuthorityPage.xaml
    /// </summary>
    public partial class AuthorityPage : UserControl
    {
        public Button buttondelete, buttonrefresh, buttondelete2, buttonrefresh2, buttondelete3, buttonrefresh3;
        public DataGrid dataGrid;
        public AuthorityPage(
            Button buttondelete,
            Button buttonrefresh,
            Button buttondelete2,
            Button buttonrefresh2,
            Button buttondelete3,
            Button buttonrefresh3,
            DataGrid dataGrid)
        {
            InitializeComponent();
            this.buttondelete = buttondelete;
            this.buttonrefresh = buttonrefresh;
            this.buttondelete2 = buttondelete2;
            this.buttonrefresh2 = buttonrefresh2;
            this.buttondelete3 = buttondelete3;
            this.buttonrefresh3 = buttonrefresh3;
            this.dataGrid = dataGrid;
        }

        private void btLock_Click(object sender, RoutedEventArgs e)
        {
            if (InputPwd.Password == "1111")
            {
                InputPwd.Password = "";
                buttondelete.IsEnabled = false;
                buttonrefresh.IsEnabled = false;
                buttondelete2.IsEnabled = false;
                buttonrefresh2.IsEnabled = false;
                buttondelete3.IsEnabled = false;
                buttonrefresh3.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("密码错误!");
            }
        }

        private void btUnlock_Click(object sender, RoutedEventArgs e)
        {
         if(InputPwd.Password == "1111")
            {
                InputPwd.Password = "";
                buttondelete.IsEnabled = true;
                buttonrefresh.IsEnabled = true;
                buttondelete2.IsEnabled = true;
                buttonrefresh2.IsEnabled = true;
                buttondelete3.IsEnabled = true;
                buttonrefresh3.IsEnabled = true;

            }
            else
            {
                MessageBox.Show("密码错误!");
            }
        }
    }
}
