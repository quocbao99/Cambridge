using Entities;
using Entities.Configuration;
using Interface.Services;
using Interface.Services.Configuration;
using Interface.Services.Specializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static Utilities.CoreContants;

namespace Service.Services.Specializing
{
    public class OTPHistoriesSpecializingService : IOTPHistoriesSpecializingService
    {
        private IOTPHistoryService oTPHistoryService;
        public OTPHistoriesSpecializingService(IOTPHistoryService oTPHistoryService) {

            this.oTPHistoryService = oTPHistoryService;
        }

        public async Task<bool> ExpiredOTP(Guid oTPHistoryID)
        {
            var oTPHistories = await oTPHistoryService.GetByIdAsync(oTPHistoryID);
            if (oTPHistories == null) throw new Exception("Lỗi hệ thống");
            oTPHistories.Status = (int)OTPStatus.Expired;
            return await oTPHistoryService.UpdateFieldAsync(oTPHistories, d => d.Status);
        }

        public async Task<OTPHistories> GenerateOTP(Users user)
        {
            var OTP = new OTPHistories()
            {
                UserId = user.Id
                ,
                OtpValue = RandomUtilities.RandomNumber(6)
                ,
                Phone = user.Phone
                ,
                Email = user.Email
                ,
                IsEmail = true
                ,
                Status = (int)OTPStatus.UnExpired
                , ExpiredDate = Timestamp.Date(DateTime.Now.AddMinutes((int)TimeExpiredOTPMinute.Time))
            };
            var res = await oTPHistoryService.CreateAsync(OTP);
            if (res == false) throw new Exception("Không tạo được mã OTP");
            return OTP;
        }
    }
}
