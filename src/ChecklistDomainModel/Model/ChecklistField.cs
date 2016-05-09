namespace ChecklistDomainModel.Model
{
    public class ChecklistField
    {
		public int Id { get; set; }

		public int ChecklistId { get; set; }

		public bool Required { get; set; }

		public bool IsChecked { get; set; }

		public string Name { get; set; }
    }
}
