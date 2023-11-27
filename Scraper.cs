using System;
using System.Net;
using System.IO;

namespace ScreenScraper
{
	/// <summary>
	/// This class opens a website and scraps the contents
	/// for weather information
	/// </summary>
	public class Scraper
	{
		// Stores a copy of the streamed site
		private string m_strSite;

		public Scraper()
		{
			// Could open the site here
		}

		/// <summary>
		/// This method attempts to open the requested URI and read
		/// the returned stream into a string for storage and later
		/// processing
		/// </summary>
		public bool OpenSite(string strURL)
		{
			try
			{
				// Create the WebRequest for the URL we are using
				WebRequest req = WebRequest.Create(strURL);
				
				// Need to set the credentials for proxy servers
				//req.Proxy.Credentials = new NetworkCredential("user_name", "password", "domain");
				
				// Get the stream from the returned web response
				StreamReader stream = new StreamReader(req.GetResponse().GetResponseStream());
			
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				string strLine;
				// Read the stream a line at a time and place each one
				// into the stringbuilder
				while( (strLine = stream.ReadLine()) != null )
				{
					// Ignore blank lines
					if(strLine.Length > 0 )
						sb.Append(strLine);
				}
				// Finished with the stream so close it now
				stream.Close();

				// Cache the streamed site now so it can be used
				// without reconnecting later
				m_strSite = sb.ToString();

				return true;
			}
			catch(Exception e)
			{
				// Handle the error in some fashion
				return false;
			}
		}
		
		///<summary>
		/// Return a string containing the HTML for current
		/// weather conditions
		///</summary>
		public string GetWeather()
		{
			return FindWeatherTable();
		}

		///<summary>
		/// Return a string containing the HTML for 5 day
		/// forecast
		///</summary>
		public string GetForecast()
		{
			return FindForecastTable();
		}

		#region Private methods
		
		/// <summary>
		/// Search the site string to find the table containing the weather
		/// information we are looking for
		/// </summary>
		private string FindWeatherTable()
		{
			int nIndexStart = 0;
			int nIndexEnd = 0;
			int nIndex = 0;

			try
			{
				// This phrase tells us where to start looking for the information
				// If it is found start looking for the first beginning table tag
				if( (nIndex = Find("Current Conditions for Pittsburgh", 0)) > 0 )
				{
					nIndexStart = Find("<TABLE", nIndex);
					if(nIndexStart > 0 )
					{
						// Need to find the second end table tag
						nIndex = Find("</TABLE>", nIndex);
						if(nIndex > 0 )
						{
							// Add 1 to the index so we don't find the same tag as above
							nIndexEnd = Find("</TABLE>", nIndex+1);
							if(nIndexEnd > 0 )
								nIndexEnd += 8; // Include the characters in the tag
						}
					}
				}
				// Extract and return the substring containing the table we want
				// after correcting the img src elements
				return CorrectImgPath(m_strSite.Substring(nIndexStart, nIndexEnd - nIndexStart));
			}
			catch(Exception e)
			{
				return e.Message;
			}
		}
		/// <summary>
		/// Search the site string to find the table containing the forecast
		/// information we are looking for
		/// </summary>
		private string FindForecastTable()
		{
			int nIndexStart = 0;
			int nIndexEnd = 0;
			int nIndex = 0;

			try
			{
				// This phrase tells us where to start looking for the information
				// If it is found start looking for the first beginning table tag
				if( (nIndex = Find("Five-day Forecast", 0)) > 0 )
				{
					nIndexStart = Find("<table", nIndex);
					if(nIndexStart > 0 )
						nIndexEnd = GetForecastEnd(nIndexStart);
				}
				// Extract and return the substring containing the table we want
				// after correcting the img src elements
				return CorrectImgPath(m_strSite.Substring(nIndexStart, nIndexEnd - nIndexStart));
			}
			catch(Exception e)
			{
				return e.Message;
			}
		}

		/// <summary>
		/// Helper method to find the end of the forecast table
		/// </summary>
		private int GetForecastEnd(int nIndexStart)
		{
			int nIndexEnd = 0;
			int nIndex = 0;
			try
			{
				// This image marks the end position we need to start at
				if( (nIndex = Find("1pix_trans.gif", nIndexStart)) > 0 )
				{
					nIndexEnd = Find("</TABLE>", nIndex+1);
					if(nIndexEnd > 0 )
						nIndexEnd += 8; // Include the characters in the tag
				}
				return nIndexEnd;
			}
			catch(Exception e)
			{
				return -1;
			}

		}

		/// <summary>
		/// Helper method to find the index of the string indicated
		/// starting at the position passed in
		/// </summary>
		private int Find(string strSearch, int nStart)
		{
			return m_strSite.IndexOf(strSearch, nStart);
		}
		
		/// <summary>
		/// Helper method to insert absolute path to images
		/// </summary>
		private string CorrectImgPath(string s)
		{
			int nIndex = 0;
			try
			{
				// Absolute path to insert
				string strInsert = "http://www.pittsburgh.com";
				// Find any and all images and insert the absolute path
				while( (nIndex = s.IndexOf("/images/", nIndex + strInsert.Length + 1)) > 0 )
				{
					s = s.Insert(nIndex, strInsert);
				}
				return s;
			}
			catch(Exception e)
			{
				return e.Message;
			}
		}

		#endregion
	}
}
