Imports System.Windows.Input

Namespace ViewModel

    ''' <summary>
    ''' Delegateを受け取るICommandの実装
    ''' </summary>
    Public Class DelegateCommand
        Implements ICommand

        ''' <summary>
        ''' CanExecute変更イベント
        ''' </summary>
        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        ''' <summary>
        ''' 実行メソッド
        ''' </summary>
        Private ReadOnly _Execute As Action

        ''' <summary>
        ''' 実行メソッド処理許可
        ''' </summary>
        Private ReadOnly _CanExecute As Func(Of Boolean)

        ''' <summary>
        ''' Delegateを受け取るICommandの実装
        ''' </summary>
        ''' <param name="execute">実行メソッド</param>
        Public Sub New(execute As Action)

            If Not IsNothing(execute) Then
                _Execute = execute
            Else
                Throw New ArgumentNullException(NameOf(DelegateCommand) & ":" & NameOf(execute))
            End If

            _CanExecute = Nothing

        End Sub

        ''' <summary>
        ''' Delegateを受け取るICommandの実装
        ''' </summary>
        ''' <param name="execute">実行メソッド</param>
        ''' <param name="canExecute">実行メソッドの処理許可</param>
        Public Sub New(execute As Action, canExecute As Func(Of Boolean))

            If Not IsNothing(execute) Then
                _Execute = execute
            Else
                Throw New ArgumentNullException(NameOf(DelegateCommand) & ":" & NameOf(execute))
            End If

            If Not IsNothing(canExecute) Then
                _CanExecute = canExecute
            Else
                Throw New ArgumentNullException(NameOf(DelegateCommand) & ":" & NameOf(canExecute))
            End If

        End Sub

        ''' <summary>
        ''' メソッド実行
        ''' </summary>
        ''' <param name="parameter">
        ''' パラメータ
        ''' このクラスでは使用しない
        ''' </param>
        Public Sub Execute(parameter As Object) Implements ICommand.Execute

            _Execute()

        End Sub

        ''' <summary>
        ''' メソッド実行許可の確認
        ''' </summary>
        ''' <param name="parameter">
        ''' パラメータ
        ''' このクラスでは使用しない
        ''' </param>
        ''' <returns>
        ''' True:OK
        ''' False:NG
        ''' </returns>
        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute

            If IsNothing(_CanExecute) Then
                Return True
            Else
                Return _CanExecute()
            End If

        End Function

    End Class

    ''' <summary>
    ''' Delegateを受け取るICommandの実装
    ''' パラメータ有
    ''' </summary>
    ''' <typeparam name="T">コマンドパラメータ</typeparam>
    Public Class DelegateCommand(Of T)
        Implements ICommand

        ''' <summary>
        ''' CanExecute変更イベント
        ''' </summary>
        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        ''' <summary>
        ''' 実行メソッド
        ''' </summary>
        Private ReadOnly _Execute As Action(Of T)

        ''' <summary>
        ''' 実行メソッド処理許可
        ''' </summary>
        Private ReadOnly _CanExecute As Func(Of Boolean)

        ''' <summary>
        ''' Delegateを受け取るICommandの実装
        ''' </summary>
        ''' <param name="execute">実行メソッド</param>
        Public Sub New(execute As Action(Of T))

            If Not IsNothing(execute) Then
                _Execute = execute
            Else
                Throw New ArgumentNullException(NameOf(DelegateCommand) & ":" & NameOf(execute))
            End If

            _CanExecute = Nothing

        End Sub

        ''' <summary>
        ''' Delegateを受け取るICommandの実装
        ''' </summary>
        ''' <param name="execute">実行メソッド</param>
        ''' <param name="canExecute">実行メソッドの処理許可</param>
        Public Sub New(execute As Action(Of T), canExecute As Func(Of Boolean))

            If Not IsNothing(execute) Then
                _Execute = execute
            Else
                Throw New ArgumentNullException(NameOf(DelegateCommand) & ":" & NameOf(execute))
            End If

            If Not IsNothing(canExecute) Then
                _CanExecute = canExecute
            Else
                Throw New ArgumentNullException(NameOf(DelegateCommand) & ":" & NameOf(canExecute))
            End If

        End Sub

        ''' <summary>
        ''' メソッド実行
        ''' </summary>
        ''' <param name="parameter">
        ''' パラメータ
        ''' このクラスでは使用しない
        ''' </param>
        Public Sub Execute(parameter As Object) Implements ICommand.Execute

            _Execute(parameter)

        End Sub

        ''' <summary>
        ''' メソッド実行許可の確認
        ''' </summary>
        ''' <param name="parameter">
        ''' パラメータ
        ''' このクラスでは使用しない
        ''' </param>
        ''' <returns>
        ''' True:OK
        ''' False:NG
        ''' </returns>
        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute

            If IsNothing(_CanExecute) Then
                Return True
            Else
                Return _CanExecute()
            End If

        End Function

    End Class

End Namespace
