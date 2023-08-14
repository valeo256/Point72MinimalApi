using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        void Create(UserExample user);

        UserExample? GetById(Guid id);
    }
}
