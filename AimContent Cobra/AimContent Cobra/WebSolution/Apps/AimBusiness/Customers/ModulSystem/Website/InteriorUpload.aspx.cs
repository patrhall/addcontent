using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website;
using AIM.Business.AimBusiness.ModulSystem.Website;

namespace AimBusiness.Customers.ModulSystem.Website
{
    public partial class InteriorUpload : AIM.Base.Modules.Page.AimBusiness.ModulSystem.Website.AimBusiness
    {
        private const string ExcelSheetCarsName = "Vehicles";
        private const string ExcelSheetInteriorName = "Kit";

        private const string ColumnCar = "Vehicle Brand";
        private const string ColumnCarModel = "Vehicle Model";
        private const string ColumnAxle = "Wheelbase";
        private const string ColumnCarInterior = "Kit";
        private const string ColumnInteriorNumber = "Kit number";        
        private const string ColumnName = "Name";
        private const string ColumnFlikInredning = "Articles in kit";
        private const string ColumnFlikGolv = "Flooring and Lining";
        private const string ColumnFlikLast = "Roof rack system";
        private const string ColumnFlikAccessories = "Accessories";
        private const string ColumnDescription = "Description";
        private const string ColumnImage = "Image";
        private const string ColumnFittingInstruction = "Fitting instruction";
        private const string ColumnWidth = "Widht";
        private const string ColumnDepth = "Depth";
        private const string ColumnHeight = "Height";
        private const string ColumnWeight = "Weight";

        private string ImportFileName { get; set; }
        private DataTable DataCars { get; set; }
        private DataTable DataInteriors { get; set; }
        private int SelectedLanguageId
        {
            get
            {
                return Convert.ToInt32(ddSelectedLanguage.SelectedValue);
            }
        }
        private int FlikInredning { get; set; }
        private int FlikGolv { get; set; }
        private int FlikLast { get; set; }
        private int FlikAccesiories { get; set; }

        public class CarModelItem
        {
            public string Car { get; set; }
            public string CarModel { get; set; }
            public int CarModelId { get; set; }
        }
        
        private Dictionary<string, int> Cars { get; set; }
        private List<CarModelItem> CarModels { get; set; }
        private Dictionary<string, int> Interiors { get; set; }        

        private List<string> Log { get; set; }

        protected override void Page_Load()
        {
            lblMsg.Text = String.Empty;

            if (!IsPostBack)
                init();
        }

        private void init()
        {
            ddSelectedLanguage.DataSource = db.Languages.OrderBy(p => p.Language1);
            ddSelectedLanguage.DataValueField = "Id";
            ddSelectedLanguage.DataTextField = "Language1";
            ddSelectedLanguage.DataBind();
            ddSelectedLanguage.SelectedIndex = 0;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (!fuExcel.HasFile || Path.GetExtension(fuExcel.FileName) != ".xlsx")
            {
                lblMsg.Text = "The file uploaded needs to be a xlsx-file";
                return;
            }
            
            Cars = new Dictionary<string, int>();
            CarModels = new List<CarModelItem>();
            Interiors = new Dictionary<string, int>();            
            Log = new List<string>();

            FlikInredning = db.InteriorArticleTypes.Single(p => p.ShortName == "Interior").Id;
            FlikGolv = db.InteriorArticleTypes.Single(p => p.ShortName == "ClothingFloor").Id;
            FlikLast = db.InteriorArticleTypes.Single(p => p.ShortName == "LoadCarriers").Id;
            FlikAccesiories = db.InteriorArticleTypes.Single(p => p.ShortName == "Accessories").Id;

            saveFile();
            getData();
            clearDb();
            importData();

            if (Log.Count == 0)
                lblMsg.Text = "The import has finished with success";
            else
            {
                lblMsg.Text = "The import has finished, but with errors :<br />";

                foreach (var logItem in Log.Distinct())
                    lblMsg.Text += logItem + "<br />";
            }
        }

