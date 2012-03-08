using System;
using System.Net;

namespace Missing.Network
{
	/// <summary>
	/// Specialization that handles "GET" requests
	/// </summary>
	public class GetEasyHttpRequest : EasyHttpRequest
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Network.GetEasyHttpRequest"/> class.
		/// </summary>
		public GetEasyHttpRequest() : base()
		{
		}
		#endregion Constructors
		
		#region Missing.Network.EasyHttpRequest : MakeRequestInstance
		/// <summary>
		/// Create the actual request instance
		/// </summary>
		protected override void MakeRequestInstance()
		{
			UriBuilder b = new UriBuilder(base.url);
			
			if (String.IsNullOrEmpty(b.Query))
			{
				b.Query = base.encodedData;
			}
			else
			{
				b.Query = String.Format("{0}&{1}", base.encodedData);
			}
			
			base.url = b.Uri.ToString();
			
			base.request = (HttpWebRequest)WebRequest.Create(base.url);
			
			base.request.Method = "GET";
		}
		#endregion Missing.Network.EasyHttpRequest : MakeRequestInstance
	}
}