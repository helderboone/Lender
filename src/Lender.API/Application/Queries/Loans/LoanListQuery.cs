using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Queries.Loans
{
    public class LoanListQuery : IRequest<LoanDto[]>
    {
    }
}
