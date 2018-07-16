using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.UserControl
{
    public abstract class General : UserControlBase
    {
        protected AIM.Business.General.DataObjectsDataContext db = new AIM.Business.General.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);

        /// <summary>
        /// Usercontrol startup function, please notice zero parameters
        /// </summary>
        protected abstract override void Page_Load();

        /// <summary>
        /// Init specific properties
        /// </summary>
        protected override void InitModule()
        {
        }
    }
}
