Imports AYam.Common.ViewModel
Imports System
Imports System.Windows

Namespace Form.ViewModel

    ''' <summary>
    ''' Keyboard.ViewModel
    ''' </summary>
    Public Class Keyboard
        Inherits VMBase
        Implements IDisposable

#Region "Property"

        ''' <summary>
        ''' 現在値プロパティ
        ''' </summary>
        Public Property NowValue As String
            Get
                Return _Model.NowValue
            End Get
            Set(value As String)

                Dim newValue As Double

                If Double.TryParse(value, newValue) Then

                    If Not _Model.NowValue.Equals(value) Then

                        _Model.NowValue = value

                    End If

                End If

                CallPropertyChanged()

            End Set
        End Property

        ''' <summary>
        ''' 入力値プロパティ
        ''' </summary>
        Public Property Text As String
            Get
                Return _Model.Text
            End Get
            Set(value As String)

                Dim newValue As Double

                If Double.TryParse(value, newValue) Then

                    If Not _Model.Text.Equals(value) Then

                        _Model.Text = value

                    End If

                End If

                CallPropertyChanged()

            End Set
        End Property

        ''' <summary>
        ''' 文字選択開始位置プロパティ
        ''' 今回未使用
        ''' </summary>
        Public Property SelectionStart As Integer
            Get
                Return _Model.SelectionStart
            End Get
            Set(value As Integer)

                If Not _Model.SelectionStart.Equals(value) Then

                    _Model.SelectionStart = value
                    CallPropertyChanged()

                End If

            End Set
        End Property

        ''' <summary>
        ''' 選択文字数プロパティ
        ''' 今回未使用
        ''' </summary>
        Public Property SelectionLength As Integer
            Get
                Return _Model.SelectionLength
            End Get
            Set(value As Integer)

                If Not _Model.SelectionLength.Equals(value) Then

                    _Model.SelectionLength = value
                    CallPropertyChanged()

                End If

            End Set
        End Property

        ''' <summary>
        ''' ボタンクリックコマンドプロパティ
        ''' </summary>
        Public ReadOnly Property InputCommand As DelegateCommand(Of String)
            Get

                If IsNothing(_Model.InputCommand) Then


                    _Model.InputCommand = New DelegateCommand(Of String)(New Action(Of String)(Sub(parameter) InputFunc(parameter)) _
                                                                         , New Func(Of Boolean)(Function() True))

                End If

                Return _Model.InputCommand

            End Get
        End Property

        ''' <summary>
        ''' ボタンクリック処理
        ''' </summary>
        ''' <param name="parameter">コマンドパラメータ</param>
        Private Sub InputFunc(parameter As String)

            Select Case _Model.InputText(parameter)

                Case 1      ' フォーカス移動
                    CallPropertyChanged("CallNextFocus")

                Case 2 To 3 ' Shift,Caps処理
                    CallPropertyChanged(NameOf(ShiftOffVisible))
                    CallPropertyChanged(NameOf(ShiftOnVisible))

                Case Else   ' 文字入力
                    CallPropertyChanged(NameOf(Text))
                    CallPropertyChanged(NameOf(SelectionStart))
                    CallPropertyChanged(NameOf(SelectionLength))
                    CallPropertyChanged(NameOf(ShiftOffVisible))
                    CallPropertyChanged(NameOf(ShiftOnVisible))

            End Select

        End Sub

        ''' <summary>
        ''' Shiftキー非押下時に表示プロパティ
        ''' </summary>
        Public Property ShiftOffVisible As Visibility
            Get
                Return _Model.ShiftOffVisible
            End Get
            Set(value As Visibility)
                _Model.ShiftOffVisible = value
                CallPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Shiftキー押下時に表示プロパティ
        ''' </summary>
        Public Property ShiftOnVisible As Visibility
            Get
                Return _Model.ShiftOnVisible
            End Get
            Set(value As Visibility)
                _Model.ShiftOnVisible = value
                CallPropertyChanged()
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Model
        ''' </summary>
        Private _Model As Model.Keyboard

        ''' <summary>
        ''' テンキー型スクリーンキーボード.ViewModel
        ''' </summary>
        ''' <param name="value">初期値</param>
        Public Sub New(value As String)
            _Model = New Model.Keyboard(value)
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
