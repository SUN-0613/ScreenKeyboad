Imports AYam.Common.ViewModel
Imports System
Imports System.Windows

Namespace Form.Model

    ''' <summary>
    ''' Keyboard.Model
    ''' </summary>
    Public Class Keyboard
        Implements IDisposable

#Region "Property"

        ''' <summary>
        ''' 現在値
        ''' </summary>
        Public NowValue As String

        ''' <summary>
        ''' 入力値
        ''' </summary>
        Public Text As String

        ''' <summary>
        ''' 文字選択開始位置
        ''' 今回未使用
        ''' </summary>
        Public SelectionStart As Integer = 0

        ''' <summary>
        ''' 選択文字数
        ''' 今回未使用
        ''' </summary>
        Public SelectionLength As Integer = 0

        ''' <summary>
        ''' ボタンクリックコマンド
        ''' </summary>
        Public InputCommand As DelegateCommand(Of String)

        ''' <summary>
        ''' Shiftキー押下
        ''' </summary>
        Private _IsShift As Boolean = False

        ''' <summary>
        ''' Shiftキー押下プロパティ
        ''' </summary>
        Public Property IsShift As Boolean
            Get
                Return _IsShift
            End Get
            Set(value As Boolean)

                If Not IsCaps Then

                    _IsShift = value

                    If value Then

                        ShiftOffVisible = Visibility.Hidden
                        ShiftOnVisible = Visibility.Visible

                    Else

                        ShiftOffVisible = Visibility.Visible
                        ShiftOnVisible = Visibility.Hidden

                    End If

                End If

            End Set
        End Property

        ''' <summary>
        ''' Caps Lockキー押下
        ''' </summary>
        Public IsCaps As Boolean = False

        ''' <summary>
        ''' Shiftキー非押下時に表示
        ''' </summary>
        Public ShiftOffVisible As Visibility = Visibility.Visible

        ''' <summary>
        ''' Shiftキー押下時に表示
        ''' </summary>
        Public ShiftOnVisible As Visibility = Visibility.Visible


#End Region

        ''' <summary>
        ''' Keyboard.Model
        ''' </summary>
        ''' <param name="value">現在値</param>
        Public Sub New(value As String)

            NowValue = value
            Initialize(value)

        End Sub

        ''' <summary>
        ''' 終了処理
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose

        End Sub

        ''' <summary>
        ''' 初期化
        ''' </summary>
        ''' <param name="nowValue">現在値</param>
        Private Sub Initialize(Optional nowValue As String = "")

            Text = nowValue
            SelectionStart = Text.Length
            SelectionLength = 0
            IsShift = False

        End Sub

        ''' <summary>
        ''' 文字入力
        ''' </summary>
        ''' <param name="content">Button.Content</param>
        ''' <returns>
        ''' -1 : 通常処理
        '''  1 : 次フォーカス移動
        '''  2 : Shift処理
        '''  3 : Caps処理
        ''' </returns>
        Public Function InputText(content As String) As Integer

            Dim returnValue As Integer = -1

            Select Case content.ToUpper()

                Case "CLEAR"

                    Initialize()

                Case "BS"

                    ' 先頭フォーカスは削除する文字がないため除外
                    If SelectionStart > 0 Then

                        SelectionStart -= 1
                        SelectionLength = 1
                        ControlText()

                    End If

                    IsShift = False

                Case "±"

                    ' ブランクでないか
                    If Text.Length > 0 Then

                        ' 先頭１文字が"-"か
                        If (Text.Substring(0, 1).Equals("-")) Then

                            Text = Text.Substring(1)

                            If (SelectionStart > 0) Then

                                SelectionStart -= 1

                            End If

                        Else

                            Text = "-" & Text
                            SelectionStart += 1

                        End If

                        SelectionLength = 0

                    Else

                        Text = "-"
                        SelectionStart = 1
                        SelectionLength = 0

                    End If

                    IsShift = False

                Case "NUMBERDOT"

                    If Text.Length.Equals(0) _
                        OrElse (Text.Length.Equals(1) AndAlso Text.Equals("-")) Then

                        Text = Text & "0."
                        SelectionStart = 2
                        SelectionLength = 0

                    Else

                        ControlText(".")

                    End If

                    IsShift = False

                Case "00"

                    If Text.Length.Equals(0) _
                        OrElse (Text.Length.Equals(1) AndAlso (Text.Equals("-") OrElse Text.Equals("0"))) _
                        OrElse (Text.Length.Equals(2) AndAlso Text.Equals("-0")) Then

                        Text = If(Text.Contains("-"), "-0", "0")
                        SelectionStart = Text.Length
                        SelectionLength = 0

                    Else

                        ControlText(content)

                    End If

                    IsShift = False

                Case "NEXT"

                    returnValue = 1

                Case "SHIFT"

                    IsShift = Not IsShift
                    returnValue = 2

                Case "CAPS"

                    If IsShift Then

                        IsCaps = Not IsCaps

                    End If

                    IsShift = False

                    returnValue = 3

                Case Else

                    ControlText(content)
                    IsShift = False

            End Select

            Return returnValue

        End Function

        ''' <summary>
        ''' Textbox.Textから選択文字列を削除し、指定文字列を挿入して返す
        ''' </summary>
        ''' <param name="insertString">挿入文字列</param>
        Private Sub ControlText(Optional insertString As String = "")

            ' 削除文字よりも前を取得
            Dim firstString As String = If(SelectionStart.Equals(0), "", Text.Substring(0, SelectionStart))

            ' 削除文字よりも後を取得
            Dim secondString As String = If((SelectionStart + SelectionLength).Equals(Text.Length), "", Text.Substring(SelectionStart + SelectionLength))

            ' 文字結合
            Text = firstString & insertString & secondString

            ' 選択位置調整
            SelectionStart = firstString.Length + insertString.Length
            SelectionLength = 0

        End Sub

    End Class

End Namespace
