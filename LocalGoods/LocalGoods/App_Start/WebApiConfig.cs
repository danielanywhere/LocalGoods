//	WebApiConfig.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace LocalGoods
{
	//*-------------------------------------------------------------------------*
	//* WebApiConfig																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Application configuration for WebAPI2 functionality.
	/// </summary>
	public static class WebApiConfig
	{
		//*-----------------------------------------------------------------------*
		//* Register																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Register the connections for WebAPI2.
		/// </summary>
		/// <param name="config">
		/// Active HTTP configuration to modify.
		/// </param>
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			config.EnableCors();
			//config.Formatters.JsonFormatter.SupportedMediaTypes.
			//	Add(new MediaTypeHeaderValue("text/html"));
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.
				MediaTypeMappings.Add(
				new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
				"text/html",
				StringComparison.InvariantCultureIgnoreCase,
				true,
				"application/json"));

			//	Explicitly allow controllers to accept multipart/form-data.
			//	This is needed for uploading images and large files.
			config.Formatters.XmlFormatter.SupportedMediaTypes.Add(
				new System.Net.Http.Headers.MediaTypeHeaderValue(
					"multipart/form-data"));

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
			);
		}
		//*-----------------------------------------------------------------------*
	}
	//*-------------------------------------------------------------------------*
}
