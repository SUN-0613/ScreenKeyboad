Imports AYam.Common.ViewModel
Imports System

Namespace Form.ViewModel

    ''' <summary>
    ''' InputDialog.ViewModel
    ''' </summary>
    Public Class InputDialog
        Inherits VMBase
        Implements IDisposable

#Region "Property"

        ''' <summary>
        ''' テンキー１プロパティ
        ''' </summary>
        Public Property NumText1 As String
            Get
                Return _Model.NumText1
            End Get
            Set(value As String)

                If Not _Model.NumText1.Equals(value) Then

                    _Model.NumText1 = value
                    CallPropertyChanged()

                End If

            End Set
        End Property

        ''' <summary>
        ''' テンキー２プロパティ
        ''' </summary>
        Public Property NumText2 As String
            Get
                Return _Model.NumText2
            End Get
            Set(value As String)

                If Not _Model.NumText2.Equals(value) Then

                    _Model.NumText2 = value
                    CallPropertyChanged()

                End If

            End Set
        End Property

        ''' <summary>
        ''' 文字入力１プロパティ
        ''' </summary>
        Public Property StringText1 As String
            Get
                Return _Model.StringText1
            End Get
            Set(value As String)

                If Not _Model.StringText1.Equals(value) Then

                    _Model.StringText1 = value
                    CallPropertyChanged()

                End If

            End Set
        End Property

        ''' <summary>
        ''' 文字入力２プロパティ
        ''' </summary>
        Public Property StringText2 As String
            Get
                Return _Model.StringText2
            End Get
            Set(value As String)

                If Not _Model.StringText2.Equals(value) Then

                    _Model.StringText2 = value
                    CallPropertyChanged()

                End If

            End Set
        End Property

#End Region

        ''' <summary>
        ''' Model
        ''' </summary>
        Private _Model As Model.InputDialog

        ''' <summary>
        ''' InputDialog.ViewModel
        ''' </summary>
        Public Sub New()

            _Model = New Model.InputDialog()

        End Sub

        ''' <summary>
        ''' 終了処理
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            _Model.Dispose()
            _Model = Nothing
        End Sub

    End Class

End Namespace

