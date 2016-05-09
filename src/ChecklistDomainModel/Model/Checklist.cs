using System.Collections.Generic;

namespace ChecklistDomainModel.Model
{
	public class Checklist
	{
		public int Id { get; set; }

		public int RenderMode { get; set; }

		public string Name { get; set; }

		public List<ChecklistField> Fields { get; set; }
	}
}