using Microsoft.AspNetCore.Mvc;
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
            List<PersonsTask> lista = _dbcontext.PersonsTasks.OrderByDescending(t => t.Idtask).ThenBy(t=>t.RegisterDate).ToList();
            return Ok(lista);
        }

        [HttpPost]
        [Route("Guardar")]
        public async  Task<IActionResult> Gurardar([FromBody] PersonsTask personsTask){
            await _dbcontext.PersonsTasks.AddAsync(personsTask);
            await _dbcontext.SaveChangesAsync();
            return Ok("ok");
        }
        [HttpPut]
        [Route("Cerrar/{id:int}")]  
        public async Task<IActionResult> Cerrar(int id, [FromBody] PersonsTask personsTask)
        {
            if (personsTask.Idtask == 0)
            {
                _dbcontext.PersonsTasks.Add(personsTask);
            }
            else
            {
                PersonsTask taskUpdate = _dbcontext.PersonsTasks.Where(p => p.Idtask == personsTask.Idtask).FirstOrDefault();

                if (taskUpdate != null)
                {
                    _dbcontext.Entry(taskUpdate).CurrentValues.SetValues(taskUpdate);
                }
            }
            _dbcontext.SaveChanges();
            //SaveProduct(personsTask, id);
            return Ok("ok"); 
        }
        
    }
}
