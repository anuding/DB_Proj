using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
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
using EF_DB_MARKET.Model;
using ExpressionEvaluator;

namespace DB_Proj
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class EditTables : UserControl
    {
       public EditTables()
        {
            InitializeComponent();
            DB = new db_MarketEntities();
            DB.Database.Connection.ConnectionString = App.DBConnectionString;
            //dg_Customer.ItemsSource = DB.TB_Customer.ToList();
            var info1 = from c in DB.TB_Customer select c;
            foreach (var item1 in info1)
            {
                tmp1.Add(item1);
            }

            var info2 = from c in DB.TB_ShoppingRecord select c;
            foreach (var item2 in info2)
            {
                tmp2.Add(item2);
            }

            var info3 = from c in DB.TB_Product select c;
            foreach (var item3 in info3)
            {
                tmp3.Add(item3);
            }
            dg_Customer.ItemsSource = tmp1;
            dg_ShoppingRecord.ItemsSource = tmp2;
            dg_Product.ItemsSource = tmp3;

            btDelete_tbCustomer.IsEnabled = false;
            btRefresh_tbCustomer.IsEnabled = false;

            btDelete_tbShoppingRecord.IsEnabled = false;
            btRefresh_tbShoppingRecord.IsEnabled = false;

            btDelete_tbProduct.IsEnabled = false;
            btRefresh_tbProduct.IsEnabled = false;
        }

        private db_MarketEntities DB;
        private db_MarketEntities DB1;
        string preValue1 = "";
        string preValue2 = "";
        string preValue3 = "";
        ObservableCollection<TB_Customer> tmp1 = new ObservableCollection<TB_Customer>();
        ObservableCollection<TB_ShoppingRecord> tmp2 = new ObservableCollection<TB_ShoppingRecord>();
        ObservableCollection<TB_Product> tmp3 = new ObservableCollection<TB_Product>();



        private void btSearch_Click_tbCustomer(object sender, RoutedEventArgs e)
        {
            AskForView1();
        }
        private void btSearch_Click_tbShoppingRecord(object sender, RoutedEventArgs e)
        {
            AskForView2();
        }
        private void btSearch_Click_tbProduct(object sender, RoutedEventArgs e)
        {
            AskForView3();
        }

        private void btRefresh_Click_tbCustomer(object sender, RoutedEventArgs e)
        {

            TB_Customer customer = new TB_Customer();
            customer.Cid = txt_Cid_tbCustomer.Text;
            customer.Cname = txt_Cname_tbCustomer.Text;
            try { customer.Cdeposit = int.Parse(txt_Cdeposit_tbCustomer.Text);}
            catch { MessageBox.Show("输入格式错误");return; }
            
            try {
                bool InsertOK = true;
                
                var info = from c in DB.TB_Customer select c;
                foreach (var item in info){
                    //待插入的记录与已有记录存在相同主键
                    if(item.Cid==customer.Cid){
                        InsertOK = false;
                        break;
                    }
                }
                if (InsertOK)
                {
                    DB.TB_Customer.Add(customer);
                    DB.SaveChanges();
                }
                else
                {
                    MessageBox.Show("主键冲突");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("主键冲突");
                return;
            }
         
            AskForView1();
        }
        private void btRefresh_Click_tbShoppingRecord(object sender, RoutedEventArgs e)
        {

            TB_ShoppingRecord spr = new TB_ShoppingRecord();
            spr.SRid = txt_SRid_tbShoppingRecord.Text;
            spr.Cid = txt_Cid_tbShoppingRecord.Text;
            spr.Pid = txt_Pid_tbShoppingRecord.Text;
            try { spr.SRnum = int.Parse(txt_SRnum_tbShoppingRecord.Text);
                spr.SRturnover = int.Parse(txt_SRturnover_tbShoppingRecord.Text);
            }
            catch { MessageBox.Show("输入格式错误"); return; }

            try
            {
                bool InsertOK = true;

                var info = from c in DB.TB_ShoppingRecord select c;
                foreach (var item in info)
                {
                    //待插入的记录与已有记录存在相同主键
                    if (item.SRid == spr.SRid)
                    {
                        InsertOK = false;
                        break;
                    }
                }
                if (InsertOK)
                {
                    DB.TB_ShoppingRecord.Add(spr);
                    DB.SaveChanges();
                }
                else
                {
                    MessageBox.Show("主键冲突");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("主键冲突");
                return;
            }

            AskForView2();
        }
        private void btRefresh_Click_tbProduct(object sender, RoutedEventArgs e)
        {

            TB_Product prd = new TB_Product();
            prd.Pid = txt_Pid_tbProduct.Text;
            prd.Pname = txt_Pname_tbProduct.Text;
          
            prd.Ptype = txt_Ptype_tbProduct.Text;

            try {
                prd.Pleft = int.Parse(txt_Pleft_tbProduct.Text);
                prd.Pprice = double.Parse(txt_Pprice_tbProduct.Text);
                prd.Pdiscount = double.Parse(txt_Pdiscount_tbProduct.Text);
                }
            catch { MessageBox.Show("输入格式错误"); return; }

            try
            {
                bool InsertOK = true;

                var info = from c in DB.TB_Product select c;
                foreach (var item in info)
                {
                    //待插入的记录与已有记录存在相同主键
                    if (item.Pid == prd.Pid)
                    {
                        InsertOK = false;
                        break;
                    }
                }
                if (InsertOK)
                {
                    DB.TB_Product.Add(prd);
                    DB.SaveChanges();
                }
                else
                {
                    MessageBox.Show("主键冲突");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("主键冲突");
                return;
            }

            AskForView3();
        }


        private void CellEditEnding_tbCustomer(object sender, DataGridCellEditEndingEventArgs e)
        {
            
            string newValue = (e.EditingElement as TextBox).Text;
           
            TB_Customer cus = e.Row.DataContext as TB_Customer;//输入
            var customer = DB.TB_Customer.Where(w => w.Cid == cus.Cid).FirstOrDefault();//现有
            if(cus.Cid==preValue1)//输入的新主键等于原来主键
            {
                try
                {
                    customer.Cid = cus.Cid;
                    customer.Cname = cus.Cname;
                    customer.Cdeposit = cus.Cdeposit;
                    DB.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("主键冲突");
                }
            }
            
              if (customer!=null&&
                customer.Cid == cus.Cid&&cus.Cid!=preValue1)//输入的新主键等于其他记录主键
                 {
                     MessageBox.Show("主键冲突"+ customer.Cid+cus.Cid);
                     cus.Cid = preValue1;
                 }
            if (customer == null)//输入的为全新的主键
            {
                MessageBox.Show("主键只读" );
                cus.Cid = preValue1;
            }

           
            AskForView1();


        }
        private void CellEditEnding_tbShoppingRecord(object sender, DataGridCellEditEndingEventArgs e)
        {

            string newValue = (e.EditingElement as TextBox).Text;

            TB_ShoppingRecord spr = e.Row.DataContext as TB_ShoppingRecord;//输入
            var s = DB.TB_ShoppingRecord.Where(w => w.SRid == spr.SRid).FirstOrDefault();//现有
            if (spr.SRid == preValue2)//输入的新主键等于原来主键
            {
                try
                {
                    s.SRid = spr.SRid;
                    s.Cid = spr.Cid;
                    s.Pid = spr.Pid;
                    s.SRnum = spr.SRnum;
                    s.SRturnover = spr.SRturnover;
                    
                    DB.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("主键冲突");
                }
            }

            if (s != null &&
              s.SRid == spr.SRid && spr.SRid != preValue2)//输入的新主键等于其他记录主键
            {
                MessageBox.Show("主键冲突");
                spr.SRid = preValue2;
            }
            if (s == null)//输入的为全新的主键
            {
                MessageBox.Show("主键只读");
                spr.SRid = preValue2;
            }
            
            AskForView2();
        }
        private void CellEditEnding_tbProduct(object sender, DataGridCellEditEndingEventArgs e)
        {

            string newValue = (e.EditingElement as TextBox).Text;

            TB_Product pro = e.Row.DataContext as TB_Product;//输入
            var product = DB.TB_Product.Where(w => w.Pid == pro.Pid).FirstOrDefault();//现有
            if (pro.Pid == preValue3)//输入的新主键等于原来主键
            {
                try
                {
                    product.Pid = pro.Pid;
                    product.Pname = pro.Pname;
                    product.Pleft = pro.Pleft;
                    product.Ptype = pro.Ptype;
                    product.Pprice = pro.Pprice;
                    product.Pdiscount = pro.Pdiscount;
                    DB.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("主键冲突");
                }
            }

            if (product != null &&
              product.Pid == pro.Pid && pro.Pid != preValue3)//输入的新主键等于其他记录主键
            {
                MessageBox.Show("主键冲突" + product.Pid + pro.Pid);
                pro.Pid = preValue3;
            }
            if (product == null)//输入的为全新的主键
            {
                MessageBox.Show("主键只读");
                pro.Pid = preValue3;
            }


            AskForView3();


        }

        private void AskForView1()
        {
            DB1 = new db_MarketEntities();
            DB1.Database.Connection.ConnectionString = App.DBConnectionString;

            String categoryForSearch = searchCategory_tbCustomer.Text;
            String termForSearch = searchTerms_tbCustomer.Text;
            String numsForSearch = searchNums_tbCustomer.Text;

            //进入重置模式
            if (categoryForSearch == "Cid" && numsForSearch == null)
            {
                tmp1.Clear();
               
                var info1 = from c in DB1.TB_Customer select c;
                foreach (var item in info1)
                {
                    tmp1.Add(item);
                }
                dg_Customer.ItemsSource = tmp1;
            }
            else//(numsForSearch!=null&&numsForSearch!="")
            {
                tmp1.Clear();
                var info = from c in DB1.TB_Customer select c;
                foreach (var item in info)
                {
                    switch (categoryForSearch)
                    {
                        case "Cid":
                            if (item.Cid.IndexOf(numsForSearch) != -1)
                            {
                                tmp1.Add(item);
                            }
                            break;
                        case "Cname":
                            if (item.Cname != null && item.Cname.IndexOf(numsForSearch) != -1)
                            {
                                tmp1.Add(item);
                            }
                            break;
                        case "Cdeposit":
                            var types = new TypeRegistry();
                            types.RegisterDefaultTypes();
                            String str = item.Cdeposit + termForSearch + numsForSearch;
                            var expression = new CompiledExpression(str) { TypeRegistry = types };
                            bool result = (bool)expression.Eval();
                            if (result)
                            {
                                tmp1.Add(item);
                            }
                            break;
                        default:
                            break;
                    }
                }
                dg_Customer.ItemsSource = tmp1;
            }

            

        }
        private void AskForView2()
        {
            String categoryForSearch = searchCategory_tbShoppingRecord.Text;
            String termForSearch = searchTerms_tbShoppingRecord.Text;
            String numsForSearch = searchNums_tbShoppingRecord.Text;

            //进入重置模式
            if (categoryForSearch == "SRid" && numsForSearch == null)
            {
                tmp2.Clear();

                var info1 = from c in DB.TB_ShoppingRecord select c;
                foreach (var item in info1)
                {
                    tmp2.Add(item);
                }
                dg_ShoppingRecord.ItemsSource = tmp1;
            }
            else//(numsForSearch!=null&&numsForSearch!="")
            {
                tmp2.Clear();
                var info = from c in DB.TB_ShoppingRecord select c;
                foreach (var item in info)
                {
                    switch (categoryForSearch)
                    {
                        case "SRid":
                            if (item.SRid.IndexOf(numsForSearch) != -1)
                            {
                                tmp2.Add(item);
                            }
                            break;
                        case "Cid":
                            if (item.Cid != null && item.Cid.IndexOf(numsForSearch) != -1)
                            {
                                tmp2.Add(item);
                            }
                            break;
                        case "Pid":
                            if (item.Pid != null && item.Pid.IndexOf(numsForSearch) != -1)
                            {
                                tmp2.Add(item);
                            }
                            break;
                    
                        case "SRnum":
                            var types21 = new TypeRegistry();
                            types21.RegisterDefaultTypes();
                            String str21 = item.SRnum + termForSearch + numsForSearch;
                            var expression21 = new CompiledExpression(str21) { TypeRegistry = types21 };
                            bool result21 = (bool)expression21.Eval();
                            if (result21)
                            {
                                tmp2.Add(item);
                            }
                            break;
                        case "SRturnover":
                            var types22 = new TypeRegistry();
                            types22.RegisterDefaultTypes();
                            String str22 = item.SRturnover + termForSearch + numsForSearch;
                            var expression22 = new CompiledExpression(str22) { TypeRegistry = types22 };
                            bool result22 = (bool)expression22.Eval();
                            if (result22)
                            {
                                tmp2.Add(item);
                            }
                            break;
                        default:
                            break;
                    }
                }
                dg_ShoppingRecord.ItemsSource = tmp2;
            }
        }
        private void AskForView3()
        {
            String categoryForSearch = searchCategory_tbProduct.Text;
            String termForSearch = searchTerms_tbProduct.Text;
            String numsForSearch = searchNums_tbProduct.Text;

            //进入重置模式
            if (categoryForSearch == "Pid" && numsForSearch == null)
            {
                tmp3.Clear();

                var info1 = from c in DB.TB_Product select c;
                foreach (var item in info1)
                {
                    tmp3.Add(item);
                }
                dg_Product.ItemsSource = tmp3;
            }
            else//(numsForSearch!=null&&numsForSearch!="")
            {
                tmp3.Clear();
                var info = from c in DB.TB_Product select c;
                foreach (var item in info)
                {
                    switch (categoryForSearch)
                    {
                        case "Pid":
                            if (item.Pid.IndexOf(numsForSearch) != -1)
                            {
                                tmp3.Add(item);
                            }
                            break;
                        case "Pname":
                            if (item.Pname != null && item.Pname.IndexOf(numsForSearch) != -1)
                            {
                                tmp3.Add(item);
                            }
                            break;
                        case "Ptype":
                            if (item.Ptype != null && item.Ptype.IndexOf(numsForSearch) != -1)
                            {
                                tmp3.Add(item);
                            }
                            break;
                        case "Pleft":
                            var types = new TypeRegistry();
                            types.RegisterDefaultTypes();
                            String str = item.Pleft + termForSearch + numsForSearch;
                            var expression = new CompiledExpression(str) { TypeRegistry = types };
                            bool result = (bool)expression.Eval();
                            if (result)
                            {
                                tmp3.Add(item);
                            }
                            break;
                        case "Pprice":
                            var types2 = new TypeRegistry();
                            types2.RegisterDefaultTypes();
                            String str2 = item.Pprice + termForSearch + numsForSearch;
                            var expression2 = new CompiledExpression(str2) { TypeRegistry = types2 };
                            bool result2 = (bool)expression2.Eval();
                            if (result2)
                            {
                                tmp3.Add(item);
                            }
                            break;
                        case "Pdiscount":
                            var types3 = new TypeRegistry();
                            types3.RegisterDefaultTypes();
                            String str3 = item.Pdiscount + termForSearch + numsForSearch;
                            var expression3 = new CompiledExpression(str3) { TypeRegistry = types3 };
                            bool result3 = (bool)expression3.Eval();
                            if (result3)
                            {
                                tmp3.Add(item);
                            }
                            break;
                        default:
                            break;
                    }
                }
                dg_Product.ItemsSource = tmp3;
            }



        }


        private void BeginningEdit_tbCustomer(object sender, DataGridBeginningEditEventArgs e)
        {
            //将修改前的值保存起来
            preValue1 = (e.Column.GetCellContent(e.Row) as TextBlock).Text;//dw
            TB_Customer cus = e.Row.DataContext as TB_Customer;
            preValue1 = cus.Cid;
        }
        private void BeginningEdit_tbShoppingRecord(object sender, DataGridBeginningEditEventArgs e)
        {

            //将修改前的值保存起来
            preValue2 = (e.Column.GetCellContent(e.Row) as TextBlock).Text;
            TB_ShoppingRecord spr = e.Row.DataContext as TB_ShoppingRecord;
            preValue2 = spr.SRid;


        }
        private void BeginningEdit_tbProduct(object sender, DataGridBeginningEditEventArgs e)
        {

            //将修改前的值保存起来
            preValue3 = (e.Column.GetCellContent(e.Row) as TextBlock).Text;
            TB_Product pro = e.Row.DataContext as TB_Product;
            preValue3 = pro.Pid;


        }

        private void searchNums_PreviewKeyDown_tbCustomer(object sender, KeyEventArgs e)
        {
            // MessageBox.Show("现在按了键");
            String str = searchCategory_tbCustomer.Text;
            if (searchCategory_tbCustomer.Text.Equals("Cdeposit"))
            {
                if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back||
                e.Key==Key.Left||e.Key==Key.Right)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
            }
        }
        private void searchNums_PreviewKeyDown_tbShoppingRecord(object sender, KeyEventArgs e)
        {
            // MessageBox.Show("现在按了键");
            String str = searchCategory_tbShoppingRecord.Text;
            if (searchCategory_tbShoppingRecord.Text.Equals("SRnum")|| searchCategory_tbShoppingRecord.Text.Equals("SRturnover"))
            {
                if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back ||
                e.Key == Key.Left || e.Key == Key.Right)
                {
                    if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
        private void searchNums_PreviewKeyDown_tbProduct(object sender, KeyEventArgs e)
        {
            // MessageBox.Show("现在按了键");
            String str = searchCategory_tbProduct.Text;
            if (searchCategory_tbProduct.Text.Equals("Pleft"))
            {
                if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back ||
                e.Key == Key.Left || e.Key == Key.Right)
                {
                    if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void btReset_Click_tbCustomer(object sender, RoutedEventArgs e)
        {
            searchCategory_tbCustomer.SelectedIndex=0;
            searchTerms_tbCustomer.SelectedIndex=2;
            searchNums_tbCustomer.Text = null;
            AskForView1();
        }
        private void btReset_Click_tbShoppingRecord(object sender, RoutedEventArgs e)
        {
            searchCategory_tbShoppingRecord.SelectedIndex = 0;
            searchTerms_tbShoppingRecord.SelectedIndex = 2;
            searchNums_tbShoppingRecord.Text = null;
            AskForView2();
        }
        private void btReset_Click_tbProduct(object sender, RoutedEventArgs e)
        {
            searchCategory_tbProduct.SelectedIndex = 0;
            searchTerms_tbProduct.SelectedIndex = 2;
            searchNums_tbProduct.Text = null;
            AskForView3();
        }

        private void btDelete_Click_tbCustomer(object sender, RoutedEventArgs e)
        {
            var a = dg_Customer.SelectedItems;
            foreach (var aa in a)
            {
                var b = aa as TB_Customer;
                //TB_Customer customer = new TB_Customer();
                //DB.TB_Customer.Remove(b);

                var t1 = DB.TB_Customer.ToList();

                foreach (var item in t1)
                {
                    if(item.Cid==b.Cid)
                        DB.TB_Customer.Remove(item);
                }

            }
            DB.SaveChanges();

            AskForView1();
           
        }
        private void btDelete_Click_tbShoppingRecord(object sender, RoutedEventArgs e)
        {
            var a = dg_ShoppingRecord.SelectedItems;
            foreach (var aa in a)
            {
                var b = aa as TB_ShoppingRecord;
                //TB_Customer customer = new TB_Customer();
                //DB.TB_Customer.Remove(b);

                var t1 = DB.TB_ShoppingRecord.ToList();

                foreach (var item in t1)
                {
                    if (item.SRid == b.SRid)
                        DB.TB_ShoppingRecord.Remove(item);
                }

            }
            DB.SaveChanges();

            AskForView2();

        }
        private void btDelete_Click_tbProduct(object sender, RoutedEventArgs e)
        {
            var a = dg_Product.SelectedItems;
            foreach (var aa in a)
            {
                var b = aa as TB_Product;
                //TB_Customer customer = new TB_Customer();
                //DB.TB_Customer.Remove(b);

                var t1 = DB.TB_Product.ToList();

                foreach (var item in t1)
                {
                    if (item.Pid == b.Pid)
                        DB.TB_Product.Remove(item);
                }

            }
            DB.SaveChanges();

            AskForView3();

        }

    

    
    }
   
}
