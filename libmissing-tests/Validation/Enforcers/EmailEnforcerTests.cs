using System;
using Missing.Validation.Enforcers;
using NUnit.Framework;

namespace Missing
{
	[TestFixture]
	public class EmailEnforcerTests
	{
		[Test]
		public void InvalidInputType()
		{
			try
			{
				(new EmailEnforcer()).Check(34);
				
				Assert.Fail("An ArgumentException should have been thrown");
			}
			
			catch (ArgumentException)
			{
			}
		}
		
		[Test]
		public void SimpleAddress()
		{
			string email = "valid@domain.tld";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void SimpleAddressInvalid()
		{
			string email = "invalid";
			
			Assert.IsNotEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void ValidFirstChar_Underscore()
		{
			string email = "_valid@domain.tld";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}		
		
			
		[Test]
		public void InvalidFirstChar_Dot()
		{
			string email = ".valid@domain.tld";
			
			Assert.IsNotEmpty((new EmailEnforcer()).Check(email));
		}
		
		/// <summary>
		/// Tests that underscore is allowed just before the @
		/// </summary>
		[Test]
		public void UnderscoreJustBeforeAt()
		{
			string email = "valid_@domain.tld";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void DoubleUnderscoreInUser()
		{
			string email = "myname__12_@somedomain.tld";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void OneLetterUserAndDashInDomain()
		{
			string email = "p@rtone-parttwo.tld";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void UppercaseOneLetterUser()
		{
			string email = "A@somedomain.tld";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void OneLetterUser()
		{
			string email = "p@valid.tld";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void TwoLetterUserAndTwoLetterDomain()
		{
			string email = "ab@cd.dk";
			
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}

		[Test]
		public void InfoAsTld()
		{
			string email = "some@domain.info";
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void AeroAsTld()
		{
			string email = "some@domain.aero";
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
		
		[Test]
		public void MuseumAsTld()
		{
			string email = "some@domain.museum";
			Assert.IsEmpty((new EmailEnforcer()).Check(email));
		}
	}
}

