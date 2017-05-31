using System.Collections.Generic;
using GigHub.Core.Dtos;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<NotificationDto> GetNewNotifications(string userId, int minNotifications);
        void MarkAsRead(string userId);
    }
}