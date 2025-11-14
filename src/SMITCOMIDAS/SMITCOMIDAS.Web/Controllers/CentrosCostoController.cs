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
    public class CentrosCostoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CentrosCostoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/centroscosto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CentroCosto>>> GetCentrosCosto()
        {
            return await _context.CentrosCosto
                .Include(cc => cc.Compania)
                .Where(cc => cc.Activo)
                .ToListAsync();
        }

        // GET: api/centroscosto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CentroCosto>> GetCentroCosto(int id)
        {
            var centroCosto = await _context.CentrosCosto
                .Include(cc => cc.Compania)
                .FirstOrDefaultAsync(cc => cc.Id == id);

            if (centroCosto == null)
                return NotFound();

            return centroCosto;
        }

        [HttpGet("proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<CentroCosto>>> GetCentrosCostoByProveedor(int proveedorId)
        {
            try
            {
                // Obtener los centros de costo de los pedidos del proveedor
                var centrosCosto = await _context.Pedidos
                    .Include(p => p.CentroCosto)
                    .Where(p => p.Detalles.Any(d => d.ElementoMenu.Menu.ProveedorId == proveedorId))
                    .Select(p => p.CentroCosto)
                    .Distinct()
                    .Where(cc => cc != null && cc.Activo)
                    .ToListAsync();

                return Ok(centrosCosto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener centros de costo: {ex.Message}");
            }
        }

        // POST: api/centroscosto
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CentroCosto>> CreateCentroCosto(CentroCosto centroCosto)
        {
            // Validar que la compañía existe
            var companiaExists = await _context.Companias.AnyAsync(c => c.Id == centroCosto.CompaniaId);
            if (!companiaExists)
                return BadRequest("Compañía no encontrada");

            // Validar que no exista otro centro con el mismo código
            var existeCodigo = await _context.CentrosCosto
                .AnyAsync(cc => cc.Codigo == centroCosto.Codigo && cc.CompaniaId == centroCosto.CompaniaId);

            if (existeCodigo)
                return BadRequest("Ya existe un centro de costo con este código en la compañía");

            _context.CentrosCosto.Add(centroCosto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCentroCosto), new { id = centroCosto.Id }, centroCosto);
        }

        // PUT: api/centroscosto/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCentroCosto(int id, CentroCosto centroCosto)
        {
            if (id != centroCosto.Id)
                return BadRequest();

            _context.Entry(centroCosto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentroCostoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/centroscosto/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCentroCosto(int id)
        {
            var centroCosto = await _context.CentrosCosto.FindAsync(id);
            if (centroCosto == null)
                return NotFound();

            centroCosto.Activo = false; // Soft delete
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CentroCostoExists(int id)
        {
            return _context.CentrosCosto.Any(e => e.Id == id);
        }

    }
}
