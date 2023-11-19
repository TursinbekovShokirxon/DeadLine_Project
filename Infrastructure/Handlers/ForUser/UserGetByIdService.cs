using Application;
using Domain;
using MediatR;

namespace Infrastructure.Handlers.ForUser
{
    public class UserGetByIdModel : IRequest<User>
    {
        public int Id { get; set; }
    }
    public class UserGetByIdService : IRequestHandler<UserGetByIdModel, User>
    {
        private readonly IUserService _service;
        public UserGetByIdService(IUserService _service)
        {
            this._service = _service;
        }
        public async Task<User> Handle(UserGetByIdModel request, CancellationToken cancellationToken)
        {
            var user = await _service.GetById(request.Id);
            return user;
        }
    }
}
