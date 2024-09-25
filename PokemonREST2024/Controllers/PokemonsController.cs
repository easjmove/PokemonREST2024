﻿using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Pokemon> Get()
        {
            return _pokemonsRepository.GetAll();
        }

        // GET api/<PokemonsController>/5
        [HttpGet]
        [Route("{id}")]
        public Pokemon? Get(int id)
        {
            return _pokemonsRepository.GetByID(id);
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

        // PUT api/<PokemonsController>/5
        [HttpPut("{id}")]
        public Pokemon? Put(int id, [FromBody] Pokemon updatedPokemon)
        {
            return _pokemonsRepository.Update(id, updatedPokemon);
        }

        // DELETE api/<PokemonsController>/5
        [HttpDelete("{id}")]
        public Pokemon? Delete(int id)
        {
            return _pokemonsRepository.Delete(id);
        }
    }
}
