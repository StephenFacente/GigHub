using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FolloweesRepository : IFolloweesRepository
    {
        private readonly ApplicationDbContext _context;

        public FolloweesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> Index(string userId)
        {
            return _context.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee);
        }
    }
}