//	HomeControlller.cs
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
	//*	HomeController																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Functions present on the Home page.
	/// </summary>
	[EnableCors("*", "*", "*")]
	public class HomeController : ApiController
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private const int MAX_PREVIEW_ITEMS = 16;

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		//*-----------------------------------------------------------------------*
		//*	GetCities																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the list of cities for selection on the home page.
		/// </summary>
		/// <returns>
		/// Reference to a CityCollection containing all of the currently
		/// recognized cities.
		/// </returns>
		[Route("api/v1/homecities")]
		public IHttpActionResult GetCities()
		{
			CityCollection result = new CityCollection();
			DataTable table = null;

			table = GetTable(
				String.Format(ResourceMain.vwCities));
			result.Load(table);

			return Ok(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDepartments																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the list of departments currently supported by this system.
		/// </summary>
		/// <returns>
		/// Reference to a DepartmentCollection containing the supported
		/// departments.
		/// </returns>
		[Route("api/v1/homedepartments")]
		public IHttpActionResult GetDepartments()
		{
			DepartmentCollection result = new DepartmentCollection();
			DataTable table = null;

			table = GetTable(
				String.Format(ResourceMain.vwDepartments));
			result.Load(table);

			return Ok(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetInfo																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the home page information data for the specified city.
		/// </summary>
		/// <param name="cityItemID">
		/// Unique record identification of the city for which information will
		/// be retrieved.
		/// </param>
		[Route("api/v1/homeinfo/{cityItemID}")]
		public IHttpActionResult GetInfo(int cityItemID)
		{
			NameValueIntItem countItem = null;
			NameValueIntCollection counts = new NameValueIntCollection();
			CatalogMinCollection data = new CatalogMinCollection();
			string department = "";
			int id = 0;
			CatalogMinItem item = null;
			IHttpActionResult result = null;
			DataTable table = GetTable(
				String.Format(ResourceMain.vwCatalogItemsForCity,
				ToSql(cityItemID)));

			if(table.Rows.Count > 0)
			{
				foreach(DataRow row in table.Rows)
				{
					department = row.Field<string>("DepartmentName");
					countItem = counts[department];
					if(countItem == null)
					{
						countItem = new NameValueIntItem();
						countItem.Name = department;
					}
					if(countItem.Value < MAX_PREVIEW_ITEMS)
					{
						id = row.Field<int>("CatalogItemID");
						item = new CatalogMinItem();
						item.CatalogItemID = id;
						item.DepartmentName = department;
						item.ImageURL =
							FixCatalogItemImageURL(id, row.Field<string>("ImageURL"));
						item.ItemPrice = row.Field<double>("ItemPrice");
						item.ItemUnit = row.Field<string>("ItemUnit");
						item.ProductDescription = row.Field<string>("ProductDescription");
						item.ProductTitle = row.Field<string>("ProductTitle");
						item.StarCount = row.Field<double>("StarCount");
						data.Add(item);
						countItem.Value++;
					}
				}
			}

			result = Ok(data);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PostRequestCity																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Request the addition of a new city.
		/// </summary>
		/// <param name="model">
		/// Reference to a city name object containing the name of the proposed
		/// city.
		/// </param>
		/// <returns>
		/// MessageItem containing a message of the outcome.
		/// </returns>
		[Route("api/v1/homerequestcity")]
		public IHttpActionResult PostRequestCity(CityNameItem model)
		{
			MessageItem message = new MessageItem();

			message.Message = "Unknown city...";
			if(model != null && model.CityName?.Length > 0)
			{
				message.Message = "Request received...";
				Update(String.Format(ResourceMain.insCityRequestAddUniqueName,
					ToSql(Guid.NewGuid()),
					ToSql(model.CityName)));
				message.Message = "Request submitted...";
			}
			return Ok(message);
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
