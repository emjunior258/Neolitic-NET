using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface ICommandKeyValues
    {
        

        T Get<T>(String name);

        void Set(String name, Object value);

		void Set (KeywordToken token);

		void InitializeTokens (IExecutionContext context);

        Object Get(String name);

		Object GetValue (String name);

        bool Contains(String name);

		bool ContainsValue (string name);

		String Apply (String target);

	
    }
}
