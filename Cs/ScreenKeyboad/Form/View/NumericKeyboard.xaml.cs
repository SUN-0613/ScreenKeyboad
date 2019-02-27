using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ScreenKeyboad.Form.View
{
    /// <summary>
    /// NumericKeyboard.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericKeyboard : Window, IDisposable
    {

        /// <summary>
        /// 戻り値
        /// </summary>
        public string ReturnValue = "";

        /// <summary>
        /// ViewModel
        /// </summary>
        private ViewModel.NumericKeyboard _ViewModel;

        /// <summary>
        /// テンキー型スクリーンキーボード.View
        /// </summary>
        /// <param name="value">初期値</param>
        public NumericKeyboard(string value = "")
        {

            InitializeComponent();

            _ViewModel = new ViewModel.NumericKeyboard(value);
            DataContext = _ViewModel;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _ViewModel.Dispose();
            _ViewModel = null;
        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallNextFocus":   //次項目へフォーカス移動

                    ReturnValue = _ViewModel.Text;
                    (FocusManager.GetFocusedElement(this.Owner) as FrameworkElement)?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                    break;

                default:
                    break;

            }

        }

    }
}
