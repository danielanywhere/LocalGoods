//	AvailableStatus.cs
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
	//*	AvailableStatusItem																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about the availability of a named reference.
	/// </summary>
	public class AvailableStatusItem
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
		//*	Available																															*
		//*-----------------------------------------------------------------------*
		private bool mAvailable = false;
		/// <summary>
		/// Get/Set a value indicating whether the specified resource is available.
		/// </summary>
		[JsonProperty(Order = 1)]
		public bool Available
		{
			get { return mAvailable; }
			set { mAvailable = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of the resource.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
