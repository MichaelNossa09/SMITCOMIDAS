using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Web.Data;

namespace SMITCOMIDAS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompaniasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/companias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compania>>> GetCompanias()
        {
            return await _context.Companias
                .Where(c => c.Activa)
                .ToListAsync();
        }

        // GET: api/companias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compania>> GetCompania(int id)
        {
            var compania = await _context.Companias.FindAsync(id);

            if (compania == null)
                return NotFound();

            return compania;
        }

        // POST: api/companias
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Compania>> CreateCompania(Compania compania)
        {
            // Validar que no exista otra compañía con el mismo NIT
            var existeNIT = await _context.Companias
                .AnyAsync(c => c.NIT == compania.NIT);

            if (existeNIT)
                return BadRequest("Ya existe una compañía con este NIT");

            _context.Companias.Add(compania);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompania), new { id = compania.Id }, compania);
        }

        // PUT: api/companias/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCompania(int id, Compania compania)
        {
            if (id != compania.Id)
                return BadRequest();

            _context.Entry(compania).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompaniaExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/companias/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCompania(int id)
        {
            var compania = await _context.Companias.FindAsync(id);
            if (compania == null)
                return NotFound();

            compania.Activa = false; // Soft delete
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompaniaExists(int id)
        {
            return _context.Companias.Any(e => e.Id == id);
        }
    }
}
