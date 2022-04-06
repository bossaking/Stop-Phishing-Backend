using System;
using Stop_Phishing.DAL.Interfaces;
using Stop_Phishing.Models;

namespace Stop_Phishing.DAL
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<User> UserRepository { get; }


        public UnitOfWork(IGenericRepository<User> userRepository, ApplicationDbContext context)
        {
            this.UserRepository = userRepository;
            this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}