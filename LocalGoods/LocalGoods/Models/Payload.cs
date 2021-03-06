//	Payload.cs
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
	//*	PayloadItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Generic payload.
	/// </summary>
	public class PayloadItem
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
		//*	Payload																																*
		//*-----------------------------------------------------------------------*
		private string mPayload = "";
		/// <summary>
		/// Get/Set the payload of this item.
		/// </summary>
		public string Payload
		{
			get { return mPayload; }
			set { mPayload = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
