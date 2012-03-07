using System;
using NUnit.Framework;
using System.IO;
using Missing.Security.Cryptography;

namespace Missing
{
	/// <summary>
	/// Tests the <see cref="Md5"/> methods
	/// </summary>
	[TestFixture]
	public class HashTest
	{
		#region FromFile
		/// <summary>
		/// Tests the <see cref="Md5.FromFile"/>
		/// </summary>
		[Test]
		public void Md5_File()
		{
			string hash = Hash.FromFile(HashType.MD5, String.Format("{1}{0}{2}{0}{3}", Path.DirectorySeparatorChar, "Security", "Cryptography", "Md5TestFile.txt"));
			
			Assert.AreEqual("ea21841da70e6405af19fabc4ff8bdd9", hash);
		}
		#endregion FromFile
		
		#region FromString
		/// <summary>
		/// Tests the <see cref="Md5.FromString"/>
		/// </summary>
		[Test]
		public void Md5_String()
		{
			Assert.AreEqual("ea21841da70e6405af19fabc4ff8bdd9", Hash.FromString(HashType.MD5, "missing"));
		}
		#endregion FromString
	}
}