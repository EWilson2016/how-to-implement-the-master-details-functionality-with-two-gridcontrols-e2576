using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace WpfApplication15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public DataSet CreateData()
        {
            DataTable mdt = new DataTable("Company");
            mdt.Columns.Add(new DataColumn("Name", typeof(string)));
            mdt.Columns.Add(new DataColumn("ID", typeof(int)));
            mdt.Rows.Add("Ford", 4);
            mdt.Rows.Add("Nissan", 5);
            mdt.Rows.Add("Mazda", 6);

            DataTable ddt = new DataTable("Models");
            ddt.Columns.Add(new DataColumn("Name", typeof(string)));
            ddt.Columns.Add(new DataColumn("MaxSpeed", typeof(int)));
            ddt.Columns.Add(new DataColumn("CompanyName", typeof(string)));
            ddt.Rows.Add("FordFocus", 400, "Ford");
            ddt.Rows.Add("FordST", 400, "Ford");
            ddt.Rows.Add("Note", 1000, "Nissan");
            ddt.Rows.Add("Mazda3", 1000, "Mazda");

            ds = new DataSet("CM");
            ds.Tables.Add(mdt);
            ds.Tables.Add(ddt);
            DataRelation dr = new DataRelation("CompanyModels", mdt.Columns["Name"], ddt.Columns["CompanyName"]);
            ds.Relations.Add(dr);

            return ds;
         }

        DataSet ds;
        bool IsLoaded = false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ds = this.CreateData();
            gridControl1.ItemsSource = ds.Tables["Company"];
            IsLoaded = true;
        }

        private void TableView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            if (IsLoaded)
            {
                DataRowView drv = gridControl1.GetRow(gridControl1.View.FocusedRowHandle) as DataRowView;
                DataView dv = new DataView(ds.Tables["Models"]);
                dv.RowFilter = String.Format("CompanyName = '{0}'", drv["Name"].ToString());
                gridControl2.ItemsSource = dv;
            }
        }
    }
}
