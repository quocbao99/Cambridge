using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Services.Specializing
{
    public interface IHangFireManageSpecializingService
    {
        public Task<bool> GenerateJobDelayForExpiredOTP(Guid OTPHistoryId);
        Task<bool> DeleteJob(Guid hangFireManageId);
        Task<bool> GenerateJobDelayForTrialDisable(Guid userId);
        
    }
}
