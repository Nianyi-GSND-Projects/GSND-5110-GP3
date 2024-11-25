using UnityEngine;

namespace Game
{
	public static class TimeUtility
	{
		public struct TimeRecord
		{
			public int hours;
			public int minutes;
			public int seconds;
			public int milliseconds;

			public static TimeRecord FromSeconds(float seconds)
			{
				int hours = Mathf.FloorToInt(seconds / 60 / 60);
				seconds -= hours * 60 * 60;
				int minutes = Mathf.FloorToInt(seconds / 60);
				seconds -= minutes * 60;
				int iseconds = Mathf.FloorToInt(seconds);
				seconds -= seconds;
				int milliseconds = Mathf.FloorToInt(seconds * 100);
				return new()
				{
					hours = hours,
					minutes = minutes,
					seconds = iseconds,
					milliseconds = milliseconds
				};
			}

			public string ToString(string format = "hh:mm:ss")
			{

				string result = format;
				result = result.Replace("hh", hours.ToString());
				result = result.Replace("mm", minutes.ToString("00"));
				result = result.Replace("ss", seconds.ToString("00"));
				result = result.Replace("ms", milliseconds.ToString("000"));
				return result;
			}
		}

		public static string SecondsToString(float seconds, string format = "hh:mm:ss")
		{
			return TimeRecord.FromSeconds(seconds).ToString(format);
		}
	}
}