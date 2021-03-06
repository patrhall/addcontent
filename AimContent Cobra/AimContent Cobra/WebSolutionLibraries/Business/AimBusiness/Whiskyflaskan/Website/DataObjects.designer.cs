﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIM.Business.AimBusiness.Whiskyflaskan.Website
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AimContent_RevisorsRingen")]
	public partial class DataObjectsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataObjectsDataContext() : 
				base(global::AIM.Business.Properties.Settings.Default.AimContent_RevisorsRingenConnectionString, mappingSource)
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
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_CalendarR_Save_Hatch")]
		public ISingleResult<usp_CalendarR_Save_HatchResult> usp_CalendarR_Save_Hatch([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LID", DbType="Int")] System.Nullable<int> lID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CID", DbType="Int")] System.Nullable<int> cID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HatchNo", DbType="Int")] System.Nullable<int> hatchNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Html", DbType="NVarChar(MAX)")] string html, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Date", DbType="DateTime")] System.Nullable<System.DateTime> date)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lID, cID, hatchNo, html, date);
			return ((ISingleResult<usp_CalendarR_Save_HatchResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_CalendarR_Get_Calendar")]
		public ISingleResult<usp_CalendarR_Get_CalendarResult> usp_CalendarR_Get_Calendar([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ObjectID", DbType="Int")] System.Nullable<int> objectID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SiteID", DbType="Int")] System.Nullable<int> siteID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), objectID, siteID);
			return ((ISingleResult<usp_CalendarR_Get_CalendarResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_CalendarR_Get_Hatches")]
		public ISingleResult<usp_CalendarR_Get_HatchesResult> usp_CalendarR_Get_Hatches([global::System.Data.Linq.Mapping.ParameterAttribute(Name="CID", DbType="Int")] System.Nullable<int> cID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cID);
			return ((ISingleResult<usp_CalendarR_Get_HatchesResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_CalendarR_Get_SpecificHatch")]
		public ISingleResult<usp_CalendarR_Get_SpecificHatchResult> usp_CalendarR_Get_SpecificHatch([global::System.Data.Linq.Mapping.ParameterAttribute(Name="LID", DbType="Int")] System.Nullable<int> lID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lID);
			return ((ISingleResult<usp_CalendarR_Get_SpecificHatchResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_CalendarR_Save")]
		public ISingleResult<usp_CalendarR_SaveResult> usp_CalendarR_Save([global::System.Data.Linq.Mapping.ParameterAttribute(Name="CID", DbType="Int")] System.Nullable<int> cID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="ObjectID", DbType="Int")] System.Nullable<int> objectID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Name", DbType="NVarChar(250)")] string name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Picture", DbType="NVarChar(250)")] string picture, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SiteID", DbType="Int")] System.Nullable<int> siteID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Text", DbType="NVarChar(500)")] string text, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(7)")] string bgColor, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Message", DbType="NVarChar(250)")] string message)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cID, objectID, name, picture, siteID, text, bgColor, message);
			return ((ISingleResult<usp_CalendarR_SaveResult>)(result.ReturnValue));
		}
	}
	
	public partial class usp_CalendarR_Save_HatchResult
	{
		
		private System.Nullable<int> _Column1;
		
		public usp_CalendarR_Save_HatchResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="", Storage="_Column1", DbType="Int")]
		public System.Nullable<int> Column1
		{
			get
			{
				return this._Column1;
			}
			set
			{
				if ((this._Column1 != value))
				{
					this._Column1 = value;
				}
			}
		}
	}
	
	public partial class usp_CalendarR_Get_CalendarResult
	{
		
		private int _CID;
		
		private System.Nullable<int> _ObjectID;
		
		private string _Name;
		
		private string _Picture;
		
		private System.Nullable<int> _SiteID;
		
		private string _Text;
		
		private string _bgColor;
		
		private string _Message;
		
		public usp_CalendarR_Get_CalendarResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CID", DbType="Int NOT NULL")]
		public int CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				if ((this._CID != value))
				{
					this._CID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ObjectID", DbType="Int")]
		public System.Nullable<int> ObjectID
		{
			get
			{
				return this._ObjectID;
			}
			set
			{
				if ((this._ObjectID != value))
				{
					this._ObjectID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(250)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Picture", DbType="NVarChar(250)")]
		public string Picture
		{
			get
			{
				return this._Picture;
			}
			set
			{
				if ((this._Picture != value))
				{
					this._Picture = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SiteID", DbType="Int")]
		public System.Nullable<int> SiteID
		{
			get
			{
				return this._SiteID;
			}
			set
			{
				if ((this._SiteID != value))
				{
					this._SiteID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(500)")]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this._Text = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bgColor", DbType="NVarChar(7)")]
		public string bgColor
		{
			get
			{
				return this._bgColor;
			}
			set
			{
				if ((this._bgColor != value))
				{
					this._bgColor = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="NVarChar(250)")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this._Message = value;
				}
			}
		}
	}
	
	public partial class usp_CalendarR_Get_HatchesResult
	{
		
		private int _LID;
		
		private System.Nullable<int> _CID;
		
		private System.Nullable<int> _HatchNo;
		
		private string _Html;
		
		private System.Nullable<System.DateTime> _Date;
		
		public usp_CalendarR_Get_HatchesResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LID", DbType="Int NOT NULL")]
		public int LID
		{
			get
			{
				return this._LID;
			}
			set
			{
				if ((this._LID != value))
				{
					this._LID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CID", DbType="Int")]
		public System.Nullable<int> CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				if ((this._CID != value))
				{
					this._CID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HatchNo", DbType="Int")]
		public System.Nullable<int> HatchNo
		{
			get
			{
				return this._HatchNo;
			}
			set
			{
				if ((this._HatchNo != value))
				{
					this._HatchNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Html", DbType="NVarChar(MAX)")]
		public string Html
		{
			get
			{
				return this._Html;
			}
			set
			{
				if ((this._Html != value))
				{
					this._Html = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this._Date = value;
				}
			}
		}
	}
	
	public partial class usp_CalendarR_Get_SpecificHatchResult
	{
		
		private int _LID;
		
		private System.Nullable<int> _CID;
		
		private System.Nullable<int> _HatchNo;
		
		private string _Html;
		
		private System.Nullable<System.DateTime> _Date;
		
		private string _Message;
		
		public usp_CalendarR_Get_SpecificHatchResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LID", DbType="Int NOT NULL")]
		public int LID
		{
			get
			{
				return this._LID;
			}
			set
			{
				if ((this._LID != value))
				{
					this._LID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CID", DbType="Int")]
		public System.Nullable<int> CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				if ((this._CID != value))
				{
					this._CID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HatchNo", DbType="Int")]
		public System.Nullable<int> HatchNo
		{
			get
			{
				return this._HatchNo;
			}
			set
			{
				if ((this._HatchNo != value))
				{
					this._HatchNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Html", DbType="NVarChar(MAX)")]
		public string Html
		{
			get
			{
				return this._Html;
			}
			set
			{
				if ((this._Html != value))
				{
					this._Html = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this._Date = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="NVarChar(250)")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this._Message = value;
				}
			}
		}
	}
	
	public partial class usp_CalendarR_SaveResult
	{
		
		private System.Nullable<int> _Column1;
		
		public usp_CalendarR_SaveResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="", Storage="_Column1", DbType="Int")]
		public System.Nullable<int> Column1
		{
			get
			{
				return this._Column1;
			}
			set
			{
				if ((this._Column1 != value))
				{
					this._Column1 = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
