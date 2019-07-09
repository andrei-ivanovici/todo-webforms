using System.Collections.Generic;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Core.Models;


namespace ToDo.Api.Core.Controllers
{
    [Route("todos")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDoService _toDosService;

        public ToDosController()
        {
            _toDosService = new ToDoService();
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _toDosService.GetToDos());
        }


        // POST api/values
        [HttpPost]
        public Task Post([FromBody] Business.ToDo value)
        {
            return _toDosService.AddItem(value);
        }

        // PUT api/values/5
        [HttpPatch("{id}")]
        public Task Patch(string id, [FromBody] ToDoStatus status)
        {
            return _toDosService.MarkAsCompleted(id, status.IsCompleted);
        }

        // DELETE api/values/5
        [HttpDelete("completed")]
        public Task Delete()
        {
            return _toDosService.ClearCompleted();
        }

        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
            return _toDosService.RemoveItem(id);
        }
    }
}