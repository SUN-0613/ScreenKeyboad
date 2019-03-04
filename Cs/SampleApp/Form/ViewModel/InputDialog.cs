using AYam.Common.ViewModel;
using System;

namespace SampleApp.Form.ViewModel
{

    /// <summary>
    /// スクリーンキーボードサンプルApp
    /// </summary>
    public class InputDialog : VMBase, IDisposable
    {

        #region Property

        /// <summary>
        /// 項目1
        /// </summary>
        public string NumText1
        {
            get { return _Model.NumText1; }
            set
            {
                if (!_Model.NumText1.Equals(value))
                {
                    _Model.NumText1 = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 項目2
        /// </summary>
        public string NumText2
        {
            get { return _Model.NumText2; }
            set
            {
                if (!_Model.NumText2.Equals(value))
                {
                    _Model.NumText2 = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        /// <summary>
        /// InputDialog.Model
        /// </summary>
        private Model.InputDialog _Model;

        /// <summary>
        /// InputDialog.ViewModel
        /// </summary>
        public InputDialog()
        {
            _Model = new Model.InputDialog();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _Model.Dispose();
        }

    }

}
