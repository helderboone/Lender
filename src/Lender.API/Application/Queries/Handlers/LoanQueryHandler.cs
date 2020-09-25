using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Application.Queries.Loans;
using Lender.API.Data;
using Lender.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Queries.Handlers
{
    public class LoanQueryHandler : IRequestHandler<LoanListQuery, LoanDto[]>

    {
        private readonly LenderContext _context;
        private readonly IMapper _mapper;

        public LoanQueryHandler(LenderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoanDto[]> Handle(LoanListQuery request, CancellationToken cancellationToken)
        {
            var loans = await _context.Loans
                .Include(x => x.Game)
                .Include(x => x.Friend)
                .Where(x => x.EndDate == null).ToArrayAsync();

            return _mapper.Map<Loan[], LoanDto[]>(loans);
        }
    }
}
