using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Weather
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		// Instance of the Screen Scraper class
		private ScreenScraper.Scraper scraper;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		/// <summary>
		/// Returns an HTML string containing the weather table
		/// </summary>
		public string GetWeather()
		{
			// If an instance is not already created
			if(scraper == null)
			{
				scraper = new ScreenScraper.Scraper();
				
				// Attempt to open weather site
				string URL = "http://www.pittsburgh.com/partners/wpxi/weather/";
				if( !scraper.OpenSite(URL) )
					return "Site not found";
			}
			// Return the weather table
			return scraper.GetWeather();
		}

		/// <summary>
		/// Returns an HTML string containing the forecast table
		/// </summary>
		public string GetForecast()
		{
			// If an instance is not already created
			if(scraper == null)
			{
				scraper = new ScreenScraper.Scraper();
				
				// Attempt to open weather site
				string URL = "http://www.pittsburgh.com/partners/wpxi/weather/";
				if( !scraper.OpenSite(URL) )
					return "Site not found";
			}

			// Return the weather table
			return scraper.GetForecast();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
