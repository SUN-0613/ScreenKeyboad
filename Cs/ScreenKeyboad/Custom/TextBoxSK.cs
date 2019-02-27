using ScreenKeyboad.Form.View;
using System;
using System.Windows;
using System.Windows.Controls;

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
                , new FrameworkPropertyMetadata("false", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
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

                _NumericDialog = new NumericKeyboard((string)GetValue(TextProperty))
                {
                    Owner = Window.GetWindow(this)
                };
                _NumericDialog.Show();

            }

        }

        /// <summary>
        /// フォーカス喪失イベント
        /// </summary>
        private void OnLeave(object sender, EventArgs e)
        {

            if (_NumericDialog != null)
            {

                SetValue(TextProperty, _NumericDialog.ReturnValue);

                _NumericDialog.Dispose();
                _NumericDialog = null;

            }

        }

        #endregion

        /// <summary>
        /// スクリーンキーボード表示対応テキスト欄
        /// </summary>
        public TextBoxSK() : base()
        { }

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

        }

    }

}
