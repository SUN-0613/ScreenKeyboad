using ScreenKeyboad.Form.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ScreenKeyboad.Custom
{

    /// <summary>
    /// スクリーンキーボード表示対応テキスト欄
    /// </summary>
    public class TextBoxSK : TextBox, IDisposable
    {

        #region DependencyProperty

        /// <summary>
        /// テンキー使用可否依存プロパティ
        /// </summary>
        public static readonly DependencyProperty IsUseNumericKeyboardProperty =
            DependencyProperty.Register(
                nameof(IsUseNumericKeyboard)
                ,typeof(bool)
                ,typeof(TextBoxSK)
                , new FrameworkPropertyMetadata(false)
                );

        #endregion

        #region Property

        /// <summary>
        /// テンキー使用可否プロパティ
        /// </summary>
        public bool IsUseNumericKeyboard
        {
            get { return (bool)GetValue(IsUseNumericKeyboardProperty); }
            set { SetValue(IsUseNumericKeyboardProperty, value); }
        }

        #endregion

        #region Dialog

        /// <summary>
        /// テンキーダイアログ
        /// </summary>
        private NumericKeyboard _NumericDialog = null;

        #endregion

        #region Event

        /// <summary>
        /// フォーカス取得イベント
        /// </summary>
        private void OnEnter(object sender, EventArgs e)
        {

            // テンキー表示
            if (IsUseNumericKeyboard)
            {

                Point point = PointToScreen(new Point(0d, 0d));

                _NumericDialog = new NumericKeyboard((string)GetValue(TextProperty), point.X, point.Y, ActualHeight)
                {
                    Owner = Window.GetWindow(this)
                };
                _NumericDialog.Show();

            }

            Background = Brushes.Yellow;

        }

        /// <summary>
        /// フォーカス喪失イベント
        /// </summary>
        private void OnLeave(object sender, EventArgs e)
        {

            if (_NumericDialog != null)
            {

                if (!_NumericDialog.ReturnValue.Length.Equals(0))
                {
                    SetValue(TextProperty, _NumericDialog.ReturnValue);
                }

                _NumericDialog.Close();
                _NumericDialog.Dispose();
                _NumericDialog = null;

            }

            Background = Brushes.White;

        }

        #endregion

        /// <summary>
        /// スクリーンキーボード表示対応テキスト欄
        /// </summary>
        public TextBoxSK() : base()
        {

            GotFocus += OnEnter;
            LostFocus += OnLeave;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_NumericDialog != null)
            {
                _NumericDialog.Dispose();
                _NumericDialog = null;
            }

            GotFocus -= OnEnter;
            LostFocus -= OnLeave;

        }

    }

}
