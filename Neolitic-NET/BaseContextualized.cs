﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Neolitic
{
    public abstract class BaseContextualized
    {
        private static ThreadLocal<IExecutionContext> _contextLocal = new ThreadLocal<IExecutionContext>();

		public static void Initialize(IExecutionContext context){

			_contextLocal.Value = context;

		}

		public static void Clean(){

			_contextLocal.Value = null;

		}

        public IExecutionContext Command {
            protected set { _contextLocal.Value = value;  }
            get { return _contextLocal.Value; }
        }
        
    }
}
