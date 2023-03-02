using Request.RequestCreate;
using Request.RequestUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Services
{
    public interface IOneSignalService
    {
        Task<bool> CreateOneSignal(string title, string content, string[] OneSignalUserID);
    }
}
