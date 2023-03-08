using AutoMapper;
using Entities;
using Entities.Catalogue;
using Entities.Configuration;
using Models.Catalogue;
using Models.Configuration;
using Request.Catalogue.CatalogueCreate;
using Request.Catalogue.CatalogueUpdate;
using Request.Configuration;
using Request.RequestCreate;
using Request.RequestUpdate;
using Utilities;

namespace Models.AutoMapper
{
    public class AppAutoMapper : Profile
    {
        public AppAutoMapper()
        {
            //người dùng
            CreateMap<UserModel, Users>().ReverseMap();
            CreateMap<UserCreate, Users>().ReverseMap();
            CreateMap<UserUpdate, Users>().ReverseMap();
            CreateMap<PagedList<UserModel>, PagedList<Users>>().ReverseMap();

            //quyền
            CreateMap<RoleModel, Role>().ReverseMap();
            CreateMap<RoleCreate, Role>().ReverseMap();
            CreateMap<RoleUpdate, Role>().ReverseMap();
            CreateMap<PagedList<RoleModel>, PagedList<Role>>().ReverseMap();

            //menu
            CreateMap<MenuModel, Menu>().ReverseMap();
            CreateMap<RequestMenuCreateModel, Menu>().ReverseMap();
            CreateMap<RequestMenuUpdateModel, Menu>().ReverseMap();
            CreateMap<PagedList<MenuModel>, PagedList<Menu>>().ReverseMap();

            

            //Brand
            CreateMap<BrandModel, Brand>().ReverseMap();
            CreateMap<BrandCreate, Brand>().ReverseMap();
            CreateMap<BrandUpdate, Brand>().ReverseMap();
            CreateMap<PagedList<BrandModel>, PagedList<Brand>>().ReverseMap();


            //Order
            CreateMap<OrderModel, Order>().ReverseMap();
            CreateMap<OrderCreate, Order>().ReverseMap();
            CreateMap<OrderUpdate, Order>().ReverseMap();
            CreateMap<PagedList<OrderModel>, PagedList<Order>>().ReverseMap();


            


            //Notification
            CreateMap<NotificationModel, Notification>().ReverseMap();
            CreateMap<NotificationCreate, Notification>().ReverseMap();
            CreateMap<NotificationUpdate, Notification>().ReverseMap();
            CreateMap<PagedList<NotificationModel>, PagedList<Notification>>().ReverseMap();

            //UserNotification
            CreateMap<UserNotificationModel, UserNotification>().ReverseMap();
            CreateMap<UserNotificationCreate, UserNotification>().ReverseMap();
            CreateMap<UserNotificationUpdate, UserNotification>().ReverseMap();
            CreateMap<PagedList<UserNotificationModel>, PagedList<UserNotification>>().ReverseMap();

            //UserNotification
            CreateMap<EmailConfigurationModel, EmailConfigurations>().ReverseMap();
            CreateMap<EmailConfigurationRequestCreate, EmailConfigurations>().ReverseMap();
            CreateMap<EmailConfigurationRequestUpdate, EmailConfigurations>().ReverseMap();
            CreateMap<PagedList<EmailConfigurationModel>, PagedList<EmailConfigurations>>().ReverseMap();

            //ProfileGroup
            CreateMap<ProfileGroupModel, ProfileGroup>().ReverseMap();
            CreateMap<ProfileGroupCreate, ProfileGroup>().ReverseMap();
            CreateMap<ProfileGroupUpdate, ProfileGroup>().ReverseMap();
            CreateMap<PagedList<ProfileGroupModel>, PagedList<ProfileGroup>>().ReverseMap();

            //StudentProfile
            CreateMap<StudentProfileModel, StudentProfile>().ReverseMap();
            CreateMap<StudentProfileCreate, StudentProfile>().ReverseMap();
            CreateMap<StudentProfileUpdate, StudentProfile>().ReverseMap();
            CreateMap<PagedList<StudentProfileModel>, PagedList<StudentProfile>>().ReverseMap();

            //ExaminationRoom
            CreateMap<ExaminationRoomModel, ExaminationRoom>().ReverseMap();
            CreateMap<ExaminationRoomCreate, ExaminationRoom>().ReverseMap();
            CreateMap<ExaminationRoomUpdate, ExaminationRoom>().ReverseMap();
            CreateMap<PagedList<ExaminationRoomModel>, PagedList<ExaminationRoom>>().ReverseMap();

            //StudentExam
            CreateMap<StudentExamModel, StudentExam>().ReverseMap();
            CreateMap<StudentExamCreate, StudentExam>().ReverseMap();
            CreateMap<StudentExamUpdate, StudentExam>().ReverseMap();
            CreateMap<PagedList<StudentExamModel>, PagedList<StudentExam>>().ReverseMap();
        }
        
    }
}