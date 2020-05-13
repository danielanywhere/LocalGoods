//	Image.cs
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

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	ImageCollection																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of ImageItem Items.
	/// </summary>
	public class ImageCollection : List<ImageItem>
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
		//*	Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a new image to the collection by member values.
		/// </summary>
		/// <param name="name">
		/// Image URL to add.
		/// </param>
		/// <param name="index">
		/// Index of the item.
		/// </param>
		/// <param name="ticket">
		/// Globally unique identification of the image.
		/// </param>
		/// <returns>
		/// Newly created and added image item.
		/// </returns>
		public ImageItem Add(string name, int index = 0, Guid? ticket = null)
		{
			ImageItem item = new ImageItem();

			if(index == 0)
			{
				item.ImageIndex = this.Count();
			}
			else
			{
				item.ImageIndex = index;
			}
			item.ImageURL = name;
			if(ticket != null)
			{
				item.ImageTicket = ((Guid)ticket).ToString("D");
			}
			this.Add(item);
			return item;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetTable																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a data table containing the values of the specified collection.
		/// </summary>
		/// <param name="items">
		/// Collection of image items.
		/// </param>
		/// <param name="catalogItemID">
		/// The catalog item ID to associate with the items.
		/// </param>
		/// <returns>
		/// Data table containing the specified image items.
		/// </returns>
		public static DataTable GetTable(List<ImageItem> items,
			int catalogItemID)
		{
			object[] fields = null;
			//DataRow row = null;
			DataTable table =
				SQLHelper.GetTable(ResourceMain.vwCatalogImageBlank);

			if(table.Rows.Count > 0)
			{
				table.Rows.Clear();
				table.AcceptChanges();
			}
			table.Columns.RemoveAt(0);    //	Record ID not used here.
			foreach(ImageItem item in items)
			{
				fields = new object[4];
				try
				{
					fields[0] = Guid.Parse(item.ImageTicket);
				}
				catch { }
				fields[1] = catalogItemID;
				fields[2] = item.ImageIndex;
				fields[3] = item.ImageURL;
				table.LoadDataRow(fields, true);
			}
			return table;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ImageItem																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about an image.
	/// </summary>
	public class ImageItem
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
		//*	ImageData																															*
		//*-----------------------------------------------------------------------*
		private string mImageData = "";
		/// <summary>
		/// Get/Set the base64 binary data associated with the image.
		/// </summary>
		/// <remarks>
		/// When ImageTicket.Length == 0 and ImageData.Length > 0, a new image
		/// is present.
		/// </remarks>
		public string ImageData
		{
			get { return mImageData; }
			set { mImageData = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ImageIndex																														*
		//*-----------------------------------------------------------------------*
		private int mImageIndex = 0;
		/// <summary>
		/// Get/Set the relative index of the item in the collection.
		/// </summary>
		[JsonProperty(Order = 0)]
		public int ImageIndex
		{
			get { return mImageIndex; }
			set { mImageIndex = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ImageTicket																														*
		//*-----------------------------------------------------------------------*
		private string mImageTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of this image.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string ImageTicket
		{
			get { return mImageTicket; }
			set { mImageTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ImageURL																															*
		//*-----------------------------------------------------------------------*
		private string mImageURL = "";
		/// <summary>
		/// Get/Set the URL of the image.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string ImageURL
		{
			get { return mImageURL; }
			set { mImageURL = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ShouldSerializeImageData																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the ImageData property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if the ImageData property will be serialized. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeImageData()
		{
			bool result = (mImageData?.Length > 0);
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
