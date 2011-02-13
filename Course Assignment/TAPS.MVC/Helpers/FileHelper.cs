// --------------------------------------------------------------------------------------------------------------------

// <copyright file="FileHelper.cs" company="U of R">

//   Copyright 2008-2009

// </copyright>

// <summary>

//   Defines the FileHelper type.

// </summary>

// --------------------------------------------------------------------------------------------------------------------


using System.Diagnostics;

namespace TAPS.MVC.Helpers
{
	/// <summary>
	/// File helper class
	/// </summary>
	public static class FileHelper
	{
		/// <summary>
		/// Write Event Log Entry
		/// </summary>
		/// <param name="source">
		/// The source.string
		/// </param>
		/// <param name="log">
		/// The log.string
		/// </param>
		/// <param name="eventString">
		/// The event String.
		/// </param>
		private static void WriteEventLogEntry(string source, string log, string eventString, int eventID)
		{

			if (!EventLog.SourceExists(source))
			{
				EventLog.CreateEventSource(source, log);
			}

			EventLog.WriteEntry(source, eventString);
			EventLog.WriteEntry(
				source,
				eventString,
				EventLogEntryType.Warning,
				eventID);
		}
	}
}


