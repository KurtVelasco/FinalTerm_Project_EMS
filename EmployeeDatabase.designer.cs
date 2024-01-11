﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalTerm_Project_EMS
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MockEMSDatabase")]
	public partial class EmployeeDatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public EmployeeDatabaseDataContext() : 
				base(global::FinalTerm_Project_EMS.Properties.Settings.Default.MockEMSDatabaseConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EmployeeDatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_LOGIN_EMPLOYEE")]
		public ISingleResult<USP_LOGIN_EMPLOYEEResult> USP_LOGIN_EMPLOYEE([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Email", DbType="NVarChar(50)")] string email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Passowrd", DbType="NVarChar(50)")] string passowrd)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), email, passowrd);
			return ((ISingleResult<USP_LOGIN_EMPLOYEEResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_SEARCH_EMPLOYEE_BY_EMAIL")]
		public ISingleResult<USP_SEARCH_EMPLOYEE_BY_EMAILResult> USP_SEARCH_EMPLOYEE_BY_EMAIL([global::System.Data.Linq.Mapping.ParameterAttribute(Name="EmployeeEmail", DbType="NVarChar(255)")] string employeeEmail)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), employeeEmail);
			return ((ISingleResult<USP_SEARCH_EMPLOYEE_BY_EMAILResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_SELECT_ALL_tblDepartments")]
		public ISingleResult<USP_SELECT_ALL_tblDepartmentsResult> USP_SELECT_ALL_tblDepartments()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<USP_SELECT_ALL_tblDepartmentsResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_SELECT_ALL_tblPositions")]
		public ISingleResult<USP_SELECT_ALL_tblPositionsResult> USP_SELECT_ALL_tblPositions()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<USP_SELECT_ALL_tblPositionsResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_SELECT_ALL_tblSchedType")]
		public ISingleResult<USP_SELECT_ALL_tblSchedTypeResult> USP_SELECT_ALL_tblSchedType()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<USP_SELECT_ALL_tblSchedTypeResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_SELECT_ALL_tblStatus")]
		public ISingleResult<USP_SELECT_ALL_tblStatusResult> USP_SELECT_ALL_tblStatus()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<USP_SELECT_ALL_tblStatusResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_INSERT_EMPLOYEE_DETAILS")]
		public int USP_INSERT_EMPLOYEE_DETAILS([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FirstName", DbType="NVarChar(255)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MiddleName", DbType="NVarChar(255)")] string middleName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LastName", DbType="NVarChar(255)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Birthday", DbType="Date")] System.Nullable<System.DateTime> birthday, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Email", DbType="NVarChar(50)")] string email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HomeAddress", DbType="NVarChar(255)")] string homeAddress, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Contact", DbType="NVarChar(11)")] string contact)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), firstName, middleName, lastName, birthday, email, homeAddress, contact);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_UPDATE_EMPLOYEE_PERSONAL")]
		public int USP_UPDATE_EMPLOYEE_PERSONAL([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FirstName", DbType="NVarChar(255)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MiddleName", DbType="NVarChar(255)")] string middleName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LastName", DbType="NVarChar(255)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Birthday", DbType="Date")] System.Nullable<System.DateTime> birthday, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PhoneNumber", DbType="NVarChar(13)")] string phoneNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="EmailAddress", DbType="NVarChar(255)")] string emailAddress, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="HomeAddress", DbType="NVarChar(255)")] string homeAddress)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), firstName, middleName, lastName, birthday, phoneNumber, emailAddress, homeAddress);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.USP_UPDATE_EMPLOYEE_EMPLOYMENT")]
		public int USP_UPDATE_EMPLOYEE_EMPLOYMENT([global::System.Data.Linq.Mapping.ParameterAttribute(Name="EmmployeeID", DbType="Int")] System.Nullable<int> emmployeeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DepartmentID", DbType="Int")] System.Nullable<int> departmentID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="PositionID", DbType="Int")] System.Nullable<int> positionID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="SchedTypeID", DbType="Int")] System.Nullable<int> schedTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="StatusID", DbType="Int")] System.Nullable<int> statusID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="EmployedON", DbType="Date")] System.Nullable<System.DateTime> employedON, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Password", DbType="NVarChar(255)")] string password)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), emmployeeID, departmentID, positionID, schedTypeID, statusID, employedON, password);
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class USP_LOGIN_EMPLOYEEResult
	{
		
		private int _EmployeeID;
		
		private string _FirstName;
		
		private string _MiddleName;
		
		private string _LastName;
		
		private System.DateTime _Birthday;
		
		private string _PhoneNumber;
		
		private string _EmailAddress;
		
		private string _HomeAddress;
		
		private int _EmployeeDetailsID;
		
		private int _EmployeeID1;
		
		private int _DepartmentID;
		
		private int _PositionID;
		
		private int _StatusID;
		
		private int _ScheduleTypeID;
		
		private string _Password;
		
		private System.DateTime _EmployedOn;
		
		private int _DepartmentID1;
		
		private string _DepartmentName;
		
		private int _PositionID1;
		
		private string _PositionName;
		
		public USP_LOGIN_EMPLOYEEResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeID", DbType="Int NOT NULL")]
		public int EmployeeID
		{
			get
			{
				return this._EmployeeID;
			}
			set
			{
				if ((this._EmployeeID != value))
				{
					this._EmployeeID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this._FirstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MiddleName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string MiddleName
		{
			get
			{
				return this._MiddleName;
			}
			set
			{
				if ((this._MiddleName != value))
				{
					this._MiddleName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this._LastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Birthday", DbType="Date NOT NULL")]
		public System.DateTime Birthday
		{
			get
			{
				return this._Birthday;
			}
			set
			{
				if ((this._Birthday != value))
				{
					this._Birthday = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhoneNumber", DbType="NVarChar(11) NOT NULL", CanBeNull=false)]
		public string PhoneNumber
		{
			get
			{
				return this._PhoneNumber;
			}
			set
			{
				if ((this._PhoneNumber != value))
				{
					this._PhoneNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailAddress", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string EmailAddress
		{
			get
			{
				return this._EmailAddress;
			}
			set
			{
				if ((this._EmailAddress != value))
				{
					this._EmailAddress = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HomeAddress", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string HomeAddress
		{
			get
			{
				return this._HomeAddress;
			}
			set
			{
				if ((this._HomeAddress != value))
				{
					this._HomeAddress = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeDetailsID", DbType="Int NOT NULL")]
		public int EmployeeDetailsID
		{
			get
			{
				return this._EmployeeDetailsID;
			}
			set
			{
				if ((this._EmployeeDetailsID != value))
				{
					this._EmployeeDetailsID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeID1", DbType="Int NOT NULL")]
		public int EmployeeID1
		{
			get
			{
				return this._EmployeeID1;
			}
			set
			{
				if ((this._EmployeeID1 != value))
				{
					this._EmployeeID1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentID", DbType="Int NOT NULL")]
		public int DepartmentID
		{
			get
			{
				return this._DepartmentID;
			}
			set
			{
				if ((this._DepartmentID != value))
				{
					this._DepartmentID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionID", DbType="Int NOT NULL")]
		public int PositionID
		{
			get
			{
				return this._PositionID;
			}
			set
			{
				if ((this._PositionID != value))
				{
					this._PositionID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StatusID", DbType="Int NOT NULL")]
		public int StatusID
		{
			get
			{
				return this._StatusID;
			}
			set
			{
				if ((this._StatusID != value))
				{
					this._StatusID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleTypeID", DbType="Int NOT NULL")]
		public int ScheduleTypeID
		{
			get
			{
				return this._ScheduleTypeID;
			}
			set
			{
				if ((this._ScheduleTypeID != value))
				{
					this._ScheduleTypeID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this._Password = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployedOn", DbType="Date NOT NULL")]
		public System.DateTime EmployedOn
		{
			get
			{
				return this._EmployedOn;
			}
			set
			{
				if ((this._EmployedOn != value))
				{
					this._EmployedOn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentID1", DbType="Int NOT NULL")]
		public int DepartmentID1
		{
			get
			{
				return this._DepartmentID1;
			}
			set
			{
				if ((this._DepartmentID1 != value))
				{
					this._DepartmentID1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string DepartmentName
		{
			get
			{
				return this._DepartmentName;
			}
			set
			{
				if ((this._DepartmentName != value))
				{
					this._DepartmentName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionID1", DbType="Int NOT NULL")]
		public int PositionID1
		{
			get
			{
				return this._PositionID1;
			}
			set
			{
				if ((this._PositionID1 != value))
				{
					this._PositionID1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string PositionName
		{
			get
			{
				return this._PositionName;
			}
			set
			{
				if ((this._PositionName != value))
				{
					this._PositionName = value;
				}
			}
		}
	}
	
	public partial class USP_SEARCH_EMPLOYEE_BY_EMAILResult
	{
		
		private int _EmployeeID;
		
		private string _FirstName;
		
		private string _MiddleName;
		
		private string _LastName;
		
		private System.DateTime _Birthday;
		
		private string _PhoneNumber;
		
		private string _EmailAddress;
		
		private string _HomeAddress;
		
		private int _EmployeeDetailsID;
		
		private int _EmployeeID1;
		
		private int _DepartmentID;
		
		private int _PositionID;
		
		private int _StatusID;
		
		private int _ScheduleTypeID;
		
		private string _Password;
		
		private System.DateTime _EmployedOn;
		
		private int _DepartmentID1;
		
		private string _DepartmentName;
		
		private int _PositionID1;
		
		private string _PositionName;
		
		public USP_SEARCH_EMPLOYEE_BY_EMAILResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeID", DbType="Int NOT NULL")]
		public int EmployeeID
		{
			get
			{
				return this._EmployeeID;
			}
			set
			{
				if ((this._EmployeeID != value))
				{
					this._EmployeeID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this._FirstName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MiddleName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string MiddleName
		{
			get
			{
				return this._MiddleName;
			}
			set
			{
				if ((this._MiddleName != value))
				{
					this._MiddleName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this._LastName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Birthday", DbType="Date NOT NULL")]
		public System.DateTime Birthday
		{
			get
			{
				return this._Birthday;
			}
			set
			{
				if ((this._Birthday != value))
				{
					this._Birthday = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhoneNumber", DbType="NVarChar(11) NOT NULL", CanBeNull=false)]
		public string PhoneNumber
		{
			get
			{
				return this._PhoneNumber;
			}
			set
			{
				if ((this._PhoneNumber != value))
				{
					this._PhoneNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailAddress", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string EmailAddress
		{
			get
			{
				return this._EmailAddress;
			}
			set
			{
				if ((this._EmailAddress != value))
				{
					this._EmailAddress = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HomeAddress", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string HomeAddress
		{
			get
			{
				return this._HomeAddress;
			}
			set
			{
				if ((this._HomeAddress != value))
				{
					this._HomeAddress = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeDetailsID", DbType="Int NOT NULL")]
		public int EmployeeDetailsID
		{
			get
			{
				return this._EmployeeDetailsID;
			}
			set
			{
				if ((this._EmployeeDetailsID != value))
				{
					this._EmployeeDetailsID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeID1", DbType="Int NOT NULL")]
		public int EmployeeID1
		{
			get
			{
				return this._EmployeeID1;
			}
			set
			{
				if ((this._EmployeeID1 != value))
				{
					this._EmployeeID1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentID", DbType="Int NOT NULL")]
		public int DepartmentID
		{
			get
			{
				return this._DepartmentID;
			}
			set
			{
				if ((this._DepartmentID != value))
				{
					this._DepartmentID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionID", DbType="Int NOT NULL")]
		public int PositionID
		{
			get
			{
				return this._PositionID;
			}
			set
			{
				if ((this._PositionID != value))
				{
					this._PositionID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StatusID", DbType="Int NOT NULL")]
		public int StatusID
		{
			get
			{
				return this._StatusID;
			}
			set
			{
				if ((this._StatusID != value))
				{
					this._StatusID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleTypeID", DbType="Int NOT NULL")]
		public int ScheduleTypeID
		{
			get
			{
				return this._ScheduleTypeID;
			}
			set
			{
				if ((this._ScheduleTypeID != value))
				{
					this._ScheduleTypeID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this._Password = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployedOn", DbType="Date NOT NULL")]
		public System.DateTime EmployedOn
		{
			get
			{
				return this._EmployedOn;
			}
			set
			{
				if ((this._EmployedOn != value))
				{
					this._EmployedOn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentID1", DbType="Int NOT NULL")]
		public int DepartmentID1
		{
			get
			{
				return this._DepartmentID1;
			}
			set
			{
				if ((this._DepartmentID1 != value))
				{
					this._DepartmentID1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string DepartmentName
		{
			get
			{
				return this._DepartmentName;
			}
			set
			{
				if ((this._DepartmentName != value))
				{
					this._DepartmentName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionID1", DbType="Int NOT NULL")]
		public int PositionID1
		{
			get
			{
				return this._PositionID1;
			}
			set
			{
				if ((this._PositionID1 != value))
				{
					this._PositionID1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string PositionName
		{
			get
			{
				return this._PositionName;
			}
			set
			{
				if ((this._PositionName != value))
				{
					this._PositionName = value;
				}
			}
		}
	}
	
	public partial class USP_SELECT_ALL_tblDepartmentsResult
	{
		
		private int _DepartmentID;
		
		private string _DepartmentName;
		
		public USP_SELECT_ALL_tblDepartmentsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentID", DbType="Int NOT NULL")]
		public int DepartmentID
		{
			get
			{
				return this._DepartmentID;
			}
			set
			{
				if ((this._DepartmentID != value))
				{
					this._DepartmentID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string DepartmentName
		{
			get
			{
				return this._DepartmentName;
			}
			set
			{
				if ((this._DepartmentName != value))
				{
					this._DepartmentName = value;
				}
			}
		}
	}
	
	public partial class USP_SELECT_ALL_tblPositionsResult
	{
		
		private int _PositionID;
		
		private string _PositionName;
		
		public USP_SELECT_ALL_tblPositionsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionID", DbType="Int NOT NULL")]
		public int PositionID
		{
			get
			{
				return this._PositionID;
			}
			set
			{
				if ((this._PositionID != value))
				{
					this._PositionID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PositionName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string PositionName
		{
			get
			{
				return this._PositionName;
			}
			set
			{
				if ((this._PositionName != value))
				{
					this._PositionName = value;
				}
			}
		}
	}
	
	public partial class USP_SELECT_ALL_tblSchedTypeResult
	{
		
		private int _ScheduleTypeID;
		
		private string _ScheduleType;
		
		private System.TimeSpan _ScheduleStartTime;
		
		private System.TimeSpan _ScheduleEndTime;
		
		public USP_SELECT_ALL_tblSchedTypeResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleTypeID", DbType="Int NOT NULL")]
		public int ScheduleTypeID
		{
			get
			{
				return this._ScheduleTypeID;
			}
			set
			{
				if ((this._ScheduleTypeID != value))
				{
					this._ScheduleTypeID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleType", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ScheduleType
		{
			get
			{
				return this._ScheduleType;
			}
			set
			{
				if ((this._ScheduleType != value))
				{
					this._ScheduleType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleStartTime", DbType="Time NOT NULL")]
		public System.TimeSpan ScheduleStartTime
		{
			get
			{
				return this._ScheduleStartTime;
			}
			set
			{
				if ((this._ScheduleStartTime != value))
				{
					this._ScheduleStartTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScheduleEndTime", DbType="Time NOT NULL")]
		public System.TimeSpan ScheduleEndTime
		{
			get
			{
				return this._ScheduleEndTime;
			}
			set
			{
				if ((this._ScheduleEndTime != value))
				{
					this._ScheduleEndTime = value;
				}
			}
		}
	}
	
	public partial class USP_SELECT_ALL_tblStatusResult
	{
		
		private int _StatusID;
		
		private string _StatusName;
		
		public USP_SELECT_ALL_tblStatusResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StatusID", DbType="Int NOT NULL")]
		public int StatusID
		{
			get
			{
				return this._StatusID;
			}
			set
			{
				if ((this._StatusID != value))
				{
					this._StatusID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StatusName", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string StatusName
		{
			get
			{
				return this._StatusName;
			}
			set
			{
				if ((this._StatusName != value))
				{
					this._StatusName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
