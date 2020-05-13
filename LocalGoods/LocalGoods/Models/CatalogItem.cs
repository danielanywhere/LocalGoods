//	CatalogItem.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

using static LocalGoods.LocalGoodsTools;
using static LocalGoods.SQLHelper;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	CatalogCollection																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of CatalogItem Items.
	/// </summary>
	public class CatalogCollection : List<CatalogItem>
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


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	CatalogItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Full information available on an item.
	/// </summary>
	public class CatalogItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	ToDepartmentID																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the Department ID associated with the specified department name.
		/// </summary>
		private static int ToDepartmentID(string departmentName)
		{
			int result = SQLHelper.GetScalarInt(
				String.Format(ResourceMain.vwDepartmentIDFromName,
				ToSql(departmentName)));
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new Instance of the CatalogItem Item.
		/// </summary>
		public CatalogItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new Instance of the CatalogItem Item.
		/// </summary>
		/// <param name="row">
		/// Reference to a CatalogItem data row with which to initialize the
		/// value.
		/// </param>
		public CatalogItem(DataRow row)
		{
			DataTable table = null;

			if(row != null)
			{
				//	CatalogItemID.
				this.CatalogItemID = row.Field<int>("CatalogItemID");
				//	CatalogItemTicket.
				//	DepartmentItemID.
				//	UserItemID.
				//	CityItemID.
				this.CityItemID = row.Field<int>("CityItemID");
				//	DateCreated.
				//	DateUpdated.
				//	DateViewed.
				//	Visible.
				this.Visible = Convert.ToBoolean(row.Field<int>("Visible"));
				//	ViewCount.
				//	ProductTitle.
				this.ProductTitle = row.Field<string>("ProductTitle");
				//	ProductDescription.
				this.ProductDescription = row.Field<string>("ProductDescription");
				//	ContactInfo.
				this.ContactInfo = row.Field<string>("ContactInfo");
				//	RatingCount.
				//	StarCount.
				this.StarCount = row.Field<double>("StarCount");
				//	ItemPrice.
				this.ItemPrice = row.Field<double>("ItemPrice");
				//	ItemUnit.
				this.ItemUnit = row.Field<string>("ItemUnit");
				this.CityName = row.Field<string>("CityName");
				this.DepartmentName = row.Field<string>("DepartmentName");
				this.FromUsername = row.Field<string>("MemberUsername");
				//	CityName.

				table = SQLHelper.GetTable(
					String.Format(
						ResourceMain.vwCatalogBulletForCatalogItemID,
						ToSql(this.CatalogItemID)));
				foreach(DataRow trow in table.Rows)
				{
					this.BulletPoints.Add(
						trow.Field<string>("BulletText"),
						trow.Field<int>("BulletIndex"),
						trow.Field<Guid>("CatalogBulletTicket"));
				}
				table = SQLHelper.GetTable(
					String.Format(
						ResourceMain.vwCatalogImageForCatalogItemID,
						ToSql(this.CatalogItemID)));
				foreach(DataRow trow in table.Rows)
				{
					this.Images.Add(
						FixCatalogItemImageURL(
						this.CatalogItemID, trow.Field<string>("ImageURL")),
						trow.Field<int>("ImageIndex"),
						trow.Field<Guid>("CatalogImageTicket"));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BulletPoints																													*
		//*-----------------------------------------------------------------------*
		private BulletPointCollection mBulletPoints = new BulletPointCollection();
		/// <summary>
		/// Get the collection of short descriptive points associated with this
		/// item.
		/// </summary>
		[JsonProperty(Order = 14)]
		public BulletPointCollection BulletPoints
		{
			get { return mBulletPoints; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CatalogItemID																													*
		//*-----------------------------------------------------------------------*
		private int mCatalogItemID = 0;
		/// <summary>
		/// Get/Set the local identification of this item.
		/// </summary>
		[JsonProperty(Order = 0)]
		public int CatalogItemID
		{
			get { return mCatalogItemID; }
			set { mCatalogItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CityItemID																														*
		//*-----------------------------------------------------------------------*
		private int mCityItemID = 0;
		/// <summary>
		/// Get/Set the ID of the market city for this item. Zero is default market
		/// for the user.
		/// </summary>
		[JsonProperty(Order = 1)]
		public int CityItemID
		{
			get { return mCityItemID; }
			set { mCityItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CityName																															*
		//*-----------------------------------------------------------------------*
		private string mCityName = "(No city selected)";
		/// <summary>
		/// Get/Set the name of the city in which the product is offered.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string CityName
		{
			get { return mCityName; }
			set { mCityName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ContactInfo																														*
		//*-----------------------------------------------------------------------*
		private string mContactInfo = "Big Hollow Food Co-Op, Laramie";
		/// <summary>
		/// Get/Set the contact information for this item.
		/// </summary>
		[JsonProperty(Order = 11)]
		public string ContactInfo
		{
			get { return mContactInfo; }
			set { mContactInfo = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DepartmentName																												*
		//*-----------------------------------------------------------------------*
		private string mDepartmentName = "";
		/// <summary>
		/// Get/Set the department name of this item.
		/// </summary>
		[JsonProperty(Order = 3)]
		public string DepartmentName
		{
			get { return mDepartmentName; }
			set { mDepartmentName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Editable																															*
		//*-----------------------------------------------------------------------*
		private bool mEditable = false;
		/// <summary>
		/// Get/Set a value indicating whether the item is editable.
		/// </summary>
		[JsonProperty(Order = 12)]
		public bool Editable
		{
			get { return mEditable; }
			set { mEditable = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FromUsername																													*
		//*-----------------------------------------------------------------------*
		private string mFromUsername = "";
		/// <summary>
		/// Get/Set the username presenting this item.
		/// </summary>
		[JsonProperty(Order = 10)]
		public string FromUsername
		{
			get { return mFromUsername; }
			set { mFromUsername = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetTable																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a data table containing the specified item data.
		/// </summary>
		/// <param name="item">
		/// Reference to a catalog item containing the information to save.
		/// </param>
		/// <returns>
		///	Data table, in the shape of CatalogItem, containing the values to
		///	write to the database.
		/// </returns>
		public static DataTable GetTable(CatalogItem item)
		{
			DataRow row = null;
			DataTable table = SQLHelper.GetTable(ResourceMain.vwCatalogItemBlank);
			
			if(table.Rows.Count > 0)
			{
				table.Rows.Clear();
				table.AcceptChanges();
			}
			if(item != null)
			{
				row = table.NewRow();
				//	CatalogItemID.
				row.SetField<int>("CatalogItemID", item.CatalogItemID);
				//	CatalogItemTicket.
				//	DepartmentItemID.
				row.SetField<int>("DepartmentItemID",
					ToDepartmentID(item.DepartmentName));
				//	UserItemID.
				//	CityItemID.
				row.SetField<int>("CityItemID", item.CityItemID);
				//	DateCreated.
				//	DateUpdated.
				row.SetField<DateTime>("DateUpdated", DateTime.Now);
				//	DateViewed.
				//	Visible.
				row.SetField<int>("Visible", (item.Visible ? 1 : 0));
				//	ViewCount.
				//	ProductTitle.
				row.SetField<string>("ProductTitle", item.ProductTitle);
				//	ProductDescription.
				row.SetField<string>("ProductDescription", item.ProductDescription);
				//	ContactInfo.
				row.SetField<string>("ContactInfo", item.ContactInfo);
				//	RatingCount.
				//	StarCount.
				//	ItemPrice.
				row.SetField<double>("ItemPrice", item.ItemPrice);
				//	ItemUnit.
				row.SetField<string>("ItemUnit", item.ItemUnit);
				table.Rows.Add(row);
				row.AcceptChanges();
			}
			return table;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Images																																*
		//*-----------------------------------------------------------------------*
		private ImageCollection mImages = new ImageCollection();
		/// <summary>
		/// Get the collection of URLs of the images associated with this item.
		/// </summary>
		[JsonProperty(Order = 13)]
		public ImageCollection Images
		{
			get { return mImages; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemPrice																															*
		//*-----------------------------------------------------------------------*
		private double mItemPrice = 0.0;
		/// <summary>
		/// Get/Set the per-unit price of this item.
		/// </summary>
		[JsonProperty(Order = 7)]
		public double ItemPrice
		{
			get { return mItemPrice; }
			set { mItemPrice = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemUnit																															*
		//*-----------------------------------------------------------------------*
		private string mItemUnit = "ea";
		/// <summary>
		/// Get/Set the measurement unit of this item.
		/// </summary>
		[JsonProperty(Order = 8)]
		public string ItemUnit
		{
			get { return mItemUnit; }
			set { mItemUnit = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ProductDescription																										*
		//*-----------------------------------------------------------------------*
		private string mProductDescription = "";
		/// <summary>
		/// Get/Set the description of the product.
		/// </summary>
		[JsonProperty(Order = 5)]
		public string ProductDescription
		{
			get { return mProductDescription; }
			set { mProductDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ProductTitle																													*
		//*-----------------------------------------------------------------------*
		private string mProductTitle = "";
		/// <summary>
		/// Get/Set the the title of the product.
		/// </summary>
		[JsonProperty(Order = 4)]
		public string ProductTitle
		{
			get { return mProductTitle; }
			set { mProductTitle = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeUserItemTicket																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the UserItemTicket property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if the UserItemTicket property should be serialized. Otherwise,
		/// false.
		/// </returns>
		public bool ShouldSerializeUserItemTicket()
		{
			bool result = false;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StarCount																															*
		//*-----------------------------------------------------------------------*
		private double mStarCount = 0;
		/// <summary>
		/// Get/Set the number of stars at which this item is rated.
		/// </summary>
		[JsonProperty(Order = 6)]
		public double StarCount
		{
			get { return mStarCount; }
			set { mStarCount = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UserItemTicket																												*
		//*-----------------------------------------------------------------------*
		private string mUserItemTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the user authorized to
		/// make this change.
		/// </summary>
		[JsonProperty(Order = 15)]
		public string UserItemTicket
		{
			get { return mUserItemTicket; }
			set { mUserItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Visible																																*
		//*-----------------------------------------------------------------------*
		private bool mVisible = true;
		/// <summary>
		/// Get/Set a value indicating whether this item is visible.
		/// </summary>
		[JsonProperty(Order = 9)]
		public bool Visible
		{
			get { return mVisible; }
			set { mVisible = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	CatalogMinCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of CatalogMinItem Items.
	/// </summary>
	public class CatalogMinCollection : List<CatalogMinItem>
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
		//*	Load																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the collection from a table containing CatalogItem minimum record
		/// values.
		/// </summary>
		/// <param name="table">
		/// Reference to the table containing CatalogItem minimum record values.
		/// </param>
		public void Load(DataTable table)
		{
			CatalogMinItem item = null;

			if(table?.Rows.Count > 0)
			{
				foreach(DataRow row in table.Rows)
				{
					item = new CatalogMinItem();
					item.CatalogItemID = row.Field<int>("CatalogItemID");
					item.CityName = row.Field<string>("CityName");
					item.DepartmentName = row.Field<string>("DepartmentName");
					item.ImageURL =
						FixCatalogItemImageURL(
							item.CatalogItemID, row.Field<string>("ImageURL"));
					item.ItemPrice = row.Field<double>("ItemPrice");
					item.ItemUnit = row.Field<string>("ItemUnit");
					item.ProductDescription = row.Field<string>("ProductDescription");
					item.ProductTitle = row.Field<string>("ProductTitle");
					item.StarCount = row.Field<double>("StarCount");
					this.Add(item);
				}
			}
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	CatalogMinItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Minimum amount of information available on an item.
	/// </summary>
	public class CatalogMinItem
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
		/// Get/Set the local identification of this item.
		/// </summary>
		[JsonProperty(Order = 0)]
		public int CatalogItemID
		{
			get { return mCatalogItemID; }
			set { mCatalogItemID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CityName																															*
		//*-----------------------------------------------------------------------*
		private string mCityName = "";
		/// <summary>
		/// Get/Set the name of the city.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string CityName
		{
			get { return mCityName; }
			set { mCityName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DepartmentName																												*
		//*-----------------------------------------------------------------------*
		private string mDepartmentName = "";
		/// <summary>
		/// Get/Set the department name of this item.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string DepartmentName
		{
			get { return mDepartmentName; }
			set { mDepartmentName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ImageURL																															*
		//*-----------------------------------------------------------------------*
		private string mImageURL = "";
		/// <summary>
		/// Get/Set the URL of the image associated with this item.
		/// </summary>
		[JsonProperty(Order = 8)]
		public string ImageURL
		{
			get { return mImageURL; }
			set { mImageURL = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemPrice																															*
		//*-----------------------------------------------------------------------*
		private double mItemPrice = 0.0;
		/// <summary>
		/// Get/Set the per-unit price of this item.
		/// </summary>
		[JsonProperty(Order = 6)]
		public double ItemPrice
		{
			get { return mItemPrice; }
			set { mItemPrice = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemUnit																															*
		//*-----------------------------------------------------------------------*
		private string mItemUnit = "ea";
		/// <summary>
		/// Get/Set the measurement unit of this item.
		/// </summary>
		[JsonProperty(Order = 7)]
		public string ItemUnit
		{
			get { return mItemUnit; }
			set { mItemUnit = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ProductDescription																										*
		//*-----------------------------------------------------------------------*
		private string mProductDescription = "";
		/// <summary>
		/// Get/Set the description of the product.
		/// </summary>
		[JsonProperty(Order = 4)]
		public string ProductDescription
		{
			get { return mProductDescription; }
			set { mProductDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ProductTitle																													*
		//*-----------------------------------------------------------------------*
		private string mProductTitle = "";
		/// <summary>
		/// Get/Set the the title of the product.
		/// </summary>
		[JsonProperty(Order = 3)]
		public string ProductTitle
		{
			get { return mProductTitle; }
			set { mProductTitle = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StarCount																															*
		//*-----------------------------------------------------------------------*
		private double mStarCount = 0;
		/// <summary>
		/// Get/Set the number of stars at which this item is rated.
		/// </summary>
		[JsonProperty(Order = 5)]
		public double StarCount
		{
			get { return mStarCount; }
			set { mStarCount = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
