using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactJS_NetCore.Server.Models;

namespace ReactJS_NetCore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly PersonDbContext _dbcontext;

        public TaskController(PersonDbContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<PersonsTask> lista = _dbcontext.PersonsTasks.OrderByDescending(t => t.Idtask).ThenBy(t=>t.Finished).ToList();
            return Ok(lista);
        }

        [HttpPut]
        [Route("Guardar")]
        public async  Task<IActionResult> Guardar([FromBody] PersonsTask personsTask){
            await _dbcontext.PersonsTasks.AddAsync(personsTask);
            await _dbcontext.SaveChangesAsync();
            return Ok("ok");
        }
        [HttpPost]
        [Route("Cerrar/{id:int}")]  
        public async Task<IActionResult> Cerrar(int id)
        { 
            var rowsModified = _dbcontext.Database.ExecuteSql($"UPDATE PersonsTasks SET Finished = 1 WHERE IDTask = {id}");
            //SaveProduct(personsTask, id);
            return Ok("ok"); 
        }
        
    }
}
