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
        /// テンキー1プロパティ
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
        /// テンキー2プロパティ
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

        /// <summary>
        /// 文字入力1プロパティ
        /// </summary>
        public string StringText1
        {
            get { return _Model.StringText1; }
            set
            {
                if (!_Model.StringText1.Equals(value))
                {
                    _Model.StringText1 = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 文字入力2プロパティ
        /// </summary>
        public string StringText2
        {
            get { return _Model.StringText2; }
            set
            {
                if (!_Model.StringText2.Equals(value))
                {
                    _Model.StringText2 = value;
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
