using AutoMapper;
using Entities.Catalogue;
using Entities.DomainEntities;
using Interface.DbContext;
using Interface.Services;
using Interface.Services.Catalogue;
using Interface.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Service.Services.DomainServices;
using System;
using System.Threading.Tasks;

namespace Service.Services.Catalogue
{
    public class MenuService : CatalogueService<Menu, BaseSearch>, IMenuService
    {
        //private readonly IAppDbContext _appDbContext;
        //private readonly IUserService _userService;
        public MenuService(IAppUnitOfWork unitOfWork, IMapper mapper/*, IAppDbContext appDbContext, IServiceProvider serviceProvider*/) : base(unitOfWork, mapper)
        {
            //this._appDbContext = appDbContext;
            //this._userService = serviceProvider.GetRequiredService<IUserService>();
        }

        protected override string GetStoreProcName()
        {
            return "Menu_GetPagingMenu";
        }

        //public override async Task<bool> CreateAsync(Menu item)
        //{
        //    using (IDbContextTransaction transaction = _appDbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            await base.CreateAsync(item);
        //            await this.CreateAsync(item);
        //            Guid id = item.Id;
        //            await _userService.CreateAsync(new Entities.Users()
        //            {
        //                IdentityCard = "tạo bug để văng ra lỗi nè - tạo bug để văng ra lỗi nè - tạo bug để văng ra lỗi nè"
        //            });
        //            await transaction.CommitAsync();
        //        }
        //        catch
        //        {
        //            await transaction.RollbackAsync();
        //        }
        //    }
        //    return true;
        //}
    }
}
