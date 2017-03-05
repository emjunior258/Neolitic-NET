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

        Object Get(String name);

        bool Contains(String name);
    }
}
