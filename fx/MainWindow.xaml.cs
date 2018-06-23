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
using System.Data.SQLite;

namespace fx
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            conn.Init();


            
        }
        private void InitLayout()
        {
            

            GMain.RowDefinitions.Add(new RowDefinition()); //退出按钮
            //GMain.RowDefinitions[0].Height = new GridLength(20, GridUnitType.Pixel); ;
            GMain.RowDefinitions[0].Height = new GridLength(20, GridUnitType.Pixel); 
            Button exitBtn = new Button
            {
                Name = "exitBtn",
                Content = "×",
                Width = 30,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin=new Thickness(0,0,0,0)
            };
            exitBtn.Click += new RoutedEventHandler(exitButton_Click);  
            exitBtn.SetValue(Grid.RowProperty,0);
            exitBtn.SetValue(Grid.ColumnProperty, 0);
            GMain.Children.Add(exitBtn);

            GMain.RowDefinitions.Add(new RowDefinition()); //disipDB行     
            GMain.RowDefinitions[1].Height = new GridLength();
            DataGrid dispDG = new DataGrid
            {
                Name = "dispDG",
                Height = 200,
                Width = 320,
                //ItemsSource = " { Binding}",
                FontSize = 12,
                HorizontalAlignment =HorizontalAlignment.Left,
                CanUserAddRows =false,
                //AutoGenerateColumns=false,
                IsReadOnly=true,
                Margin = new Thickness(0, 0, 0, 0),

            };
            dispDG.LoadingRow += new EventHandler<DataGridRowEventArgs>(dispDGLoadingRow);
            dispDG.SetValue(Grid.RowProperty, 1);
            dispDG.SetValue(Grid.ColumnProperty, 0);
            GMain.Children.Add(dispDG);

            GMain.RowDefinitions.Add(new RowDefinition()); //功能按钮行
            GMain.RowDefinitions[2].Height = new GridLength();

            Grid GMainBtn = new Grid
            {
                Name = "GMainBtn",
            };

            GMainBtn.RowDefinitions.Add(new RowDefinition()); //功能按钮行
            GMainBtn.ColumnDefinitions.Add(new ColumnDefinition());
            GMainBtn.ColumnDefinitions.Add(new ColumnDefinition());
            GMainBtn.ColumnDefinitions.Add(new ColumnDefinition());
            GMainBtn.ColumnDefinitions.Add(new ColumnDefinition());
            GMainBtn.ColumnDefinitions[0].Width =new GridLength();
            GMainBtn.ColumnDefinitions[1].Width = new GridLength();
            GMainBtn.ColumnDefinitions[2].Width = new GridLength();
            GMainBtn.ColumnDefinitions[3].Width = new GridLength();
            TextBox qhTB = new TextBox
            {
                Name = "qhTB",
                Height = 23,
                Width = 100,
                MaxLength = 3,

            };
            PreviewKeyDown += new KeyEventHandler(checkNumber_PreviewKeyDown);
            qhTB.SetValue(Grid.RowProperty,0);
            qhTB.SetValue(Grid.ColumnProperty, 0);
            GMainBtn.Children.Add(qhTB);

            TextBox jhTB = new TextBox
            {
                Name = "jhTB",
                Height = 23,
                Width = 100,
                MaxLength = 3,
            };
            PreviewKeyDown += new KeyEventHandler(checkNumber1to6_PreviewKeyDown);
            jhTB.SetValue(Grid.RowProperty, 0);
            jhTB.SetValue(Grid.ColumnProperty, 1);
            GMainBtn.Children.Add(jhTB);

            Button addBtn = new Button
            {
                Name = "addBtn",
                Content = "添加",
                Width = 30,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 0, 0)
            };
            addBtn.Click += new RoutedEventHandler(addBtn_Click);
            addBtn.SetValue(Grid.RowProperty, 0);
            addBtn.SetValue(Grid.ColumnProperty, 2);
            GMainBtn.Children.Add(addBtn);

            Button showBtn = new Button
            {
                Name = "showBtn",
                Content = "显示",
                Width = 30,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 0, 0)
            };
            showBtn.Click += new RoutedEventHandler(dispBtn_Click);
            showBtn.SetValue(Grid.RowProperty, 0);
            showBtn.SetValue(Grid.ColumnProperty, 3);
            GMainBtn.Children.Add(showBtn);


            GMainBtn.SetValue(Grid.RowProperty, 2);
            GMainBtn.SetValue(Grid.ColumnProperty, 0);
     
            GMain.Children.Add(GMainBtn);
        }

        private void Init()
        {
            InitLayout();

        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var c in GMain.Children)
            {
                if (c is TextBox)
                {
                    string qh="";
                    string jh="";
                    TextBox tb = (TextBox)c;
                    if (tb.Name == "qhTB")
                    {
                        qh = tb.Text.Replace(" ", "");
                    }
                    if (tb.Name == "jhTB")
                    {
                        jh = tb.Text.Replace(" ", "");
                    }
                    if (qh != "" && jh != "")
                    {
                        Core.SqlAction.AddH(initDic(qh, jh));
                    }
                }

            }


            ymdLB.Content = DateTime.Now.ToString("yyMMdd");

            
        }
        private Dictionary<string, object> initDic(string qh, string jh)
        {
            
            var dic = new Dictionary<string, object>();
            dic["qh"] = qh;
            dic["jh"] = jh;
            return dic;
        }
        private void checkNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        private void checkNumber1to6_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber1to6(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        private void dispDGLoadingRow(object sender, DataGridRowEventArgs e)
        {
            //加载行
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        private void qhTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dispBtn_Click(object sender, RoutedEventArgs e)
        {   
            foreach (var c in GMain.Children)
            {
                if (c is DataGrid)
                {
                    DataGrid dg = (DataGrid)c;
                    if (dg.Name == "dispDG")
                    {
                        dg.ItemsSource = Core.SqlAction.SelectH("").DefaultView;
                        dg.GridLinesVisibility = DataGridGridLinesVisibility.All;
                    }
                }
            }   
        }

        private void InitBtn_Click(object sender, RoutedEventArgs e)
        {
            Init();

        }
    }
}
