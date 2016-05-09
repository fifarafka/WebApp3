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
    public class PaintingsController : ApiController
    {
        private readonly IPaintingRepository _repo;
        private readonly ILogger _logger;

        public PaintingsController(IPaintingRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/Paintings
        public IEnumerable<Painting> GetPaintings()
        {
            _logger.Write("GET all was called", LogLevel.INFO);

            return _repo.GetAll();
        }

        // GET: api/Paintings/5
        [ResponseType(typeof(Painting))]
        public IHttpActionResult GetPainting(int id)
        {
            _logger.Write("GET was called", LogLevel.INFO);

            Painting painting = _repo.Get(id);
            if (painting == null)
            {
                return NotFound();
            }

            return Ok(painting);
        }

        // PUT: api/Paintings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPainting(int id, Painting painting)
        {
            _logger.Write("PUT was called", LogLevel.INFO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != painting.Id)
            {
                return BadRequest();
            }

            try
            {
                _repo.Update(painting);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaintingExists(id))
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

        // POST: api/Paintings
        [ResponseType(typeof(Painting))]
        public IHttpActionResult PostPainting(Painting painting)
        {
            _logger.Write("POST was called", LogLevel.INFO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(painting);

            return CreatedAtRoute("DefaultApi", new { id = painting.Id }, painting);
        }

        // DELETE: api/Paintings/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult DeletePainting(int id)
        {
            _logger.Write("DELETE was called", LogLevel.INFO);

            return Ok(_repo.Delete(id));
        }

        private bool PaintingExists(int id)
        {
            return _repo.GetAll().Count(e => e.Id == id) > 0;
        }
    }
}