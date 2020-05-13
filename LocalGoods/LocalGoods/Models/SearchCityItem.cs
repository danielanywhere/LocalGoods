//	SearchCityItem.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	SearchCityItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Search parameters for searches within the specified city.
	/// </summary>
	public class SearchCityItem
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
		/// Get/Set the record ID of the city to search. Zero = all.
		/// </summary>
		public int CityItemID
		{
			get { return mCityItemID; }
			set { mCityItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SearchText																														*
		//*-----------------------------------------------------------------------*
		private string mSearchText = "";
		/// <summary>
		/// Get/Set the search text.
		/// </summary>
		public string SearchText
		{
			get { return mSearchText; }
			set { mSearchText = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
