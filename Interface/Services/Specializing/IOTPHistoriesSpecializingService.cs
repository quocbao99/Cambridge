using Entities;
using Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Services.Specializing
{
    public interface IOTPHistoriesSpecializingService
    {
        public Task<bool> ExpiredOTP(Guid oTPHistoryID);
        public Task<OTPHistories> GenerateOTP(Users user);
    }
}
