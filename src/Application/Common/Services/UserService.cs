using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Common.Services
{
    public class UserService : IUserService
    {
        private readonly Dictionary<Guid, UserExample> _user = new();

        public void Create(UserExample? user)
        {
            if (user is null)
            {
                return;
            }

            _user[user.Id] = user;
        }
        public UserExample? GetById(Guid id)
        {
            return _user.GetValueOrDefault(id);
        }

    }
}
