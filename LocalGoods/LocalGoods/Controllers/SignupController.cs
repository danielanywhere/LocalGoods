//	SignupController.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using static LocalGoods.LocalGoodsTools;
using static LocalGoods.SQLHelper;

namespace LocalGoods.Controllers
{
	//*-------------------------------------------------------------------------*
	//*	SignupController																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Web API2 controller for signup activities.
	/// </summary>
	[EnableCors("*", "*", "*")]
	public class SignupController : ApiController
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
		//* PostEmailCheck																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the email address is available.
		/// </summary>
		/// <param name="email">
		/// Email to test.
		/// </param>
		/// <returns>
		/// IHttpActionResult OK containing the object AvailableStatusItem.
		/// </returns>
		[Route("api/v1/signupemailcheck")]
		public IHttpActionResult PostEmailCheck(NameValueStringItem email)
		{
			AvailableStatusItem data = new AvailableStatusItem();

			//	TODO: Migrate away from NameValueStringItem to specialized EmailItem.
			data.Name = email.Value;
			data.Available = false;

			if(EmailIsValid(email.Value))
			{
				try
				{
					data.Available =
						GetScalarBool(
							String.Format(ResourceMain.vwEmailNotFound, ToSql(email.Value)),
							true);
				}
				catch { }
			}

			return Ok(data);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PostRetrieveProfile																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Retrieve the user's profile via two-factor information.
		/// </summary>
		/// <param name="model">
		/// Reference to a UsernameUserTicket object containing the user's
		/// login authorization.
		/// </param>
		/// <returns>
		/// An OK response containing a SignupItem set to user's profile.
		/// </returns>
		[Route("api/v1/signupretrieveprofile")]
		public IHttpActionResult PostRetrieveProfile(UsernameUserTicket model)
		{
			SignupItem result = new SignupItem();
			DataRow row = null;
			DataTable table = null;
			Guid ticket = Guid.Empty;

			if(model != null &&
				model.Username?.Length > 0 && model.UserTicket?.Length > 0)
			{
				try
				{
					ticket = Guid.Parse(model.UserTicket);
				}
				catch { }
				if(ticket != Guid.Empty)
				{
					table = GetTable(
						String.Format(ResourceMain.vwUserItemForTicketUsername,
						ToSql(ticket), ToSql(model.Username)));
					if(table.Rows.Count > 0)
					{
						row = table.Rows[0];
						result.CityItemID = row.Field<int>("CityItemID");
						result.Email = row.Field<string>("MemberEmail");
						result.Password = row.Field<string>("MemberPassword");
						result.Username = row.Field<string>("MemberUsername");
						result.UserTicket = ticket.ToString("D");
					}
				}
			}
			return Ok(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PostSignup																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Post the signup form.
		/// </summary>
		/// <param name="model">
		/// Reference to a username, email, and password model containing the
		/// signup information.
		/// </param>
		/// <returns>
		/// Reference to an IHttpActionResult containing a UsernameTicketModel
		/// with user information.
		/// </returns>
		[Route("api/v1/signup")]
		public IHttpActionResult PostSignup(SignupItem model)
		{
			UsernameUserTicket payload = new UsernameUserTicket();
			IHttpActionResult result = null;
			DataRow row = null;

			//	Create the new record.
			Update(String.Format(ResourceMain.insUserItem,
				ToSql(Guid.NewGuid()),
				ToSql(model.Username), ToSql(model.Email), ToSql(model.Password),
				ToSql(model.CityItemID)));
			//	Retrieve the user info, including ticket.
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

		//*-----------------------------------------------------------------------*
		//* PostUpdateUserProfile																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the caller's user profile data.
		/// </summary>
		/// <param name="model">
		/// Reference to a signup item for the user.
		/// </param>
		/// <returns>
		/// Ok response from server, with MessageItem payload.
		/// </returns>
		[Route("api/v1/signupupdateprofile")]
		public IHttpActionResult PostUpdateUserProfile(SignupItem model)
		{
			MessageItem result = new MessageItem();
			DataRow row = null;
			DataTable table = null;
			Guid ticket = Guid.Empty;

			result.Message = "Parameter not found...";

			if(model != null && model.UserTicket?.Length > 0)
			{
				try
				{
					ticket = Guid.Parse(model.UserTicket);
				}
				catch { }
				if(ticket != Guid.Empty)
				{
					result.Message = "You must leave either your email or your " +
						"password unchanged per-entry. If you want to change both " +
						"values, please use two separate edits...";
					table = GetTable(String.Format(ResourceMain.vwUserItemForTwoFactor,
						ToSql(ticket), ToSql(model.Email), ToSql(model.Password)));
					if(table.Rows.Count > 0)
					{
						row = table.Rows[0];
						row.SetField<string>("MemberUsername", model.Username);
						row.SetField<string>("MemberEmail", model.Email);
						row.SetField<string>("MemberPassword", model.Password);
						Update(table);
						result.Message = "User profile updated...";
					}
				}
			}
			return Ok(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PostUsernameCheck																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the username is available.
		/// </summary>
		/// <param name="username">
		/// Username to test.
		/// </param>
		/// <returns>
		/// IHttpActionResult OK containing the object AvailableStatusItem.
		/// </returns>
		[Route("api/v1/signupusernamecheck")]
		public IHttpActionResult PostUsernameCheck(NameValueStringItem username)
		{
			AvailableStatusItem data = new AvailableStatusItem();

			//	TODO: Migrate from NameValueStringItem to specialized UsernameItem.
			data.Name = username.Value;
			data.Available = false;
			try
			{
				data.Available =
					GetScalarBool(
						String.Format(
							ResourceMain.vwUsernameNotFound, ToSql(username.Value)), true);
			}
			catch { }

			return Ok(data);
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
