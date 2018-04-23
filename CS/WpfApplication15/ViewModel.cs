using System;
using System.Linq;
using System.ComponentModel;
using System.Data;
namespace WpfApplication15 {
    public class ViewModel : INotifyPropertyChanged {
        DataSet DataSet;
        object _masterSource;
        object _detailSource;
        DataRowView _currentRow;

        public object MasterSource {
            get { return _masterSource; }
            private set {
                if (_masterSource == value) return;
                _masterSource = value;
                RaisePropertyChanged("MasterSource");
            }
        }
        public object DetailSource {
            get { return _detailSource; }
            private set {
                if (_detailSource == value) return;
                _detailSource = value;
                RaisePropertyChanged("DetailSource");
            }
        }
        public DataRowView CurrentRow {
            get { return _currentRow; }
            set {
                if (_currentRow == value) return;
                _currentRow = value;
                RaisePropertyChanged("CurrentRow");
                UpdateDetailSource();
            }
        }

        public ViewModel() {
            DataSet = CreateData();
            MasterSource = DataSet.Tables["Company"];
        }

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
        void UpdateDetailSource() {
            DataView dv = new DataView(DataSet.Tables["Models"]);
            dv.RowFilter = String.Format("CompanyName = '{0}'", CurrentRow["Name"]);
            DetailSource = DataSet.Tables["Models"];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propName) {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}