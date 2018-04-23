Imports System
Imports System.Linq
Imports System.ComponentModel
Imports System.Data
Namespace WpfApplication15
    Public Class ViewModel
        Implements INotifyPropertyChanged

        Private DataSet As DataSet
        Private _masterSource As Object
        Private _detailSource As Object
        Private _currentRow As DataRowView

        Public Property MasterSource() As Object
            Get
                Return _masterSource
            End Get
            Private Set(ByVal value As Object)
                If _masterSource Is value Then
                    Return
                End If
                _masterSource = value
                RaisePropertyChanged("MasterSource")
            End Set
        End Property
        Public Property DetailSource() As Object
            Get
                Return _detailSource
            End Get
            Private Set(ByVal value As Object)
                If _detailSource Is value Then
                    Return
                End If
                _detailSource = value
                RaisePropertyChanged("DetailSource")
            End Set
        End Property
        Public Property CurrentRow() As DataRowView
            Get
                Return _currentRow
            End Get
            Set(ByVal value As DataRowView)
                If _currentRow Is value Then
                    Return
                End If
                _currentRow = value
                RaisePropertyChanged("CurrentRow")
                UpdateDetailSource()
            End Set
        End Property

        Public Sub New()
            DataSet = CreateData()
            MasterSource = DataSet.Tables("Company")
        End Sub

        Private Function CreateData() As DataSet
            Dim mdt As New DataTable("Company")
            mdt.Columns.Add(New DataColumn("Name", GetType(String)))
            mdt.Columns.Add(New DataColumn("ID", GetType(Integer)))
            mdt.Rows.Add("Ford", 4)
            mdt.Rows.Add("Nissan", 5)
            mdt.Rows.Add("Mazda", 6)

            Dim ddt As New DataTable("Models")
            ddt.Columns.Add(New DataColumn("Name", GetType(String)))
            ddt.Columns.Add(New DataColumn("MaxSpeed", GetType(Integer)))
            ddt.Columns.Add(New DataColumn("CompanyName", GetType(String)))
            ddt.Rows.Add("FordFocus", 400, "Ford")
            ddt.Rows.Add("FordST", 400, "Ford")
            ddt.Rows.Add("Note", 1000, "Nissan")
            ddt.Rows.Add("Mazda3", 1000, "Mazda")

            Dim ds As New DataSet("CM")
            ds.Tables.Add(mdt)
            ds.Tables.Add(ddt)
            Dim dr As New DataRelation("CompanyModels", mdt.Columns("Name"), ddt.Columns("CompanyName"))
            ds.Relations.Add(dr)

            Return ds
        End Function
        Private Sub UpdateDetailSource()
            Dim dv As New DataView(DataSet.Tables("Models"))
            dv.RowFilter = String.Format("CompanyName = '{0}'", CurrentRow("Name"))
            DetailSource = DataSet.Tables("Models")
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Private Sub RaisePropertyChanged(ByVal propName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
        End Sub
    End Class
End Namespace