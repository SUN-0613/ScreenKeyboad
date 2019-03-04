using AYam.Common.ViewModel;
using System;

namespace ScreenKeyboad.Form.Model
{

    /// <summary>
    /// NumericKeyboard.Model
    /// </summary>
    internal class NumericKeyboard : IDisposable
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

        #endregion

        /// <summary>
        /// テンキー型スクリーンキーボード.Model
        /// </summary>
        /// <param name="value">初期値</param>
        public NumericKeyboard(string value)
        {
            Initialize(value);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

        }

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

        }

        /// <summary>
        /// 文字入力
        /// </summary>
        /// <param name="content">Button.Content</param>
        /// <returns>
        /// True  : 次フォーカスへ移動
        /// false : 通常処理
        /// </returns>
        public bool InputText(string content)
        {

            bool returnValue = false;

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

                    break;

                case "NEXT":
                    returnValue = true;
                    break;

                default:
                    ControlText(content);
                    break;

            }

            return returnValue;

        }

        /// <summary>
        /// Textbox.Textから選択文字列を削除し、指定文字列を挿入して返す
        /// </summary>
        /// <param name="insertString">挿入文字列</param>
        /// /// <returns>Textbox.Textから選択文字列を削除し指定文字列を挿入した文字列</returns>
        private void ControlText(string insertString = "")
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
