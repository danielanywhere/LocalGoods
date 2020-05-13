//	BulletPoint.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using static LocalGoods.SQLHelper;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	BulletIDCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of BulletIDItem Items.
	/// </summary>
	public class BulletIDCollection : List<BulletIDItem>
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
	//*	BulletIDItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Minimal ID and text information about a bullet point.
	/// </summary>
	public class BulletIDItem
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
		//*	BulletID																															*
		//*-----------------------------------------------------------------------*
		private int mBulletID = 0;
		/// <summary>
		/// Get/Set the Local identification of the record.
		/// </summary>
		[JsonProperty(Order = 0)]
		public int BulletID
		{
			get { return mBulletID; }
			set { mBulletID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BulletText																														*
		//*-----------------------------------------------------------------------*
		private string mBulletText = "";
		/// <summary>
		/// Get/Set the text of the point.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string BulletText
		{
			get { return mBulletText; }
			set { mBulletText = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	BulletPointCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of BulletPointItem Items.
	/// </summary>
	public class BulletPointCollection : List<BulletPointItem>
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
		/// Add a new bullet point to the collection by member values.
		/// </summary>
		/// <param name="text">
		/// Bullet point to add.
		/// </param>
		/// <param name="index">
		/// Relative index of the item to add.
		/// </param>
		/// <param name="ticket">
		/// Globally unique identification of the entry.
		/// </param>
		/// <returns>
		/// Newly created and added bullet point.
		/// </returns>
		public BulletPointItem Add(string text, int index = 0, Guid? ticket = null)
		{
			BulletPointItem item = new BulletPointItem();

			if(index == 0)
			{
				item.BulletIndex = this.Count();
			}
			else
			{
				item.BulletIndex = index;
			}
			item.BulletText = text;
			if(ticket != null)
			{
				item.BulletTicket = ((Guid)ticket).ToString("D");
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
		/// Collection of bullet point items.
		/// </param>
		/// <param name="catalogItemID">
		/// The catalog item ID to associate with the items.
		/// </param>
		/// <returns>
		/// Data table containing the specified bullet point items.
		/// </returns>
		public static DataTable GetTable(List<BulletPointItem> items,
			int catalogItemID)
		{
			object[] fields = null;
			//DataRow row = null;
			DataTable table =
				SQLHelper.GetTable(ResourceMain.vwCatalogBulletBlank);

			if(table.Rows.Count > 0)
			{
				table.Rows.Clear();
				table.AcceptChanges();
			}
			table.Columns.RemoveAt(0);		//	Record ID not used here.
			foreach(BulletPointItem item in items)
			{
				fields = new object[4];
				try
				{
					fields[0] = Guid.Parse(item.BulletTicket);
				}
				catch { }
				fields[1] = catalogItemID;
				fields[2] = item.BulletIndex;
				fields[3] = item.BulletText;
				table.LoadDataRow(fields, true);
			}
			return table;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToBulletIDCollection																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert a collection of bullet points to a bullet ID collection.
		/// </summary>
		/// <param name="bullets">
		/// Collection of bullet points to convert.
		/// </param>
		public static BulletIDCollection ToBulletIDCollection(
			BulletPointCollection bullets)
		{
			BulletIDItem bullet = null;
			BulletIDCollection result = new BulletIDCollection();
			DataTable table = null;

			if(bullets?.Count > 0)
			{
				table = SQLHelper.GetTable(
					String.Format(ResourceMain.vwCatalogBulletForTicketList,
					string.Join(",",
					bullets.Select(x => ToSql(Guid.Parse(x.BulletTicket))))));
				foreach(DataRow row in table.Rows)
				{
					bullet = new BulletIDItem();
					bullet.BulletID = row.Field<int>("CatalogBulletID");
					bullet.BulletText = row.Field<string>("BulletText");
					result.Add(bullet);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	BulletPointItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a bullet point.
	/// </summary>
	public class BulletPointItem
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
		//*	BulletIndex																														*
		//*-----------------------------------------------------------------------*
		private int mBulletIndex = 0;
		/// <summary>
		/// Get/Set the relative index of this bullet in the current topic.
		/// </summary>
		[JsonProperty(Order = 0)]
		public int BulletIndex
		{
			get { return mBulletIndex; }
			set { mBulletIndex = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BulletText																														*
		//*-----------------------------------------------------------------------*
		private string mBulletText = "";
		/// <summary>
		/// Get/Set the text of the point.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string BulletText
		{
			get { return mBulletText; }
			set { mBulletText = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BulletTicket																													*
		//*-----------------------------------------------------------------------*
		private string mBulletTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of this item.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string BulletTicket
		{
			get { return mBulletTicket; }
			set { mBulletTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
