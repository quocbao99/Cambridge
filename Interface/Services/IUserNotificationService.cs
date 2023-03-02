using Entities;
using Entities.Search;
using Interface.Services.DomainServices;
using Request.RequestCreate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Services
{
    public interface IUserNotificationService : IDomainService<UserNotification, UserNotificationSearch>
    {
        Task<bool> addUserNotification(NotificationCreate notificationCreate);
    }
}
