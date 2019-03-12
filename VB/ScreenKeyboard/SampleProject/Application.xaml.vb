Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports System.Windows

''' <summary>
''' 相互作用ロジック
''' </summary>
Class Application

    ''' <summary>
    ''' 初回起動
    ''' </summary>
    Protected Overrides Sub OnStartup(e As StartupEventArgs)

        ' 実行パス取得
        Dim appName As String = Assembly.GetExecutingAssembly().GetName().Name

        ' 実行ファイル名取得
        appName = Path.GetFileNameWithoutExtension(appName)

        ' 多重起動情報取得
        Dim mutex As Mutex = New Mutex(False, appName)

        ' 多重起動確認
        If (mutex.WaitOne(0, False)) Then

            ' 基本処理
            MyBase.OnStartup(e)

            ' 画面表示
            Dim form As Form.View.InputDialog = New Form.View.InputDialog()
            form.ShowDialog()

        Else

            ' 多重起動時はメッセージ出力、終了
            MessageBox.Show(My.Resources.App_MutexMessage, appName, MessageBoxButton.OK, MessageBoxImage.Error)
            Shutdown()

        End If

    End Sub

End Class
