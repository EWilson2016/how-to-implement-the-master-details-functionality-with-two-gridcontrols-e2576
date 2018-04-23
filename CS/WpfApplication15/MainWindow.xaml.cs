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
using System.ComponentModel;

namespace WpfApplication15 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            DataContext = new VM();
            InitializeComponent();
        }
    }
    public class VM : INotifyPropertyChanged {
        DataSet DS;
        DataSet CreateData() {
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

            DataSet ds = new DataSet("CM");
            ds.Tables.Add(mdt);
            ds.Tables.Add(ddt);
            DataRelation dr = new DataRelation("CompanyModels", mdt.Columns["Name"], ddt.Columns["CompanyName"]);
            ds.Relations.Add(dr);

            return ds;
        }
        
        public VM() {
            DS = CreateData();
            Source1 = new DataView(DS.Tables["Company"]);
        }

        DataView source1;
        public DataView Source1 {
            get { return source1; }
            private set {
                if(source1 == value) return;
                source1 = value;
                RaisePropertyChanged("Source1");
            }
        }
        DataView source2;
        public DataView Source2 {
            get { return source2; }
            private set {
                if(source2 == value) return;
                source2 = value;
                RaisePropertyChanged("Source2");
            }
        }

        DataRowView currectRow1;
        public DataRowView CurrectRow1 {
            get { return currectRow1; }
            set {
                if(currectRow1 == value) return;
                currectRow1 = value;
                RaisePropertyChanged("CurrentRow1");
                UpdateSource2();
            }
        }

        void UpdateSource2() {
            DataView dv = new DataView(DS.Tables["Models"]);
            dv.RowFilter = String.Format("CompanyName = '{0}'", CurrectRow1["Name"].ToString());
            Source2 = dv;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propName) {
            if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
