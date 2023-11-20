using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfacesCrudServices
{
    public interface IGetAllService<T>
    {
        public IEnumerable<T> GetAll();
    }
}
