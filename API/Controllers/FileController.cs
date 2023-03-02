using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utilities;

namespace API.Controllers
{
    /// <summary>
    /// Upload file lên hệ thống
    /// </summary>
    [Route("api/file")]
    [ApiController]
    [Description("Upload file lên hệ thống")]
    [Authorize]
    public class FileController : BaseAPI.Controllers.BaseFileController
    {
        public FileController(IServiceProvider serviceProvider, ILogger<FileController> logger, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : base(serviceProvider, logger, env, httpContextAccessor)
        {
        }
        /// <summary>
        /// Upload Single File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload-file")]
        [Description("Upload Single File lên hệ thống")]
        public override async Task<AppDomainResult> UploadFile(IFormFile file)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            await Task.Run(() =>
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = string.Format("{0}-{1}", Guid.NewGuid().ToString(), file.FileName);
                    //string fileUploadPath = Path.Combine(env.ContentRootPath, CoreContants.UPLOAD_FOLDER_NAME);
                    string fileUploadPath = Path.Combine(env.ContentRootPath, CoreContants.UPLOAD_FOLDER_NAME);
                    string path = Path.Combine(fileUploadPath, fileName);
                    FileUtilities.CreateDirectory(fileUploadPath);
                    var fileByte = FileUtilities.StreamToByte(file.OpenReadStream());
                    FileUtilities.SaveToPath(path, fileByte);

                    var currentLinkSite = $"{Extensions.HttpContext.Current.Request.Scheme}://{Extensions.HttpContext.Current.Request.Host}/{CoreContants.UPLOAD_FOLDER_NAME}/";
                    
                    if (!currentLinkSite.Contains("https")) {
                        currentLinkSite = currentLinkSite.Replace("http", "https");
                    }
                    string fileUrl = Path.Combine(currentLinkSite, fileName);
                    var fileStr = new FileModel() { fileName = fileName, fileUrl = fileUrl };
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = fileStr,
                        ResultMessage = "Upload hình ảnh thành công!",
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
        [Description("Upload Multiple File lên hệ thống")]
        public override async Task<AppDomainResult> UploadFiles(List<IFormFile> files)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            await Task.Run(() =>
            {
                if (files != null && files.Any())
                {
                    List<FileModel> fileNames = new List<FileModel>();
                    foreach (var file in files)
                    {
                        string fileName = string.Format("{0}-{1}", Guid.NewGuid().ToString(), file.FileName);
                        string fileUploadPath = Path.Combine(env.ContentRootPath, CoreContants.UPLOAD_FOLDER_NAME);
                        string path = Path.Combine(fileUploadPath, fileName);
                        FileUtilities.CreateDirectory(fileUploadPath);
                        var fileByte = FileUtilities.StreamToByte(file.OpenReadStream());
                        FileUtilities.SaveToPath(path, fileByte);

                        var currentLinkSite = $"{Extensions.HttpContext.Current.Request.Scheme}://{Extensions.HttpContext.Current.Request.Host}/{CoreContants.UPLOAD_FOLDER_NAME}/";
                        string fileUrl = Path.Combine(currentLinkSite, fileName);
                        var fileStr = new FileModel() { fileName = fileName, fileUrl = fileUrl };

                        fileNames.Add(fileStr);
                    }
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = fileNames,
                        ResultMessage = "Upload danh sách hình ảnh thành công!",
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
            });
            return appDomainResult;
        }
    }
}
