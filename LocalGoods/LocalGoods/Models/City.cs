//	City.cs
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
	//*	CityCollection																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of CityItem Items.
	/// </summary>
	public class CityCollection : List<CityItem>
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new Instance of the CityCollection Item.
		/// </summary>
		public CityCollection()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the CityCollection Item.
		/// </summary>
		/// <param name="table">
		/// Reference to a data table containing the city information.
		/// </param>
		public CityCollection(DataTable table)
		{
			Load(table);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Load																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the collection with information from the data table.
		/// </summary>
		public void Load(DataTable table)
		{
			CityItem city = null;

			if(table != null)
			{
				foreach(DataRow row in table.Rows)
				{
					city = new CityItem();
					city.CityItemID = row.Field<int>("CityItemID");
					city.CityItemTicket = row.Field<Guid>("CityItemTicket");
					city.CityName = row.Field<string>("CityName");
					this.Add(city);
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	CityItem																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a city.
	/// </summary>
	public class CityItem
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
		//*	CityItemID																														*
		//*-----------------------------------------------------------------------*
		private int mCityItemID = 0;
		/// <summary>
		/// Get/Set the unique local ID of the city.
		/// </summary>
		[JsonProperty(Order = 0)]
		public int CityItemID
		{
			get { return mCityItemID; }
			set { mCityItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CityName																															*
		//*-----------------------------------------------------------------------*
		private string mCityName = "";
		/// <summary>
		/// Get/Set the name of the city.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string CityName
		{
			get { return mCityName; }
			set { mCityName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CityItemTicket																												*
		//*-----------------------------------------------------------------------*
		private Guid mCityItemTicket = Guid.Empty;
		/// <summary>
		/// Get/Set the globally unique identification of the item.
		/// </summary>
		[JsonProperty(Order = 1)]
		public Guid CityItemTicket
		{
			get { return mCityItemTicket; }
			set { mCityItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	CityNameItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// City name.
	/// </summary>
	public class CityNameItem
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
		//*	CityName																															*
		//*-----------------------------------------------------------------------*
		private string mCityName = "";
		/// <summary>
		/// Get/Set the name of the city.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string CityName
		{
			get { return mCityName; }
			set { mCityName = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
