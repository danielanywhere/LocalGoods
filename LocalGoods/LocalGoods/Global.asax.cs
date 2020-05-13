//	Global.asax.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

using Newtonsoft.Json;

namespace LocalGoods
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		public static ColumnMetadataCollection ColumnDefinitions = null;
		protected void Application_Start()
		{
			//string path = HttpContext.Current.Server.MapPath("~");
			//System.Diagnostics.Debug.WriteLine("Executing assembly path: " + path);
			//AppDomain.CurrentDomain.SetData("DataDirectory", path);
			GlobalConfiguration.Configure(WebApiConfig.Register);
			//	Keep the SQLite data types in memory as long as the application
			//	is running.
			ColumnDefinitions =
				JsonConvert.DeserializeObject<ColumnMetadataCollection>(
					ResourceMain.ColumnMetadata);
			ColumnDefinitions.InitializeDbTypes();
		}
	}
}
