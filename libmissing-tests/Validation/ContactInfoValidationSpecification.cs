using System;
using Missing.Validation;

namespace Missing
{
	public class ContactInfoValidationSpecification : ValidationSpecification<ContactInfo>
	{
		public ContactInfoValidationSpecification()
		{
			base.Field(y => y.Name)
				.Required();
			
			base.Field(y => y.Email)
				.Required();
			
			base.Field(y => y.Phone);
		}
	}
}

