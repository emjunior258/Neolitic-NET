using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface ICapturer
    {
        /// <summary>
        /// The values will already be parsed.
        /// </summary>
        /// <param name="values"></param>
        void InterpretValues(IDictionary<String, Object> values);
    }
}
