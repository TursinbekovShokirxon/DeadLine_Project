using Application.InterfacesCrudServices;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesModelServices
{
    public interface ITaskStatusService : 
        ICreateService<Domain.Models.TaskStatus>,
        IGetAllService<Domain.Models.TaskStatus>,
        IGetByIdService<Domain.Models.TaskStatus>
    {
    }
}
