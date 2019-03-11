Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input

Namespace Form.View

    ''' <summary>
    ''' StringKeyboard.Codebehind
    ''' </summary>
    Public Class StringKeyboard
        Implements IDisposable

        ''' <summary>
        ''' Nextキーを押下したか
        ''' </summary>
        Private _IsPushNextKey As Boolean = False

        ''' <summary>
        ''' Nextキーを押下したか
        ''' </summary>
        Public Property IsPushNextKey As Boolean
            Get
                Return _IsPushNextKey
            End Get
            Private Set(value As Boolean)
                _IsPushNextKey = value
            End Set
        End Property

        ''' <summary>
        ''' 戻り値
        ''' </summary>
        Private _ReturnValue As String = ""

        ''' <summary>
        ''' 戻り値
        ''' </summary>
        Public Property ReturnValue As String
            Get
                Return _ReturnValue
            End Get
            Private Set(value As String)
                _ReturnValue = value
            End Set
        End Property

        ''' <summary>
        ''' ViewModel
        ''' </summary>
        Private _ViewModel As ViewModel.Keyboard

        ''' <summary>
        ''' テンキー型スクリーンキーボード.View
        ''' </summary>
        ''' <param name="value">初期値</param>
        ''' <param name="left">呼出元Control.PointToScreen.X</param>
        ''' <param name="top">呼出元Control.PointToScreen.Y</param>
        ''' <param name="height">呼出元Control.Height</param>
        ''' <param name="width">呼出元Control.Width</param>
        Public Sub New(Optional value As String = "", Optional left As Double = -1.0, Optional top As Double = -1.0, Optional height As Double = -1.0, Optional width As Double = -1.0)

            InitializeComponent()

            _ViewModel = New ViewModel.Keyboard(value)
            DataContext = _ViewModel

            AddHandler _ViewModel.PropertyChanged, AddressOf OnViewModelPropertyChanged

            ' 表示位置
            If Not left.Equals(-1.0) _
                AndAlso Not top.Equals(-1.0) _
                AndAlso Not height.Equals(-1.0) _
                AndAlso Not width.Equals(-1.0) Then

                ' 横方向
                If left + width + Me.Width > SystemParameters.WorkArea.Width Then
                    Me.Left = left - Me.Width
                Else
                    Me.Left = left + width
                End If

                ' 縦方向
                If top + height + Me.Height > SystemParameters.WorkArea.Height Then
                    Me.Top = top - Me.Height
                Else
                    Me.Top = top + height
                End If

            End If

        End Sub

        ''' <summary>
        ''' 終了処理
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose

            RemoveHandler _ViewModel.PropertyChanged, AddressOf OnViewModelPropertyChanged

            _ViewModel.Dispose()
            _ViewModel = Nothing

        End Sub

        ''' <summary>
        ''' ViewModelプロパティ変更イベント
        ''' </summary>
        Private Sub OnViewModelPropertyChanged(sender As Object, e As PropertyChangedEventArgs)

            Select Case e.PropertyName

                Case "CallNextFocus"    ' 次項目へフォーカス移動

                    IsPushNextKey = True
                    ReturnValue = _ViewModel.Text

                    Dim element As FrameworkElement = TryCast(FocusManager.GetFocusedElement(Me.Owner), FrameworkElement)

                    If Not IsNothing(element) Then
                        element.MoveFocus(New TraversalRequest(FocusNavigationDirection.Next))
                    End If

            End Select

        End Sub

    End Class

End Namespace
