using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website
{
    public class Excel
    {
        public DataSet getDataSet(string file, string tabname)
        {
            //Standard converting from excel            
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
            //    "Data Source=" + file + ";Jet OLEDB:Engine Type=5;" +
            //    "Extended Properties=Excel 8.0;";

            //Standard converting from excel            
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                "Data Source=" + file + ";" +
                "Extended Properties=\"Excel 12.0;IMEX=1\"";

            OleDbConnection conn = new OleDbConnection(strConn);

            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [" + tabname + "$]", strConn);
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet);
            conn.Close();
            return myDataSet;
        }
    }
}