        private void clearDb()
        {
            db.Interior_X_Articles.DeleteAllOnSubmit(db.Interior_X_Articles.Where(p => p.Interior_FK.HasValue && p.Interior.Language_FK == SelectedLanguageId));
            db.Interior_X_Articles.DeleteAllOnSubmit(db.Interior_X_Articles.Where(p => p.CarAxle_FK.HasValue && p.CarAxle.CarModel.Car.Language_FK == SelectedLanguageId));
            db.Interior_X_CarAxles.DeleteAllOnSubmit(db.Interior_X_CarAxles.Where(p => p.Interior.Language_FK == SelectedLanguageId));
            db.Interiors.DeleteAllOnSubmit(db.Interiors.Where(p => p.Language_FK == SelectedLanguageId));            
            db.CarAxles.DeleteAllOnSubmit(db.CarAxles.Where(p => p.CarModel.Car.Language_FK == SelectedLanguageId));
            db.CarModels.DeleteAllOnSubmit(db.CarModels.Where(p => p.Car.Language_FK == SelectedLanguageId));
            db.Cars.DeleteAllOnSubmit(db.Cars.Where(p => p.Language_FK == SelectedLanguageId));

            db.SubmitChanges();
        }

        private void saveFile()
        {
            ImportFileName = Server.MapPath("~/importinterior.xlsx");

            if (File.Exists(ImportFileName))
                File.Delete(ImportFileName);

            fuExcel.PostedFile.SaveAs(ImportFileName);
        }
        private void getData()
        {
            Excel excel = new Excel();
            DataCars = excel.getDataSet(ImportFileName, ExcelSheetCarsName).Tables[0];
            DataInteriors = excel.getDataSet(ImportFileName, ExcelSheetInteriorName).Tables[0];            
        }

