using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface ICapturer
    {
        /// <summary>
        /// The value will already be parsed.
        /// </summary>
        /// <param name="value"></param>
        void Captured(Object value);
    }
}
