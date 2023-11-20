using Application.ModelServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForUser
{
    public class UserDeleteModel : IRequest<string>
    {
        public int Id { get; set; }
    }
    public class UserDeleteHandler : IRequestHandler<UserDeleteModel, string>
    {
        private readonly IUserService _service;
        public UserDeleteHandler(IUserService _service)
        {
            this._service = _service;
        }

        public async Task<string> Handle(UserDeleteModel request, CancellationToken cancellationToken)
        {
          var isDelete= await _service.Delete(request.Id);
            return isDelete?"Пользователь был удален":"Пользователь не может быть удален";
        }
    }
}
