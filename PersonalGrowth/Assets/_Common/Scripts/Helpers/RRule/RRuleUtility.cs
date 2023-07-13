using System;
using System.Collections.Generic;

namespace Olly.core
{
	public static class RRuleUtility
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rrule"></param>
		/// <param name="nextXOccurences"></param>
		/// <param name="minDate">If left to null, is changed to DateTime.Now</param>
		/// <returns></returns>
		public static List<DateTime> GetNextOccurences(Model_RRuleType rrule, int nextXOccurences = 1, DateTime? minDate = null)
		{
			if (minDate == null)
				minDate = DateTime.Now;

			DateTime lStart = DateTime.Parse(rrule.dtstart);
			DateTime? lEnd = null;

			if (!string.IsNullOrEmpty(rrule.until))
				lEnd = DateTime.Parse(rrule.until);

			int lIntervalDays = 0;
			switch (rrule.freq)
			{
				case -1: // No repetition
					lIntervalDays = 0;
					break;
				case 0: // Yearly
					lIntervalDays = 365 * rrule.interval; // assuming 365-day years
					break;
				case 1: // Monthly
					lIntervalDays = 30 * rrule.interval; // assuming 30-day months
					break;
				case 2: // Weekly
					lIntervalDays = 7 * rrule.interval;
					break;
				case 3: // Daily
					lIntervalDays = 1 * rrule.interval;
					break;
			}

			DateTime lCurrent = lStart;
			List<DateTime> lOccurrences = new List<DateTime>();

			while ((lEnd == null || lCurrent <= lEnd.Value) && lOccurrences.Count < nextXOccurences)
			{
				if (lCurrent > minDate.Value && (rrule.byweekday == null || rrule.byweekday.Count == 0
					|| rrule.byweekday.Contains(lCurrent.DayOfWeek.ToString().Substring(0, 2).ToUpper())))
					lOccurrences.Add(lCurrent);

				if (lIntervalDays == 0)
					break;

				lCurrent = lCurrent.AddDays(lIntervalDays);
			}

			return lOccurrences;
		}
	}
}