using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Zadania_8.DAL;
using Zadania_8.Models;
using Zadania_8.Interfaces;
using Zadania_8.Logger;

namespace Zadania_8.Controllers
{
    public class ArtistsController : ApiController
    {
        private readonly IArtistRepository _repo;
        private readonly ILogger _logger;

        public ArtistsController(IArtistRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/Artists
        public IEnumerable<Artist> GetArtists()
        {
            _logger.Write("GET all was called", LogLevel.INFO);

            return _repo.GetAll();
        }

        // GET: api/Artists/5
        [ResponseType(typeof(Artist))]
        public IHttpActionResult GetArtist(int id)
        {
            _logger.Write("GET was called", LogLevel.INFO);

            Artist artist = _repo.Get(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        // PUT: api/Artists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtist(int id, Artist artist)
        {
            _logger.Write("PUT was called", LogLevel.INFO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.Id)
            {
                return BadRequest();
            }

            try
            {
                _repo.Update(artist);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Artists
        [ResponseType(typeof(Artist))]
        public IHttpActionResult PostArtist(Artist artist)
        {
            _logger.Write("POST was called", LogLevel.INFO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.Add(artist);

            return CreatedAtRoute("DefaultApi", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteArtist(int id)
        {
            _logger.Write("DELETE was called", LogLevel.INFO);

            _repo.Delete(id);
            return Ok();
        }

        private bool ArtistExists(int id)
        {
            return _repo.GetAll().Count(e => e.Id == id) > 0;
        }
    }
}