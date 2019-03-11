using AYam.Common.ViewModel;
using System;
using System.Windows;

namespace AYam.ScreenKeyboad.Form.ViewModel
{

    /// <summary>
    /// Keyboard.ViewModel
    /// </summary>
    internal class Keyboard : VMBase, IDisposable
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
        /// ボタンクリックコマンドプロパティ
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

                            switch (_Model.InputText(parameter))
                            {

                                case 1:     // フォーカス移動
                                    CallPropertyChanged("CallNextFocus");
                                    break;

                                case 2:     // Shift押下
                                case 3:     // Caps押下
                                    CallPropertyChanged(nameof(ShiftOffVisible));
                                    CallPropertyChanged(nameof(ShiftOnVisible));
                                    break;

                                default:    // 通常処理:文字入力
                                    CallPropertyChanged(nameof(Text));
                                    CallPropertyChanged(nameof(SelectionStart));
                                    CallPropertyChanged(nameof(SelectionLength));
                                    CallPropertyChanged(nameof(ShiftOffVisible));
                                    CallPropertyChanged(nameof(ShiftOnVisible));
                                    break;

                            }

                        },
                        () => true
                    );

                }

                return _Model.InputCommand;

            }
        }

        /// <summary>
        /// Shiftキー非押下時に表示プロパティ
        /// </summary>
        public Visibility ShiftOffVisible
        {
            get { return _Model.ShiftOffVisible; }
            set
            {
                _Model.ShiftOffVisible = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// Shiftキー押下時に表示プロパティ
        /// </summary>
        public Visibility ShiftOnVisible
        {
            get { return _Model.ShiftOnVisible; }
            set
            {
                _Model.ShiftOnVisible = value;
                CallPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Model
        /// </summary>
        private Model.Keyboard _Model;

        /// <summary>
        /// テンキー型スクリーンキーボード.ViewModel
        /// </summary>
        /// <param name="value">初期値</param>
        public Keyboard(string value)
        {
            _Model = new Model.Keyboard(value);
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
