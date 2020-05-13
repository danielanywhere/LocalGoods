//	SearchController.cs
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
using System.Text;
using System.Web.Http;

using static LocalGoods.LocalGoodsTools;
using static LocalGoods.SQLHelper;

namespace LocalGoods.Controllers
{
	//*-------------------------------------------------------------------------*
	//*	SearchController																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Web API2 controller for Search.
	/// </summary>
	public class SearchController : ApiController
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
		//*	PostSearch																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Post a search request.
		/// </summary>
		/// <param name="model">
		/// Reference to a name/value pair containing the search information.
		/// </param>
		/// <returns>
		/// Reference to an IHttpActionResult containing a CatalogMinCollection
		/// with matching information.
		/// </returns>
		[Route("api/v1/search")]
		public IHttpActionResult PostSearch(SearchCityItem model)
		{
			List<int> catalogItemIDs = new List<int>();
			DataTable idTable = null;
			CatalogMinCollection payload = new CatalogMinCollection();

			catalogItemIDs = GetCatalogSearchResultList(model.SearchText);
			if(model.CityItemID == 0)
			{
				idTable = GetTable(
					String.Format(ResourceMain.vwCatalogItemsForIDList,
					string.Join(",", catalogItemIDs.Select(x => ToSql(x)))));
			}
			else
			{
				idTable = GetTable(
					String.Format(
						ResourceMain.vwCatalogItemsForIDListInCity,
						string.Join(",", catalogItemIDs.Select(x => ToSql(x))),
						ToSql(model.CityItemID)));
			}
			payload.Load(idTable);

			return Ok(payload);
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
