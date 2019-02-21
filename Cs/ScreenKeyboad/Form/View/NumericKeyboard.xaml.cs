using System;
using System.Windows;

namespace ScreenKeyboad.Form.View
{
    /// <summary>
    /// NumericKeyboard.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericKeyboard : Window, IDisposable
    {

        /// <summary>
        /// ViewModel
        /// </summary>
        private ViewModel.NumericKeyboard _ViewModel;

        /// <summary>
        /// テンキー型スクリーンキーボード.View
        /// </summary>
        public NumericKeyboard()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.NumericKeyboard();
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

    }
}
