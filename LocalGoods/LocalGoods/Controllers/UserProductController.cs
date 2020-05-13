//	UserProductController.cs
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

using static LocalGoods.LocalGoodsTools;
using static LocalGoods.SQLHelper;

namespace LocalGoods.Controllers
{
	//*-------------------------------------------------------------------------*
	//*	UserProductController																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// User product management functions.
	/// </summary>
	public class UserProductController : ApiController
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
		//* PostRetrieveProductCount																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the count of items owned by the logged-in user.
		/// </summary>
		/// <param name="model">
		/// UserTicketItem identifying the user requesting the list.
		/// </param>
		/// <returns>
		/// IHttpActionResult set to Ok with CountItem.
		/// </returns>
		[Route("api/v1/userproductcount")]
		public IHttpActionResult PostRetrieveProductCount(UserTicketItem model)
		{
			CountItem payload = new CountItem();
			Guid ticket = Guid.Empty;

			if(model != null && model.UserTicket?.Length > 0)
			{
				try
				{
					ticket = Guid.Parse(model.UserTicket);
				}
				catch { }
				if(ticket != Guid.Empty)
				{
					payload.Count = GetScalarInt(
						String.Format(
							ResourceMain.vwCatalogItemCountForUserTicket, ToSql(ticket)));
				}
			}
			return Ok(payload);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PostRetrieveProductList																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the list of product items owned by the logged-in user.
		/// </summary>
		/// <param name="model">
		/// UserTicketItem identifying the user requesting the list.
		/// </param>
		/// <returns>
		/// IHttpActionResult set to Ok with CatalogMinCollection.
		/// </returns>
		[Route("api/v1/userproductlist")]
		public IHttpActionResult PostRetrieveProductList(UserTicketItem model)
		{
			CatalogMinCollection result = new CatalogMinCollection();
			DataTable table = null;
			Guid ticket = Guid.Empty;

			if(model != null && model.UserTicket?.Length > 0)
			{
				try
				{
					ticket = Guid.Parse(model.UserTicket);
				}
				catch { }
				if(ticket != Guid.Empty)
				{
					table = GetTable(
						String.Format(
							ResourceMain.vwCatalogItemsForUserTicket, ToSql(ticket)));
					result.Load(table);
				}
			}
			return Ok(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PostRetrieveProductSearch																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the list of product items owned by the logged-in user, and
		/// specified in the search filter.
		/// </summary>
		/// <param name="model">
		/// UserTicketSearchTextItem identifying the user requesting the list,
		/// along with the search term.
		/// </param>
		/// <returns>
		/// IHttpActionResult set to Ok with CatalogMinCollection.
		/// </returns>
		[Route("api/v1/userproductsearch")]
		public IHttpActionResult PostRetrieveProductSearch(
			UserTicketSearchTextItem model)
		{
			List<int> catalogItemIDs = null;
			CatalogMinCollection payload = new CatalogMinCollection();
			DataTable table = null;
			Guid ticket = Guid.Empty;

			if(model != null && model.UserTicket?.Length > 0)
			{
				try
				{
					ticket = Guid.Parse(model.UserTicket);
				}
				catch { }
				if(ticket != Guid.Empty)
				{
					catalogItemIDs = GetCatalogSearchResultList(model.SearchText);
					if(catalogItemIDs.Count > 0)
					{
						table = GetTable(
							String.Format(
								ResourceMain.vwCatalogItemsForIDsAndUserTicket,
								string.Join(",", catalogItemIDs.Select(x => ToSql(x))),
								ToSql(ticket)));
						payload.Load(table);
					}
				}
			}
			return Ok(payload);
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
