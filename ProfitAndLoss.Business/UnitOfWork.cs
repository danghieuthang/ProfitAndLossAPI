using ProfitAndLoss.Business.Repositories;
using ProfitAndLoss.Business.Services;
using ProfitAndLoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IUnitOfWork
    {
        #region properties
        IMemberRepository MemberRepository { get; }
        IBrandRepository BrandRepository { get; }
        IStoreRepository StoreRepository { get; }
        IEvidenceRepository EvidenceRepository { get; }
        IReceiptRepository ReceiptRepository { get; }
        IReceiptTypeRepository ReceiptTypeRepository { get; }
        IReceiptHistoryRepository ReceiptHistoryRepository { get; }
        IAccountRepository AccountRepository { get; }
        IStoreAccountRepository StoreAccountRepository { get; }
        ITransactionCategoryRepository TransactionCategoryRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IAccountingPeriodRepository AccountingPeriodRepository { get; }
        IAccountingPeriodDetailRepository AccountingPeriodDetailRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IAccountingPeriodInStoreRepository AccountingPeriodInStoreRepository { get; }

        #endregion properties

        void Commit();
        void CommitAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        #region fields

        private DataContext _context;
        private IMemberRepository _memberRepository;
        private IBrandRepository _brandRepository;
        private IStoreRepository _storeRepository;
        private IReceiptRepository _receptRepository;
        private IEvidenceRepository _evidenceRepository;
        private IReceiptRepository _transactionRepository;
        private IReceiptTypeRepository _transactionTypeRepository;
        private IReceiptHistoryRepository _transactionHistoryRepository;
        private IAccountRepository _accountRepository;
        private IStoreAccountRepository _storeAccountRepository;
        private ITransactionCategoryRepository _TransactionCategoryRepository;
        private ITransactionRepository _transactionDetailRepository;
        private IFeedbackRepository _feedbackRepository;
        private IAccountingPeriodRepository _accountingPeriodRepository;
        private IAccountingPeriodDetailRepository _accountingPeriodDetailRepository;
        private ISupplierRepository _supplierRepository;
        private IAccountingPeriodInStoreRepository _accountingPeriodInStoreRepository;

        #endregion fields

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        #region properties
        public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(_context);

        public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(_context);

        public IStoreRepository StoreRepository => _storeRepository ??= new StoreRepository(_context);

        public IReceiptRepository ReceiptRepository => _receptRepository ??= new ReceiptRepository(_context);

        public IEvidenceRepository EvidenceRepository => _evidenceRepository ??= new EvidenceRepository(_context);


        public IReceiptTypeRepository ReceiptTypeRepository => _transactionTypeRepository ??= new ReceiptTypeRepository(_context);

        public IReceiptHistoryRepository ReceiptHistoryRepository => _transactionHistoryRepository ??= new ReceiptHistoryRepository(_context);

        public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context);

        public IStoreAccountRepository StoreAccountRepository => _storeAccountRepository ??= new StoreAccountRepository(_context);

        public ITransactionCategoryRepository TransactionCategoryRepository => _TransactionCategoryRepository ??= new TransactionCategoryRepository(_context);

        public ITransactionRepository TransactionRepository => _transactionDetailRepository ??= new TransactionRepository(_context);

        public IFeedbackRepository FeedbackRepository => _feedbackRepository ??= new FeedbackRepository(_context);

        public IAccountingPeriodRepository AccountingPeriodRepository => _accountingPeriodRepository ??= new AccountingPeriodRepository(_context);

        public IAccountingPeriodDetailRepository AccountingPeriodDetailRepository => _accountingPeriodDetailRepository ??= new AccountingPeriodDetailRepository(_context);

        public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(_context);

        public IAccountingPeriodInStoreRepository AccountingPeriodInStoreRepository => _accountingPeriodInStoreRepository ??= new AccountingPeriodInStoreRepository(_context);

        #endregion properties

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
