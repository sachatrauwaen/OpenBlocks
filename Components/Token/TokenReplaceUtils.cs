using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Xml;

namespace Satrabel.Token
{
    public class TokenReplaceUtils
    {
        public static string Replace(string output)
        {
            //return Regex.Replace(output, @"{{([a-zA-Z]{1,10}(?:\s+\S+=""[^""]+"")+)\s*}}", delegate(Match match)
            return Regex.Replace(output, @"{{([a-zA-Z]{1,10}(?:\s+\S+=[^\s]+)+)\s*}}", delegate(Match match)
            {
                string m = match.ToString();
                try
                {
                    string xmlText = "<" + HttpUtility.HtmlDecode(match.Groups[1].Value) + "/>";

                    /*
                    var xml = new XmlDocument();
                    xml.LoadXml(xmlText);
                    string TokenName = xml.DocumentElement.Name;
                    Dictionary<string, string> parameters = new Dictionary<string, string>();

                    if (xml.DocumentElement.Attributes != null)
                    foreach (XmlAttribute attribute in xml.DocumentElement.Attributes)
                    {
                        parameters.Add(attribute.Name.ToLower(), attribute.Value);
                    }
                    */
                    string TokenName ="";
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    foreach (var item in Parse(xmlText, out TokenName))
                    {
                        parameters.Add(item.Key.ToLower(), item.Value);
                    }


                    return Output(TokenName, parameters);
                }
                catch (Exception)
                {

                    return m;
                }
            });


        }

        public static string Output(string ProviderName,  params string[] parameters)
        {
            Dictionary<string, string> parametersDic = new Dictionary<string, string>();
            foreach (var item in parameters)
            {
                string[] par = item.Split('=');
                parametersDic.Add(par[0], par[1]);

            }
            TokenProvider handler = TokenProvider.Instance(ProviderName);
            if (handler == null)
            {
                string pars = "";
                foreach (var item in parametersDic)
                {
                    pars += " " + item.Key + "=\"" + item.Value + "\"";
                }
                return "{{" + ProviderName + pars + "}}";
            }
            return handler.Execute(parametersDic);
        }


        public static string Output(string ProviderName, Dictionary<string, string> parameters)
        {
            TokenProvider handler = TokenProvider.Instance(ProviderName);
            if (handler == null)
            {
                string pars = "";
                foreach (var item in parameters)
                {
                    pars += " " + item.Key + "=\"" + item.Value + "\"";
                }
                return "{{" + ProviderName + pars + "}}";
            }
            return handler.Execute(parameters);

        }

        public static Dictionary<string,string> Parse(string node, out string Name) {
            string pattern = @"
                                (?:<)(?<Tag>[^\s/>]+)       # Extract the tag name.
                                (?![/>])                    # Stop if /> is found
                                                            # -- Extract Attributes Key Value Pairs  --
 
                                ((?:\s+)                    # One to many spaces start the attribute
                                 (?<Key>[^=]+)              # Name/key of the attribute
                                 (?:=)                      # Equals sign needs to be matched, but not captured.
 
                                (?([\x22\x27])              # If quotes are found
                                  (?:[\x22\x27])
                                  (?<Value>[^\x22\x27]+)    # Place the value into named Capture
                                  (?:[\x22\x27])
                                 |                          # Else no quotes
                                   (?<Value>[^\s/>]*)       # Place the value into named Capture
                                 )
                                )+                          # -- One to many attributes found!";

            var tag = (from Match mt in Regex.Matches(node, pattern, RegexOptions.IgnorePatternWhitespace)
                              select new
                              {
                                  Name = mt.Groups["Tag"],
                                  Attrs = (from cpKey in mt.Groups["Key"].Captures.Cast<Capture>().Select((a, i) => new { a.Value, i })
                                           join cpValue in mt.Groups["Value"].Captures.Cast<Capture>().Select((b, i) => new { b.Value, i }) on cpKey.i equals cpValue.i
                                           select new KeyValuePair<string, string>(cpKey.Value, cpValue.Value)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
                              }).First();
            Name = tag.Name.Value;
            return tag.Attrs;
        }

        
    }
}