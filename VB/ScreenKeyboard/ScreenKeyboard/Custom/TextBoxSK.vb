Imports AYam.ScreenKeyboard.Form.View
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace Custom

    ''' <summary>
    ''' スクリーンキーボード表示対応テキスト欄
    ''' </summary>
    Public Class TextBoxSK
        Inherits TextBox
        Implements IDisposable

#Region "DependencyProperty"

        ''' <summary>
        ''' テンキー使用可否依存プロパティ
        ''' </summary>
        Public Shared ReadOnly IsUseNumericKeyboardProperty As DependencyProperty =
            DependencyProperty.Register(
            NameOf(IsUseNumericKeyboard) _
            , GetType(Boolean) _
            , GetType(TextBoxSK) _
            , New FrameworkPropertyMetadata(False)
            )

        ''' <summary>
        ''' アルファベットキーボード使用可否依存プロパティ
        ''' </summary>
        Public Shared ReadOnly IsUseStringKeyboardProperty As DependencyProperty =
            DependencyProperty.Register(
            NameOf(IsUseStringKeyboard) _
            , GetType(Boolean) _
            , GetType(TextBoxSK) _
            , New FrameworkPropertyMetadata(False)
            )

#End Region

#Region "Property"

        ''' <summary>
        ''' テンキー使用可否プロパティ
        ''' </summary>
        Public Property IsUseNumericKeyboard As Boolean

        ''' <summary>
        ''' アルファベットキーボード使用可否プロパティ
        ''' </summary>
        Public Property IsUseStringKeyboard As Boolean

#End Region

#Region "Dialog"

        ''' <summary>
        ''' テンキーダイアログ
        ''' </summary>
        Private _NumericDialog As NumericKeyboard = Nothing

        ''' <summary>
        ''' アルファベットキーボードダイアログ
        ''' </summary>
        Private _StringDialog As StringKeyboard = Nothing

#End Region

#Region "Event"

        ''' <summary>
        ''' フォーカス取得イベント
        ''' </summary>
        Private Sub OnEnter(sender As Object, e As EventArgs)

            Dim point As Point = PointToScreen(New Point(0.0, 0.0))

            If IsUseNumericKeyboard Then

                ' テンキー表示
                _NumericDialog = New NumericKeyboard(DirectCast(Me.GetValue(TextProperty), String), point.X, point.Y, Me.ActualHeight, Me.ActualWidth) With
                {
                    .Owner = Window.GetWindow(Me)
                }
                _NumericDialog.Show()

            ElseIf IsUseStringKeyboard Then

                ' アルファベットキーボード表示
                _StringDialog = New StringKeyboard(DirectCast(Me.GetValue(TextProperty), String), point.X, point.Y, Me.ActualHeight, Me.ActualWidth) With
                {
                    .Owner = Window.GetWindow(Me)
                }
                _StringDialog.Show()


            End If

            Me.Background = Brushes.Yellow

        End Sub

        ''' <summary>
        ''' フォーカス喪失イベント
        ''' </summary>
        Private Sub OnLeave(sender As Object, e As EventArgs)

            If Not IsNothing(_NumericDialog) Then

                If _NumericDialog.IsPushNextKey Then
                    Me.SetValue(TextProperty, _NumericDialog.ReturnValue)
                End If

                _NumericDialog.Close()
                _NumericDialog.Dispose()
                _NumericDialog = Nothing

            End If

            If Not IsNothing(_StringDialog) Then

                If _StringDialog.IsPushNextKey Then
                    Me.SetValue(TextProperty, _StringDialog.ReturnValue)
                End If

                _StringDialog.Close()
                _StringDialog.Dispose()
                _StringDialog = Nothing

            End If

            Me.Background = Brushes.White

        End Sub

#End Region

        ''' <summary>
        ''' スクリーンキーボード表示対応テキスト欄
        ''' </summary>
        Public Sub New()

            MyBase.New()

            AddHandler Me.GotFocus, AddressOf OnEnter
            AddHandler Me.LostFocus, AddressOf OnLeave

        End Sub

        ''' <summary>
        ''' 終了処理
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose

            If Not IsNothing(_NumericDialog) Then

                _NumericDialog.Dispose()
                _NumericDialog = Nothing

            End If

            If Not IsNothing(_StringDialog) Then

                _StringDialog.Dispose()
                _StringDialog = Nothing

            End If

            RemoveHandler Me.GotFocus, AddressOf OnEnter
            RemoveHandler Me.LostFocus, AddressOf OnLeave

        End Sub

    End Class

End Namespace
