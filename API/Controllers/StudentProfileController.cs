using ClosedXML.Excel;
using Entities;
using Entities.Search;
using Extensions;
using Interface.DbContext;
using Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Request.RequestCreate;
using Request.RequestUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Utilities;
using static Utilities.CatalogueEnums;

namespace BaseAPI.Controllers
{
    /// <summary>
    /// Quản lý Profile
    /// </summary>
    [Route("api/studnent-profile")]
    [ApiController]
    [Description("Quản lý hồ sơ")]
    [Authorize]
    public class StudentProfileController : BaseController<StudentProfile, StudentProfileModel, StudentProfileCreate, StudentProfileUpdate, StudentProfileSearch>
    {
        private IAppDbContext coreDbContext;
        private IProfileGroupService profileGroupService;

        public StudentProfileController(IServiceProvider serviceProvider, ILogger<BaseController<StudentProfile, StudentProfileModel, StudentProfileCreate, StudentProfileUpdate, StudentProfileSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IStudentProfileService>();
            this.coreDbContext = serviceProvider.GetRequiredService<IAppDbContext>();
            this.profileGroupService = serviceProvider.GetRequiredService<IProfileGroupService>();
        }

        //Import Hồ sơ thí sinh
        [HttpPost("import-StudentProfile")]
        [Authorize]
        public async Task<AppDomainResult> UploadFile(IFormFile file)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            await Task.Run(async () =>
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
                    List<StudentProfile> StudentProfiles = new List<StudentProfile>();
                    using (IDbContextTransaction transaction = coreDbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            ProfileGroup profileGroup = new ProfileGroup() {
                                Created = Timestamp.Now(),
                                Code = GenerateCodeUtilities.GenerateCode("Profile",""),
                                Name = GenerateCodeUtilities.GenerateCode("Profile",""),
                            };

                            await profileGroupService.CreateAsync(profileGroup);

                            /// đọc file excel
                            int rowno = 1;
                            XLWorkbook workbook = XLWorkbook.OpenFromTemplate(path);
                            var sheets = workbook.Worksheets.First();
                            var rows = sheets.Rows().ToList();
                            if (rows.Count()==0) throw new MyException("Dữ liệu rỗng", HttpStatusCode.BadRequest);

                            var mails = rows.GroupBy(d => d.Cell(10).Value.ToString());
                            if (mails.Where(d => d.Count() > 1).Count() > 1) throw new MyException("Email trùng lặp", HttpStatusCode.BadRequest);
                            foreach (var row in rows)
                            {
                                if (rowno != 1)
                                {
                                    DateTime dob;
                                    try {
                                        dob = Convert.ToDateTime(row.Cell(3).Value.ToString(), new CultureInfo("en-US"));
                                    }
                                    catch (Exception ex) {
                                        throw new MyException("Sai Định dạng ngày", HttpStatusCode.BadRequest);
                                    }
                                    var email = row.Cell(11).Value.ToString();
                                    if(String.Empty.Equals(email) || email is null)
                                        throw new MyException("Thiếu thông tin mail", HttpStatusCode.BadRequest);

                                    StudentProfiles.Add(new StudentProfile() { 
                                        Created = Timestamp.Now(),
                                        FullName = row.Cell(2).Value.ToString(),
                                        Dob = Timestamp.Date(dob) ,
                                        Pob = row.Cell(4).Value.ToString(),
                                        Gender = Int32.Parse(row.Cell(5).Value.ToString()),
                                        IdentificationNumber= row.Cell(6).Value.ToString(),
                                        SpecificAddress = row.Cell(7).Value.ToString(),
                                        ExamLevel = row.Cell(8).Value.ToString(),
                                        PhoneNumber = row.Cell(9).Value.ToString(),
                                        PhoneBackupNumber = row.Cell(10).Value.ToString(),
                                        Email = row.Cell(11).Value.ToString(),
                                        PlaceOfWork = row.Cell(12).Value.ToString(),
                                        Occupation = row.Cell(13).Value.ToString(),
                                    });
                                }
                                else
                                {
                                    rowno = 2;
                                }
                            }
                            await domainService.CreateAsync(StudentProfiles);
                            await transaction.CommitAsync();
                        }
                        catch (MyException ex)
                        {
                            await transaction.RollbackAsync();

                            throw new MyException(ex.Message.ToString(), ex.HttpStatusCode);
                        }
                    }


                    var currentLinkSite = $"{Extensions.HttpContext.Current.Request.Scheme}://{Extensions.HttpContext.Current.Request.Host}/{CoreContants.UPLOAD_FOLDER_NAME}/";
                    if (!currentLinkSite.Contains("https"))
                    {
                        currentLinkSite.Replace("http", "https");
                    }
                    string fileUrl = Path.Combine(currentLinkSite, fileName);
                    var fileStr = new FileModel() { fileName = fileName, fileUrl = fileUrl };
                    appDomainResult = new AppDomainResult()
                    {
                        Success = true,
                        Data = fileStr,
                        ResultMessage = "Upload File Excel thành công!",
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
            });
            return appDomainResult;
        }
    }
}