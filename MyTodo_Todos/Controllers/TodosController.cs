using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTodo_Todos.Interfaces;

namespace MyTodo_Todos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodosService todosService;

        public TodosController(ITodosService todosService)
        {
            this.todosService = todosService;
        }

        // GET: api/<TodosController>
        [HttpGet]
        public IEnumerable<TodoDto> Get()
        {
            return todosService.GetAll();
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public TodoDto Get(long id)
        {
            return todosService.GetById(id);
        }

        // POST api/<TodosController>
        [HttpPost]
        public void Post([FromBody] TodoDto todoDto)
        {
            todosService.Create(todoDto);
        }

        // PUT api/<TodosController>/5
        [HttpPut]
        public void Put([FromBody] TodoDto todoDto)
        {
            todosService.Update(todoDto);
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            todosService.Delete(id);
        }
    }
}
