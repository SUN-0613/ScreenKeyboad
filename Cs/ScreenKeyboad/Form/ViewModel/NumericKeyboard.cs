using AYam.Common.ViewModel;
using System;

namespace ScreenKeyboad.Form.ViewModel
{

    /// <summary>
    /// NumericKeyboard.ViewModel
    /// </summary>
    internal class NumericKeyboard : VMBase, IDisposable
    {

        #region Property

        /// <summary>
        /// 現在値プロパティ
        /// </summary>
        public string NowValue
        {
            get { return _Model.NowValue; }
            set
            {

                if (double.TryParse(value, out double newValue))
                {

                    if (!_Model.NowValue.Equals(value))
                    {
                        _Model.NowValue = value;
                    }

                }

                CallPropertyChanged();

            }
        }

        /// <summary>
        /// 入力値プロパティ
        /// </summary>
        public string Text
        {
            get { return _Model.Text; }
            set
            {

                if (double.TryParse(value, out double newValue))
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
        /// 今回未使用
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
        /// 今回未使用
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

                            if (_Model.InputText(parameter))
                            {

                                CallPropertyChanged("CallNextFocus");

                            }
                            else
                            {

                                CallPropertyChanged(nameof(Text));
                                CallPropertyChanged(nameof(SelectionStart));
                                CallPropertyChanged(nameof(SelectionLength));

                            }

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
        /// <param name="value">初期値</param>
        public NumericKeyboard(string value)
        {
            _Model = new Model.NumericKeyboard(value);
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
