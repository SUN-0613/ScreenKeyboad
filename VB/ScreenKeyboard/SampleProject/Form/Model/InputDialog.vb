Imports System

Namespace Form.Model

    ''' <summary>
    ''' InputDialog.Model
    ''' </summary>
    Public Class InputDialog
        Implements IDisposable

#Region "ViewModel.Property"

        ''' <summary>
        ''' テンキー１
        ''' </summary>
        Public NumText1 As String = ""

        ''' <summary>
        ''' テンキー２
        ''' </summary>
        Public NumText2 As String = ""

        ''' <summary>
        ''' 文字入力１
        ''' </summary>
        Public StringText1 As String = ""

        ''' <summary>
        ''' 文字入力２
        ''' </summary>
        Public StringText2 As String = ""

#End Region

        ''' <summary>
        ''' InputDialog.Model
        ''' </summary>
        Public Sub New()

        End Sub

        ''' <summary>
        ''' 終了処理
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose

        End Sub

    End Class

End Namespace

