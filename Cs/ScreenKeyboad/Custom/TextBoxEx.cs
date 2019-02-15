using System;
using System.Windows;
using System.Windows.Controls;

namespace ScreenKeyboad.Custom
{

    /// <summary>
    /// TextBox機能拡張
    /// </summary>
    /// <remarks>参考URL：https://social.msdn.microsoft.com/Forums/vstudio/en-US/7eee698c-328b-4044-b8e4-cce538b1f1b7/dependencyproperty-of-textboxselectionstart-?forum=wpf</remarks>
    public class TextBoxEx : TextBox, IDisposable
    {

        #region DependencyProperty

        /// <summary>
        /// 選択位置プロパティの宣言
        /// </summary>
        public static readonly DependencyProperty BindableSelectionStartProperty =
            DependencyProperty.Register(
                nameof(BindableSelectionStart)
                , typeof(int)
                , typeof(TextBoxEx)
                , new PropertyMetadata(OnBindableSelectionStartChanged)
                );

        /// <summary>
        /// 選択文字数プロパティの宣言
        /// </summary>
        public static readonly DependencyProperty BindableSelectionLengthProperty =
            DependencyProperty.Register(
                nameof(BindableSelectionLength)
                , typeof(int)
                , typeof(TextBoxEx)
                , new PropertyMetadata()
                );


        #endregion

        #region Property

        /// <summary>
        /// 文字列選択開始位置プロパティ
        /// </summary>
        public int BindableSelectionStart
        {
            get { return (int)GetValue(BindableSelectionStartProperty); }
            set { SetValue(BindableSelectionStartProperty, value); }
        }

        /// <summary>
        /// 選択文字数プロパティ
        /// </summary>
        public int BindableSelectionLength
        {
            get { return (int)GetValue(BindableSelectionLengthProperty); }
            set { SetValue(BindableSelectionLengthProperty, value); }
        }

        #endregion

        #region Event

        /// <summary>
        /// 文字列選択開始位置変更イベント
        /// </summary>
        /// <param name="dependencyObject">TextBoxEx</param>
        /// <param name="e">開始位置情報</param>
        private static void OnBindableSelectionStartChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {

            if (dependencyObject is TextBoxEx textBox)
            {

                // OnSelectionChanged()にて変更済か
                if (!textBox._ChangeFromUI)
                {
                    textBox.SelectionStart = (int)e.NewValue;
                }
                else
                {
                    // FLG初期化
                    textBox._ChangeFromUI = false;
                }

            }

        }

        /// <summary>
        /// 文字列選択数変更イベント
        /// </summary>
        /// <param name="dependencyObject">TextBoxEx</param>
        /// <param name="e">選択文字数情報</param>
        private static void OnSelectionLengthChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {

            if (dependencyObject is TextBoxEx textBox)
            {

                // OnSelectionChanged()にて変更済か
                if (!textBox._ChangeFromUI)
                {
                    textBox.SelectionLength = (int)e.NewValue;
                }
                else
                {
                    // FLG初期化
                    textBox._ChangeFromUI = false;
                }

            }

        }

        /// <summary>
        /// 選択位置変更イベント
        /// </summary>
        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {

            // 開始位置変更
            if (!BindableSelectionStart.Equals(SelectionStart))
            {
                _ChangeFromUI = true;
                BindableSelectionStart = SelectionStart;
            }

            // 選択文字数変更
            if (!BindableSelectionLength.Equals(SelectionLength))
            {
                _ChangeFromUI = true;
                BindableSelectionLength = SelectionLength;
            }

        }

        #endregion

        /// <summary>
        /// ユーザインターフェースによる変更FLG
        /// </summary>
        private bool _ChangeFromUI;

        /// <summary>
        /// new
        /// </summary>
        public TextBoxEx() : base()
        {
            SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        ///  終了処理
        /// </summary>
        public void Dispose()
        {
            SelectionChanged -= OnSelectionChanged;
        }

    }

}
