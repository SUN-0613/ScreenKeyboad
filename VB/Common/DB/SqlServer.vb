Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Namespace DB

    ''' <summary>
    ''' SQL Server操作管理
    ''' </summary>
    Public Class SqlServer
        Implements IDisposable

        ''' <summary>
        ''' Dispose()の重複呼び出し検出
        ''' </summary>
        Private disposedValue As Boolean

#Region "エラー情報"

        ''' <summary>
        ''' エラーメッセージ
        ''' </summary>
        Private _ExceptionMessage As String = ""

        ''' <summary>
        ''' エラーメッセージ
        ''' </summary>
        Public ReadOnly Property ExceptionMessage As String
            Get
                Return _ExceptionMessage
            End Get
        End Property

        ''' <summary>
        ''' エラー発生
        ''' </summary>
        ''' <returns>
        ''' True:エラー有
        ''' False:エラー無
        ''' </returns>
        Public ReadOnly Property IsError As Boolean
            Get
                Return Not _ExceptionMessage.Length.Equals(0)
            End Get
        End Property

#End Region

#Region "SQL Server 接続情報"

        ''' <summary>
        ''' 接続するサーバ名
        ''' </summary>
        Private _ServerName As String

        ''' <summary>
        ''' DB名
        ''' </summary>
        Private _DbName As String

        ''' <summary>
        ''' ユーザ名
        ''' </summary>
        Private _UserName As String

        ''' <summary>
        ''' パスワード
        ''' </summary>
        Private _Password As String

        ''' <summary>
        ''' 接続インスタンス
        ''' </summary>
        Private _SqlConnection As SqlConnection

        ''' <summary>
        ''' 接続FLG
        ''' </summary>
        Private _IsConnect As Boolean = False

        ''' <summary>
        ''' トランザクション
        ''' </summary>
        Private _SqlTransaction As SqlTransaction

#End Region

        ''' <summary>
        ''' SQL Server操作管理
        ''' SQL Server認証によるDB接続
        ''' </summary>
        ''' <param name="serverName">接続するサーバ名</param>
        ''' <param name="dbName">DB名</param>
        ''' <param name="userName">ユーザ名</param>
        ''' <param name="password">パスワード</param>
        ''' <param name="timeOut">タイムアウト時間(秒)</param>
        Public Sub New(serverName As String, dbName As String, userName As String, password As String, Optional timeOut As Integer = 30)

            ' 接続情報の保存
            _ServerName = serverName
            _DbName = dbName
            _UserName = userName
            _Password = password

            ' 接続開始
            Open(False, timeOut)

        End Sub

        ''' <summary>
        ''' SQL Server操作管理
        ''' Windows認証によるDB接続
        ''' </summary>
        ''' <param name="serverName">接続するサーバ名</param>
        ''' <param name="dbName">DB名</param>
        ''' <param name="timeOut">タイムアウト時間(秒)</param>
        Public Sub New(serverName As String, dbName As String, Optional timeOut As Integer = 30)

            ' 接続情報の保存
            _ServerName = serverName
            _DbName = dbName
            _UserName = ""
            _Password = ""

            ' 接続開始
            Open(True, timeOut)

        End Sub

#Region "IDisposable Support"

        ''' <summary>
        ''' 終了処理
        ''' </summary>
        ''' <param name="disposing"></param>
        Protected Overridable Sub Dispose(disposing As Boolean)

            If Not disposedValue Then

                If disposing Then

                    ' トランザクションの途中ならロールバック
                    Rollback()

                    ' 切断
                    Close()

                End If

                ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下の Finalize() をオーバーライドします。
                ' TODO: 大きなフィールドを null に設定します。

            End If

            disposedValue = True

        End Sub

        ''' <summary>
        ''' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose

            ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
            Dispose(True)
            ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
            ' GC.SuppressFinalize(Me)

        End Sub

