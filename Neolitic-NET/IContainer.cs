using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface IContainer
    {
        void MapServices(Object servicesHolder);

        void AddFormatter(IValueFormatter formatter);

        void AddParser(IValueParser parser);

        void AddCapturer(ICapturer interpreter);

        void Start(IContextFactory contextFactory, IServiceIdentifier serviceIdentifier,
            IErrorMessageResolver errMessageResolver);

        ExecutionResult ExecuteCommand(String command);

        IValueFormatter GetFormatter(String name);

        IValueParser GetParser(String name);

    }
}
