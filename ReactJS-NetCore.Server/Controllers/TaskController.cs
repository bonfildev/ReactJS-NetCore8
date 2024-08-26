using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactJS_NetCore.Server.Models;
using System.Threading;
//controlador de tipo API
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

        /// <summary>
        ///si esta funcion tiene un error
        ///es porque no se estann mandando todos los datos a la bd
        ///la solucion es mandar todos los datos o dejar defaults
        ///es importante quitar el nulo al dato tipo bit para que funcione
        ///y agregar un valor default(0)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] PersonsTask request)
        {
            await _dbcontext.PersonsTasks.AddAsync(request);
            await _dbcontext.SaveChangesAsync();
            //var desc = request.Description;
            //FormattableString str = $"Insert into PersonsTasks (Description) Values('{desc}')";
            //var rowsModified = _dbcontext.Database.ExecuteSqlAsync(str);

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
        [HttpPost]
        [Route("Eliminar/{id:int}")]  
        public async Task<IActionResult> Eliminar(int id)
        {
            PersonsTask tarea = _dbcontext.PersonsTasks.Where(t => t.Idtask == id).FirstOrDefault();

            _dbcontext.PersonsTasks.Remove(tarea);
            await _dbcontext.SaveChangesAsync();

            return Ok("ok");
        }
        
    }
}
