using System;
using System.Collections.Generic;
using System.Text;

namespace msac_tpa.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<SportMan> Sportman { get; }
        void Save();
    }
}
