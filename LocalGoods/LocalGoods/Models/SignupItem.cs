//	SignupItem.cs
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
	//*	SignupItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Model of the signup data.
	/// </summary>
	public class SignupItem
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
		private int mCityItemID = 1;
		/// <summary>
		/// Get/Set the city selected at the time of user signup.
		/// </summary>
		[JsonProperty(Order = 3)]
		public int CityItemID
		{
			get { return mCityItemID; }
			set { mCityItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Email																																	*
		//*-----------------------------------------------------------------------*
		private string mEmail = "";
		/// <summary>
		/// Get/Set the email address of the user.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string Email
		{
			get { return mEmail; }
			set { mEmail = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Password																															*
		//*-----------------------------------------------------------------------*
		private string mPassword = "";
		/// <summary>
		/// Get/Set the user's password.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string Password
		{
			get { return mPassword; }
			set { mPassword = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeUserTicket																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the UserTicket property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if the user ticket should be serialized. Otherise, false.
		/// </returns>
		public bool ShouldSerializeUserTicket()
		{
			return mUserTicket?.Length > 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Username																															*
		//*-----------------------------------------------------------------------*
		private string mUsername = "";
		/// <summary>
		/// Get/Set the Username of the user.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Username
		{
			get { return mUsername; }
			set { mUsername = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UserTicket																														*
		//*-----------------------------------------------------------------------*
		private string mUserTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the user.
		/// </summary>
		[JsonProperty(Order = 4)]
		public string UserTicket
		{
			get { return mUserTicket; }
			set { mUserTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
