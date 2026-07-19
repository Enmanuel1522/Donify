using Donify.API.Data;
using Donify.API.DTOs;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donify.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ReceiptController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptDto>> GetReceipt(int id)
        {
            var receipt = await _uow.Receipts.GetByIdAsync(id);
            if (receipt == null)
                return NotFound($"No se encontró un recibo con id {id}");

            var dto = new ReceiptDto
            {
                Id = receipt.Id,
                DonationId = receipt.DonationId,
                ReceiptNumber = receipt.ReceiptNumber,
                IssuedAt = receipt.IssuedAt,
                SentByEmail = receipt.SentByEmail
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptDto>> CreateReceipt(ReceiptDto dto)
        {
            var donationExists = await _uow.Donations.GetByIdAsync(dto.DonationId);
            if (donationExists == null)
                return NotFound($"No se encontró una donación con id {dto.DonationId}");

            var receipt = new Receipt
            {
                DonationId = dto.DonationId,
                ReceiptNumber = dto.ReceiptNumber,
                IssuedAt = DateTime.UtcNow,
                SentByEmail = false
            };

            await _uow.Receipts.AddAsync(receipt);
            await _uow.SaveAsync();

            dto.Id = receipt.Id;
            return CreatedAtAction(nameof(GetReceipt), new { id = receipt.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceipt(int id, ReceiptDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la ruta no coincide con el del cuerpo");

            var receipt = await _uow.Receipts.GetByIdAsync(id);
            if (receipt == null)
                return NotFound($"No se encontró un recibo con id {id}");

            receipt.ReceiptNumber = dto.ReceiptNumber;
            receipt.SentByEmail = dto.SentByEmail;

            await _uow.Receipts.UpdateAsync(receipt);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            var receipt = await _uow.Receipts.GetByIdAsync(id);
            if (receipt == null)
                return NotFound($"No se encontró un recibo con id {id}");

            await _uow.Receipts.DeleteAsync(id);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}