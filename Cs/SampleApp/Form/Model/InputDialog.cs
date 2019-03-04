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
        /// 項目1
        /// </summary>
        public string NumText1 = "";

        /// <summary>
        /// 項目2
        /// </summary>
        public string NumText2 = "";

        #endregion

        /// <summary>
        /// InputDialog.Model
        /// </summary>
        public InputDialog()
        {

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

        }

    }

}
