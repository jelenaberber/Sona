using Application;
using Newtonsoft.Json;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Implementation.Logging.UseCase
{
    public class DBUseCaseLogger : IUseCaseLogger
    {
        private Context _context;

        public DBUseCaseLogger(Context context)
        {
            _context = context;
        }

        public void Log(UseCaseLog log)
        {
            UseCaseAuditLog auditLog = new UseCaseAuditLog
            {
                UseCaseName = log.UseCaseName,
                UseCaseData = JsonConvert.SerializeObject(log.UseCaseData),
                Username = log.Username,
                ExecutedAt = log.ExecutedAt
            };

            _context.UseCaseLogs.Add(auditLog);
            _context.SaveChanges();
        }
    }
}
