using System.Globalization;

namespace BudgetCalculator.Finance.Calendar
{
	public static class FinanceCalendar
	{
		/// <summary>
		/// Represents 4-4-5 Week model
		/// This is fixed model
		/// Dictionary have Month names as Keys and Week count as values
		/// <para name="Test"></para>
		/// </summary>
		
		public static Dictionary<string, int> FinanceCalendarWeekModel = new Dictionary<string, int>()
		{
			{ "January",4},
			{ "February",4},
			{"March",5 },
			{"April",4 },
			{ "May",4},
			{ "June",5},
			{"July",4 },
			{"August",4 },
			{"September",5 },
			{"October",4 },
			{"November",4 },
			{"December",5 }

		};

		


		public static int GetWeeksPerMonth(int year, int month)
		{
			int result = 0;
			
			var DaysInMonth = DateTime.DaysInMonth(year, month);

			
			for (int x = 1; x <= Convert.ToInt32(DaysInMonth); x++)
			{
				if (new DateTime(year, month, x).DayOfWeek == DayOfWeek.Friday)
				{
					result++;
				}
			}

			return result;

		}

	}
}