using System;

namespace tojsonperformanceconsoletest
{
	public class OneLevel
	{
		public OneLevel()
		{
			this.MyString = @"Something kewl with
a newline in it";
			
			this.MyBool = true;
			this.MyDecimal = 35.3m;
			this.MyDouble = 235.2d;
			this.MyFloat = 242.7f;
			this.MyInt = 254;
			this.MyLong = 35385L;
		}
		
		public string MyString { get; set; }
		public int MyInt { get; set; }
		public long MyLong { get; set; }
		public float MyFloat { get; set; }
		public double MyDouble { get; set; }
		public decimal MyDecimal { get; set; }
		public bool MyBool { get; set; }
	}
}