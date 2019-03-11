
Namespace ViewModel.Custom

    ''' <summary>
    ''' ViewModel基幹
    ''' 編集FLG付
    ''' </summary>
    Public Class EditedBase
        Inherits ViewModel.VMBase

        ''' <summary>
        ''' 編集FLG
        ''' True:編集済
        ''' False:未編集
        ''' </summary>
        Private _IsEdited As Boolean = False

        ''' <summary>
        ''' 編集FLGプロパティ
        ''' True:編集済
        ''' False:未編集
        ''' </summary>
        ''' <returns>
        ''' True:編集済
        ''' False:未編集
        ''' </returns>
        Public Property IsEdited As Boolean
            Get
                Return _IsEdited
            End Get
            Set(value As Boolean)

                _IsEdited = value
                CallPropertyChanged(NameOf(IsEdited))

            End Set
        End Property

        ''' <summary>
        ''' 編集FLGの更新対象外プロパティ名
        ''' 初期値："Call"
        ''' </summary>
        ''' <remarks>先頭一致するプロパティ名の場合はIsEditedを更新しない</remarks>
        Private _ThrowEditEventName As String = "Call"

        ''' <summary>
        ''' 編集FLGの更新対象外プロパティ名
        ''' 初期値："Call"
        ''' </summary>
        ''' <remarks>先頭一致するプロパティ名の場合はIsEditedを更新しない</remarks>
        Public WriteOnly Property ThrowEditEventName As String
            Set(value As String)
                _ThrowEditEventName = value
            End Set
        End Property

        ''' <summary>
        ''' PropertyChanged()呼び出し
        ''' </summary>
        ''' <param name="propertyName">Changedイベントを発生させたいプロパティ名</param>
        ''' <param name="stackFrameIndex">呼び出し元StackFrame</param>
        Protected Overrides Sub CallPropertyChanged(Optional propertyName As String = "", Optional stackFrameIndex As Integer = 1)

            MyBase.CallPropertyChanged(propertyName, stackFrameIndex + 1)

            ' 編集FLGの更新
            ' 編集FLGプロパティ更新時は除外
            ' ThrowEditEventNameにて指定されたプロパティ更新時は除外
            If Not propertyName.Equals(NameOf(IsEdited)) _
                And Not (propertyName.Length >= _ThrowEditEventName.Length _
                        And propertyName.Substring(0, _ThrowEditEventName.Length).ToUpper().Equals(_ThrowEditEventName.ToUpper())) Then

                IsEdited = True

            End If

        End Sub

    End Class

End Namespace
