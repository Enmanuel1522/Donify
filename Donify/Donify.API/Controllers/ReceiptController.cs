using Donify.Application.DTOs;
using Donify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptDto>> GetReceipt(int id)
        {
            var receipt = await _receiptService.GetByIdAsync(id);
            if (receipt == null)
                return NotFound($"No se encontró un recibo con id {id}");

            return Ok(receipt);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptDto>>> GetAllReceipts()
        {
            var receipts = await _receiptService.GetAllAsync();
            return Ok(receipts);
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptDto>> CreateReceipt(ReceiptDto dto)
        {
            var created = await _receiptService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetReceipt), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceipt(int id, ReceiptDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            await _receiptService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            await _receiptService.DeleteAsync(id);
            return NoContent();
        }
    }
}