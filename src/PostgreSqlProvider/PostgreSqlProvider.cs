using System.Linq;
using ChecklistDomainModel;
using ChecklistDomainModel.Model;

namespace PostgreSqlProvider
{
	public class PostgreSqlProvider : IDataAccessProvider
	{
		private readonly ChecklistContext _context;

		public PostgreSqlProvider(ChecklistContext context)
		{
			this._context = context;
		}

		public Checklist GetChecklist(int id)
		{
			return this._context.Checklists.First(t => t.Id == id);
		}

		public IQueryable<Checklist> GetChecklists()
		{
			return this._context.Checklists;
		}
	}
}