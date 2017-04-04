using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdNiva
{
    interface ICreateCamp
    {
        event Action<CreateCampaingResponse> Create;
    }
}
