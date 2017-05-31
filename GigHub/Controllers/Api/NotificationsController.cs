using GigHub.Core;
using GigHub.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            return _unitOfWork.Notifications.GetNewNotifications(User.Identity.GetUserId(), 5);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            _unitOfWork.Notifications.MarkAsRead(User.Identity.GetUserId());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
