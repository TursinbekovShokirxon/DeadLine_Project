using Application.ModelServices;
using Domain.Models;
using MediatR;

namespace Infrastructure.Handlers.ForUser
{
    public class UserGetByIdModel : IRequest<User>
    {
        public int Id { get; set; }
    }
    public class UserGetByIdHandler : IRequestHandler<UserGetByIdModel, User>
    {
        private readonly IUserService _service;
        public UserGetByIdHandler(IUserService _service)
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
