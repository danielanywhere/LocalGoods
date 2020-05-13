//	CatalogItemController.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

using static LocalGoods.LocalGoodsTools;
using static LocalGoods.SQLHelper;

namespace LocalGoods.Controllers
{
	//*-------------------------------------------------------------------------*
	//*	CatalogItemController																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Web API2 controller for catalog items.
	/// </summary>
	public class CatalogItemController : ApiController
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
		//* PostCatalogItemCreate																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new catalog item record for the specified user.
		/// </summary>
		/// <param name="model">
		/// </param>
		/// <returns>
		/// </returns>
		[Route("api/v1/catalogitemcreate")]
		public IHttpActionResult PostCatalogItemCreate(CatalogIDUserTicket model)
		{
			List<object> cells = new List<object>();
			List<string> columns = new List<string>();
			CatalogIDUserTicket result = new CatalogIDUserTicket();
			Guid ticket = Guid.Empty;
			int userID = 0;

			if(model != null && model.UserItemTicket?.Length > 0)
			{
				try
				{
					ticket = Guid.Parse(model.UserItemTicket);
				}
				catch { }
				if(ticket != Guid.Empty)
				{
					//	Valid user ticket presented. Get the User ID.
					userID = GetScalarInt(
						String.Format(ResourceMain.vwUserItemIDForTicket,
						ToSql(ticket)));
					if(userID != 0)
					{
						//	User found.
						//	Create the new record early to get the reference ID.
						//	Precreate the ticket to give us the reference for the
						//	new record without using SQL Server variables.
						result.UserItemTicket = ticket.ToString("D");
						ticket = Guid.NewGuid();
						columns.Clear();
						columns.Add("CatalogItemTicket");
						columns.Add("UserItemID");
						cells.Clear();
						cells.Add(ToSql(ticket));
						cells.Add(ToSql(userID));
						InsertRecord("CatalogItem", columns, cells);
						//	Get the CatalogItemID.
						result.CatalogItemID = GetScalarInt(
							String.Format(
								ResourceMain.vwCatalogItemIDForTicket, ToSql(ticket)));
					}
				}
			}
			return Ok(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PostCatalogItemRetrieve																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the full information for the catalog item page.
		/// </summary>
		/// <param name="model">
		/// Command-line parameters.
		/// </param>
		/// <returns>
		/// Reference to an IHttpActionResult containing a CatalogItem
		/// with matching information.
		/// </returns>
		[Route("api/v1/catalogitem")]
		public IHttpActionResult PostCatalogItemRetrieve(CatalogIDUserTicket model)
		{
			CatalogItem result = null;
			DataRow row = null;
			DataTable table =
				GetTable(
					String.Format(
						ResourceMain.vwCatalogItemForID,
						ToSql(model.CatalogItemID)));
			Guid ticket = Guid.Empty;

			if(table.Rows.Count > 0)
			{
				row = table.Rows[0];
				result = new CatalogItem(row);
				//	Process user ticket.
				if(model.UserItemTicket?.Length > 0)
				{
					try
					{
						ticket = Guid.Parse(model.UserItemTicket);
					}
					catch { }
					if(ticket != Guid.Empty)
					{
						//	Valid guid.
						if(GetScalarBool(
							String.Format(
								ResourceMain.vwUserIDMatchesUserTicket,
								ToSql(row.Field<int>("UserItemID")),
								ToSql(ticket))))
						{
							result.Editable = true;
						}
					}
				}
				if(!result.Visible)
				{
					//	If the item is invisible, then only the editing user is allowed
					//	to view it.
					if(!result.Editable)
					{
						//	The item was not allowed. Delete the info.
						result.BulletPoints.Clear();
						result.CatalogItemID = 0;
						result.CityItemID = 0;
						result.CityName = "";
						result.DepartmentName = "";
						result.Editable = false;
						result.FromUsername = "";
						result.ContactInfo = "";
						result.Images.Clear();
						result.ItemPrice = 0.0;
						result.ItemUnit = "";
						result.ProductDescription = "";
						result.ProductTitle = "Item not found";
						result.StarCount = 0.0;
						result.Visible = true;
					}
				}
			}
			if(result == null)
			{
				//	Return a default item.
				result = new CatalogItem();
				result.ProductTitle = "Item not found";
				result.StarCount = 0.0;
				result.Visible = true;
			}
			return Ok(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PostCatalogItemSave																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Save the changes to the provided catalog item, if authorized.
		/// </summary>
		/// <param name="item">
		/// Reference to the catalog item to save.
		/// </param>
		/// <returns>
		/// OK.
		/// </returns>
		[Route("api/v1/catalogitemsave")]
		public IHttpActionResult PostCatalogItemSave(CatalogItem model)
		{
			bool authorized = false;
			BulletPointItem bullet = null;
			List<BulletPointItem> bullets = null;
			List<string> columns = new List<string>();
			int count = 0;
			DirectoryInfo dir = null;
			FileInfo file = null;
			FileInfo[] files = null;
			ImageItem image = null;
			List<ImageItem> images = null;
			int index = 0;
			MessageItem message = new MessageItem();
			int rowIndex = 0;
			DataTable table = null;
			Guid ticket = Guid.Empty;
			List<Guid> ticketList = new List<Guid>();
			List<object> values = new List<object>();

			if(model != null && model.CatalogItemID != 0)
			{
				//	Verify the user is allowed to make a change to this catalog
				//	item.
				if(model.UserItemTicket?.Length > 0)
				{
					try
					{
						ticket = Guid.Parse(model.UserItemTicket);
					}
					catch { }
					if(model.CatalogItemID != 0)
					{
						//	Existing catalog item.
						authorized = GetScalarBool(
							String.Format(
								ResourceMain.vwUserItemTicketAuthorizedForCatalogItemID,
								ToSql(ticket), ToSql(model.CatalogItemID)));
					}
					else
					{
						////	New catalog item.
						////	Any user is allowed to add new items.
						////	Create the new record early to get the reference ID.
						////	Precreate the ticket to give us the reference for the
						////	new record without using SQL Server variables.
						//ticket = Guid.NewGuid();
						//columns.Clear();
						//columns.Add("CatalogItemTicket");
						//values.Clear();
						//values.Add(ToSql(ticket));
						//InsertRecord("CatalogItem", columns, values);
						////	Get the CatalogItemID.
						//model.CatalogItemID = GetScalarInt(
						//	String.Format(
						//		ResourceMain.vwCatalogItemIDForTicket, ToSql(ticket)));
						//authorized = true;
					}
				}
				if(authorized)
				{
					//	This user is allowed to change the item.
					//	Process deleted bullet points.
					table = GetTable(
						String.Format(
							ResourceMain.vwCatalogBulletForCatalogItemID,
							ToSql(model.CatalogItemID)));
					ticketList.Clear();
					foreach(DataRow row in table.Rows)
					{
						bullet = model.BulletPoints.FirstOrDefault(x =>
							x.BulletTicket ==
							row.Field<Guid>("CatalogBulletTicket").ToString("D"));
						if(bullet == null)
						{
							//	The client's item has been deleted.
							ticketList.Add(row.Field<Guid>("CatalogBulletTicket"));
						}
					}
					if(ticketList.Count > 0)
					{
						//	Delete the keyword locations for the bullets.
						Update(
							String.Format(
								ResourceMain.delKeywordLocationBulletForTicketList,
								string.Join(",", ticketList.Select(i => ToSql(i)))));
						//	Delete the actual bullet records.
						Update(
							String.Format(
								ResourceMain.delCatalogBulletForTicketList,
								string.Join(",", ticketList.Select(i => ToSql(i)))));
					}
					//	Process deleted images.
					table = GetTable(
						String.Format(
							ResourceMain.vwCatalogImageForCatalogItemID,
							ToSql(model.CatalogItemID)));
					ticketList.Clear();
					foreach(DataRow row in table.Rows)
					{
						image = model.Images.FirstOrDefault(x =>
							x.ImageTicket ==
							row.Field<Guid>("CatalogImageTicket").ToString("D"));
						if(image == null)
						{
							//	The client's item has been deleted.
							ticketList.Add(row.Field<Guid>("CatalogImageTicket"));
						}
					}
					if(ticketList.Count > 0)
					{
						Update(
							String.Format(
								ResourceMain.delCatalogImageForTicketList,
								string.Join(",", ticketList.Select(i => ToSql(i)))));
						dir = new DirectoryInfo(
							HostingEnvironment.MapPath("~/images/CatalogItems/"));
						if(dir.Exists)
						{
							foreach(Guid ticketi in ticketList)
							{
								files =
									dir.GetFiles(
										$"i{model.CatalogItemID}-{ticketi.ToString("D")}.*");
								count = files.Length;
								for(index = 0; index < count; index++)
								{
									file = files[index];
									try
									{
										file.Delete();
									}
									catch { }
								}
							}
						}
					}
					//	Reorder the bullet index according to placement.
					index = 0;
					foreach(BulletPointItem bulleti in model.BulletPoints)
					{
						bulleti.BulletIndex = index++;
					}
					//	Add any new bullet points.
					//	These items do not have associated tickets.
					bullets = model.BulletPoints.FindAll(x =>
						x.BulletTicket == null || x.BulletTicket.Length == 0);
					if(bullets?.Count > 0)
					{
						table =
							BulletPointCollection.GetTable(bullets, model.CatalogItemID);
						//	In this version, ticket will be pre-defined so we can perform
						//	indexing on the new bullet.
						columns.Clear();
						foreach(DataColumn column in table.Columns)
						{
							columns.Add(column.ColumnName);
						}
						count = table.Columns.Count;
						rowIndex = 0;
						foreach(DataRow row in table.Rows)
						{
							values.Clear();
							ticket = Guid.NewGuid();
							bullets[rowIndex].BulletTicket = ticket.ToString("D");
							row[0] = ticket;
							values.Add(ToSql(row[0]));
							for(index = 1; index < count; index ++)
							{
								values.Add(ToSql(row[index]));
							}
							InsertRecord("CatalogBullet", columns, values);
							rowIndex++;
						}
					}

					//	Reorder the image index according to placement.
					index = 0;
					foreach(ImageItem imagei in model.Images)
					{
						imagei.ImageIndex = index++;
					}
					//	Add any new images.
					//	These items do not have associated tickets.
					images = model.Images.FindAll(x =>
						(x.ImageTicket == null || x.ImageTicket.Length == 0) &&
						x.ImageData != null && x.ImageData.EndsWith(".tmp"));
					if(images?.Count > 0)
					{
						//	At this stage, image data field is the filename of the
						//	temporary file that was saved prior to calling this method.
						//	Convert the image to user image file.
						count = images.Count;
						for(index = 0; index < count; index ++)
						{
							//	Convert each image to binary and store its record in
							//	the database.
							image = images[index];
							if(!ConvertFileToImage(image, model.CatalogItemID))
							{
								//	This item could not be converted.
								images.RemoveAt(index);
								index--;    //	Deindex.
								count--;		//	Decount.
							}
						}
						//if(images.Count > 0)
						//{
						//	table =
						//		ImageCollection.GetTable(images, model.CatalogItemID);
						//	table.Columns.RemoveAt(0);
						//	foreach(DataRow row in table.Rows)
						//	{
						//		row.SetAdded();
						//	}
						//	Update(table);
						//}
					}

					//	Update existing bullet points.
					bullets = model.BulletPoints.FindAll(x =>
						x.BulletTicket?.Length > 0);
					if(bullets.Count > 0)
					{
						table =
							BulletPointCollection.GetTable(bullets, model.CatalogItemID);
						foreach(DataRow row in table.Rows)
						{
							row.SetModified();
						}
						Update(table);
					}

					//	In this version, update to images is for ImageIndex only.
					images = model.Images.FindAll(x =>
						x.ImageTicket?.Length > 0);
					if(images.Count > 0)
					{
						table =
							ImageCollection.GetTable(images, model.CatalogItemID);
						RemoveColumns(table,
							new string[] { "CatalogImageID", "CatalogItemID", "ImageURL" });
						foreach(DataRow row in table.Rows)
						{
							row.SetModified();
						}
						Update(table);
					}

					//	Store the main item data.
					table = CatalogItem.GetTable(model);
					if(table.Rows.Count > 0)
					{
						RemoveColumns(table,
							new string[] {
								"CatalogItemTicket", "UserItemID", "DateCreated",
								"DateViewed", "ViewCount", "RatingCount", "StarCount" });
						table.Rows[0].SetModified();
						Update(table);
						UpdateKeywordIndex(model);
						message.Message = "Saved...";
					}
					else
					{
						message.Message = "Table row not found...";
					}
				}
				else
				{
					message.Message = "User not authorized...";
				}
			}
			else
			{
				message.Message = "Unknown model...";
			}
			return Ok(message);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PostCatalogNewImageItemTicket																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Retrieve the globally unique identification of a new image record for
		/// the specified catalog item.
		/// </summary>
		/// <param name="model">
		/// Reference to a catalog item ID and user item ticket structure.
		/// </param>
		/// <returns>
		/// If the user is validated as an owner of the catalog item, then
		/// a TicketItem containing a newly instantiated GUID.
		/// Otherwise, empty.
		/// </returns>
		[Route("api/v1/catalognewimageitemticket")]
		public IHttpActionResult PostCatalogNewImageItemTicket(
			CatalogIDUserTicket model)
		{
			bool authorized = false;
			TicketItem payload = new TicketItem();
			FileStream stream = null;
			Guid ticket = Guid.Empty;

			if(model != null &&
				model.CatalogItemID != 0)
			{
				if(model.UserItemTicket?.Length > 0)
				{
					try
					{
						ticket = Guid.Parse(model.UserItemTicket);
					}
					catch { }
					authorized = GetScalarBool(
						String.Format(
							ResourceMain.vwUserItemTicketAuthorizedForCatalogItemID,
							ToSql(ticket), ToSql(model.CatalogItemID)));
				}
			}
			if(authorized)
			{
				payload.Ticket = Guid.NewGuid().ToString("D");
				stream = File.Create(
					HostingEnvironment.MapPath(
						$"~/images/CatalogTemp/{payload.Ticket}.tmp"));
				stream.Flush();
				stream.Close();
				stream.Dispose();
			}
			return Ok(payload);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PostCatalogSendFileChunk																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Post a chunk of file data to a temporary file.
		/// </summary>
		/// <param name="model">
		/// Reference to the ticket and chunk being posted.
		/// </param>
		/// <returns>
		/// OK.
		/// </returns>
		[Route("api/v1/catalogsendfilechunk")]
		public IHttpActionResult PostCatalogSendFileChunk(TicketChunkItem model)
		{
			FileInfo file = null;
			MessageItem message = new MessageItem();
			StreamWriter writer = null;

			if(model != null && model.Ticket?.Length > 0 && model.Chunk?.Length > 0)
			{
				file = new FileInfo(HostingEnvironment.MapPath(
					$"~/images/CatalogTemp/{model.Ticket}.tmp"));
				if(file.Exists)
				{
					//	Only post if the file was already created by
					//	PostCatalogNewImageItemTicket.
					writer = file.AppendText();
					writer.Write(model.Chunk);
					writer.Flush();
					writer.Close();
					writer.Dispose();
					message.Message = "OK";
				}
			}
			else
			{
				message.Message = "File not found...";
			}
			return Ok(message);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PutRating																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Store a rating for a catalog item.
		/// </summary>
		/// <param name="catalogItemID">
		/// Unique item to rate.
		/// </param>
		/// <param name="rating">
		/// Rating to apply.
		/// </param>
		[Route("api/v1/catalogitemrate/{catalogItemID}/{rating}")]
		public IHttpActionResult PutRating(int catalogItemID, int rating)
		{
			int ratingCount = 0;
			DataRow row = null;
			double starCount = 0;
			DataTable table = GetTable(
				String.Format(ResourceMain.vwCatalogItem, ToSql(catalogItemID)));

			if(table.Rows.Count > 0)
			{
				//	Record was found.
				if(rating > 5)
				{
					rating = 5;
				}
				else if(rating < 1)
				{
					rating = 1;
				}
				row = table.Rows[0];
				ratingCount = row.Field<int>("RatingCount");
				starCount = row.Field<double>("StarCount");
				//	Update the average.
				starCount = (((double)ratingCount * starCount) + (double)rating) /
					(double)(ratingCount + 1);
				ratingCount++;
				Update(String.Format(ResourceMain.upCatalogItemRate,
					ToSql(row.Field<int>("CatalogItemID")),
					ToSql(starCount), ToSql(ratingCount)));
			}
			return Ok();
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
