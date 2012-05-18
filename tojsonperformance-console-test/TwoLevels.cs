using System;

namespace tojsonperformanceconsoletest
{
	public class TwoLevels
	{
		public TwoLevels()
		{
			this.OneBool = false;
			this.OneDecimal = 3632.2m;
			this.OneDouble = 2525.224d;
			this.OneFloat = 1243.24f;
			this.OneInt = 5432;
			this.OneLong = 987765L;
			this.OneString = "Two Levels";
			this.Sub = new OneLevel();
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