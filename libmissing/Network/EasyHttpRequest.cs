using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Missing.Network
{
	/// <summary>
	/// Abstract base class for easy-to-use http requests
	/// </summary>
	public abstract class EasyHttpRequest
	{
		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public EasyHttpRequest()
		{
		}
		#endregion Constructors
		
		/// <summary>
		/// The cookies
		/// </summary>
		protected CookieContainer cookies = new CookieContainer();
		
		/// <summary>
		/// How long it took to get the response
		/// in milliseconds
		/// </summary>
		protected long responseTime = 0;
		
		/// <summary>
		/// The timeout to use for the request
		/// </summary>
		/// <remarks>
		/// Defaults to the very conservative
		/// Int32.MaxValue
		/// </remarks>
		protected int timeout = Int32.MaxValue;
		
		/// <summary>
		/// The data that was actually sent to the server
		/// </summary>
		protected string encodedData = String.Empty;
		
		/// <summary>
		/// The user-agent to supply in the request
		/// </summary>
		protected string useragent = "Missing (https://github.com/isharpsolutions/Missing)";
		
		/// <summary>
		/// The string to send as referer
		/// </summary>
		protected string referer = String.Empty;
		
		/// <summary>
		/// The encoding for reading response data
		/// </summary>
		protected Encoding responseEncoding = Encoding.UTF8;
		
		/// <summary>
		/// The response stream as a string
		/// </summary>
		protected string body = String.Empty;
		
		/// <summary>
		/// The actual request instance
		/// </summary>
		protected HttpWebRequest request;
		
		/// <summary>
		/// The actual response instance
		/// </summary>
		protected HttpWebResponse response;
		
		/// <summary>
		/// The data to post/get
		/// </summary>
		protected NameValueCollection data = new NameValueCollection();
		
		/// <summary>
		/// The actual request URL
		/// </summary>
		protected string url = String.Empty;
		
		#region Add data
		/// <summary>
		/// Add name-value-pair to the data that will be sent
		/// </summary>
		/// <param name="name">
		/// The name of the data
		/// </param>
		/// <param name="val">
		/// The actual value
		/// </param>
		public void AddData(string name, string val)
		{
			this.data.Add(name, val);
		}
		
		/// <summary>
		/// Add name-value-pair to the data that will be sent
		/// </summary>
		/// <param name="name">
		/// The name of the data
		/// </param>
		/// <param name="val">
		/// The actual value
		/// </param>
		public void AddData(string name, object val)
		{
			this.AddData(name, val.ToString());
		}
		#endregion Add data
		
		#region Send
		/// <summary>
		/// Send the request
		/// </summary>
		/// <exception cref="InvalidOperationException">
		/// Thrown if the URL has not been set
		/// </exception>
		public void Send()
		{
			if (String.IsNullOrEmpty(this.url))
			{
				throw new InvalidOperationException("You must set 'Url' before sending the request");
			}
			
			this.EncodeData();
			
			// create the actual request
			this.MakeRequestInstance();
			
			this.SetDefaultHeaderOptions();
			this.SpoofUserAgent();
			this.SpoofReferer();
			
			Stopwatch watch = new Stopwatch();
			
			watch.Start();
			this.SendWorker();
			
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			this.body = new StreamReader(response.GetResponseStream(), responseEncoding).ReadToEnd();
			
			watch.Stop();
			
			this.responseTime = watch.ElapsedMilliseconds;
			
			this.response = response;
		}
		
		/// <summary>
		/// Do anything that needs to be done in
		/// order to _send_ the request.
		/// 
		/// Could be writing stuff to the request stream
		/// </summary>
		protected virtual void SendWorker()
		{
		}
		
		/// <summary>
		/// Create the actual request instance
		/// </summary>
		protected abstract void MakeRequestInstance();
		#endregion Send
		
		#region Get status
		/// <summary>
		/// Get status from custom headers like (cs-status-code and cs-status-message)
		/// </summary>
		/// <returns>
		/// The status
		/// </returns>
		/// <param name="prefix">
		/// The prefix (prefix-status-code)
		/// </param>
		public HttpRequestStatus GetStatus(string prefix)
		{
			return HttpRequestStatus.FromHeaders(this.response.Headers, prefix);
		}
		#endregion Get status
		
		#region Encode data
		/// <summary>
		/// Encodes the data for use as either post or get
		/// </summary>
		/// <remarks>
		/// Pick up the result using <c>this.encodedData</c>
		/// </remarks>
		protected void EncodeData()
		{
			string[] keys = this.data.AllKeys;
			
			if (keys.Length != 0)
			{
				StringBuilder sb = new StringBuilder();
				
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
		#endregion Encode data
		
		#region Set default headers
		/// <summary>
		/// Set a bunch of default header values
		/// </summary>
		protected void SetDefaultHeaderOptions()
		{
			// set a few options
			this.request.KeepAlive = true;
			this.request.ProtocolVersion = HttpVersion.Version11;
			this.request.AllowAutoRedirect = true;
			this.request.Timeout = this.timeout;
			this.request.CookieContainer = this.cookies;
		}
		#endregion Set default headers
		
		#region Spoof user-agent
		/// <summary>
		/// Set the value of the User-Agent header
		/// </summary>
		protected void SpoofUserAgent()
		{
			this.request.UserAgent = this.useragent;
		}
		#endregion Spoof user-agent
		
		#region Spoof referer
		/// <summary>
		/// Set the value of the referer header
		/// </summary>
		protected void SpoofReferer()
		{
			if (!String.IsNullOrEmpty(this.referer))
			{
				this.request.Referer = this.referer;
			}
		}
		#endregion Spoof referer
		
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
		
		/// <summary>
		/// Get the actual request instance
		/// </summary>
		public HttpWebRequest Request
		{
			get { return this.request; }
		}
		
		/// <summary>
		/// Get/set the URL
		/// </summary>
		public string Url
		{
			get { return this.url; }
			set { this.url = value; }
		}
		#endregion Properties
	}
}