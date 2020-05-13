//	ColumnMetadata.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	ColumnMetadataCollection																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of ColumnMetadataItem Items.
	/// </summary>
	public class ColumnMetadataCollection : List<ColumnMetadataItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Indexer																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the item from the collection by name.
		/// </summary>
		public ColumnMetadataItem this[string name]
		{
			get
			{
				ColumnMetadataItem ro = null;

				if(name?.Length > 0)
				{
					ro = this.FirstOrDefault(x =>
						x.ColumnName == name.ToLower());
				}
				return ro;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InitializeDbTypes																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the database data types of each of the loaded columns.
		/// </summary>
		public void InitializeDbTypes()
		{
			foreach(ColumnMetadataItem item in this)
			{
				item.ColumnName = item.ColumnName.ToLower();
				switch(item.DataType.ToUpper())
				{
					case "INTEGER":
						item.DbType = System.Data.DbType.Int32;
						break;
					case "REAL":
						item.DbType = System.Data.DbType.Double;
						break;
					case "TEXT":
						switch(item.Size)
						{
							case 23:
								item.DbType = System.Data.DbType.DateTime;
								break;
							case 36:
								item.DbType = System.Data.DbType.Guid;
								break;
							default:
								item.DbType = System.Data.DbType.String;
								break;
						}
						break;
					default:
						item.DbType = System.Data.DbType.Object;
						break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ColumnMetadataItem																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a single column definition.
	/// </summary>
	public class ColumnMetadataItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	ColumnName																														*
		//*-----------------------------------------------------------------------*
		private string mColumnName = "";
		/// <summary>
		/// Get/Set the name of the column.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string ColumnName
		{
			get { return mColumnName; }
			set { mColumnName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DataType																															*
		//*-----------------------------------------------------------------------*
		private string mDataType = "";
		/// <summary>
		/// Get/Set the explicit data type of the column.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string DataType
		{
			get { return mDataType; }
			set { mDataType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DbType																																*
		//*-----------------------------------------------------------------------*
		private System.Data.DbType mDbType = System.Data.DbType.Object;
		/// <summary>
		/// Get/Set the translated database data type.
		/// </summary>
		[JsonIgnore]
		public System.Data.DbType DbType
		{
			get { return mDbType; }
			set { mDbType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Size																																	*
		//*-----------------------------------------------------------------------*
		private int mSize = 0;
		/// <summary>
		/// Get/Set the size of this column, if variable.
		/// </summary>
		[JsonProperty(Order = 2)]
		public int Size
		{
			get { return mSize; }
			set { mSize = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
