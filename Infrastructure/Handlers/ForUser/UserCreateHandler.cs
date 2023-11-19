using Application;
using Domain;
using MediatR;

namespace Infrastructure.Handlers.ForUser
{
    public class UserCreateModel : IRequest<string>
    {
        public string NickName { get; set; } = string.Empty;
        public string Universty { get; set; } = string.Empty;
        public int Course { get; set; }
        public string Faculty { get; set; } = string.Empty;
        public string Budget { get; set; } = string.Empty;

    }
    public class UserCreateHandler : IRequestHandler<UserCreateModel, string>
    {
        private readonly IUserService _service;
        public UserCreateHandler(IUserService _service)
        {
            this._service = _service;
        }

        public async Task<string> Handle(UserCreateModel request, CancellationToken cancellationToken)
        {
            User user = new();
            user.NickName = request.NickName;
            user.Universty = request.Universty;
            user.Course = request.Course;
            user.Faculty = request.Faculty;
            user.Budget = request.Budget;

            var res = await _service.Create(user);

            return res;
        }
    }
}
