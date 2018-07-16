using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Linq;
using AIM.Business.AimBusiness.Timberbridge.Website;
using System.IO;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AimBusiness.Customers.Timberbridge.Website
{
    public partial class Overview : AIM.Base.Modules.Page.AimBusiness.Timberbridge.Website.AimBusiness
    {

        protected override void Page_Load()
        {

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (fuExcel.HasFile)
            {
                saveCase();
            }

        }

        private void saveCase()
        {
            string date1 = txtYear.Text;
            string date2 = (Convert.ToInt32(txtYear.Text) - 100).ToString();


            string fileName = fuExcel.FileName;
            string path = Server.MapPath(config.customerpath + "/documents/");

            if (!File.Exists(path + fileName))
            {
                fuExcel.SaveAs(path + fileName);

                string excelSource = path + fileName;
                String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + excelSource + ";" + "Extended Properties=Excel 8.0;";

                OleDbConnection objConn = new OleDbConnection(sConnectionString);

                objConn.Open();

                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Data]", objConn);

                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

                objAdapter1.SelectCommand = objCmdSelect;

                DataSet objDataset1 = new DataSet();

                objAdapter1.Fill(objDataset1);

                objConn.Close();

                int countrysum1 = 0;
                int countrysum2 = 0;

                int sum1 = 0;
                int sum2 = 0;

                List<string> countries = new List<string>();

                //string[,] countryexport = new String[100, 3];

                foreach (DataTable thisTable in objDataset1.Tables)
                {

                    // For each row, get distinct countries
                    foreach (DataRow row in thisTable.Rows)
                    {

                        if (!countries.Contains(row[1].ToString()))
                            countries.Add(row[1].ToString());

                    }

                    int i = 0;

                    foreach (var country in countries) //for every country get total
                    {

                        countrysum1 = 0; //FirstYear sum
                        countrysum2 = 0; //SecondYear sum

                        sum1 = 0; //FirstYear tkr
                        sum2 = 0; //SecondYear tkr

                        foreach (DataRow row in thisTable.Rows)
                        {

                            if (row[1].ToString() == country)
                            {

                                if (row[4] != DBNull.Value) //FirstYear
                                {
                                    countrysum1 = countrysum1 + Convert.ToInt32(row[4]);
                                }
                                if (row[5] != DBNull.Value) //FirstYear Tkr
                                {
                                    sum1 = sum1 + Convert.ToInt32(row[5]);
                                }

                                if (row[7] != DBNull.Value) //SecondYear
                                {
                                    countrysum2 = countrysum2 + Convert.ToInt32(row[7]);
                                }
                                if (row[8] != DBNull.Value) //SecondYear Tkr
                                {
                                    sum2 = sum2 + Convert.ToInt32(row[8]);
                                }
                            }

                        }

                        Timberbridge_ExportStatistic article = new Timberbridge_ExportStatistic();
                        Timberbridge_ExportStatistic article2 = new Timberbridge_ExportStatistic();

                        article.Date = date1; //firstdate
                        article.Country = country; //country
                        article.Export = Convert.ToInt32(countrysum1.ToString()); //firstdate export stats
                        article.Tkr = Convert.ToInt32(sum1.ToString()); //firstdate tkr stats

                        db.Timberbridge_ExportStatistics.InsertOnSubmit(article);

                        article2.Date = date2; //seconddate
                        article2.Country = country; //country
                        article2.Export = Convert.ToInt32(countrysum2.ToString()); //seconddate export stats
                        article2.Tkr = Convert.ToInt32(sum2.ToString()); //seconddate export stats

                        db.Timberbridge_ExportStatistics.InsertOnSubmit(article2);

                        db.SubmitChanges();

                        //countryexport[i, 0] = country;
                        //countryexport[i, 1] = countrysum1.ToString();
                        //countryexport[i, 2] = countrysum2.ToString();
                        i++;
                    }

                }

            }

        }

    }
}