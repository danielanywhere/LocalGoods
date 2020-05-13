//	LocalGoodsTools.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

using static LocalGoods.SQLHelper;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	LocalGoodsTools																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Common tools for use with the LocalGoods application.
	/// </summary>
	public class LocalGoodsTools
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
		//*	ConvertFileToImage																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the temporary file for a caller's image to a local image in the
		/// catalog folder.
		/// </summary>
		/// <param name="image">
		/// Reference to the image information to be converted.
		/// </param>
		/// <param name="catalogItemID">
		/// The catalog item to which this image will be associated.
		/// </param>
		/// <returns>
		/// Value indicating whether the conversion was successful.
		/// </returns>
		/// <remarks>
		/// The syntax of the catalog image name is
		/// i{CatalogItemID}-{ImageTicket}.{Ext}, where {CatalogItemID} is
		/// the unique local identification of the catalog, {ImageTicket} is
		/// the globally unique identification of the image, formatted with "D",
		/// and {Ext} is the extension name of the file, such as 'png', etc.
		/// </remarks>
		public static bool ConvertFileToImage(ImageItem image, int catalogItemID)
		{
			bool bContinue = true;
			Bitmap bitmap = null;
			Bitmap bitmapo = null;
			string content = "";
			byte[] data = new byte[0];
			string ext = "";
			FileInfo file = null;
			Graphics graph = null;
			double height = 0.0;
			string name = "";
			bool result = false;
			double scale = 0.0;
			string sql = "";
			Guid ticket = Guid.Empty;
			double width = 0.0;

			if(image != null && catalogItemID != 0)
			{
				if(image.ImageData.EndsWith(".tmp"))
				{
					//	File data has been uploaded to a temporary file.
					bContinue = false;
					try
					{
						//	File ticket.
						ticket = Guid.Parse(
							image.ImageData.Substring(0, image.ImageData.Length - 4));
						bContinue = true;
					}
					catch { }
					if(bContinue)
					{
						//	File data.
						bContinue = false;
						file = new FileInfo(HostingEnvironment.MapPath(
							$"~/images/CatalogTemp/{image.ImageData}"));
						if(file.Exists)
						{
							//	The file was found. The content is base64, and has a type
							//	prefix.
							content = File.ReadAllText(file.FullName);
							if(content.Contains(","))
							{
								content = content.Split(new char[] { ',' })[1];
							}
							data = Convert.FromBase64String(content);
							bContinue = data.Length > 0;
						}
					}
					if(bContinue)
					{
						//	Extension.
						bContinue = false;
						if(image.ImageURL.Contains("."))
						{
							ext =
								image.ImageURL.Substring(image.ImageURL.LastIndexOf(".") + 1);
							bContinue = ext.Length > 0;
						}
					}
					if(bContinue)
					{
						//	Check file size for maximum of 1024 width / height.
						bContinue = false;
						using(var memStream = new MemoryStream(data))
						{
							bitmap = new Bitmap(memStream);
						}
						if(bitmap != null &&
							bitmap.Width > 1024 || bitmap.Height > 1024)
						{
							if(bitmap.Width >= bitmap.Height)
							{
								//	Use the width as the shrinking factor.
								scale = 1024.0 / (double)bitmap.Width;
							}
							else
							{
								//	Use the height as the shrinking factor.
								scale = 1024.0 / (double)bitmap.Height;
							}
							//	Scaling needed.
							width = (double)bitmap.Width * scale;
							height = (double)bitmap.Height * scale;
							bitmapo = new Bitmap((int)width, (int)height);
							graph = Graphics.FromImage(bitmapo);
							graph.InterpolationMode =
								System.Drawing.Drawing2D.InterpolationMode.High;
							graph.CompositingQuality =
								System.Drawing.Drawing2D.CompositingQuality.HighQuality;
							graph.SmoothingMode =
								System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
							graph.DrawImage(bitmap, 0, 0, bitmapo.Width, bitmapo.Height);
							graph.Dispose();
							using(var memStream = new MemoryStream())
							{
								bitmapo.Save(memStream,
									System.Drawing.Imaging.ImageFormat.Png);
								ext = "png";
								data = memStream.ToArray();
								bContinue = true;
							}
						}
						else
						{
							//	No updates needed.
							bContinue = true;
						}
					}
					if(bContinue)
					{
						//	Write file data.
						name = $"i{catalogItemID}-{ticket.ToString("D")}.{ext}";
						try
						{
							File.WriteAllBytes(HostingEnvironment.MapPath(
								"~/images/CatalogItems/" + name), data);
							bContinue = true;
						}
						catch { }
					}
					if(bContinue)
					{
						//	Delete the temporary file.
						try
						{
							File.Delete(file.FullName);
						}
						catch { }
					}
					if(bContinue)
					{
						//	Write database record.
						sql = String.Format(
							ResourceMain.insCatalogImageWithTicket,
							ToSql(ticket), ToSql(catalogItemID), ToSql(image.ImageIndex),
							ToSql($"{ticket.ToString("D")}.{ext}"));
						Update(sql);
						result = true;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EmailIsValid																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified email address is valid.
		/// </summary>
		public static bool EmailIsValid(string email)
		{
			Match match = Regex.Match(email, ResourceMain.rxValidEmail);
			return match.Success;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FixCatalogItemImageURL																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a catalog item image URL, adjusted for the location of images
		/// on this application.
		/// </summary>
		/// <param name="catalogItemID">
		/// Catalog item record ID.
		/// </param>
		/// <param name="imageURL">
		/// Base name or URL of the image.
		/// </param>
		/// <returns>
		/// The URL of the image, as accessible by pages at the root folder of
		/// this application.
		/// </returns>
		public static string FixCatalogItemImageURL(
			int catalogItemID, string imageURL)
		{
			bool hasHeader = false;
			string result = "";

			if(imageURL?.Length > 0)
			{
				hasHeader = imageURL.IndexOf(':') > -1 || imageURL.IndexOf('/') > -1;
				if(hasHeader)
				{
					//	The item has a header and is assumed to be a fully qualified
					//	link.
					result = imageURL;
				}
				else
				{
					//	Ths item doesn't have a header and is assumed to be a
					//	simple filename.
					result = "images/CatalogItems/i" +
						$"{catalogItemID}-{imageURL}";
				}
			}

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetCatalogSearchResultList																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a list of catalog item IDs matching the specified search term.
		/// </summary>
		/// <param name="searchTerm">
		/// Clear text search term.
		/// </param>
		/// <returns>
		/// List of catalog item IDs matching the provided search term.
		/// </returns>
		public static List<int> GetCatalogSearchResultList(string searchTerm)
		{
			List<int> catalogItemIDs = new List<int>();
			int id = 0;
			List<int> idList = new List<int>();
			DataTable idTable = null;
			DataTable ieTable = null;
			KeywordCollection keywords = null;
			DataRow[] rows = null;

			if(searchTerm?.Length > 0)
			{
				keywords = GetSearchWords(searchTerm);
				if(keywords.Count > 0)
				{
					//	Create a list of all keywords to search for.
					idList.Clear();
					foreach(KeywordItem keyword in keywords)
					{
						idList.Add(keyword.KeywordID);
					}
					//	List of matching record IDs for keywords.
					idTable = GetTable(
						String.Format(ResourceMain.vwRecordIDsForKeywordIDs,
						string.Join(",", idList.Select(x => ToSql(x)))));
					//	Catalog items.
					rows = idTable.Select("TableIndexID=3");
					if(rows.Length > 0)
					{
						//	Catalog Item IDs.
						foreach(DataRow row in rows)
						{
							id = row.Field<int>("RecordID");
							if(!catalogItemIDs.Exists(x => x == id))
							{
								catalogItemIDs.Add(id);
							}
						}
					}
					//	Bullets.
					rows = idTable.Select("TableIndexID=1");
					if(rows.Length > 0)
					{
						//	Resolve bullet IDs.
						idList.Clear();
						foreach(DataRow row in rows)
						{
							idList.Add(row.Field<int>("RecordID"));
						}
						ieTable = GetTable(
							String.Format(ResourceMain.vwCatalogItemIDsForBulletIDs,
							string.Join(",", idList.Select(x => ToSql(x)))));
						foreach(DataRow row in ieTable.Rows)
						{
							id = row.Field<int>("CatalogItemID");
							if(!catalogItemIDs.Exists(x => x == id))
							{
								catalogItemIDs.Add(id);
							}
						}
					}
				}
			}
			return catalogItemIDs;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetSearchWords																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection of basic search terms and their associated keyword
		/// IDs from the caller's string.
		/// expression.
		/// </summary>
		/// <param name="value">
		/// String expression to partition.
		/// </param>
		/// <returns>
		/// Collection of keywords found in the caller's statement.
		/// </returns>
		public static KeywordCollection GetSearchWords(string value)
		{
			StringBuilder builder = new StringBuilder();
			int count = 0;
			int index = 0;
			MatchCollection matches = null;
			KeywordItem keyword = null;
			KeywordCollection keywords = new KeywordCollection();
			DataTable table = null;
			string word = "";

			matches = Regex.Matches(value, ResourceMain.rxBasicSearchTerm);
			foreach(Match match in matches)
			{
				word = GetValue(match, "word").ToLower();
				if(!keywords.Exists(x => x.Keyword == word))
				{
					keywords.Add(word);
				}
			}
			//	All of the unique word shapes have been added.
			if(keywords.Count > 0)
			{
				table = GetTable(
					String.Format(ResourceMain.vwKeywordsForList,
					String.Join(",", keywords.Select(x => ToSql(x.Keyword)))));
				foreach(DataRow row in table.Rows)
				{
					keyword = keywords.FirstOrDefault(x =>
						x.Keyword == row.Field<string>("KeywordName"));
					if(keyword != null)
					{
						keyword.KeywordID = row.Field<int>("KeywordID");
					}
				}
			}
			//	Remove all unassigned words.
			count = keywords.Count;
			for(index = 0; index < count; index++)
			{
				keyword = keywords[index];
				if(keyword.KeywordID == 0)
				{
					keywords.RemoveAt(index);
					index--;  //	Deindex.
					count--;  //	Decount.
				}
			}
			return keywords;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetValue																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the value of the specified group from the caller's match.
		/// </summary>
		/// <param name="match">
		/// Reference to the match to be inspected.
		/// </param>
		/// <param name="group">
		/// Name of the group whose content will be returned.
		/// </param>
		/// <returns>
		/// Content of the specified group, if found. Otherwise, and empty string.
		/// </returns>
		public static string GetValue(Match match, string group)
		{
			string result = "";

			if(match != null && match.Success)
			{
				if(match.Groups[group] != null && match.Groups[group].Value != null)
				{
					result = match.Groups[group].Value;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpdateKeywordIndex																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the keyword index for the specified catalog item and associated
		/// elements.
		/// </summary>
		/// <param name="model">
		/// Reference to the catalog item to inspect.
		/// </param>
		public static void UpdateKeywordIndex(CatalogItem model)
		{
			BulletIDCollection bullets = null;
			List<object> cells = new List<object>();
			List<string> columns = new List<string>();
			int count = 0;
			int id = 0;
			List<int> idList = new List<int>();
			int index = 0;
			KeywordCollection keywords = null;
			MatchCollection matches = null;
			List<KeywordItem> newItems = null;
			DataTable table = null;
			List<string> values = new List<string>();
			string word = "";

			if(model != null && model.CatalogItemID != 0)
			{
				bullets =
					BulletPointCollection.ToBulletIDCollection(model.BulletPoints);
				//	Update the master keywords list.
				values.Add(model.ContactInfo);
				values.Add(model.DepartmentName);
				values.Add(model.FromUsername);
				values.Add(model.ItemUnit);
				values.Add(model.ProductDescription);
				values.Add(model.ProductTitle);
				foreach(BulletIDItem bullet in bullets)
				{
					values.Add(bullet.BulletText);
				}
				table = GetTable(ResourceMain.vwKeywords);
				keywords = new KeywordCollection();
				foreach(DataRow row in table.Rows)
				{
					keywords.Add(
						row.Field<string>("KeywordName"), row.Field<int>("KeywordID"));
				}
				matches = Regex.Matches(string.Join(" ", values),
					ResourceMain.rxBasicSearchTerm);
				foreach(Match match in matches)
				{
					word = GetValue(match, "word").ToLower();
					if(!keywords.Exists(x => x.Keyword == word))
					{
						keywords.Add(word);
					}
				}
				//	Find all newly added words.
				newItems = keywords.FindAll(x => x.KeywordID == 0);
				if(newItems.Count > 0)
				{
					columns = new List<string>();
					cells = new List<object>();
					cells.Add(ToSql(null));
					columns.Add("KeywordName");
					foreach(KeywordItem keywordi in newItems)
					{
						cells[0] = ToSql(keywordi.Keyword);
						InsertRecord("Keyword", columns, cells);
					}
				}
				//	Update the keyword references for the main record.
				keywords.Clear();
				values.Clear();
				values.Add(model.ContactInfo);
				values.Add(model.DepartmentName);
				values.Add(model.FromUsername);
				values.Add(model.ItemUnit);
				values.Add(model.ProductDescription);
				values.Add(model.ProductTitle);
				matches = Regex.Matches(string.Join(" ", values),
					ResourceMain.rxBasicSearchTerm);
				foreach(Match match in matches)
				{
					word = GetValue(match, "word").ToLower();
					if(!keywords.Exists(x => x.Keyword == word))
					{
						keywords.Add(word);
					}
				}
				table = GetTable(String.Format(ResourceMain.vwKeywordsForList,
					string.Join(",", keywords.Select(x => ToSql(x.Keyword)))));
				idList.Clear();
				foreach(DataRow row in table.Rows)
				{
					idList.Add(row.Field<int>("KeywordID"));
				}
				//	Delete all of the keyword references that no longer apply on the
				//	main catalog.
				Update(
					String.Format(
						ResourceMain.delKeywordLocationCatalogItemNotInIDList,
						ToSql(model.CatalogItemID),
						string.Join(",", idList.Select(x => ToSql(x)))));
				//	Retrieve the list of references that are known.
				table = GetTable(
					String.Format(
						ResourceMain.vwKeywordLocationCatalogItemInIDList,
						ToSql(model.CatalogItemID),
						string.Join(",", idList.Select(x => ToSql(x)))));
				count = idList.Count;
				foreach(DataRow row in table.Rows)
				{
					id = row.Field<int>("KeywordID");
					for(index = 0; index < count; index++)
					{
						if(idList[index] == id)
						{
							//	This item exists.
							idList.RemoveAt(index);
							count--;
							break;
						}
					}
				}
				//	The only items remaining in idList are those needing to be
				//	inserted.
				if(idList.Count > 0)
				{
					columns.Clear();
					columns.Add("KeywordID");
					columns.Add("TableIndexID");
					columns.Add("RecordID");
					cells.Clear();
					cells.Add(ToSql((int)0));
					cells.Add(ToSql((int)3));
					cells.Add(model.CatalogItemID);
					foreach(int ivalue in idList)
					{
						cells[0] = ToSql(ivalue);
						InsertRecord("KeywordLocation", columns, cells);
					}
				}
				//	Process each bullet independently.
				foreach(BulletIDItem bullet in bullets)
				{
					keywords.Clear();
					matches = Regex.Matches(bullet.BulletText,
						ResourceMain.rxBasicSearchTerm);
					foreach(Match match in matches)
					{
						word = GetValue(match, "word").ToLower();
						if(!keywords.Exists(x => x.Keyword == word))
						{
							keywords.Add(word);
						}
					}
					table = GetTable(String.Format(ResourceMain.vwKeywordsForList,
						string.Join(",", keywords.Select(x => ToSql(x.Keyword)))));
					idList.Clear();
					foreach(DataRow row in table.Rows)
					{
						idList.Add(row.Field<int>("KeywordID"));
					}
					//	Delete all of the keyword references that no longer apply on the
					//	bullet.
					Update(
						String.Format(
							ResourceMain.delKeywordLocationBulletItemNotInIDList,
							ToSql(bullet.BulletID),
							string.Join(",", idList.Select(x => ToSql(x)))));
					//	Retrieve the list of references that are known.
					table = GetTable(
						String.Format(
							ResourceMain.vwKeywordLocationBulletItemInIDList,
							ToSql(bullet.BulletID),
							string.Join(",", idList.Select(x => ToSql(x)))));
					count = idList.Count;
					foreach(DataRow row in table.Rows)
					{
						id = row.Field<int>("KeywordID");
						for(index = 0; index < count; index++)
						{
							if(idList[index] == id)
							{
								//	This item exists.
								idList.RemoveAt(index);
								count--;
								break;
							}
						}
					}
					//	The only items remaining in idList are those needing to be
					//	inserted.
					if(idList.Count > 0)
					{
						columns.Clear();
						columns.Add("KeywordID");
						columns.Add("TableIndexID");
						columns.Add("RecordID");
						cells.Clear();
						cells.Add(ToSql((int)0));
						cells.Add(ToSql((int)1));
						cells.Add(bullet.BulletID);
						foreach(int ivalue in idList)
						{
							cells[0] = ToSql(ivalue);
							InsertRecord("KeywordLocation", columns, cells);
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
