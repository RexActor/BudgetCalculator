namespace BudgetCalculator.Models
{
	public class DailyRolesData
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public int DailyHeadCountOfRole { get; set; }
		public int DailyHoursOfRole { get; set; }

		public double DailyTotalHoursOfRole { get { return DailyHeadCountOfRole * DailyHoursOfRole; } }
	}
}
