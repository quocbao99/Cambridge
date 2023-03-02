using Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utilities;

namespace BaseAPI.Controllers
{
    [ApiController]
    public abstract class BaseFileController : ControllerBase
    {
        protected readonly ILogger<ControllerBase> logger;
        protected readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseFileController(IServiceProvider serviceProvider, ILogger<ControllerBase> logger, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.env = env;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Upload Single File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload-file")]
        [AppAuthorize]
        [Description("Upload Single File lên hệ thống")]
        public virtual async Task<AppDomainResult> UploadFile(IFormFile file)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            await Task.Run(() =>
            {
                if (file != null && file.Length > 0)
                {
                    string host = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value;
                    string fileName = string.Format("{0}-{1}", Guid.NewGuid().ToString(), file.FileName);
                    string fileUploadPath = Path.Combine(env.ContentRootPath, CoreContants.UPLOAD_FOLDER_NAME);
                    string path = Path.Combine(fileUploadPath, fileName);
                    FileUtilities.CreateDirectory(fileUploadPath);
                    var fileByte = FileUtilities.StreamToByte(file.OpenReadStream());
                    FileUtilities.SaveToPath(path, fileByte);
                    string fileImgPath = host + "/" + fileName;
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = fileName,
                        ResultMessage = ApiMessage.SuccessfulUpload,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
            });
            return appDomainResult;
        }

        /// <summary>
        /// Upload Multiple File
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("upload-multiple-files")]
        [AppAuthorize]
        [Description("Upload Multiple File lên hệ thống")]
        public virtual async Task<AppDomainResult> UploadFiles(List<IFormFile> files)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            await Task.Run(() =>
            {
                if (files != null && files.Any())
                {
                    List<string> fileNames = new List<string>();
                    string host = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value;
                    foreach (var file in files)
                    {
                        string fileName = string.Format("{0}-{1}", Guid.NewGuid().ToString(), file.FileName);
                        string fileUploadPath = Path.Combine(env.ContentRootPath, CoreContants.UPLOAD_FOLDER_NAME);
                        string path = Path.Combine(fileUploadPath, fileName);
                        FileUtilities.CreateDirectory(fileUploadPath);
                        var fileByte = FileUtilities.StreamToByte(file.OpenReadStream());
                        FileUtilities.SaveToPath(path, fileByte);
                        string fileImgPath = host + "/" + fileName;
                        fileNames.Add(fileImgPath);
                    }
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = fileNames,
                        ResultMessage = ApiMessage.SuccessfulUpload,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
            });
            return appDomainResult;
        }
    }
}