using MediatR;
using Microsoft.Extensions.Logging;
using PrestamoBancario.Application.PrestamosFeature.Command;
using PrestamoBancario.Domain.Constracts;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class RechazarPrestamoHandler : IRequestHandler<RechazarPrestamoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RechazarPrestamoHandler> _logger;
        private readonly ICache _cache;
        public RechazarPrestamoHandler(IUnitOfWork unitOfWork, ICache cache, ILogger<RechazarPrestamoHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }
        public async Task<Unit> Handle(RechazarPrestamoCommand request, CancellationToken cancellationToken)
        {
            var req = request;

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var prestamo = await _unitOfWork.Prestamos.GetByIdAsync(req.Id, cancellationToken); 
                if (prestamo==null) throw new KeyNotFoundException("Prestamo no encontrado");
                prestamo.Rechazar(req.AdminUser);
                await _unitOfWork.Prestamos.UpdateAsync(prestamo, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error denegando prestamo {id}", req.Id);
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
            return Unit.Value;
        }
    }
}
