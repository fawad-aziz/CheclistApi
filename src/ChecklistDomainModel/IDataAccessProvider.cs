using System.Linq;
using ChecklistDomainModel.Model;

namespace ChecklistDomainModel
{
	public interface IDataAccessProvider
	{
		IQueryable<Checklist> GetChecklists();

		Checklist GetChecklist(int id);
	}
}