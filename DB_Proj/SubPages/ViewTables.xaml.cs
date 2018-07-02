using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace DB_Proj
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class ViewTables : UserControl
    {
        private db_MarketEntities DB;

        //ObservableCollection<TB_Customer> tmp1,
        //    ObservableCollection<TB_ShoppingRecord> tmp2,
        //    ObservableCollection<TB_Product> tmp3

        public ViewTables()
        {                                                         
            InitializeComponent();
            asdasda();
            DataContext = this;
            //adding values or series will update and animate the chart automatically
            //SeriesCollection.Add(new PieSeries());
            //SeriesCollection[0].Values.Add(5);


        }
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public SeriesCollection SeriesCollection3 { get; set; }

        private void btRefresh_charts_Click(object sender, RoutedEventArgs e)
        {
        
            asdasda();
            DataContext = this;
        }
        private void asdasda()
        {
            DB = new db_MarketEntities();
            DB.Database.Connection.ConnectionString = App.DBConnectionString;

            var info1 = from c in DB.TB_Customer select c;
            int lt1k = 0, lt1w = 0, lt5w = 0, lt1h = 0, gt5w = 0;
            foreach (var item in info1)
            {
                if (item.Cdeposit > 0 && item.Cdeposit < 100)
                    lt1h++;
                if (item.Cdeposit > 100 && item.Cdeposit < 1000)
                    lt1k++;
                if (item.Cdeposit > 1000 && item.Cdeposit < 10000)
                    lt1w++;
                if (item.Cdeposit > 10000 && item.Cdeposit < 50000)
                    lt5w++;
                else
                    gt5w++;
            }

            //剩余商品
            var info2 = from c in DB.TB_Product select c;
            int fushi = 0, yanjiu = 0, yinliao = 0, shenghuo = 0, liangyou = 0;
            foreach (var item in info2)
            {
                if (item.Ptype == "副食")
                    fushi += item.Pleft;
                if (item.Ptype == "烟酒")
                    yanjiu += item.Pleft;
                if (item.Ptype == "饮料")
                    yinliao += item.Pleft;
                if (item.Ptype == "生活用品")
                    shenghuo += item.Pleft;
                if (item.Ptype == "粮油")
                    liangyou += item.Pleft;
            }

            //交易额
            var info3 = from c in DB.TB_ShoppingRecord select c;
            double fushi1 = 0, yanjiu1 = 0, yinliao1 = 0, shenghuo1 = 0, liangyou1 = 0;
            foreach (var item in info3)
            {
                foreach (var item1 in info2)
                {
                    if (item.Pid == item1.Pid)
                    {
                        if (item1.Ptype == "副食")
                            fushi1 += (double)item.SRturnover;
                        if (item1.Ptype == "烟酒")
                            yanjiu1 += (double)item.SRturnover;
                        if (item1.Ptype == "饮料")
                            yinliao1 += (double)item.SRturnover;
                        if (item1.Ptype == "生活用品")
                            shenghuo1 += (double)item.SRturnover;
                        if (item1.Ptype == "粮油")
                            liangyou1 += (double)item.SRturnover;
                    }
                }
            }
            
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "小于100",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(lt1h) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "小于1000",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(lt1k) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "小于10000",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(lt1w) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "小于50000",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(lt5w) },
                    DataLabels = true
                },
                 new PieSeries
                {
                    Title = "大于50000",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(gt5w) },
                    DataLabels = true
                }
            };
            SeriesCollection1 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "副食",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(fushi) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "烟酒",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(yanjiu) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "饮料",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(yinliao) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "生活用品",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(shenghuo) },
                    DataLabels = true
                },
                  new PieSeries
                {
                    Title = "粮油",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(liangyou) },
                    DataLabels = true
                }
            };
            SeriesCollection2 = new SeriesCollection
            {
                 new PieSeries
                {
                    Title = "副食",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(fushi1) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "烟酒",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(yanjiu1) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "饮料",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(yinliao1) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "生活用品",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(shenghuo1) },
                    DataLabels = true
                },
                  new PieSeries
                {
                    Title = "粮油",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(liangyou1) },
                    DataLabels = true
                }
            };
            SeriesCollection3 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Chrome",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Mozilla",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Opera",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Explorer",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                }
            };
            DataContext = this;
        }
    }
}
