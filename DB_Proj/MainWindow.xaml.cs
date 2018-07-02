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

namespace DB_Proj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            editpage = new EditTables();
            ContentControl2.Content = editpage;
        }
        public bool IsFirst = true;
        EditTables editpage;
        ViewTables viewpage;
        SubPages.AuthorityPage authoritypage;
        SubPages.BackupPage backuppage;
        SubPages.InfoPage infopage;
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, SelectionChangedEventArgs e)
        {
            if(IsFirst)
            {
                IsFirst = !IsFirst;
                return;
            }
            int index = DemoItemsListBox.SelectedIndex;
            
            switch(index)
            {
                case 1://编辑
                    if (editpage == null)
                        editpage = new EditTables();
                    ContentControl2.Content = editpage;
                    break;
                case 2://统计
                    if (viewpage == null)
                        viewpage = new ViewTables();
                    ContentControl2.Content = viewpage;
                    break;
                case 3://备份
                    if (backuppage == null)
                        backuppage = new SubPages.BackupPage();
                    ContentControl2.Content = backuppage;
                    break;
                case 4://权限
                    if (authoritypage == null)
                        authoritypage = new SubPages.AuthorityPage(
                            editpage.btDelete_tbCustomer,
                            editpage.btRefresh_tbCustomer,
                             editpage.btDelete_tbShoppingRecord,
                            editpage.btRefresh_tbShoppingRecord,
                             editpage.btDelete_tbProduct,
                            editpage.btRefresh_tbProduct,
                            editpage.dg_Customer
                            );
                    ContentControl2.Content = authoritypage;
                    break;
                case 5://Info
                    if (infopage == null)
                        infopage = new SubPages.InfoPage();
                    ContentControl2.Content = infopage;
                    break;
                default:
                    break;

            }
           

        }

     
    }
}
