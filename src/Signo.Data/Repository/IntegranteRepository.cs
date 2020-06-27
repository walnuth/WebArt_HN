using System;
using System.Collections.Generic;
using System.Text;
using Signo.Business.Interfaces;
using Signo.Business.Models;
using Signo.Data.Context;

namespace Signo.Data.Repository
{
    public class IntegranteRepository : Repository<Integrante>, IIntegranteRepository
    {

        public IntegranteRepository(MeuDbContext context) : base(context) { }



    }
}
