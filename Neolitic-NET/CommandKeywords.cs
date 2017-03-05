using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class CommandKeywords : ICommandKeywords
    {
        public T Get<T>(string name)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(string name)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(string name)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(string name)
        {
            throw new NotImplementedException();
        }

        public int GetInt(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(string name)
        {
            throw new NotImplementedException();
        }

        public void Set(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
