//	CatalogIDUserTicket.cs
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
	//*	CatalogIDUserTicket																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Catalog Item ID and User Item Ticket.
	/// </summary>
	public class CatalogIDUserTicket
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
		//*	CatalogItemID																													*
		//*-----------------------------------------------------------------------*
		private int mCatalogItemID = 0;
		/// <summary>
		/// Get/Set the Catalog Item ID.
		/// </summary>
		public int CatalogItemID
		{
			get { return mCatalogItemID; }
			set { mCatalogItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UserItemTicket																												*
		//*-----------------------------------------------------------------------*
		private string mUserItemTicket = "";
		/// <summary>
		/// Get/Set the User Item Ticket.
		/// </summary>
		public string UserItemTicket
		{
			get { return mUserItemTicket; }
			set { mUserItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
