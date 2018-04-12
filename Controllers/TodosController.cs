using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using core_pg.Entities;
using Microsoft.AspNetCore.Mvc;

namespace core_pg.Controllers
{
    [Route("api/[controller]")]
    public class TodosController: Controller
    {
        private DbCtx _db;

        public TodosController(DbCtx db)
        {
            _db = db;
        }

        public List<Todo> Get()
        {
            return _db.Todos.ToList();
        }
        
        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]Todo todo)
        {
            if (ModelState.IsValid)
            {
                _db.Todos.Add(todo);
                _db.SaveChanges();
                return Ok(todo);
            }

            return BadRequest();
        }

    }
}