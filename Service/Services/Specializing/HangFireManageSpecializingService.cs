using Entities;
using Entities.Configuration;
using Extensions;
using Hangfire;
using Interface.DbContext;
using Interface.Services;
using Interface.Services.Configuration;
using Interface.Services.Specializing;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static Utilities.CoreContants;

namespace Service.Services.Specializing
{
    public class HangFireManageSpecializingService : IHangFireManageSpecializingService
    {
        private IAppDbContext context;
        private IUserService userService;
        private IOTPHistoryService oTPHistoryService;
        private IOTPHistoriesSpecializingService oTPHistoriesSpecializingService;
        private IHangFireManageService hangFireManageService;
        //private IPaymentByMomoService PaymentByMomoService;
        public HangFireManageSpecializingService(
             IAppDbContext context
            ,IUserService userService
            , IOTPHistoryService oTPHistoryService
            , IOTPHistoriesSpecializingService oTPHistoriesSpecializingService
            , IHangFireManageService hangFireManageService
            //, IPaymentByMomoService PaymentByMomoService
            ) {
            this.context = context;
            this.userService = userService;
            this.oTPHistoryService = oTPHistoryService;
            this.oTPHistoriesSpecializingService = oTPHistoriesSpecializingService;
            this.hangFireManageService = hangFireManageService;
            //this.PaymentByMomoService = PaymentByMomoService;
        }

        public async Task<bool> GenerateJobDelayForExpiredOTP(Guid OTPHistoryID)
        {
            var oTP = await oTPHistoryService.GetByIdAsync(OTPHistoryID);
            if (oTP == null) throw new Exception("Không tìm thấy mã OTP");
            try {
                var jobId = BackgroundJob.Schedule(
                            () => oTPHistoriesSpecializingService.ExpiredOTP(OTPHistoryID),
                            Timestamp.ToDateTimeLocal((double)oTP.ExpiredDate));
                    
                var hangfireManage = new HangfireManage();
                hangfireManage.JobID = jobId;
                hangfireManage.OTPHistoryID = OTPHistoryID;
                hangfireManage.HangfireManageType = (int)HangfireManageType.HangFireOTP;
                var res = await hangFireManageService.CreateAsync(hangfireManage);
                if (res == false) throw new Exception("Lỗi hệ thống");

                return true;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());

            }
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteJob(Guid hangFireManageId)
        {
            try
            {
                // Xóa lịch
                var Job = await hangFireManageService.GetByIdAsync(hangFireManageId);
                if (Job == null) throw new Exception("Không tìm thấy Job");
                    
                BackgroundJob.Delete(Job.JobID);
                await hangFireManageService.DeleteAsync(Job.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());

            }
            throw new NotImplementedException();
        }

        public async Task<bool> GenerateJobDelayForTrialDisable(Guid userId)
        {
            var user = await userService.GetByIdAsync(userId);
            if (user == null) throw new Exception("Không tìm thấy User");
            try
            {
                var jobId = BackgroundJob.Schedule(
                            () => userService.ExpiredTrial(userId),
                            Timestamp.ToDateTimeLocal((double)user.Created).AddDays((double)TimeTrial.Time));

                var hangfireManage = new HangfireManage();
                hangfireManage.JobID = jobId;
                hangfireManage.UserID = userId;
                hangfireManage.HangfireManageType = (int)HangfireManageType.HangFireTrial;
                var res = await hangFireManageService.CreateAsync(hangfireManage);
                if (res == false) throw new Exception("Lỗi hệ thống");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());

            }
            throw new NotImplementedException();
        }

       
    }
}
