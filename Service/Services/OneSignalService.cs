using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Interface;
using Interface.DbContext;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using Request.RequestCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using System.IO;
using Models;
using static Utilities.CoreContants;
using System.Net;

namespace Service.Services
{
    public class OneSignalService : IOneSignalService
    {
        private IAppDbContext coreDbContext;

        public OneSignalService(
            IAppUnitOfWork unitOfWork,
            IMapper mapper,
            IAppDbContext coreDbContext
            ) 
        {
            this.coreDbContext = coreDbContext;
        }

        public async Task<bool> CreateOneSignal(string title, string content, string[] OneSignalUserID)
        {
            string onesignalAppID = "8d22ff2b-fbe8-44ef-a5c5-376fb8e24f0f";//cái này sửa lại
            string onesignalRestID = "NjBiMzM2ODItZGQ1OC00ZjJkLThmYTItOTdjMjVkY2JlNmQx";//cái này sửa lại

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic " + onesignalRestID);

            var obj = new
            {
                app_id = onesignalAppID,
                headings = new { en = title },
                contents = new { en = content },
                channel_for_external_user_ids = "push",
                //include_player_ids = new string[] { "4ecd269c-7356-11ec-9a39-2255d3251ce2" }//Gửi cho user đc chỉ định
                include_player_ids = OneSignalUserID,//Gửi cho user đc chỉ định
                //included_segments = new string[] { "Subscribed Users" } //Gửi cho tất cả user nào đăng ký
            };
            var param = System.Text.Json.JsonSerializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
                
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
            System.Diagnostics.Debug.WriteLine(responseContent);
            return true;
        }
    }
}
