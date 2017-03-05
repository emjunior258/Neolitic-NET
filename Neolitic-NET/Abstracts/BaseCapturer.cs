using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Abstracts
{
    public abstract class BaseCapturer : BaseContextualized, ICapturer
    {
        public abstract void Captured(Object value);
    }
}
