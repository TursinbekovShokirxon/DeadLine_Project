using Infrastructure.Contexts;
using MediatR;
namespace Infrastructure.Handlers.ForRoles
{
    public class AddUserInRoleModel : IRequest<string>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class AddUserInRoleHandler : IRequestHandler<AddUserInRoleModel, string>
    {
        private readonly AppDbContext _db;
        public AddUserInRoleHandler(AppDbContext db)
        {
            _db = db;
        }
        public async Task<string> Handle(AddUserInRoleModel request, CancellationToken cancellationToken)
        {
           var User = _db.UserAuthifications.FirstOrDefault(x => x.UserId == request.UserId);

            if (User != null) return "Роль не может быть создан из отсутствия Пользователя";

            var Role = _db.Roles.FirstOrDefault(x => x.Id == request.RoleId);

            if (Role != null) return "Роль не может быть создан из отсутствия данной роли";
             Role?.UserAuthes?.Add(User);
            return "Роль создана";
        }
    }
}
