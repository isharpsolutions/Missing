using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Security
{
	public static class PasswordStrength
	{
		#region Useless Passwords HashSet
		/// <summary>
		/// A hashset with the 500 most common passwords, from http://www.whatsmypass.com/the-top-500-worst-passwords-of-all-time
		/// </summary>
		private static HashSet<string> useless = new HashSet<string>()
		{
			"123456", "porsche", "firebird", "prince", "rosebud", "password", "guitar", "butter", "beach", "jaguar", "12345678", 
			"chelsea", "united", "amateur", "great", "1234", "black", "turtle", "7777777", "cool", "pussy", "diamond", "steelers",
			"muffin", "cooper", "12345", "nascar", "tiffany", "redsox", "1313", "dragon", "jackson", "zxcvbn", "star", "scorpio",
			"qwerty", "cameron", "tomcat", "testing", "mountain", "696969", "654321", "golf", "shannon", "madison", "mustang", 
			"computer", "bond007", "murphy", "987654", "letmein", "amanda", "bear", "frank", "brazil", "baseball", "wizard", "tiger", 
			"hannah", "lauren", "master", "xxxxxxxx", "doctor", "dave", "japan", "michael", "money", "gateway", "eagle1", "naked", 
			"football", "phoenix", "gators", "11111", "squirt", "shadow", "mickey", "angel", "mother", "stars", "monkey", "bailey", 
			"junior", "nathan", "apple", "abc123", "knight", "thx1138", "raiders", "alexis", "pass", "iceman", "porno", "steve", 
			"aaaa", "fuckme", "tigers", "badboy", "forever", "bonnie", "6969", "purple", "debbie", "angela", "peaches", "jordan", 
			"andrea", "spider", "viper", "jasmine", "harley", "horny", "melissa", "ou812", "kevin", "ranger", "dakota", "booger",
			"jake", "matt", "iwantu", "aaaaaa", "1212", "lovers", "qwertyui", "jennifer", "player", "flyers", "suckit", "danielle", 
			"hunter", "sunshine", "fish", "gregory", "beaver", "fuck", "morgan", "porn", "buddy", "4321", "2000", "starwars", 
			"matrix", "whatever", "4128", "test", "boomer", "teens", "young", "runner", "batman", "cowboys", "scooby", "nicholas", 
			"swimming", "trustno1", "edward", "jason", "lucky", "dolphin", "thomas", "charles", "walter", "helpme", "gordon", "tigger",
			"girls", "cumshot", "jackie", "casper", "robert", "booboo", "boston", "monica", "stupid", "access", "coffee", "braves", 
			"midnight", "shit", "love", "xxxxxx", "yankee", "college", "saturn", "buster", "bulldog", "lover", "baby", "gemini", 
			"1234567", "ncc1701", "barney", "cunt", "apples", "soccer", "rabbit", "victor", "brian", "august", "hockey", "peanut",
			"tucker", "mark", "3333", "killer", "john", "princess", "startrek", "canada", "george", "johnny", "mercedes", "sierra",
			"blazer", "sexy", "gandalf", "5150", "leather", "cumming", "45", "andrew", "spanky", "doggie", "232323", "hunting", 
			"charlie", "winter", "zzzzzz", "4444", "kitty", "superman", "brandy", "gunner", "beavis", "rainbow", "asshole", "compaq",
			"horney", "bigcock", "112233", "fuckyou", "carlos", "bubba", "happy", "arthur", "dallas", "tennis", "2112", "sophie", 
			"cream", "jessica", "james", "fred", "ladies", "calvin", "panties", "mike", "johnson", "naughty", "shaved", "pepper", 
			"brandon", "xxxxx", "giants", "surfer", "1111", "fender", "tits", "booty", "samson", "austin", "anthony", "member", "blonde",
			"kelly", "william", "blowme", "boobs", "fucked", "paul", "daniel", "ferrari", "donald", "golden", "mine", "golfer", "cookie",
			"bigdaddy", "0", "king", "summer", "chicken", "bronco", "fire", "racing", "heather", "maverick", "penis", "sandra", "5555", 
			"hammer", "chicago", "voyager", "pookie", "eagle", "yankees", "joseph", "rangers", "packers", "hentai", "joshua", "diablo", 
			"birdie", "einstein", "newyork", "maggie", "sexsex", "trouble", "dolphins", "little", "biteme", "hardcore", "white", "0", 
			"redwings", "enter", "666666", "topgun", "chevy", "smith", "ashley", "willie", "bigtits", "winston", "sticky", "thunder", 
			"welcome", "bitches", "warrior", "cocacola", "cowboy", "chris", "green", "sammy", "animal", "silver", "panther", "super", 
			"slut", "broncos", "richard", "yamaha", "qazwsx", "8675309", "private", "fucker", "justin", "magic", "zxcvbnm", "skippy",
			"orange", "banana", "lakers", "nipples", "marvin", "merlin", "driver", "rachel", "power", "blondesmichelle", "marine", 
			"slayer", "victoria", "enjoy", "corvette", "angels", "scott", "asdfgh", "girl", "bigdog", "fishing", "2222", "vagina",
			"apollo", "cheese", "david", "asdf", "toyota", "parker", "matthew", "maddog", "video", "travis", "qwert", "121212", 
			"hooters", "london", "hotdog", "time", "patrick", "wilson", "7777", "paris", "sydney", "martin", "butthead", "marlboro",
			"rock", "women", "freedom", "dennis", "srinivas", "xxxx", "voodoo", "ginger", "fucking", "internet", "extreme", "magnum",
			"blowjob", "captain", "action", "redskins", "juice", "nicole", "bigdick", "carter", "erotic", "abgrtyu", "sparky", 
			"chester", "jasper", "dirty", "777777", "yellow", "smokey", "monster", "ford", "dreams", "camaro", "xavier", "teresa", 
			"freddy", "maxwell", "secret", "steven", "jeremy", "arsenal", "music", "dick", "viking", "11111111", "access14", "rush2112",
			"falcon", "snoopy", "bill", "wolf", "russia", "taylor", "blue", "crystal", "nipple", "scorpion", "111111", "eagles",
			"peter", "iloveyou", "rebecca", "131313", "winner", "pussies", "alex", "tester", "123123", "samantha", "cock", "florida", 
			"mistress", "bitch", "house", "beer", "eric", "phantom", "hello", "miller", "rocket", "legend", "billy", "scooter", "flower", 
			"theman", "movie", "6666", "please", "jack", "oliver", "success", "albert"
		};
		#endregion Useless Passwords HashSet

		#region Evaluate method
		/// <summary>
		/// Evaluates the specified password and returns a <see cref="PasswordStrenghtScore"/>
		/// </summary>
		/// <remarks>
		/// The scoring algorithm is devised into multiple steps.
		/// 
		/// First of all, it attemps to flag the password as <see cref="PasswordStrenghtScore.Useless"/> by
		/// checking an internal dictionary of the most commonly used passwords. If the user has attempted to 
		/// "l33t" up the password, it will also attemp to discover that. i.e "password" and "p4ssw0rd" will 
		/// both be caught by the "Useless" checker.
		/// 
		/// If a password cannot be scored as useless, key space computations will be started. Therefore, all
		/// other scores than <see cref="PasswordStrenghtScore.Useless"/> are based solely on the size of the 
		/// keyspace that the password exercises.
		/// 
		/// This is determined by evaluating which "parts" the password is made up of. I.e "myAwesomePassword1"
		/// will have: alpha, alpha uppercase and numbers. Thus this password will have an "symbol pool size" of:
		/// <c>28 (alpha lower) + 28 (alpha upper + 10 (numbers) = 66</c>
		/// The pool size can then be utilized for computing the size of the keyspace that an attacker would have
		/// to traverse during a bruteforce attack. This is simple combination theory:
		/// <c>symbolPoolSize^numerOfChars</c>
		/// Which means that in this example we will have:
		/// <c>66^18 = 5,646649614×(10^32)</c>
		/// Which is quite a big number, and thus the given password would be evaluated as being <see cref="PasswordStrenghtScore.Great"/>
		/// 
		/// The list of "useless" passwords has been taken directly from: http://www.whatsmypass.com/the-top-500-worst-passwords-of-all-time
		/// So thanks goes to the guys there :)
		/// </remarks>
		/// <param name="password">The password.</param>
		/// <returns>A <see cref="PasswordStrenghtScore"/> instance</returns>
		public static PasswordStrenghtScore Evaluate(string password)
		{
			throw new NotImplementedException();
		}
		#endregion Evaluate method
	}
}
