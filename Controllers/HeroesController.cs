using Microsoft.AspNetCore.Mvc;
using MobileLegendsAPI.Db;
using MobileLegendsAPI.Models;
using MobileLegendsAPI.ViewModels;
using SqlKata;
using SqlKata.Execution;
using static Humanizer.In;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobileLegendsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        // GET: api/<HeroesController>
        [HttpGet]
        public List<Heroes> Get([FromQuery] string? role, [FromQuery] string? type)
        {

            var db = DbManager.Connect();

            var query = db.Query("Heroes");

            
            if(role != null)
            {
                query.Where("Role", "LIKE", role);
                  
            }

            if (type != null)
            {
                query.Where("Type", "LIKE", $"%{type}%");
            }
            query.OrderByDesc("Price");



            var heroes = query.GetAsync<Heroes>().Result.ToList();

            return heroes;

        }

        // POST api/<HeroesController>
        [HttpPost]
        public async Task<string> Post([FromBody] HeroesViewModel hero)
        {
            var db = DbManager.Connect();

            Heroes heroData = new Heroes
            {
                Id = Guid.NewGuid().ToString(),
                Name = hero.Name,
                Description = hero.Description,
                Role = hero.Role,
                Type = hero.Type,
                Price = hero.Price,
                Enabled = hero.Enabled
            };

            await db.Query("Heroes").InsertAsync(heroData); 

            return heroData.Id; 
        }

        // PUT api/<HeroesController>/5
        [HttpPut("{id}")]
        public async Task<string> Put(string id, [FromBody] HeroesViewModel hero)
        {
            var db = DbManager.Connect();

            Heroes heroData = new Heroes
            {
                Id = id,
                Name = hero.Name,
                Description = hero.Description,
                Role = hero.Role,
                Type = hero.Type,
                Price = hero.Price,
                Enabled = hero.Enabled
            };

            await db.Query("Heroes").Where("Id", "LIKE", id).UpdateAsync(heroData);

            return heroData.Id;


        }

        // DELETE api/<HeroesController>/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteAsync(string id, string name)
        {
            var db = DbManager.Connect();

            var query = db.Query("Heroes").Where("Id", "LIKE", id);

            await query.DeleteAsync();

            return id + "has been deleted successfully";
        }
    }
}
