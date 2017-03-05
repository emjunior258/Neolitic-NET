using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
	public class CommandKeywords : ICommandKeywords
    {

		private IDictionary<String,KeywordToken> _tokens = new Dictionary<String,KeywordToken> ();
		private IDictionary<String,Object> _values = new Dictionary<String,Object> ();

		#region ICommandKeywords implementation
		public string GetString (string name)
		{
			throw new NotImplementedException ();
		}
		public int GetInt (string name)
		{
			throw new NotImplementedException ();
		}
		public double GetDouble (string name)
		{
			throw new NotImplementedException ();
		}
		public decimal GetDecimal (string name)
		{
			throw new NotImplementedException ();
		}
		public bool GetBoolean (string name)
		{
			throw new NotImplementedException ();
		}
		public T Get<T> (string name)
		{
			throw new NotImplementedException ();
		}
		public void Set (string name, object value)
		{
			throw new NotImplementedException ();
		}
		public void Set (KeywordToken token)
		{
			throw new NotImplementedException ();
		}
		public void InitializeTokens (IExecutionContext context)
		{
			foreach (KeywordToken token in _tokens.Values)
				token.Initialize(context);

			
		}
		public object Get (string name)
		{
			Object value = null;

			if (_tokens.ContainsKey (name)) {

				KeywordToken token = null;
				_tokens.TryGetValue (name, out token);
				value = token.GetValue (this);

			}else if(_values.ContainsKey(name)){

				 _values.TryGetValue(name,out value);

			}
				

			return value;

		}

		public Object GetValue (String name){

			Object value = null;

			if(_values.ContainsKey(name))
				_values.TryGetValue(name,out value);

			return value;

		}


		public bool Contains (string name)
		{
			return _tokens.ContainsKey (name)||_values.ContainsKey(name);
		}


		public bool ContainsValue (string name)
		{
			return _values.ContainsKey(name);
		}

		public string Apply (IContainer container, string target)
		{
			String result = target;

			foreach (String name in _values.Keys) {

				KeywordToken token = null;

				if (_tokens.ContainsKey (name)) {
					
					_tokens.TryGetValue (name, out token);

				} else {

					token = new KeywordToken ();
					token.Name = name;
					token.Container = container;

				}

				result = token.ApplyValue (this, result);
					
			}

			return result;
			
		}

		#endregion
        
    }
}
