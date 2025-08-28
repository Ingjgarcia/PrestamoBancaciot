using MediatR;
using Microsoft.Extensions.Logging;
using PrestamoBancario.Application.PrestamosFeature.Command;
using PrestamoBancario.Domain.Constracts;

namespace PrestamoBancario.Application.Prestamo.Handler
{
    internal class AprobarPrestamoHandler : IRequestHandler<AprobarPrestamoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AprobarPrestamoHandler> _logger;
        private readonly ICache _cache;
        public AprobarPrestamoHandler(IUnitOfWork unitOfWork, ICache cache, ILogger<AprobarPrestamoHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }
        public async Task<Unit> Handle(AprobarPrestamoCommand request, CancellationToken cancellationToken)
        {
            var req = request;
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var prestamo = await _unitOfWork.Prestamos.GetByIdAsync(req.Id, cancellationToken);
                if (prestamo == null) throw new KeyNotFoundException("Prestamo no encontrado");
                prestamo.Aprobar(req.AdminUser);
                await _unitOfWork.Prestamos.UpdateAsync(prestamo, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Aprobando Prestamo {id}", req.Id);
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }

            _cache?.Remove($"Prestamo:{request.Id}");
            return Unit.Value;

        }
    }
}
