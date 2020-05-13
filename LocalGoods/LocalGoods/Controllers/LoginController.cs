//	LoginController.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


using static LocalGoods.SQLHelper;

namespace LocalGoods.Controllers
{
	//*-------------------------------------------------------------------------*
	//*	LoginController																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Login web functions.
	/// </summary>
	[EnableCors("*", "*", "*")]
	public class LoginController : ApiController
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
		//*	Post																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Post the login submission.
		/// </summary>
		/// <param name="model">
		/// Reference to an email and password model containing the login
		/// information.
		/// </param>
		/// <returns>
		/// Reference to an IHttpActionResult containing a UsernameTicketModel
		/// with user information.
		/// </returns>
		[Route("api/v1/login")]
		public IHttpActionResult Post(EmailPasswordItem model)
		{
			UsernameUserTicket payload = new UsernameUserTicket();
			IHttpActionResult result = null;
			DataRow row = null;
			DataTable table = GetTable(
				String.Format(ResourceMain.vwUserItemForEmailPassword,
				ToSql(model.Email), ToSql(model.Password)));

			if(table.Rows.Count > 0)
			{
				row = table.Rows[0];
				payload.Username = row.Field<string>("MemberUsername");
				payload.UserTicket = row.Field<Guid>("UserItemTicket").ToString("D");
			}
			else
			{
				payload.Username = "BADUSERNAMEORPASSWORD";
				payload.UserTicket = Guid.NewGuid().ToString("D");
			}
			result = Ok(payload);
			return result;
		}
		//*-----------------------------------------------------------------------*
	}
	//*-------------------------------------------------------------------------*
}
