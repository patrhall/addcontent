using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIM.Base.Modules.Page
{
    public abstract class WebSolution : PageBase
    {
        protected AIM.Business.WebSolution.DataObjectsDataContext db = new AIM.Business.WebSolution.DataObjectsDataContext(AIM.Business.DataManager.ConnectionString);        

        /// <summary>
        /// Web Form startup function, please notice zero parameters
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
