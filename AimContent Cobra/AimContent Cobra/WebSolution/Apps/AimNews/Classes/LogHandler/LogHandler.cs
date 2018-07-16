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

namespace AimNews.Classes.LogHandler
{
    public abstract class LogHandler
    {
        protected string _message;
        protected bool _messageisset = false;
        protected string _errorMessage;


        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                _messageisset = true;
            }
        }

        public abstract void Write();
    }
}
