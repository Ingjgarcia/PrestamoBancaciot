using Microsoft.Extensions.Logging;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Application.Servicios
{
    internal class ServicioPrestamo 
    {
        private readonly IPrestamoRepository _prestamo;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<ServicioPrestamo> _logger;

        public ServicioPrestamo(IPrestamoRepository prestamo, IUnitOfWork uow, ILogger<ServicioPrestamo> logger)
        { _prestamo = prestamo; _uow = uow; _logger = logger; }

        public async Task<Prestamo> CrearAsync(Guid idUsuario, decimal cantidad, int tiempo, CancellationToken ct)
        {
            if (cantidad <= 0) throw new ArgumentException("cantidad debe ser mayor que cero");
            if (tiempo <= 0) throw new ArgumentException("el tiempo debe ser mayor a cero");
            var loan = new Prestamo { IdUsuario = idUsuario, Cantida = cantidad, Tiempo = tiempo };
            await _prestamo.AddAsync(loan, ct);
            return loan;
        }

        public async Task AprobarAsync(Guid id, string adminEmail, CancellationToken ct)
        {
            await _uow.BeginTransactionAsync(ct);
            try
            {
                var loan = await _prestamo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Prestamo no encontrado");
                loan.Aprobar(adminEmail);
                await _prestamo.UpdateAsync(loan, ct);
                await _uow.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Aprobando Prestamo {id}", id);
                await _uow.RollbackAsync(ct);
                throw;
            }
        }

        public async Task DenegarAsync(Guid id, string adminEmail, CancellationToken ct)
        {
            await _uow.BeginTransactionAsync(ct);
            try
            {
                var prestamo = await _prestamo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Prestamo no encontrado");
                prestamo.Rechazar(adminEmail);
                await _prestamo.UpdateAsync(prestamo, ct);
                await _uow.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error denegando prestamo {id}", id);
                await _uow.RollbackAsync(ct);
                throw;
            }
        }

    }
}
