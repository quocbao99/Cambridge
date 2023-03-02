using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAPI.AppHangfire
{
    public class AppHangfireFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return true;
        }
    }
}