        private void importData()
        {
            int count = 1;

            foreach (DataRow d in DataInteriors.Rows)
            {
                try
                {
                    if (d[ColumnInteriorNumber].ToString().Trim() == "")
                        continue;                    

                    var interior = new AIM.Business.AimBusiness.ModulSystem.Website.Interior();
                    interior.Language_FK = SelectedLanguageId;
                    interior.InteriorNumber = d[ColumnInteriorNumber].ToString();
                    interior.Name = d[ColumnName].ToString();
                    interior.Description = d[ColumnDescription].ToString();
                    interior.Width = d[ColumnWidth].ToString();
                    interior.Height = d[ColumnHeight].ToString();
                    interior.Depth = d[ColumnDepth].ToString();
                    interior.Weight = d[ColumnWeight].ToString();
                    interior.FittingInstructions = d[ColumnFittingInstruction].ToString();
                    interior.Image = d[ColumnImage].ToString();
                    interior.SortOrder = count++;

                    db.Interiors.InsertOnSubmit(interior);

                    db.SubmitChanges();

                    Interiors.Add(d[ColumnInteriorNumber].ToString(), interior.Id);                    

                    if (d[ColumnFlikInredning].ToString().Contains("="))
                        addFlik(d[ColumnFlikInredning].ToString(), FlikInredning, interior.Id, null);

                    if (d[ColumnFlikAccessories].ToString().Contains("="))
                        addFlik(d[ColumnFlikAccessories].ToString(), FlikAccesiories, interior.Id, null);
                }
                catch 
                {                    
                    log("Interior " + d[ColumnInteriorNumber].ToString() + ": The interior was not added");
                }
            }

            foreach (DataRow d in DataCars.Rows)
            {
                try
                {
                    if (d[ColumnAxle].ToString().Trim() == "")
                        continue;     

                    int carId, carModelId;

                    if (Cars.ContainsKey(d[ColumnCar].ToString()))
                        carId = Cars[d[ColumnCar].ToString()];
                    else
                    {
                        var carDb = new AIM.Business.AimBusiness.ModulSystem.Website.Car();
                        carDb.Name = d[ColumnCar].ToString();
                        carDb.Language_FK = SelectedLanguageId;
                        db.Cars.InsertOnSubmit(carDb);
                        db.SubmitChanges();

                        Cars.Add(d[ColumnCar].ToString(), carDb.Id);
                        carId = carDb.Id;
                    }

                    if (CarModels.Any(p => p.Car == d[ColumnCar].ToString() && p.CarModel == d[ColumnCarModel].ToString()))
                        carModelId = CarModels.Single(p => p.Car == d[ColumnCar].ToString() && p.CarModel == d[ColumnCarModel].ToString()).CarModelId;
                    else
                    {
                        var carModelDb = new AIM.Business.AimBusiness.ModulSystem.Website.CarModel();
                        carModelDb.Car_FK = carId;
                        carModelDb.Name = d[ColumnCarModel].ToString();

                        db.CarModels.InsertOnSubmit(carModelDb);
                        db.SubmitChanges();

                        CarModels.Add(new CarModelItem() { CarModelId = carModelDb.Id, CarModel = d[ColumnCarModel].ToString(), Car = d[ColumnCar].ToString() });
                        carModelId = carModelDb.Id;
                    }

                    var carAxle = new AIM.Business.AimBusiness.ModulSystem.Website.CarAxle();
                    carAxle.CarModel_FK = carModelId;
                    carAxle.Name = d[ColumnAxle].ToString();

                    db.CarAxles.InsertOnSubmit(carAxle);
                    db.SubmitChanges();

                    if (d[ColumnCarInterior].ToString().Contains(","))
                    {
                        int sort = 0;
                        foreach (var item in d[ColumnCarInterior].ToString().Split(new char[] { ',' }))
                        { 
                            addCarInterior(item, carAxle.Id, sort);
                            sort++;
                        }
                    }
                    else
                        addCarInterior(d[ColumnCarInterior].ToString(), carAxle.Id, 0);

                    if (d[ColumnFlikGolv].ToString().Contains("="))
                        addFlik(d[ColumnFlikGolv].ToString(), FlikGolv, null, carAxle.Id);

                    if (d[ColumnFlikLast].ToString().Contains("="))
                        addFlik(d[ColumnFlikLast].ToString(), FlikLast, null, carAxle.Id);
                }
                catch
                {                    
                    log("CarAxle " + d[ColumnAxle].ToString() + ": The caraxle was not added");
                }
            }

        }

        private void addCarInterior(string columnData, int carAxleId, int sort)
        {
            if (!Interiors.ContainsKey(columnData))
            {
                log(columnData + " does not exist!");
                return;
            }

            var x = new AIM.Business.AimBusiness.ModulSystem.Website.Interior_X_CarAxle();
            x.Interior_FK = Interiors[columnData];
            x.CarAxle_FK = carAxleId;
            x.Sort = sort;
            db.Interior_X_CarAxles.InsertOnSubmit(x);
            db.SubmitChanges();
        }


        private void addFlik(string columnData, int articleTypeId, int? interiorId, int? carAxleId)
        {
            if (columnData.Contains(","))
            {
                var items = columnData.Split(new char[] { ',' });

                foreach (var item in items)
                    addFlikItem(item, articleTypeId, interiorId, carAxleId);
            }
            else
                addFlikItem(columnData, articleTypeId, interiorId, carAxleId);
        }

        private void addFlikItem(string item, int articleTypeId, int? interiorId, int? carAxleId)
        {
            var items = item.Split(new char[] { '=' });

            var dbItem = new AIM.Business.AimBusiness.ModulSystem.Website.Interior_X_Article();
            dbItem.Interior_FK = interiorId;
            dbItem.CarAxle_FK = carAxleId;            
            dbItem.InteriorArticleType_FK = articleTypeId;
            dbItem.ArticleNumber = items[0];
            dbItem.NumberOfArticles = Convert.ToInt32(items[1]);

            db.Interior_X_Articles.InsertOnSubmit(dbItem);
            db.SubmitChanges();
        }


        private void log(string str)
        {
            Log.Add(str);
        }
    }
}