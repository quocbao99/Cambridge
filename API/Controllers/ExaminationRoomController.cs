using Entities;
using Entities.Search;
using Extensions;
using Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Request.RequestCreate;
using Request.RequestUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Utilities;
using ZoomLibary;
using static Utilities.CatalogueEnums;

namespace BaseAPI.Controllers
{
    /// <summary>
    /// Quản lý phòng thi
    /// </summary>
    [Route("api/examination-Room")]
    [ApiController]
    [Description("Quản lý phòng thi")]
    [Authorize]
    public class ExaminationRoomController : BaseController<ExaminationRoom, ExaminationRoomModel, ExaminationRoomCreate, ExaminationRoomUpdate, ExaminationRoomSearch>
    {
        public ExaminationRoomController(IServiceProvider serviceProvider, ILogger<BaseController<ExaminationRoom, ExaminationRoomModel, ExaminationRoomCreate, ExaminationRoomUpdate, ExaminationRoomSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IExaminationRoomService>();
        }
        [AllowAnonymous]
        public override async Task<AppDomainResult> AddItem([FromBody] ExaminationRoomCreate itemModel)
        {
            ZoomClient zoomClient = new ZoomClient();
            var authorizationResponseData = await zoomClient.GetAuthorizationRequest();
            if (string.Empty.Equals(authorizationResponseData.access_token) || authorizationResponseData is null) 
                throw new MyException("Lỗi liên kết với Zoom!", HttpStatusCode.BadRequest);
            zoomClient.SetToken(authorizationResponseData.access_token);
            await zoomClient.CreateMeeting(new ZoomAPI.Models.Request.CreateMeetingRequest()
            {
                topic = itemModel.ExamRoomCode
                , type = 2 // default value
                ,pre_schedule = false
                ,default_password = false
                ,password = itemModel.ExamRoomCode
                ,start_time = DateTime.Now.AddMinutes(5)
                ,duration = 30
            });
            return await base.AddItem(itemModel);
        }
    }
}