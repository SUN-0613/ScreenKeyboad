using AYam.ScreenKeyboad.Form.View;
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

        /// <summary>
        /// アルファベットキーボード使用可否依存プロパティ
        /// </summary>
        public static readonly DependencyProperty IsUseStringKeyboardProperty =
            DependencyProperty.Register(
                nameof(IsUseStringKeyboard)
                , typeof(bool)
                , typeof(TextBoxSK)
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

        /// <summary>
        /// アルファベットキーボード使用可否プロパティ
        /// </summary>
        public bool IsUseStringKeyboard
        {
            get { return (bool)GetValue(IsUseStringKeyboardProperty); }
            set { SetValue(IsUseStringKeyboardProperty, value); }
        }

        #endregion

        #region Dialog

        /// <summary>
        /// テンキーダイアログ
        /// </summary>
        private NumericKeyboard _NumericDialog = null;

        /// <summary>
        /// アルファベットキーボードダイアログ
        /// </summary>
        private StringKeyboard _StringDialog = null;

        #endregion

        #region Event

        /// <summary>
        /// フォーカス取得イベント
        /// </summary>
        private void OnEnter(object sender, EventArgs e)
        {

            Point point = PointToScreen(new Point(0d, 0d));

            // テンキー表示
            if (IsUseNumericKeyboard)
            {

                _NumericDialog = new NumericKeyboard((string)GetValue(TextProperty), point.X, point.Y, ActualHeight, ActualWidth)
                {
                    Owner = Window.GetWindow(this)
                };
                _NumericDialog.Show();

            }
            else if (IsUseStringKeyboard)
            {

                _StringDialog = new StringKeyboard((string)GetValue(TextProperty), point.X, point.Y, ActualHeight, ActualWidth)
                {
                    Owner = Window.GetWindow(this)
                };
                _StringDialog.Show();

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

                if (_NumericDialog.IsPushNextKey)
                {
                    SetValue(TextProperty, _NumericDialog.ReturnValue);
                }

                _NumericDialog.Close();
                _NumericDialog.Dispose();
                _NumericDialog = null;

            }

            if (_StringDialog != null)
            {

                if (_StringDialog.IsPushNextKey)
                {
                    SetValue(TextProperty, _StringDialog.ReturnValue);
                }

                _StringDialog.Close();
                _StringDialog.Dispose();
                _StringDialog = null;

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

            if (_StringDialog != null)
            {
                _StringDialog.Dispose();
                _StringDialog = null;
            }

            GotFocus -= OnEnter;
            LostFocus -= OnLeave;

        }

    }

}
