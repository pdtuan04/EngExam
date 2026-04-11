using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace Application.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var response = await next();
                await _unitOfWork.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                await _unitOfWork.CancelAsync();
                throw;
            }
        }
    }
}
