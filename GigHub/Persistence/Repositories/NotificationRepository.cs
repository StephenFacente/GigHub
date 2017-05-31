using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<NotificationDto> GetNewNotifications(string userId, int minNotifications)
        {
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            if (notifications.Count < minNotifications)
            {
                foreach (var notification in _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .Take(5 - notifications.Count)
                .ToList())
                {
                    notifications.Add(notification);
                }
            }

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        public void MarkAsRead(string userId)
        {
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());
        }
    }
}