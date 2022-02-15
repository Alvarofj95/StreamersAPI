using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamersAPI.Data;

namespace StreamersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamersController : ControllerBase
    {
        private static List<Streamers> streamers = new()
        {
                new Streamers {
                    Id = 1,
                    Nombre = "IlloJuan",
                    Pais = "Espana",
                    Seguidores = "1.000.000"
                },
                new Streamers {
                    Id = 2,
                    Nombre = "Ibai",
                    Pais = "Espana",
                    Seguidores = "4.000.000"
                },
            };
        private readonly DataContext context;

        public StreamersController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Streamers>>> Get()
        {
            return Ok(await this.context.Streamers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Streamers>> Get(int id)
        {
            var streamer = await this.context.Streamers.FindAsync(id);
            if (streamer == null)
                return BadRequest("Streamer no encontrado.");
            return Ok(await this.context.Streamers.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<List<Streamers>>> AddStreamer(Streamers streamer)
        {
            this.context.Streamers.Add(streamer);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Streamers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Streamers>>> UpdateStreamer(Streamers request)
        {
            var streamer = await this.context.Streamers.FindAsync(request.Id);
            if (streamer == null)
                return BadRequest("Streamer no encontrado.");
            streamer.Nombre = request.Nombre;
            streamer.Pais = request.Pais;
            streamer.Seguidores = request.Seguidores;

            await this.context.SaveChangesAsync();
            return Ok(await this.context.Streamers.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Streamers>>> Delete(int id)
        {
            var streamer = await this.context.Streamers.FindAsync(id);
            if (streamer == null)
                return BadRequest("Streamer no encontrado.");

            this.context.Streamers.Remove(streamer);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.Streamers.ToListAsync());
        }


    }
}
