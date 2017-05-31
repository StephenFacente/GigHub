using GigHub.Core;
using GigHub.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class UnfollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnfollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Unfollow(FollowingDto dto)
        {
            var theFollowing = _unitOfWork.Followings.GetFollowing(User.Identity.GetUserId(), dto.FolloweeId);

            if (theFollowing == null)
            {
                return NotFound();
            }

            _unitOfWork.Followings.Remove(theFollowing);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
