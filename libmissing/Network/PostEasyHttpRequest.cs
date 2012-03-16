using System;
using System.IO;
using System.Net;
using System.Text;

namespace Missing.Network
{
	/// <summary>
	/// Specialization that handles "POST" requests
	/// </summary>
	public class PostEasyHttpRequest : EasyHttpRequest
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Network.PostEasyHttpRequest"/> class.
		/// </summary>
		public PostEasyHttpRequest() : base()
		{
			this.contentType = "application/x-www-form-urlencoded";
		}
		#endregion Constructors
		
		/// <summary>
		/// The MIME-type of the content
		/// </summary>
		private string contentType = String.Empty;
		
		/// <summary>
		/// Get/set MIME-type of data (not used with GET)
		/// </summary>
		public string ContentType
		{
			get { return this.contentType; }
			set { this.contentType = value; }
		}

		#region Missing.Network.EasyHttpRequest : SendWorker
		/// <summary>
		/// Do anything that needs to be done in
		/// order to _send_ the request.
		/// 
		/// Could be writing stuff to the request stream
		/// </summary>
		protected override void SendWorker()
		{
			Byte[] postBytes = Encoding.ASCII.GetBytes(base.encodedData);
	
			// define length of content
			base.request.ContentLength = postBytes.Length;
			
			// send request
			Stream requestStream = request.GetRequestStream();
			
			requestStream.Write(postBytes, 0, postBytes.Length);
			requestStream.Close();
		}
		#endregion Missing.Network.EasyHttpRequest : SendWorker
		
		#region Missing.Network.EasyHttpRequest : MakeRequestInstance
		/// <summary>
		/// Create the actual request instance
		/// </summary>
		protected override void MakeRequestInstance()
		{
			base.request = (HttpWebRequest)WebRequest.Create(base.url);
			
			base.request.Method = "POST";
			base.request.ContentType = this.contentType;
		}
		#endregion Missing.Network.EasyHttpRequest : MakeRequestInstance
	}
}