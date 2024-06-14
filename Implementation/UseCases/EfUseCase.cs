using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public abstract class EfUseCase
    {
        private readonly Context _context;

        protected EfUseCase(Context context)
        {
            _context = context;
        }

        protected Context Context => _context;
    }
}
