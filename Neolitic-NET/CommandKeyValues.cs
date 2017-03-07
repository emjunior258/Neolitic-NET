using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
	public class CommandKeyValues : ICommandKeyValues
    {

		private IDictionary<String,KeywordToken> _tokens = new Dictionary<String,KeywordToken> ();
		private IDictionary<String,Object> _values = new Dictionary<String,Object> ();
		private IExecutionContext _context = null;


		public T Get<T> (string name)
		{
			throw new NotImplementedException ();
		}

		public void Set (string name, object value)
		{
			_values.Add (name, value);
		}

		public void Set (KeywordToken token)
		{
			_tokens.Add (token.Name, token);
		}

		public void InitializeTokens (IExecutionContext context)
		{
			_context = context;
			foreach (KeywordToken token in _tokens.Values)
				token.Initialize(context);

			
		}
		public object Get (string name)
		{
			Object value = null;

			if (_tokens.ContainsKey (name)) {

				KeywordToken token = null;
				_tokens.TryGetValue (name, out token);
				token.Container = _context.Container;
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

		public string Apply (string target)
		{
			String result = target;

			IList<KeywordToken> tokens = KeywordToken.Scan (result);
			foreach (KeywordToken token in tokens) {

				token.Container = _context.Container;
				result = token.ApplyValue (this, result);

			}
				
			return result;
			
		}

        
    }
}
