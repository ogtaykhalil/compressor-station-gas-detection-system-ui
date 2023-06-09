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

namespace ScadaShablon
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Data")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAlarmTable(AlarmTable instance);
    partial void UpdateAlarmTable(AlarmTable instance);
    partial void DeleteAlarmTable(AlarmTable instance);
    partial void InsertCurrentAlarmTable(CurrentAlarmTable instance);
    partial void UpdateCurrentAlarmTable(CurrentAlarmTable instance);
    partial void DeleteCurrentAlarmTable(CurrentAlarmTable instance);
    partial void InsertEventTable(EventTable instance);
    partial void UpdateEventTable(EventTable instance);
    partial void DeleteEventTable(EventTable instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::ScadaShablon.Properties.Settings.Default.DataConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<AlarmTable> AlarmTables
		{
			get
			{
				return this.GetTable<AlarmTable>();
			}
		}
		
		public System.Data.Linq.Table<CurrentAlarmTable> CurrentAlarmTables
		{
			get
			{
				return this.GetTable<CurrentAlarmTable>();
			}
		}
		
		public System.Data.Linq.Table<EventTable> EventTables
		{
			get
			{
				return this.GetTable<EventTable>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AlarmTable")]
	public partial class AlarmTable : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _device_name;
		
		private string _message;
		
		private System.DateTime _date_and_time;
		
		private string _description;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void Ondevice_nameChanging(string value);
    partial void Ondevice_nameChanged();
    partial void OnmessageChanging(string value);
    partial void OnmessageChanged();
    partial void Ondate_and_timeChanging(System.DateTime value);
    partial void Ondate_and_timeChanged();
    partial void OndescriptionChanging(string value);
    partial void OndescriptionChanged();
    #endregion
		
		public AlarmTable()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_device_name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string device_name
		{
			get
			{
				return this._device_name;
			}
			set
			{
				if ((this._device_name != value))
				{
					this.Ondevice_nameChanging(value);
					this.SendPropertyChanging();
					this._device_name = value;
					this.SendPropertyChanged("device_name");
					this.Ondevice_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_message", DbType="NVarChar(100)")]
		public string message
		{
			get
			{
				return this._message;
			}
			set
			{
				if ((this._message != value))
				{
					this.OnmessageChanging(value);
					this.SendPropertyChanging();
					this._message = value;
					this.SendPropertyChanged("message");
					this.OnmessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_date_and_time", DbType="DateTime NOT NULL")]
		public System.DateTime date_and_time
		{
			get
			{
				return this._date_and_time;
			}
			set
			{
				if ((this._date_and_time != value))
				{
					this.Ondate_and_timeChanging(value);
					this.SendPropertyChanging();
					this._date_and_time = value;
					this.SendPropertyChanged("date_and_time");
					this.Ondate_and_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_description", DbType="NVarChar(100)")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				if ((this._description != value))
				{
					this.OndescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("description");
					this.OndescriptionChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CurrentAlarmTable")]
	public partial class CurrentAlarmTable : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _device_name;
		
		private string _message;
		
		private System.DateTime _date_and_time;
		
		private string _description;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void Ondevice_nameChanging(string value);
    partial void Ondevice_nameChanged();
    partial void OnmessageChanging(string value);
    partial void OnmessageChanged();
    partial void Ondate_and_timeChanging(System.DateTime value);
    partial void Ondate_and_timeChanged();
    partial void OndescriptionChanging(string value);
    partial void OndescriptionChanged();
    #endregion
		
		public CurrentAlarmTable()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_device_name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string device_name
		{
			get
			{
				return this._device_name;
			}
			set
			{
				if ((this._device_name != value))
				{
					this.Ondevice_nameChanging(value);
					this.SendPropertyChanging();
					this._device_name = value;
					this.SendPropertyChanged("device_name");
					this.Ondevice_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_message", DbType="NVarChar(100)")]
		public string message
		{
			get
			{
				return this._message;
			}
			set
			{
				if ((this._message != value))
				{
					this.OnmessageChanging(value);
					this.SendPropertyChanging();
					this._message = value;
					this.SendPropertyChanged("message");
					this.OnmessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_date_and_time", DbType="DateTime NOT NULL")]
		public System.DateTime date_and_time
		{
			get
			{
				return this._date_and_time;
			}
			set
			{
				if ((this._date_and_time != value))
				{
					this.Ondate_and_timeChanging(value);
					this.SendPropertyChanging();
					this._date_and_time = value;
					this.SendPropertyChanged("date_and_time");
					this.Ondate_and_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_description", DbType="NVarChar(100)")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				if ((this._description != value))
				{
					this.OndescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("description");
					this.OndescriptionChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.EventTable")]
	public partial class EventTable : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _device_name;
		
		private string _message;
		
		private System.DateTime _date_and_time;
		
		private string _description;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void Ondevice_nameChanging(string value);
    partial void Ondevice_nameChanged();
    partial void OnmessageChanging(string value);
    partial void OnmessageChanged();
    partial void Ondate_and_timeChanging(System.DateTime value);
    partial void Ondate_and_timeChanged();
    partial void OndescriptionChanging(string value);
    partial void OndescriptionChanged();
    #endregion
		
		public EventTable()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_device_name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string device_name
		{
			get
			{
				return this._device_name;
			}
			set
			{
				if ((this._device_name != value))
				{
					this.Ondevice_nameChanging(value);
					this.SendPropertyChanging();
					this._device_name = value;
					this.SendPropertyChanged("device_name");
					this.Ondevice_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_message", DbType="NVarChar(100)")]
		public string message
		{
			get
			{
				return this._message;
			}
			set
			{
				if ((this._message != value))
				{
					this.OnmessageChanging(value);
					this.SendPropertyChanging();
					this._message = value;
					this.SendPropertyChanged("message");
					this.OnmessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_date_and_time", DbType="DateTime NOT NULL")]
		public System.DateTime date_and_time
		{
			get
			{
				return this._date_and_time;
			}
			set
			{
				if ((this._date_and_time != value))
				{
					this.Ondate_and_timeChanging(value);
					this.SendPropertyChanging();
					this._date_and_time = value;
					this.SendPropertyChanged("date_and_time");
					this.Ondate_and_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_description", DbType="NVarChar(100)")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				if ((this._description != value))
				{
					this.OndescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("description");
					this.OndescriptionChanged();
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
