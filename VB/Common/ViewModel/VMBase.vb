Imports System.ComponentModel

Namespace ViewModel

    ''' <summary>
    ''' ViewModel基幹
    ''' </summary>
    Public Class VMBase
        Implements INotifyPropertyChanged

        ''' <summary>
        ''' Event
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ''' <summary>
        ''' PropertyChanged()呼び出し
        ''' </summary>
        ''' <param name="propertyName">Changedイベントを発生させたいプロパティ名</param>
        ''' <param name="stackFrameIndex">呼び出し元StackFrame</param>
        Protected Overridable Sub CallPropertyChanged(Optional propertyName As String = "", Optional stackFrameIndex As Integer = 1)

            ' プロパティ名が指定されていない場合は呼び出し元メソッド名とする
            If (propertyName.Length.Equals(0)) Then

                Dim caller As StackFrame = New StackFrame(stackFrameIndex)      ' 呼び出し元メソッド情報
                Dim methodName As String() = caller.GetMethod().Name.Split("_") ' 呼び出し元メソッド名

                propertyName = methodName(methodName.Length - 1)

            End If

            ' イベント発生
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))

        End Sub

    End Class

End Namespace
