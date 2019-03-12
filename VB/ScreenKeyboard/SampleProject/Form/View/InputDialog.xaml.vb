Imports System
Imports System.Windows

Namespace Form.View

    ''' <summary>
    ''' InputDialog.View
    ''' </summary>
    Public Class InputDialog
        Implements IDisposable

        ''' <summary>
        ''' ViewModel
        ''' </summary>
        Private _ViewModel As ViewModel.InputDialog

        ''' <summary>
        ''' InputDialog.View
        ''' </summary>
        Public Sub New()

            InitializeComponent()

            _ViewModel = New ViewModel.InputDialog()
            DataContext = _ViewModel

        End Sub

        ''' <summary>
        ''' 終了処理
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose

            _ViewModel.Dispose()

        End Sub

    End Class

End Namespace

