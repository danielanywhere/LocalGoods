//	TicketChunk.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//*	TicketChunkItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a globally unique ticket and a data chunk.
	/// </summary>
	public class TicketChunkItem
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
		//*	Chunk																																	*
		//*-----------------------------------------------------------------------*
		private string mChunk = "";
		/// <summary>
		/// Get/Set the data chunk.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string Chunk
		{
			get { return mChunk; }
			set { mChunk = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of this item.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Ticket
		{
			get { return mTicket; }
			set { mTicket = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