#End Region

        ''' <summary>
        ''' DB接続
        ''' </summary>
        ''' <param name="integratedSecurity">Windows認証</param>
        ''' <param name="timeOut">タイムアウト時間(秒)</param>
        Private Sub Open(integratedSecurity As Boolean, timeOut As Integer)

            Dim connectionString As StringBuilder = New StringBuilder(256)

            Try

                ' 接続文字列作成
                If (integratedSecurity) Then

                    ' Windows認証
                    connectionString.Append("Data Source = ").Append(_ServerName).Append(";")
                    connectionString.Append("Initial Catalog = ").Append(_DbName).Append(";")
                    connectionString.Append("Integrated Security = True;")
                    connectionString.Append("MultipleActiveResultSets = True;")
                    connectionString.Append("Connection Timeout = ").Append(timeOut.ToString())

                Else

                    ' SQL Server 認証
                    connectionString.Append("Data Source = ").Append(_ServerName).Append(";")
                    connectionString.Append("Initial Catalog = ").Append(_DbName).Append(";")
                    connectionString.Append("User ID = ").Append(_UserName).Append(";")
                    connectionString.Append("Password = ").Append(_Password).Append(";")
                    connectionString.Append("MultipleActiveResultSets = True")
                    connectionString.Append("Connection Timeout = ").Append(timeOut.ToString())


                End If

                ' 接続済の場合、接続解除
                If Not IsNothing(_SqlConnection) Then

                    Close()

                End If

                ' インスタンス生成
                _SqlConnection = New SqlConnection(connectionString.ToString())

                ' DB接続
                _SqlConnection.Open()
                _IsConnect = True

            Catch ex As Exception

                _ExceptionMessage = ex.Message

            Finally

                connectionString.Clear()
                connectionString = Nothing

            End Try

        End Sub

        ''' <summary>
        ''' DB切断
        ''' </summary>
        Private Sub Close()

            Try

                ' 切断
                If _IsConnect Then

                    _SqlConnection.Close()
                    _IsConnect = False

                End If

                ' メモリ解放
                If Not IsNothing(_SqlConnection) Then

                    _SqlConnection.Dispose()
                    _SqlConnection = Nothing

                End If

            Catch ex As Exception

                _ExceptionMessage = ex.Message

            End Try

        End Sub

        ''' <summary>
        ''' トレンザクション開始
        ''' </summary>
        Public Sub BeginTransaction()

            Try

                _SqlTransaction = _SqlConnection.BeginTransaction()

            Catch ex As Exception

                _ExceptionMessage = ex.Message

            End Try

        End Sub

        ''' <summary>
        ''' コミット
        ''' </summary>
        Public Sub Commit()

            Try

                If Not IsNothing(_SqlTransaction) Then

                    _SqlTransaction.Commit()
                    _SqlTransaction.Dispose()

                End If

            Catch ex As Exception

                _ExceptionMessage = ex.Message

            End Try

        End Sub

        ''' <summary>
        ''' ロールバック
        ''' </summary>
        Public Sub Rollback()

            Try

                If Not IsNothing(_SqlTransaction) Then

                    _SqlTransaction.Rollback()
                    _SqlTransaction.Dispose()

                End If

            Catch ex As Exception

                _ExceptionMessage = ex.Message

            End Try

        End Sub

        ''' <summary>
        ''' クエリ実行
        ''' </summary>
        ''' <param name="query">SQL文</param>
        ''' <param name="parameters">SQLパラメータ</param>
        ''' <param name="queryTimeout">クエリ実行時間(秒)</param>
        ''' <returns>クエリ実行結果</returns>
        Public Function ExecuteQuery(query As String, parameters As Dictionary(Of String, Object), Optional queryTimeout As Integer = 30)

            Try

                Using sqlCommand As SqlCommand = New SqlCommand(query, _SqlConnection, _SqlTransaction)

                    ' タイムアウトの設定
                    sqlCommand.CommandTimeout = queryTimeout

                    ' パラメータの設定
                    For Each parameter As KeyValuePair(Of String, Object) In parameters

                        sqlCommand.Parameters.Add(New SqlParameter(parameter.Key, parameter.Value))

                    Next

                    ' 実行
                    Return sqlCommand.ExecuteReader()

                End Using

            Catch ex As Exception

                _ExceptionMessage = ex.Message
                Return Nothing

            End Try

        End Function

        ''' <summary>
        ''' クエリ実行
        ''' </summary>
        ''' <param name="query">SQL文</param>
        ''' <param name="queryTimeout">クエリ実行時間(秒)</param>
        ''' <returns>クエリ実行結果</returns>
        Public Function ExecuteQuery(query As String, Optional queryTimeout As Integer = 30)

            Return ExecuteQuery(query, New Dictionary(Of String, Object), queryTimeout)

        End Function

        ''' <summary>
        ''' クエリ実行
        ''' </summary>
        ''' <param name="query">SQL文</param>
        ''' <param name="parameters">SQLパラメータ</param>
        ''' <param name="queryTimeout">クエリ実行時間(秒)</param>
        Public Sub ExecuteNonQuery(query As String, parameters As Dictionary(Of String, Object), Optional queryTimeout As Integer = 30)

            Try

                Using sqlCommand As SqlCommand = New SqlCommand(query, _SqlConnection, _SqlTransaction)

                    ' タイムアウトの設定
                    sqlCommand.CommandTimeout = queryTimeout

                    ' パラメータの設定
                    For Each parameter As KeyValuePair(Of String, Object) In parameters

                        sqlCommand.Parameters.Add(New SqlParameter(parameter.Key, parameter.Value))

                    Next

                    ' 実行
                    sqlCommand.ExecuteNonQuery()

                End Using

            Catch ex As Exception

                _ExceptionMessage = ex.Message

            End Try

        End Sub

        ''' <summary>
        ''' クエリ実行後、DataTableにて返す
        ''' </summary>
        ''' <param name="query">SQL文</param>
        ''' <param name="queryTimeout">クエリ実行時間(秒)</param>
        ''' <returns>クエリ実行結果</returns>
        Public Function ExecuteNonQuery(query As String, Optional queryTimeout As Integer = 30) As DataTable

            Try

                Using sqlCommand As SqlCommand = New SqlCommand(query, _SqlConnection, _SqlTransaction)

                    ' タイムアウトの設定
                    sqlCommand.CommandTimeout = queryTimeout

                    ' 実行
                    sqlCommand.ExecuteNonQuery()

                    ' 戻り値作成
                    Dim readData As DataTable = New DataTable()
                    Dim dataAdapter As SqlDataAdapter = New SqlDataAdapter(sqlCommand)

                    dataAdapter.Fill(readData)

                    Return readData

                End Using

            Catch ex As Exception

                _ExceptionMessage = ex.Message
                Return Nothing

            End Try

        End Function

    End Class

End Namespace

