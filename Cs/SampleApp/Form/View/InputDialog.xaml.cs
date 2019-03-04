using System;
using System.Windows;

namespace SampleApp.Form.View
{
    /// <summary>
    /// InputDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class InputDialog : Window, IDisposable
    {

        /// <summary>
        /// ViewModel
        /// </summary>
        private ViewModel.InputDialog _ViewModel;

        /// <summary>
        /// スクリーンキーボードサンプルApp.View
        /// </summary>
        public InputDialog()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.InputDialog();
            DataContext = _ViewModel;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _ViewModel.Dispose();
        }

    }
}
