using AYam.Common.ViewModel;
using System;

namespace ScreenKeyboad.Form.ViewModel
{

    /// <summary>
    /// NumericKeyboard.ViewModel
    /// </summary>
    public class NumericKeyboard : VMBase, IDisposable
    {

        #region Property

        /// <summary>
        /// 入力値プロパティ
        /// </summary>
        public string Text
        {
            get { return _Model.Text; }
            set
            {

                if (double.TryParse(value, out double dummy))
                {

                    if (!_Model.Text.Equals(value))
                    {
                        _Model.Text = value;
                    }

                }

                CallPropertyChanged();

            }
        }

        /// <summary>
        /// 文字選択開始位置プロパティ
        /// </summary>
        public int SelectionStart
        {
            get { return _Model.SelectionStart; }
            set
            {

                if (!_Model.SelectionStart.Equals(value))
                {

                    _Model.SelectionStart = value;
                    CallPropertyChanged();

                }

            }
        }

        /// <summary>
        /// 選択文字数プロパティ
        /// </summary>
        public int SelectionLength
        {
            get { return _Model.SelectionLength; }
            set
            {

                if (!_Model.SelectionLength.Equals(value))
                {

                    _Model.SelectionLength = value;
                    CallPropertyChanged();

                }

            }
        }

        /// <summary>
        /// ボタンクリックコマンド
        /// </summary>
        public DelegateCommand<string> InputCommand
        {
            get
            {

                if (_Model.InputCommand == null)
                {

                    _Model.InputCommand = new DelegateCommand<string>
                    (
                        (parameter) => 
                        {

                            _Model.InputText(parameter);

                            CallPropertyChanged(nameof(Text));
                            CallPropertyChanged(nameof(SelectionStart));
                            CallPropertyChanged(nameof(SelectionLength));

                        },
                        () => true
                    );

                }

                return _Model.InputCommand;

            }
        }

        #endregion

        /// <summary>
        /// Model
        /// </summary>
        private Model.NumericKeyboard _Model;

        /// <summary>
        /// テンキー型スクリーンキーボード.ViewModel
        /// </summary>
        public NumericKeyboard()
        {
            _Model = new Model.NumericKeyboard();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _Model.Dispose();
            _Model = null;

        }

    }

}
