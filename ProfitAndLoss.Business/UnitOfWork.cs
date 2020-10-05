using ProfitAndLoss.Business.Repositories;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IUnitOfWork
    {
        IMemberRepository MemberRepository { get; }
        IBrandRepository BrandRepository { get; }
        void Commit();
        void CommitAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        DataContext _context;

        private IMemberRepository _memberRepository;
        private IBrandRepository _brandRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(_context);

        public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void CommitAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
