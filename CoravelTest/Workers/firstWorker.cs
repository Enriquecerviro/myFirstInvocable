using Coravel.Invocable;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Persistence.Repositories;

namespace CoravelTest.Workers
{
    public class firstWorker : IInvocable
    {
        private IMailer _mailer;
        private IUserRepository _repo;

        public SendDailyReportEmailJob(IMailer mailer, IUserRepository repo)
        {
            this._mailer = mailer;
            this._repo = repo;
        }
        public async Task Invoke()
        {
            var users = await this._repo.GetUsersAsync();
            foreach(var user in users)
            {
                var mailable = new DailyReportMailable(user);
                await this._mailer.SendAsync(mailable);
            }
        }
    }
}
