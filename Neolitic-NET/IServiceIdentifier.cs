using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface IServiceIdentifier
    {
        IServiceInfo IdentifyService(String command, out String arguments);

    }
}
