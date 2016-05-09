using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using ChecklistDomainModel;
using ChecklistDomainModel.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChecklistApi.Controllers
{
	[Route("api/[controller]")]
    public class ChecklistController : Controller
    {
		private readonly IDataAccessProvider _provider;

		public ChecklistController(IDataAccessProvider provider)
		{
			this._provider = provider;
		}

		// GET: api/values
		[HttpGet]
		public IEnumerable<Checklist> Get()
		{
			return this._provider.GetChecklists();
		}

		// GET api/values/5
		[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
