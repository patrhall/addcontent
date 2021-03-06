﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIM.Business.AimStats
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Aimit_Aim")]
	public partial class DataObjectsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertConfig(Config instance);
    partial void UpdateConfig(Config instance);
    partial void DeleteConfig(Config instance);
    partial void InsertSite(Site instance);
    partial void UpdateSite(Site instance);
    partial void DeleteSite(Site instance);
    #endregion
		
		public DataObjectsDataContext() : 
				base(global::AIM.Business.Properties.Settings.Default.Aimit_AimConnectionString7, mappingSource)
		{
			OnCreated();
		}
		
		public DataObjectsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataObjectsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataObjectsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataObjectsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Config> Configs
		{
			get
			{
				return this.GetTable<Config>();
			}
		}
		
		public System.Data.Linq.Table<Site> Sites
		{
			get
			{
				return this.GetTable<Site>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Config")]
	public partial class Config : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _siteurl;
		
		private string _customerpath;
		
		private string _cssfile;
		
		private string _cssfile_editor;
		
		private string _cssfile_public;
		
		private string _frommail;
		
		private string _mailserver;
		
		private string _mailserverlogin;
		
		private string _mailserverpassword;
		
		private string _documentfilters;
		
		private string _imagefilters;
		
		private string _editorscheme;
		
		private System.Nullable<int> _maxuploadbytesize;
		
		private string _imagespathstostrip;
		
		private string _anchorpathstostrip;
		
		private System.Nullable<int> _startpageID;
		
		private System.Nullable<int> _rootID;
		
		private System.Nullable<int> _lcid;
		
		private string _applicationname;
		
		private string _objectpath;
		
		private string _filearchivepath;
		
		private string _aimbusinesspath;
		
		private string _aimstatspath;
		
		private System.Nullable<int> _visitors;
		
		private System.Nullable<bool> _showContentDropDown;
		
		private string _liveStats;
		
		private string _aimphotopath;
		
		private string _secureEditing;
		
		private System.Nullable<bool> _useJournal;
		
		private int _objectTrashedNumOfDays;
		
		private string _productregisterpath;
		
		private string _backgroundcolor;
		
		private string _publicurl;
		
		private bool _useAdminRoles;
		
		private EntitySet<Site> _Sites;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnsiteurlChanging(string value);
    partial void OnsiteurlChanged();
    partial void OncustomerpathChanging(string value);
    partial void OncustomerpathChanged();
    partial void OncssfileChanging(string value);
    partial void OncssfileChanged();
    partial void Oncssfile_editorChanging(string value);
    partial void Oncssfile_editorChanged();
    partial void Oncssfile_publicChanging(string value);
    partial void Oncssfile_publicChanged();
    partial void OnfrommailChanging(string value);
    partial void OnfrommailChanged();
    partial void OnmailserverChanging(string value);
    partial void OnmailserverChanged();
    partial void OnmailserverloginChanging(string value);
    partial void OnmailserverloginChanged();
    partial void OnmailserverpasswordChanging(string value);
    partial void OnmailserverpasswordChanged();
    partial void OndocumentfiltersChanging(string value);
    partial void OndocumentfiltersChanged();
    partial void OnimagefiltersChanging(string value);
    partial void OnimagefiltersChanged();
    partial void OneditorschemeChanging(string value);
    partial void OneditorschemeChanged();
    partial void OnmaxuploadbytesizeChanging(System.Nullable<int> value);
    partial void OnmaxuploadbytesizeChanged();
    partial void OnimagespathstostripChanging(string value);
    partial void OnimagespathstostripChanged();
    partial void OnanchorpathstostripChanging(string value);
    partial void OnanchorpathstostripChanged();
    partial void OnstartpageIDChanging(System.Nullable<int> value);
    partial void OnstartpageIDChanged();
    partial void OnrootIDChanging(System.Nullable<int> value);
    partial void OnrootIDChanged();
    partial void OnlcidChanging(System.Nullable<int> value);
    partial void OnlcidChanged();
    partial void OnapplicationnameChanging(string value);
    partial void OnapplicationnameChanged();
    partial void OnobjectpathChanging(string value);
    partial void OnobjectpathChanged();
    partial void OnfilearchivepathChanging(string value);
    partial void OnfilearchivepathChanged();
    partial void OnaimbusinesspathChanging(string value);
    partial void OnaimbusinesspathChanged();
    partial void OnaimstatspathChanging(string value);
    partial void OnaimstatspathChanged();
    partial void OnvisitorsChanging(System.Nullable<int> value);
    partial void OnvisitorsChanged();
    partial void OnshowContentDropDownChanging(System.Nullable<bool> value);
    partial void OnshowContentDropDownChanged();
    partial void OnliveStatsChanging(string value);
    partial void OnliveStatsChanged();
    partial void OnaimphotopathChanging(string value);
    partial void OnaimphotopathChanged();
    partial void OnsecureEditingChanging(string value);
    partial void OnsecureEditingChanged();
    partial void OnuseJournalChanging(System.Nullable<bool> value);
    partial void OnuseJournalChanged();
    partial void OnobjectTrashedNumOfDaysChanging(int value);
    partial void OnobjectTrashedNumOfDaysChanged();
    partial void OnproductregisterpathChanging(string value);
    partial void OnproductregisterpathChanged();
    partial void OnbackgroundcolorChanging(string value);
    partial void OnbackgroundcolorChanged();
    partial void OnpublicurlChanging(string value);
    partial void OnpublicurlChanged();
    partial void OnuseAdminRolesChanging(bool value);
    partial void OnuseAdminRolesChanged();
    #endregion
		
		public Config()
		{
			this._Sites = new EntitySet<Site>(new Action<Site>(this.attach_Sites), new Action<Site>(this.detach_Sites));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_siteurl", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string siteurl
		{
			get
			{
				return this._siteurl;
			}
			set
			{
				if ((this._siteurl != value))
				{
					this.OnsiteurlChanging(value);
					this.SendPropertyChanging();
					this._siteurl = value;
					this.SendPropertyChanged("siteurl");
					this.OnsiteurlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_customerpath", DbType="NVarChar(100)")]
		public string customerpath
		{
			get
			{
				return this._customerpath;
			}
			set
			{
				if ((this._customerpath != value))
				{
					this.OncustomerpathChanging(value);
					this.SendPropertyChanging();
					this._customerpath = value;
					this.SendPropertyChanged("customerpath");
					this.OncustomerpathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cssfile", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string cssfile
		{
			get
			{
				return this._cssfile;
			}
			set
			{
				if ((this._cssfile != value))
				{
					this.OncssfileChanging(value);
					this.SendPropertyChanging();
					this._cssfile = value;
					this.SendPropertyChanged("cssfile");
					this.OncssfileChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cssfile_editor", DbType="NVarChar(100)")]
		public string cssfile_editor
		{
			get
			{
				return this._cssfile_editor;
			}
			set
			{
				if ((this._cssfile_editor != value))
				{
					this.Oncssfile_editorChanging(value);
					this.SendPropertyChanging();
					this._cssfile_editor = value;
					this.SendPropertyChanged("cssfile_editor");
					this.Oncssfile_editorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cssfile_public", DbType="NVarChar(100)")]
		public string cssfile_public
		{
			get
			{
				return this._cssfile_public;
			}
			set
			{
				if ((this._cssfile_public != value))
				{
					this.Oncssfile_publicChanging(value);
					this.SendPropertyChanging();
					this._cssfile_public = value;
					this.SendPropertyChanged("cssfile_public");
					this.Oncssfile_publicChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_frommail", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string frommail
		{
			get
			{
				return this._frommail;
			}
			set
			{
				if ((this._frommail != value))
				{
					this.OnfrommailChanging(value);
					this.SendPropertyChanging();
					this._frommail = value;
					this.SendPropertyChanged("frommail");
					this.OnfrommailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_mailserver", DbType="NVarChar(100)")]
		public string mailserver
		{
			get
			{
				return this._mailserver;
			}
			set
			{
				if ((this._mailserver != value))
				{
					this.OnmailserverChanging(value);
					this.SendPropertyChanging();
					this._mailserver = value;
					this.SendPropertyChanged("mailserver");
					this.OnmailserverChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_mailserverlogin", DbType="NVarChar(50)")]
		public string mailserverlogin
		{
			get
			{
				return this._mailserverlogin;
			}
			set
			{
				if ((this._mailserverlogin != value))
				{
					this.OnmailserverloginChanging(value);
					this.SendPropertyChanging();
					this._mailserverlogin = value;
					this.SendPropertyChanged("mailserverlogin");
					this.OnmailserverloginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_mailserverpassword", DbType="NVarChar(50)")]
		public string mailserverpassword
		{
			get
			{
				return this._mailserverpassword;
			}
			set
			{
				if ((this._mailserverpassword != value))
				{
					this.OnmailserverpasswordChanging(value);
					this.SendPropertyChanging();
					this._mailserverpassword = value;
					this.SendPropertyChanged("mailserverpassword");
					this.OnmailserverpasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_documentfilters", DbType="NVarChar(100)")]
		public string documentfilters
		{
			get
			{
				return this._documentfilters;
			}
			set
			{
				if ((this._documentfilters != value))
				{
					this.OndocumentfiltersChanging(value);
					this.SendPropertyChanging();
					this._documentfilters = value;
					this.SendPropertyChanged("documentfilters");
					this.OndocumentfiltersChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_imagefilters", DbType="NVarChar(100)")]
		public string imagefilters
		{
			get
			{
				return this._imagefilters;
			}
			set
			{
				if ((this._imagefilters != value))
				{
					this.OnimagefiltersChanging(value);
					this.SendPropertyChanging();
					this._imagefilters = value;
					this.SendPropertyChanged("imagefilters");
					this.OnimagefiltersChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_editorscheme", DbType="NVarChar(50)")]
		public string editorscheme
		{
			get
			{
				return this._editorscheme;
			}
			set
			{
				if ((this._editorscheme != value))
				{
					this.OneditorschemeChanging(value);
					this.SendPropertyChanging();
					this._editorscheme = value;
					this.SendPropertyChanged("editorscheme");
					this.OneditorschemeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_maxuploadbytesize", DbType="Int")]
		public System.Nullable<int> maxuploadbytesize
		{
			get
			{
				return this._maxuploadbytesize;
			}
			set
			{
				if ((this._maxuploadbytesize != value))
				{
					this.OnmaxuploadbytesizeChanging(value);
					this.SendPropertyChanging();
					this._maxuploadbytesize = value;
					this.SendPropertyChanged("maxuploadbytesize");
					this.OnmaxuploadbytesizeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_imagespathstostrip", DbType="NVarChar(200)")]
		public string imagespathstostrip
		{
			get
			{
				return this._imagespathstostrip;
			}
			set
			{
				if ((this._imagespathstostrip != value))
				{
					this.OnimagespathstostripChanging(value);
					this.SendPropertyChanging();
					this._imagespathstostrip = value;
					this.SendPropertyChanged("imagespathstostrip");
					this.OnimagespathstostripChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_anchorpathstostrip", DbType="NVarChar(200)")]
		public string anchorpathstostrip
		{
			get
			{
				return this._anchorpathstostrip;
			}
			set
			{
				if ((this._anchorpathstostrip != value))
				{
					this.OnanchorpathstostripChanging(value);
					this.SendPropertyChanging();
					this._anchorpathstostrip = value;
					this.SendPropertyChanged("anchorpathstostrip");
					this.OnanchorpathstostripChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_startpageID", DbType="Int")]
		public System.Nullable<int> startpageID
		{
			get
			{
				return this._startpageID;
			}
			set
			{
				if ((this._startpageID != value))
				{
					this.OnstartpageIDChanging(value);
					this.SendPropertyChanging();
					this._startpageID = value;
					this.SendPropertyChanged("startpageID");
					this.OnstartpageIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rootID", DbType="Int")]
		public System.Nullable<int> rootID
		{
			get
			{
				return this._rootID;
			}
			set
			{
				if ((this._rootID != value))
				{
					this.OnrootIDChanging(value);
					this.SendPropertyChanging();
					this._rootID = value;
					this.SendPropertyChanged("rootID");
					this.OnrootIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lcid", DbType="Int")]
		public System.Nullable<int> lcid
		{
			get
			{
				return this._lcid;
			}
			set
			{
				if ((this._lcid != value))
				{
					this.OnlcidChanging(value);
					this.SendPropertyChanging();
					this._lcid = value;
					this.SendPropertyChanged("lcid");
					this.OnlcidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_applicationname", DbType="NVarChar(100)")]
		public string applicationname
		{
			get
			{
				return this._applicationname;
			}
			set
			{
				if ((this._applicationname != value))
				{
					this.OnapplicationnameChanging(value);
					this.SendPropertyChanging();
					this._applicationname = value;
					this.SendPropertyChanged("applicationname");
					this.OnapplicationnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_objectpath", DbType="NVarChar(255)")]
		public string objectpath
		{
			get
			{
				return this._objectpath;
			}
			set
			{
				if ((this._objectpath != value))
				{
					this.OnobjectpathChanging(value);
					this.SendPropertyChanging();
					this._objectpath = value;
					this.SendPropertyChanged("objectpath");
					this.OnobjectpathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_filearchivepath", DbType="NVarChar(255)")]
		public string filearchivepath
		{
			get
			{
				return this._filearchivepath;
			}
			set
			{
				if ((this._filearchivepath != value))
				{
					this.OnfilearchivepathChanging(value);
					this.SendPropertyChanging();
					this._filearchivepath = value;
					this.SendPropertyChanged("filearchivepath");
					this.OnfilearchivepathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_aimbusinesspath", DbType="NVarChar(255)")]
		public string aimbusinesspath
		{
			get
			{
				return this._aimbusinesspath;
			}
			set
			{
				if ((this._aimbusinesspath != value))
				{
					this.OnaimbusinesspathChanging(value);
					this.SendPropertyChanging();
					this._aimbusinesspath = value;
					this.SendPropertyChanged("aimbusinesspath");
					this.OnaimbusinesspathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_aimstatspath", DbType="NVarChar(255)")]
		public string aimstatspath
		{
			get
			{
				return this._aimstatspath;
			}
			set
			{
				if ((this._aimstatspath != value))
				{
					this.OnaimstatspathChanging(value);
					this.SendPropertyChanging();
					this._aimstatspath = value;
					this.SendPropertyChanged("aimstatspath");
					this.OnaimstatspathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_visitors", DbType="Int")]
		public System.Nullable<int> visitors
		{
			get
			{
				return this._visitors;
			}
			set
			{
				if ((this._visitors != value))
				{
					this.OnvisitorsChanging(value);
					this.SendPropertyChanging();
					this._visitors = value;
					this.SendPropertyChanged("visitors");
					this.OnvisitorsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_showContentDropDown", DbType="Bit")]
		public System.Nullable<bool> showContentDropDown
		{
			get
			{
				return this._showContentDropDown;
			}
			set
			{
				if ((this._showContentDropDown != value))
				{
					this.OnshowContentDropDownChanging(value);
					this.SendPropertyChanging();
					this._showContentDropDown = value;
					this.SendPropertyChanged("showContentDropDown");
					this.OnshowContentDropDownChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_liveStats", DbType="NVarChar(255)")]
		public string liveStats
		{
			get
			{
				return this._liveStats;
			}
			set
			{
				if ((this._liveStats != value))
				{
					this.OnliveStatsChanging(value);
					this.SendPropertyChanging();
					this._liveStats = value;
					this.SendPropertyChanged("liveStats");
					this.OnliveStatsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_aimphotopath", DbType="NVarChar(255)")]
		public string aimphotopath
		{
			get
			{
				return this._aimphotopath;
			}
			set
			{
				if ((this._aimphotopath != value))
				{
					this.OnaimphotopathChanging(value);
					this.SendPropertyChanging();
					this._aimphotopath = value;
					this.SendPropertyChanged("aimphotopath");
					this.OnaimphotopathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_secureEditing", DbType="NVarChar(255)")]
		public string secureEditing
		{
			get
			{
				return this._secureEditing;
			}
			set
			{
				if ((this._secureEditing != value))
				{
					this.OnsecureEditingChanging(value);
					this.SendPropertyChanging();
					this._secureEditing = value;
					this.SendPropertyChanged("secureEditing");
					this.OnsecureEditingChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_useJournal", DbType="Bit")]
		public System.Nullable<bool> useJournal
		{
			get
			{
				return this._useJournal;
			}
			set
			{
				if ((this._useJournal != value))
				{
					this.OnuseJournalChanging(value);
					this.SendPropertyChanging();
					this._useJournal = value;
					this.SendPropertyChanged("useJournal");
					this.OnuseJournalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_objectTrashedNumOfDays", DbType="Int NOT NULL")]
		public int objectTrashedNumOfDays
		{
			get
			{
				return this._objectTrashedNumOfDays;
			}
			set
			{
				if ((this._objectTrashedNumOfDays != value))
				{
					this.OnobjectTrashedNumOfDaysChanging(value);
					this.SendPropertyChanging();
					this._objectTrashedNumOfDays = value;
					this.SendPropertyChanged("objectTrashedNumOfDays");
					this.OnobjectTrashedNumOfDaysChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_productregisterpath", DbType="NVarChar(255)")]
		public string productregisterpath
		{
			get
			{
				return this._productregisterpath;
			}
			set
			{
				if ((this._productregisterpath != value))
				{
					this.OnproductregisterpathChanging(value);
					this.SendPropertyChanging();
					this._productregisterpath = value;
					this.SendPropertyChanged("productregisterpath");
					this.OnproductregisterpathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_backgroundcolor", DbType="NVarChar(7)")]
		public string backgroundcolor
		{
			get
			{
				return this._backgroundcolor;
			}
			set
			{
				if ((this._backgroundcolor != value))
				{
					this.OnbackgroundcolorChanging(value);
					this.SendPropertyChanging();
					this._backgroundcolor = value;
					this.SendPropertyChanged("backgroundcolor");
					this.OnbackgroundcolorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_publicurl", DbType="NVarChar(255)")]
		public string publicurl
		{
			get
			{
				return this._publicurl;
			}
			set
			{
				if ((this._publicurl != value))
				{
					this.OnpublicurlChanging(value);
					this.SendPropertyChanging();
					this._publicurl = value;
					this.SendPropertyChanged("publicurl");
					this.OnpublicurlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_useAdminRoles", DbType="Bit NOT NULL")]
		public bool useAdminRoles
		{
			get
			{
				return this._useAdminRoles;
			}
			set
			{
				if ((this._useAdminRoles != value))
				{
					this.OnuseAdminRolesChanging(value);
					this.SendPropertyChanging();
					this._useAdminRoles = value;
					this.SendPropertyChanged("useAdminRoles");
					this.OnuseAdminRolesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Config_Site", Storage="_Sites", ThisKey="ID", OtherKey="ConfigID")]
		public EntitySet<Site> Sites
		{
			get
			{
				return this._Sites;
			}
			set
			{
				this._Sites.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Sites(Site entity)
		{
			this.SendPropertyChanging();
			entity.Config = this;
		}
		
		private void detach_Sites(Site entity)
		{
			this.SendPropertyChanging();
			entity.Config = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Site")]
	public partial class Site : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SiteID;
		
		private string _SiteName;
		
		private System.DateTime _Created;
		
		private System.Nullable<int> _ConfigID;
		
		private EntityRef<Config> _Config;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSiteIDChanging(int value);
    partial void OnSiteIDChanged();
    partial void OnSiteNameChanging(string value);
    partial void OnSiteNameChanged();
    partial void OnCreatedChanging(System.DateTime value);
    partial void OnCreatedChanged();
    partial void OnConfigIDChanging(System.Nullable<int> value);
    partial void OnConfigIDChanged();
    #endregion
		
		public Site()
		{
			this._Config = default(EntityRef<Config>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SiteID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				if ((this._SiteID != value))
				{
					this.OnSiteIDChanging(value);
					this.SendPropertyChanging();
					this._SiteID = value;
					this.SendPropertyChanged("SiteID");
					this.OnSiteIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SiteName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string SiteName
		{
			get
			{
				return this._SiteName;
			}
			set
			{
				if ((this._SiteName != value))
				{
					this.OnSiteNameChanging(value);
					this.SendPropertyChanging();
					this._SiteName = value;
					this.SendPropertyChanged("SiteName");
					this.OnSiteNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Created", DbType="DateTime NOT NULL")]
		public System.DateTime Created
		{
			get
			{
				return this._Created;
			}
			set
			{
				if ((this._Created != value))
				{
					this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConfigID", DbType="Int")]
		public System.Nullable<int> ConfigID
		{
			get
			{
				return this._ConfigID;
			}
			set
			{
				if ((this._ConfigID != value))
				{
					if (this._Config.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnConfigIDChanging(value);
					this.SendPropertyChanging();
					this._ConfigID = value;
					this.SendPropertyChanged("ConfigID");
					this.OnConfigIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Config_Site", Storage="_Config", ThisKey="ConfigID", OtherKey="ID", IsForeignKey=true)]
		public Config Config
		{
			get
			{
				return this._Config.Entity;
			}
			set
			{
				Config previousValue = this._Config.Entity;
				if (((previousValue != value) 
							|| (this._Config.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Config.Entity = null;
						previousValue.Sites.Remove(this);
					}
					this._Config.Entity = value;
					if ((value != null))
					{
						value.Sites.Add(this);
						this._ConfigID = value.ID;
					}
					else
					{
						this._ConfigID = default(Nullable<int>);
					}
					this.SendPropertyChanged("Config");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
