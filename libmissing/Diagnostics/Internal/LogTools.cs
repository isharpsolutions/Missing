using System;
using System.Text;
using System.Diagnostics;

namespace Missing.Diagnostics.Internal
{
	/// <summary>
	/// Provides nifty methods which the log writers can use
	/// </summary>
	internal static class LogTools
	{
		/// <summary>
		/// Returns the currently executing threads managed thread id
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/> containing the thread id
		/// </returns>
		public static int GetCurrentThreadId()
		{
			return System.Threading.Thread.CurrentThread.ManagedThreadId;
		}
		
		/// <summary>
		/// Searches through the stack trace, and locates the first method in the frames NOT matching the <see cref="Log"/> methods
		/// </summary>
		/// <param name="caller">
		/// A <see cref="System.String"/> which upon success will contain the name of the caller
		/// </param>
		/// <param name="callerClass">
		/// A <see cref="System.String"/> which upon success will contain the class name of the calling method
		/// </param>
		/// <param name="callerName">
		/// A <see cref="System.String"/> which upon success will contain the full assembly name of the caller.
		/// </param>
		/// <param name="fullName">
		/// A <see cref="System.String"/> which upon success will contain the fully qualified name of the caller
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/> which is <c>true</c> if a match was found, <c>false</c> otherwise
		/// </returns>
		public static bool FindFrame(out string caller, out string callerClass, out string callerName, out string fullName, out string callerNamespace)// find first frame that is not "Trace"
		{
			string ignoredCallers = "Trace,Debug,Information,Warning,Error,Fatal,SetCallerInContext,ToLog";
			
			StackTrace stackTrace = new StackTrace();
			
			for (int i=1; i<=stackTrace.FrameCount; i++)
			{
				caller = stackTrace.GetFrame(i).GetMethod().Name;
				callerClass = stackTrace.GetFrame(i).GetMethod().DeclaringType.Name;
				callerName = stackTrace.GetFrame(i).GetMethod().DeclaringType.Assembly.GetName().Name;
				fullName = stackTrace.GetFrame(i).GetMethod().DeclaringType.FullName;
				callerNamespace = stackTrace.GetFrame(i).GetMethod().DeclaringType.Namespace;
				
				if (!ignoredCallers.Contains(caller))
				{
					return true;
				}
					
			}
			
			caller = String.Empty;
			callerClass = String.Empty;
			callerName = String.Empty;
			fullName = String.Empty;
			callerNamespace = String.Empty;
			return false;
		}
	}
}