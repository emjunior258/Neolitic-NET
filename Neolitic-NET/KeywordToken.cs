using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Neolitic
{
    public class KeywordToken
    {
        public bool Capturable { get; set; }

		public bool Nullable { get; set; }

        public String TokenText { get; set; }

        public String Name { get; set; }

        public String Parser { get; set; }

        public String Formatter { get; set; }

		public IContainer Container { get; set;}


        /// <summary>
        /// Index of the token in the string this token was found.
        /// </summary>
        public int ArgumentIndex { get; set; }

        private const String MATCH_TOKENS_EXPRESSION = 
			@"(\{(((?<KeywordParse>@?[A-Za-z0-9]+\??)[:]{1}(?<Parser>[A-Za-z]+))|((?<KeywordFormat>@?[A-Za-z0-9]+\??)[|]{1}(?<Formatter>[A-Za-z]+))|(?<Keyword>@?[A-Za-z0-9]+\??))\})";

        /// <summary>
        /// Extracts from a <see cref="ICommandKeywords"/> the value correspondent to the <see cref="KeywordToken"/> and
        /// if the <see cref="KeywordToken"/> has the Formatter property set, then, the correspondent <see cref="IValueFormatter"/> will be used to 
        /// format the value, otherwise, the value will be returned as stored in the <see cref="ICommandKeywords"/>.
        /// </summary>
        /// <param name="keywords">the <see cref="ICommandKeywords"/> from which the value must be extracted.</param>
        /// <returns>the extracted and formatted value (if a formatter is set) if found in the <see cref="ICommandKeywords"/> or null if not found</returns>
        public String FormatValue(ICommandKeywords keywords)
        {
            if (!keywords.ContainsValue(Name))
                return null;

            Object value = keywords.GetValue(Name);
            if (Formatter != null)
                callFormatter(value, Container);

            return value.ToString();

        }


        /// <summary>
        /// Extracts the value correspondent to the <see cref="KeywordToken"/> from a <see cref="ICommandKeywords"/> and then applies
        /// the extracted value on a string. This method uses the <see cref="FormatValue(ICommandKeywords)"/> method internally to extract
        /// the <see cref="KeywordToken"/> value.
        /// </summary>
        /// <param name="keywords">the <see cref="ICommandKeywords"/> from which the value must be extracted.</param>
        /// <param name="target">the string to apply the extracted value on</param>
        public String ApplyValue(ICommandKeywords keywords, String target)
        {
            String formatted = FormatValue(keywords);
            target = target.ToString();

            if (formatted == null)
                return target;

			return target.Replace("{"+Name+"}", formatted); 

        }


        /// <summary>
        /// Extracts from a <see cref="ICommandKeywords"/> the value correspondent to the <see cref="KeywordToken"/> and
        /// if the <see cref="KeywordToken"/> has the Parser property set, then, the correspondent <see cref="IValueParser"/> will be used to 
        /// parse the value, otherwise, the value will be returned as it is stored in the <see cref="ICommandKeywords"/>.
        /// </summary>
        /// <param name="keywords">the <see cref="ICommandKeywords"/> from which the value must be extracted.</param>
        /// <returns></returns>
        public Object GetValue(ICommandKeywords keywords)
        {
            if (!keywords.ContainsValue(Name))
                return null;

            Object value = keywords.GetValue(Name);
            if (Parser != null)      
                value = callParser(value, Container);

            return value;
        }


		public void Initialize(IExecutionContext context){

			if (isNull (context))
				return;

			this.Container = context.Container;
			String argValue = context.Arguments.Split(' ')[ArgumentIndex];
			context.Keywords.Set (Name, argValue);


		}


        public static List<KeywordToken> Scan(String template)
        {        
            Regex regex = new Regex(MATCH_TOKENS_EXPRESSION,RegexOptions.IgnoreCase);
            MatchCollection matchCollection = regex.Matches(template);
            List<KeywordToken> tokens = new List<KeywordToken>();

            int scanIndex = 0;
            foreach(Match m in matchCollection)
            {
                KeywordToken token = new KeywordToken();
                token.ArgumentIndex = scanIndex;
                token.TokenText = m.Value;

                Group keywordParser = m.Groups["KeywordParse"];
                Group parser = m.Groups["Parser"];
                Group keywordFormatter = m.Groups["KeywordFormatte"];
                Group formatter = m.Groups["Formatter"];
                Group keyword = m.Groups["Keyword"];

                if (keywordParser.Success)
                { 
                    //keyword with parser
                    token.Name = keywordParser.Value;
                    token.Parser = parser.Value;    

                }else if (keywordFormatter.Success)
                {  
                    //keyword with formatter
                    token.Name = keywordFormatter.Value;
                    token.Formatter = formatter.Value;
                }
                else
                {   
                    //Plain keyword
                    token.Name = keyword.Value;
                }

                CheckCapturability(token);
				CheckNullability (token);
                tokens.Add(token);
                scanIndex++;
            }

            return tokens;

        }


		public bool isNull(IExecutionContext context){

			String[] tokens = context.Arguments.Split (' ');

			if(ArgumentIndex>=tokens.Length){
				
				//TODO: Throw InvalidCommandArguments
				return true;//TODO: Remove this

			}

			return tokens [ArgumentIndex] == context.NullToken;

		}

        private static void CheckCapturability(KeywordToken token)
        {
            //Token name must have the @ marker to be automatically-interpretable
            if (!token.Name.StartsWith("@"))
                return;

            //Remove automatic-interpretation marker
            token.Name = token.Name.Substring(1);
            token.Capturable = true;
            
        }

		private static void CheckNullability(KeywordToken token){

			if(!token.Name.EndsWith("?"))
				return;


			token.Name = token.Name.Replace("?","");
			token.Nullable = true;

		}

        private Object callParser(Object value, IContainer container)
        {
            IValueParser parser = container.GetParser(Parser);
            if (parser == null)
                return value;

            return parser.Parse(value.ToString());
        }


        private String callFormatter(Object value, IContainer container)
        {
            IValueFormatter formatter = container.GetFormatter(Formatter);
            if (formatter == null)
                return null;

            return formatter.Format(value.ToString());
        }


    }
}
