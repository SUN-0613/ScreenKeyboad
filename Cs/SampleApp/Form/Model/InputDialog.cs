using System;

namespace SampleApp.Form.Model
{

    /// <summary>
    /// InputDialog.Model
    /// </summary>
    public class InputDialog : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// テンキー1
        /// </summary>
        public string NumText1 = "";

        /// <summary>
        /// テンキー2
        /// </summary>
        public string NumText2 = "";

        /// <summary>
        /// 文字入力1
        /// </summary>
        public string StringText1 = "";

        /// <summary>
        /// 文字入力2
        /// </summary>
        public string StringText2 = "";

        #endregion

        /// <summary>
        /// InputDialog.Model
        /// </summary>
        public InputDialog()
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        { }

    }

}
