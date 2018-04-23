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
Imports System.ComponentModel

Namespace WpfApplication15
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			DataContext = New VM()
			InitializeComponent()
		End Sub
	End Class
	Public Class VM
		Implements INotifyPropertyChanged
		Private DS As DataSet
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

		Public Sub New()
			DS = CreateData()
			Source1 = New DataView(DS.Tables("Company"))
		End Sub

		Private source1_Renamed As DataView
		Public Property Source1() As DataView
			Get
				Return source1_Renamed
			End Get
			Private Set(ByVal value As DataView)
				If source1_Renamed Is value Then
					Return
				End If
				source1_Renamed = value
				RaisePropertyChanged("Source1")
			End Set
		End Property
		Private source2_Renamed As DataView
		Public Property Source2() As DataView
			Get
				Return source2_Renamed
			End Get
			Private Set(ByVal value As DataView)
				If source2_Renamed Is value Then
					Return
				End If
				source2_Renamed = value
				RaisePropertyChanged("Source2")
			End Set
		End Property

		Private currectRow1_Renamed As DataRowView
		Public Property CurrectRow1() As DataRowView
			Get
				Return currectRow1_Renamed
			End Get
			Set(ByVal value As DataRowView)
				If currectRow1_Renamed Is value Then
					Return
				End If
				currectRow1_Renamed = value
				RaisePropertyChanged("CurrentRow1")
				UpdateSource2()
			End Set
		End Property

		Private Sub UpdateSource2()
			Dim dv As New DataView(DS.Tables("Models"))
			dv.RowFilter = String.Format("CompanyName = '{0}'", CurrectRow1("Name").ToString())
			Source2 = dv
		End Sub

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Private Sub RaisePropertyChanged(ByVal propName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
		End Sub
	End Class
End Namespace
