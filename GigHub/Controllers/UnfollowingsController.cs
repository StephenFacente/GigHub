using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class UnfollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public UnfollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Unfollow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
            {
                var theFollowing =
                    _context.Followings.First(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);
                _context.Followings.Remove(theFollowing);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest("Could not find the following");
        }
    }
}
