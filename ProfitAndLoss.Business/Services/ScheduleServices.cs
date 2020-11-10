using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProfitAndLoss.Business.Services
{
    public class ScheduleServices : BackgroundService, IDisposable
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountingPeriodRepository _accountingPeriodRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IdentityServices _identityServices;
        private readonly ILogger<ScheduleServices> _logger;

        #endregion fields
        #region consts
        private const string APP_EMAIL = "dhthang1998@gmail.com";
        private const string APP_PASSWORD = "anhthangdepZai123";
        #endregion consts
        public ScheduleServices(ILogger<ScheduleServices> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _accountingPeriodRepository = unitOfWork.AccountingPeriodRepository;
            _memberRepository = unitOfWork.MemberRepository;

 
        }


        /// <summary>
        /// Send report of a accounting period
        /// </summary>
        /// <param name="accountingPeriodId">A closed accounting period</param>
        public void SendReport(Guid accountingPeriodId)
        {
            using (var mainClient = new SmtpClient("smtp.gmail.com", 587))
            {
                mainClient.EnableSsl = true;
                mainClient.Credentials = new NetworkCredential("dhthang1998@gmail.com", "anhthangdepZai123");
                //Get all email of investor
                var emails = _identityServices.GetAllInvestor().Result;
                foreach (var email in emails)
                {
                    MailMessage message = new MailMessage(APP_EMAIL, APP_PASSWORD);
                    message.Subject = "Báo cáo P&L cuối kì";
                    string bodyMessage = "This is body";
                    message.Body = bodyMessage;
                    mainClient.Send(message);
                }

            }

        }
        /// <summary>
        /// Get Current Close Accoungting Period
        /// </summary>
        /// <returns></returns>
        private Tuple<bool, Guid?> CurrentCloseAccountingPeriod()
        {
            var data = _accountingPeriodRepository
                .GetAll(x => x.CloseDate.Date.AddDays(1) == DateTime.Now.Date)
                .Select(x => x.Id).ToList();
            if (data.Any()) // if close accounting period 
            {
                return new Tuple<bool, Guid?>(true, data.FirstOrDefault());
            }
            // if is not close accounting period
            return new Tuple<bool, Guid?>(false, null);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var closePeriodInfor = CurrentCloseAccountingPeriod();
                // If close accounting period
                if (closePeriodInfor.Item1)
                {
                    SendReport(closePeriodInfor.Item2.Value);
                    _logger.LogInformation("Close accounting period at - " + DateTime.Now.Date);
                }
                await Task.Delay(new TimeSpan(24, 0, 0), stoppingToken);
            }
        }
    }
}
