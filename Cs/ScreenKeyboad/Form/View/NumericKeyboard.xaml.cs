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
        /// <param name="left">呼出元Control.PointToScreen.X</param>
        /// <param name="top">呼出元Control.PointToScreen.Y</param>
        /// <param name="height">呼出元Control.Height</param>
        public NumericKeyboard(string value = "", double left = -1d, double top = -1d, double height = -1d)
        {

            InitializeComponent();

            _ViewModel = new ViewModel.NumericKeyboard(value);
            DataContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

            // 表示位置
            if (!left.Equals(-1d) && !top.Equals(-1d) && !height.Equals(-1d))
            {

                Left = left;

                if (top + height + Height > SystemParameters.WorkArea.Height)
                {
                    Top = top - Height;
                }
                else
                {
                    Top = top + height;
                }

            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

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
