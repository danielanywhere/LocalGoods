//	Department.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	DepartmentCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of DepartmentItem Items.
	/// </summary>
	public class DepartmentCollection : List<DepartmentItem>
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
		//*	Load																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the collection with records from the caller's data table.
		/// </summary>
		/// <param name="table">
		/// Reference to the data table containing department records to load.
		/// </param>
		public void Load(DataTable table)
		{
			DepartmentItem department = null;

			if(table != null)
			{
				foreach(DataRow row in table.Rows)
				{
					department = new DepartmentItem();
					department.DepartmentItemID = row.Field<int>("DepartmentItemID");
					department.DepartmentTicket =
						row.Field<Guid>("DepartmentItemTicket").ToString("D");
					department.DepartmentName = row.Field<string>("DepartmentName");
					this.Add(department);
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	DepartmentItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a single Department record.
	/// </summary>
	public class DepartmentItem
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
		//*	DepartmentItemID																											*
		//*-----------------------------------------------------------------------*
		private int mDepartmentItemID = 0;
		/// <summary>
		/// Get/Set the local identification of the record.
		/// </summary>
		[JsonProperty(Order = 0)]
		public int DepartmentItemID
		{
			get { return mDepartmentItemID; }
			set { mDepartmentItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DepartmentName																												*
		//*-----------------------------------------------------------------------*
		private string mDepartmentName = "";
		/// <summary>
		/// Get/Set the name of the department.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string DepartmentName
		{
			get { return mDepartmentName; }
			set { mDepartmentName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DepartmentTicket																											*
		//*-----------------------------------------------------------------------*
		private string mDepartmentTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the record.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string DepartmentTicket
		{
			get { return mDepartmentTicket; }
			set { mDepartmentTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
