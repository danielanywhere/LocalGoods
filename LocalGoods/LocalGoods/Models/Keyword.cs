//	Keyword.cs
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
	//*	KeywordCollection																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of KeywordItem Items.
	/// </summary>
	public class KeywordCollection : List<KeywordItem>
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
		//*	Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an item to the collection by value.
		/// </summary>
		/// <param name="keyword">
		/// The keyword to add.
		/// </param>
		/// <param name="keywordID">
		/// Optional record ID of the keyword.
		/// </param>
		/// <returns>
		/// Newly created and added keyword item.
		/// </returns>
		public KeywordItem Add(string keyword, int keywordID = 0)
		{
			KeywordItem result = new KeywordItem();

			result.Keyword = keyword;
			result.KeywordID = keywordID;
			this.Add(result);
			return result;
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	KeywordItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about the keyword.
	/// </summary>
	public class KeywordItem
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
		//*	Keyword																																*
		//*-----------------------------------------------------------------------*
		private string mKeyword = "";
		/// <summary>
		/// Get/Set the keyword value.
		/// </summary>
		public string Keyword
		{
			get { return mKeyword; }
			set { mKeyword = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	KeywordID																															*
		//*-----------------------------------------------------------------------*
		private int mKeywordID = 0;
		/// <summary>
		/// Get/Set the keyword record ID.
		/// </summary>
		public int KeywordID
		{
			get { return mKeywordID; }
			set { mKeywordID = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
