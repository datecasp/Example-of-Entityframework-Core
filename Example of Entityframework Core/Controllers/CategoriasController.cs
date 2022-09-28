﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Models.DataModels;

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly EntityDBContext _context;

        public CategoriasController(EntityDBContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetCategorias(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);

            if (categorias == null)
            {
                return NotFound();
            }

            return categorias;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorias(int id, Categorias categorias)
        {
            if (id != categorias.CategoriasId)
            {
                return BadRequest();
            }

            _context.Entry(categorias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categorias>> PostCategorias(Categorias categorias)
        {
            _context.Categorias.Add(categorias);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorias", new { id = categorias.CategoriasId }, categorias);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorias(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categorias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriasExists(int id)
        {
            return _context.Categorias.Any(e => e.CategoriasId == id);
        }
    }
}
