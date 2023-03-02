using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
    public class NotInPermissionsAttribute : Attribute
    {
        //attribute này dùng để đánh dấu các action ko nằm trong permissions
    }
}
