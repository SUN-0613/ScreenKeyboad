using AYam.Common.ViewModel;
using System;
using System.Windows;

namespace AYam.ScreenKeyboad.Form.Model
{

    /// <summary>
    /// Keyboard.Model
    /// </summary>
    internal class Keyboard : IDisposable
    {

        #region Property

        /// <summary>
        /// 現在値
        /// </summary>
        public string NowValue;

        /// <summary>
        /// 入力値
        /// </summary>
        public string Text;

        /// <summary>
        /// 文字選択開始位置
        /// 今回未使用
        /// </summary>
        public int SelectionStart = 0;

        /// <summary>
        /// 選択文字数
        /// 今回未使用
        /// </summary>
        public int SelectionLength = 0;

        /// <summary>
        /// ボタンクリックコマンド
        /// </summary>
        public DelegateCommand<string> InputCommand;

        /// <summary>
        /// Shiftキー押下
        /// </summary>
        private bool _IsShift = false;

        /// <summary>
        /// Shiftキー押下プロパティ
        /// </summary>
        public bool IsShift
        {
            get { return _IsShift; }
            set
            {

                if (!IsCaps)
                {

                    _IsShift = value;

                    if (value)
                    {

                        ShiftOffVisible = Visibility.Hidden;
                        ShiftOnVisible = Visibility.Visible;

                    }
                    else
                    {

                        ShiftOffVisible = Visibility.Visible;
                        ShiftOnVisible = Visibility.Hidden;

                    }

                }

            }
        }

        /// <summary>
        /// Caps Lockキー押下
        /// </summary>
        public bool IsCaps = false;

        /// <summary>
        /// Shiftキー非押下時に表示
        /// </summary>
        public Visibility ShiftOffVisible = Visibility.Visible;

        /// <summary>
        /// Shiftキー押下時に表示
        /// </summary>
        public Visibility ShiftOnVisible = Visibility.Hidden;

        #endregion

        /// <summary>
        /// キーボード.ModelのBase
        /// </summary>
        /// <param name="value"></param>
        public Keyboard(string value)
        {
            Initialize(value);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="nowValue">現在値</param>
        private void Initialize(string nowValue = "")
        {

            NowValue = nowValue;
            Text = "";
            SelectionStart = 0;
            SelectionLength = 0;
            IsShift = false;

        }

        /// <summary>
        /// 文字入力
        /// </summary>
        /// <param name="content">Button.Content</param>
        /// <returns>
        /// -1 : 通常処理
        ///  1 : 次フォーカスへ移動
        ///  2 : Shift処理
        /// </returns>
        public int InputText(string content)
        {

            int returnValue = -1;

            switch (content.ToUpper())
            {

                case "CLEAR":

                    Initialize();
                    break;

                case "BS":

                    // 先頭フォーカスは削除する文字がないため除外
                    if (SelectionStart > 0)
                    {
                        SelectionStart -= 1;
                        SelectionLength = 1;
                        ControlText();
                    }

                    IsShift = false;
                    break;

                case "±":

                    // ブランクでないか
                    if (Text.Length > 0)
                    {

                        // 先頭1文字が"-"か
                        if (Text.Substring(0, 1).Equals("-"))
                        {

                            Text = Text.Substring(1);

                            if (SelectionStart > 0)
                            {
                                SelectionStart -= 1;
                            }

                        }
                        else
                        {

                            Text = "-" + Text;
                            SelectionStart += 1;

                        }
                        SelectionLength = 0;

                    }
                    else
                    {

                        Text = "-";
                        SelectionStart = 1;
                        SelectionLength = 0;

                    }

                    IsShift = false;
                    break;

                case "NUMBERDOT":

                    if (Text.Length.Equals(0) 
                        || (Text.Length.Equals(1) 
                            && Text.Equals("-")
                            )
                        )
                    {
                        Text = Text + "0.";
                        SelectionStart = 2;
                        SelectionLength = 0;
                    }
                    else
                    {
                        ControlText(".");
                    }

                    IsShift = false;
                    break;

                case "00":

                    if (Text.Length.Equals(0) 
                        || (Text.Length.Equals(1) 
                            && (Text.Equals("-") || Text.Equals("0"))
                            )
                        || (Text.Length.Equals(2) && Text.Equals("-0"))
                        )
                    {
                        Text = Text.Contains("-") ? "-0" :  "0";
                        SelectionStart = Text.Length;
                        SelectionLength = 0;
                    }
                    else
                    {
                        ControlText("00");
                    }

                    IsShift = false;
                    break;

                case "NEXT":
                    returnValue = 1;
                    break;

                case "SHIFT":
                    IsShift = !IsShift;
                    returnValue = 2;
                    break;

                case "CAPS":

                    if (IsShift)
                    {
                        IsCaps = !IsCaps;
                    }
                    IsShift = false;

                    returnValue = 3;
                    break;

                default:
                    ControlText(content);
                    IsShift = false;
                    break;

            }

            return returnValue;

        }

        /// <summary>
        /// Textbox.Textから選択文字列を削除し、指定文字列を挿入して返す
        /// </summary>
        /// <param name="insertString">挿入文字列</param>
        /// /// <returns>Textbox.Textから選択文字列を削除し指定文字列を挿入した文字列</returns>
        protected virtual void ControlText(string insertString = "")
        {

            // 削除文字よりも前を取得
            string firstString = SelectionStart.Equals(0) ? "" : Text.Substring(0, SelectionStart);

            // 削除文字よりも後を取得
            string secondString = (SelectionStart + SelectionLength).Equals(Text.Length) ? "" : Text.Substring(SelectionStart + SelectionLength);

            // 文字結合
            Text = firstString + insertString + secondString;

            // 選択位置調整
            SelectionStart = firstString.Length + insertString.Length;
            SelectionLength = 0;

        }

    }

}
