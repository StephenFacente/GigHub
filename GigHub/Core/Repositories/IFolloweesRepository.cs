using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFolloweesRepository
    {
        IEnumerable<ApplicationUser> Index(string userId);
    }
}