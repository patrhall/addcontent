using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Profile;
using System.Data.Linq;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using AIM.General.UserControls;
using Telerik.Web.UI;

namespace AimNews
{
    public partial class EditUser : AIM.Base.Modules.Page.AimNews
    {   
        int userID;
        int groupID;

        protected override void Page_Load()
        {

            //  AIM.Base.Classes.RadHandler.RadEditor_LoadEditorPreference(ref editor1, config);

            initPagePreference();
            initLanguage();
            initStatus();

            if (!IsPostBack)
            {
                if (Request["ID"] != null && Request["ID"] != "0")
                    getUserData();

            }
        }

        private void initPagePreference()
        {

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Firstname", "used")))
            {
                lblFirstname.Visible = true;
                tbFirstname.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Firstnamevalidate", "used")))
                rfvFirstname.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Lastname", "used")))
            {
                lblLastName.Visible = true;
                tbLastName.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Lastnamevalidate", "used")))
                rfvLastname.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Company", "used")))
            {
                lblCompany.Visible = true;
                tbCompany.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Companyvalidate", "used")))
                rfvCompany.Visible = true;
            
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Telephone", "used")))
            {               
                lblTelephone.Visible = true;
                tbTelephone.Visible = true;
            }            
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Telephonevalidate", "used")))
                rfvTelephone.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Mobile", "used")))
            {
                lblMobile.Visible = true;
                tbMobile.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Mobilevalidate", "used")))
                rfvMobile.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Fax", "used")))
            {
                lblFax.Visible = true;
                tbFax.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Faxvalidate", "used")))
                rfvFax.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Address", "used")))
            {
                lblAddress.Visible = true;
                tbAddress.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Addressvalidate", "used")))
                rfvAddress.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Postal", "used")))
            {
                lblPostal.Visible = true;
                tbPostal.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Postalvalidate", "used")))
                rfvPostal.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_City", "used")))
            {
                lblCity.Visible = true;
                tbCity.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Cityvalidate", "used")))
                rfvCity.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Country", "used")))
            {
                lblCountry.Visible = true;
                tbCountry.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Countryvalidate", "used")))
                rfvCountry.Visible = true;

            if (Boolean.Parse(Preference.getValue("aimnews", "user_Orgnr", "used")))
            {
                lblOrgnr.Visible = true;
                tbOrgnr.Visible = true;
            }
            if (Boolean.Parse(Preference.getValue("aimnews", "user_Orgnrvalidate", "used")))
                rfvOrgnr.Visible = true;
        }

        private void initStatus()
        {
            if (Request["S"] != null)
            {
                switch (Request["S"].ToString())
                {
                    case "0":
                        lblStatus.Text = Language.getValue("userCreatedStatus");
                        lblStatus.Visible = true;
                        break;
                    case "1":
                        lblStatus.Text = Language.getValue("userSavedStatus");
                        lblStatus.Visible = true;
                        break;
                }
            }
        }

        private void initLanguage()
        {
            lblFirstname.Text = Language.getValue("FirstnameLabelText");
            lblLastName.Text = Language.getValue("LastnameLabelText");
            lblEmail.Text = Language.getValue("EmailLabelText");
            lblCompany.Text = Language.getValue("CompanyLabelText");
            lblTelephone.Text = Language.getValue("TelephoneLabelText");
            lblMobile.Text = Language.getValue("MobileLabelText");
            lblFax.Text = Language.getValue("FaxLabelText");
            lblAddress.Text = Language.getValue("AddressLabelText");
            lblPostal.Text = Language.getValue("PostalLabelText");
            lblCity.Text = Language.getValue("CityLabelText");
            lblCountry.Text = Language.getValue("CountryLabelText");
            lblOrgnr.Text = Language.getValue("OrgnrLabelText");
            
            rfvFirstname.Text = Language.getValue("FirstnameValidatorText");
            rfvLastname.Text  = Language.getValue("LastnameValidatorText");
            rfvEmail.Text = Language.getValue("EmailValidatorText");
            revEmail.Text = Language.getValue("EmailExpressionText");
            rfvCompany.Text = Language.getValue("CompanyValidatorText");
            rfvTelephone.Text = Language.getValue("TelephoneValidatorText");
            rfvMobile.Text = Language.getValue("MobileValidatorText");
            rfvFax.Text = Language.getValue("FaxValidatorText");
            rfvAddress.Text = Language.getValue("AddressValidatorText");
            rfvPostal.Text = Language.getValue("PostalValidatorText");
            rfvCity.Text = Language.getValue("CityValidatorText");
            rfvCountry.Text = Language.getValue("CountryValidatorText");
            rfvOrgnr.Text = Language.getValue("OrgnrValidatorText");
           
            btnSave.Text = Language.getValue("savebuttonText");
        }

        private void getUserData()
        {
            userID = Convert.ToInt32(Request["ID"].ToString());
            AIM.Business.AimNews.AimNews_User theUser = (from linq_user in db.AimNews_Users
                                                          where linq_user.ID == userID
                                                          select linq_user).First<AIM.Business.AimNews.AimNews_User>();

            tbFirstname.Text = theUser.FirstName;
            tbLastName.Text = theUser.LastName;
            tbEmail.Text = theUser.Email;
            tbCompany.Text = theUser.Company;
            tbTelephone.Text = theUser.Telephone;
            tbMobile.Text = theUser.Mobile;
            tbFax.Text = theUser.Fax;
            tbAddress.Text = theUser.Address;
            tbPostal.Text = theUser.Postal;
            tbCity.Text = theUser.City;
            tbCountry.Text = theUser.Country;
            tbOrgnr.Text = theUser.Orgnr;
            
        }

        private void createNewUser()
        {
            AIM.Business.AimNews.AimNews_User newUser = new AIM.Business.AimNews.AimNews_User();

            newUser.AimNewsID = AimNewsID;
            newUser.FirstName = tbFirstname.Text;
            newUser.LastName = tbLastName.Text;
            newUser.Email = tbEmail.Text;
            newUser.Company = tbCompany.Text;
            newUser.Telephone = tbTelephone.Text;
            newUser.Mobile = tbMobile.Text;
            newUser.Fax = tbFax.Text;
            newUser.Address = tbAddress.Text;
            newUser.Postal = tbPostal.Text;
            newUser.City = tbCity.Text;
            newUser.Country = tbCountry.Text;
            newUser.Orgnr = tbOrgnr.Text;

            db.AimNews_Users.InsertOnSubmit(newUser);
            db.SubmitChanges();

            if (Request["gID"] != null && Request["gID"] != "0")
            {
                groupID = int.Parse(Request["gID"].ToString());
                AIM.Business.AimNews.AimNews_GroupUser newGroupUser = new AIM.Business.AimNews.AimNews_GroupUser();

                newGroupUser.GroupID = groupID;
                newGroupUser.UserID = newUser.ID;
                db.AimNews_GroupUsers.InsertOnSubmit(newGroupUser);
                db.SubmitChanges();

                Response.Redirect("EditUser.aspx?gID=" + groupID + "&ID=0&S=0");
            }
        }

        private void saveUser()
        {
            userID = Convert.ToInt32(Request["ID"].ToString());
            AIM.Business.AimNews.AimNews_User theUser = (from linq_user in db.AimNews_Users
                                                           where linq_user.ID == userID
                                                           select linq_user).First <AIM.Business.AimNews.AimNews_User>();
            theUser.FirstName = tbFirstname.Text;
            theUser.LastName = tbLastName.Text;
            theUser.Email = tbEmail.Text;
            theUser.Company = tbCompany.Text;
            theUser.Telephone = tbTelephone.Text;
            theUser.Mobile = tbMobile.Text;
            theUser.Fax = tbFax.Text;
            theUser.Address = tbAddress.Text;
            theUser.Postal = tbPostal.Text;
            theUser.City = tbCity.Text;
            theUser.Country = tbCountry.Text;
            theUser.Orgnr = tbOrgnr.Text;

            db.SubmitChanges();

            if (Request["gID"] != null && Request["gID"] != "0")
                Response.Redirect("EditUser.aspx?gID=" + Request["gID"].ToString() + "&ID=" + userID + "&S=1");

        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Request["ID"] != null && Request["ID"].ToString() != "0")
                saveUser();
            else
                createNewUser();
        }

        protected void ibBack_OnClick(object sender, System.EventArgs e)
        {
            if (Request["gID"] != null && Request["gID"] != "0")
            {
                groupID = int.Parse(Request["gID"].ToString());
                Response.Redirect("EditGroup.aspx?ID=" + groupID);
            }
            else
                Response.Redirect("Groups.aspx");
        }
   
    }
}
