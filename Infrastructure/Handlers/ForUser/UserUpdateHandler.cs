using Application.ModelServices;
using Domain.Models;
using Domain.State;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForUser
{
    public class UserUpdateModel:IRequest<string>
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string Universty { get; set; } = string.Empty;
        public Course Course { get; set; }
        public string Faculty { get; set; } = string.Empty;
        public string Budget { get; set; } = string.Empty;
    }
    public class UserUpdateHandler : IRequestHandler<UserUpdateModel, string>
    {
        private readonly IUserService _Service;
        public UserUpdateHandler(IUserService _Service)
        {
            this._Service = _Service;
        }
        public async Task<string> Handle(UserUpdateModel request, CancellationToken cancellationToken)
        {
            User user = new()
            {
                Budget = request.Budget,
                Course = request.Course,
                Faculty = request.Faculty,
                Id = request.Id,
                NickName = request.NickName,
                Universty = request.Universty
            };
            var IsUpdate=await _Service.Update(user);
            
            return IsUpdate?"Пользователь был обновлен":"Пользователь не может быть обновлен";
        }
    }
}
