using System;

namespace tojsonperformanceconsoletest
{
	public class TwoLevels
	{
		public TwoLevels()
		{
		}
		
		public string OneString { get; set; }
		public int OneInt { get; set; }
		public long OneLong { get; set; }
		public float OneFloat { get; set; }
		public double OneDouble { get; set; }
		public decimal OneDecimal { get; set; }
		public bool OneBool { get; set; }
		
		public OneLevel Sub { get; set; }
	}
}