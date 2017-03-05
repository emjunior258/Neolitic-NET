using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface ICommandKeywords
    {
        string GetString(String name);

        int GetInt(String name);

        double GetDouble(String name);

        decimal GetDecimal(String name);

        bool GetBoolean(String name);

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
