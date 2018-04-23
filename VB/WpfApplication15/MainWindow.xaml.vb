Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Data

Namespace WpfApplication15
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
		End Sub

		Public Function CreateData() As DataSet
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

			ds = New DataSet("CM")
			ds.Tables.Add(mdt)
			ds.Tables.Add(ddt)
			Dim dr As New DataRelation("CompanyModels", mdt.Columns("Name"), ddt.Columns("CompanyName"))
			ds.Relations.Add(dr)

			Return ds
		End Function

		Private ds As DataSet
		Private IsLoaded As Boolean = False
		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			ds = Me.CreateData()
			gridControl1.ItemsSource = ds.Tables("Company")
			IsLoaded = True
		End Sub

		Private Sub TableView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.FocusedRowChangedEventArgs)
			If IsLoaded Then
				Dim drv As DataRowView = TryCast(gridControl1.GetRow(gridControl1.View.FocusedRowHandle), DataRowView)
				Dim dv As New DataView(ds.Tables("Models"))
				dv.RowFilter = String.Format("CompanyName = '{0}'", drv("Name").ToString())
				gridControl2.ItemsSource = dv
			End If
		End Sub
	End Class
End Namespace
