using System;
using System.Net;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Missing.Network
{
	public class HttpRequestUserFriendly
	{
		#region Fields
		private CookieContainer cookies;
		
		private long responseTime = 0;

		#region REQUEST
		/// <summary>
		/// Holds the request instance
		/// </summary>
		private HttpWebRequest request;
		
		/// <summary>
		/// Holds the data to post/get
		/// </summary>
		private NameValueCollection data;
		
		/// <summary>
		/// Holds the timeout value
		/// </summary>
		private int timeout;
		
		/// <summary>
		/// MIME-type of content (only used by POST)
		/// </summary>
		private string contentType;
		
		/// <summary>
		/// The data that was actually sent to the server
		/// </summary>
		private string encodedData;
		
		/// <summary>
		/// Holds the request method to use
		/// </summary>
		private HttpRequestMethod method;
		
		/// <summary>
		/// Holds the user agent string to send
		/// </summary>
		private string useragent;
		
		/// <summary>
		/// Holds the string to send as referer
		/// </summary>
		private string referer;
		
		/// <summary>
		/// The default encoding for reading response data
		/// </summary>
		private Encoding defaultEncoding = Encoding.GetEncoding("utf-8");
		#endregion REQUEST
		
		#region RESPONSE
		/// <summary>
		/// Holds the response stream as a string
		/// </summary>
		private string body;
		
		/// <summary>
		/// The actual response instance
		/// </summary>
		private HttpWebResponse response;
		#endregion RESPONSE
		
		#endregion Fields

		#region Contructors
		/// <summary>
		/// Create a new POST request
		/// </summary>
		/// <param name="url">
		/// A <see cref="System.String"/> containing a full URL (http and all)
		/// </param>
		public HttpRequestUserFriendly(string url) : this(url, HttpRequestMethod.POST) {}
		
		/// <summary>
		/// Create a new request
		/// </summary>
		/// <param name="url">
		/// A <see cref="System.String"/> containing a full URL (http and all)
		/// </param>
		/// <param name="method">
		/// A <see cref="HttpRequestMethod"/>
		/// </param>
		public HttpRequestUserFriendly(string url, HttpRequestMethod method)
		{
			this.method = method;
			
			this.request = (HttpWebRequest)WebRequest.Create(url);

			this.cookies = new CookieContainer();
			this.data = new NameValueCollection();
			this.body = String.Empty;
			this.timeout = Int32.MaxValue;
			this.useragent = "Missing (https://github.com/isharpsolutions/Missing)";
			this.referer = String.Empty;
		}
		#endregion Constructors
		
		#region Public methods
		/// <summary>
		/// Add name-value-pair to the data that will be sent
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="val">
		/// A <see cref="System.String"/>
		/// </param>
		public void AddData(string name, string val)
		{
			this.data.Add(name, val);
		}
		
		/// <summary>
		/// Finalize the request and send it
		/// </summary>
		/// <remarks>
		/// This method expects that the response is in utf-8 encoding. Use the overload if data is garbeled...
		/// </remarks>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the method is not known by this method
		/// </exception>
		public void Send()
		{
			this.Send(this.defaultEncoding);
		}
		
		/// <summary>
		/// Finalize the request and send it
		/// </summary>
		/// <param name='responseEncoding'>
		/// Response encoding
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the method is not known by this method
		/// </exception>
		public void Send(Encoding responseEncoding)
		{
			switch (this.method)
			{
				case HttpRequestMethod.POST:
					this.SendPost(responseEncoding);
					break;
					
				case HttpRequestMethod.GET:
					this.SendGet(responseEncoding);
					break;
					
				default:
					throw new InvalidOperationException(String.Format("Unable to send request due to unknown request method.. Received '{0}'", this.method));
			}
		}
		/// <summary>
		/// Get status from custom headers like (cs-status-code and cs-status-message)
		/// </summary>
		/// <param name="prefix">
		/// A <see cref="System.String"/> with the prefix (prefix-status-code)
		/// </param>
		/// <returns>
		/// A <see cref="HttpRequestStatus"/>
		/// </returns>
		public HttpRequestStatus GetStatus(string prefix)
		{
			return HttpRequestStatus.FromHeaders(this.response.Headers, prefix);
		}
		#endregion Public methods
		
		#region Private methods
		/// <summary>
		/// Creates an UrlEncoded string from the data list.
		/// </summary>
		/// <remarks>
		/// Pick up the result using <c>this.encodedData</c>
		/// </remarks>
		private void CreateDataString()
		{
			string[] keys = this.data.AllKeys;
			
			if (keys.Length != 0)
			{
				StringBuilder sb = new StringBuilder(this.data.Count);
				
				for (int i = 0; i < this.data.Count; i++)
				{
					sb.AppendFormat("&{0}={1}", HttpUtility.UrlEncode(keys[i]), HttpUtility.UrlEncode(this.data[i]));
				}

				// set the data to the string without the first "&"
				this.encodedData = sb.ToString().Remove(0, 1);
				sb = null;
			}
			
			keys = null;
		}
		
		/// <summary>
		/// Send a POST request
		/// </summary>
		private void SendPost(Encoding responseEncoding)
		{
			Stopwatch watch = new Stopwatch();
			
			this.SetDefaultHeaderOptions();
			this.SpoofUserAgent();
			this.SpoofReferer();
			
			// set method and content type
			this.request.Method = "POST";
			this.request.ContentType = "application/x-www-form-urlencoded";
			
			this.CreateDataString();
			
			#region Send data
			Byte[] postBytes = Encoding.ASCII.GetBytes(this.encodedData);
	
			// define length of content
			this.request.ContentLength = postBytes.Length;
			
			// send request
			Stream requestStream = request.GetRequestStream();
			watch.Start();
			requestStream.Write(postBytes, 0, postBytes.Length);
			requestStream.Close();
			#endregion Send data
			
			// get response
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			this.body = new StreamReader(response.GetResponseStream(), responseEncoding).ReadToEnd();
			watch.Stop();
			
			this.responseTime = watch.ElapsedMilliseconds;
			
			this.response = response;
		}
		
		/// <summary>
		/// Send a GET request
		/// </summary>
		private void SendGet(Encoding responseEncoding)
		{
			Stopwatch watch = new Stopwatch();
			
			this.CreateDataString();
			string url = String.Format("{0}?{1}", this.request.Address.ToString(), this.encodedData);
			
			// get data is posted as part of the url, hence we need to create
			// a new request
			this.request = (HttpWebRequest)WebRequest.Create(url);
			
			this.SetDefaultHeaderOptions();
			this.SpoofUserAgent();
			this.SpoofReferer();
			
			// set method and content type
			this.request.Method = "GET";
			this.request.ContentType = String.Empty;
			
			watch.Start();
			
			// get response
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			this.body = new StreamReader(response.GetResponseStream(), responseEncoding).ReadToEnd();
			watch.Stop();
			
			this.responseTime = watch.ElapsedMilliseconds;
			
			this.response = response;
		}
		
		/// <summary>
		/// Set a bunch of default header values
		/// </summary>
		private void SetDefaultHeaderOptions()
		{
			// set a few options
			this.request.KeepAlive = true;
			this.request.ProtocolVersion = HttpVersion.Version11;
			this.request.AllowAutoRedirect = true;
			this.request.Timeout = this.timeout;
			this.request.CookieContainer = this.cookies;
			this.request.ContentType = this.contentType;
		}
		
		/// <summary>
		/// Set the value of the User-Agent header
		/// </summary>
		private void SpoofUserAgent()
		{
			this.request.UserAgent = this.useragent;
		}

		/// <summary>
		/// Set the value of the referer header
		/// </summary>
		private void SpoofReferer()
		{
			if (!this.referer.Equals(String.Empty))
				this.request.Referer = this.referer;
		}
		#endregion Private methods
		
		#region Properties
		/// <summary>
		/// Get/set the cookie container
		/// </summary>
		public CookieContainer Cookies
		{
			get { return this.cookies; }
			set { this.cookies = value; }
		}
		
		/// <summary>
		/// Get/set NameValueCollection with data to POST/GET
		/// </summary>
		public NameValueCollection Data
		{
			get { return this.data; }
			set { this.data = value; }
		}
		
		/// <summary>
		/// Get the body of the response (typically HTML)
		/// </summary>
		public string Body
		{
			get { return this.body; }
		}
		
		/// <summary>
		/// Get <see cref="HttpRequestStatus"/> from Response
		/// </summary>
		public HttpRequestStatus Status
		{
			get { return HttpRequestStatus.FromString(this.body); }
		}
		
		/// <summary>
		/// Get/set the timeout
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown for SET if the value is less than 0
		/// </exception>
		public int Timeout
		{
			get { return this.timeout; }
			
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("Timeout must be larger than 0");
				}
				
				this.timeout = value;
			}
		}
		
		/// <summary>
		/// Get/set MIME-type of data (not used with GET)
		/// </summary>
		public string ContentType
		{
			get { return this.contentType; }
			set { this.contentType = value; }
		}
		
		/// <summary>
		/// Get the Response instance returned from the request
		/// </summary>
		public HttpWebResponse Response
		{
			get { return this.response; }
		}
		
		/// <summary>
		/// Get the encoded data that was sent to the server
		/// </summary>
		public string EncodedData
		{
			get { return this.encodedData; }
		}
		
		/// <summary>
		/// Get the headers from the request
		/// </summary>
		public WebHeaderCollection Headers
		{
			get { return this.request.Headers; }
		}
		
		/// <summary>
		/// Get/set the user agent string to send
		/// </summary>
		public string UserAgent
		{
			get { return this.useragent; }
			set { this.useragent = value; }
		}
		
		/// <summary>
		/// Get/set the string to send as referer
		/// </summary>
		public string Referer
		{
			get { return this.referer; }
			set { this.referer = value; }
		}
		
		/// <summary>
		/// Get the response time [in milliseconds]
		/// </summary>
		public long ResponseTime
		{
			get { return this.responseTime; }
		}
		
		public HttpWebRequest Request
		{
			get { return this.request; }
		}
		#endregion Properties
	}
}