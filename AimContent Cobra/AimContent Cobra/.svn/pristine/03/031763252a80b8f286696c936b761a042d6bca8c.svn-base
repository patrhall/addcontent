using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AIM.Base.Classes;

namespace AimNews.Classes.LogHandler
{
    public class NewsLetterLogHandler : LogHandler
    {
        private int _newsLetterHistory_FK;
        private int _userID_FK;
        private string _mailAddress;

        public int NewsLetterHistory_FK
        {
            get
            {
                return _newsLetterHistory_FK;
            }
            set
            {
                _newsLetterHistory_FK = value;
            }
        }

        public int userID_FK
        {
            get
            {
                return _userID_FK;
            }
            set
            {
                _userID_FK = value;
            }
        }

        public string MailAddress
        {
            get
            {
                return _mailAddress;
            }
            set
            {
                _mailAddress = value;
            }
        }

        private AIM.Business.AimNews.DataObjectsDataContext db = new AIM.Business.AimNews.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        public override void Write()
        {

            AIM.Business.AimNews.AimNews_Log newLog = new AIM.Business.AimNews.AimNews_Log();

            
            newLog.HistoryID = _newsLetterHistory_FK;
            newLog.UserID = _userID_FK;
            newLog.MailAdress = _mailAddress;
            newLog.isError = _messageisset;
            newLog.ErrorMessage = _message;
            newLog.DateTime = DateTime.Now;

            db.AimNews_Logs.InsertOnSubmit(newLog);
            db.SubmitChanges();

            //LogHandlerTableAdapters.NewsLetterLogTableAdapter logAdapter = new LogHandlerTableAdapters.NewsLetterLogTableAdapter();
            //logAdapter.InsertLog(Convert.ToInt32(_newsLetterHistory_FK), _mailAddress, _messageisset, _message);
        }
    }
}
