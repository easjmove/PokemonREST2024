using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PokemonLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonREST2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private PokemonsRepository _pokemonsRepository;

        public PokemonsController(PokemonsRepository pokemonsRepository)
        {
            _pokemonsRepository = pokemonsRepository;
        }

        // GET: api/<PokemonsController>
        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> Get()
        {
            return _pokemonsRepository.GetAll();
        }

        [HttpGet("test")]
        public ActionResult<string> GetString([FromHeader] string? hej)
        {
            if (hej == null)
            {
                return Ok("Du har ikke udfyldt hej, men det er ok!");
            }
            if (int.TryParse(hej, out int parsedHej))
            {
                Response.Headers.Add("monster", "666");
                return Ok("Du har sendt: " + parsedHej);
            }
            else
            {
               return BadRequest("Hej header skal være en int!");
            }
        }

        // GET api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Pokemon> Get(int id)
        {
            Pokemon? foundPokemon = _pokemonsRepository.GetByID(id);
            if (foundPokemon == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(foundPokemon);
            }
        }

        // POST api/<PokemonsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Pokemon> Post([FromBody] Pokemon newPokemon)
        {
            try
            {
                Pokemon createdPokemon = _pokemonsRepository.Add(newPokemon);
                return Created("/" + createdPokemon.Id, createdPokemon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // PUT api/<PokemonsController>/5
        [HttpPut("{id}")]
        public ActionResult<Pokemon?> Put(int id, [FromBody] Pokemon updatedPokemon)
        {
            try
            {
                Pokemon? pokemon = _pokemonsRepository.Update(id, updatedPokemon);
                if (pokemon != null)
                {
                    return Ok(pokemon);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PokemonsController>/5
        [HttpDelete("{id}")]
        public Pokemon? Delete(int id)
        {
            return _pokemonsRepository.Delete(id);
        }
    }
}
