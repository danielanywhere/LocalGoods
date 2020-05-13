//	EmailPassword.cs
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
	//*	EmailPasswordItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single email address and password combination.
	/// </summary>
	public class EmailPasswordItem
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
		//*	Email																																	*
		//*-----------------------------------------------------------------------*
		private string mEmail = "";
		/// <summary>
		/// Get/Set the email address.
		/// </summary>
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
		/// Get/Set the Password.
		/// </summary>
		public string Password
		{
			get { return mPassword; }
			set { mPassword = value; }
		}
		//*-----------------------------------------------------------------------*
	}
	//*-------------------------------------------------------------------------*

}
