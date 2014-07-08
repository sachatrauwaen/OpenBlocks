using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using DotNetNuke.Services.Localization;
using System.Text.RegularExpressions;

namespace Satrabel.OpenBlocks.DataSource
{

    public abstract class DataSourceConfigurator : UserControl
    {
        private string _localResourceFile;

        public int PortalId { get; set; }
        //public string LocalResourceFile { get; set; }

        public string getToken() {
            string Token = "";
            foreach (DictionaryEntry item in SaveSettings())
            {
                Token += " " +item.Key + "=\"" + item.Value + "\"";
            }
            return Token;
        }

        public void setToken(string token) {
            Dictionary<string, string> dic = ParseToken(token);
            Hashtable ht = new Hashtable();
            foreach (var item in dic)
            {
                ht.Add(item.Key, item.Value); 
            }
            LoadSettings(ht);
        }
        public abstract void LoadSettings(Hashtable settings);
        public abstract Hashtable SaveSettings();

        public string LocalResourceFile
        {
            get
            {
                string fileRoot;
                if (string.IsNullOrEmpty(_localResourceFile))
                {
                    
                    fileRoot = TemplateSourceDirectory + "/" + Localization.LocalResourceDirectory + "/" + ID;
                }
                else
                {
                    fileRoot = _localResourceFile;
                }
                return fileRoot;
            }
            set
            {
                _localResourceFile = value;
            }
        }

        private static Dictionary<string, string> ParseToken(string node/*, out string Name*/)
        {
            string pattern = @"
                                (?:{{)(?<Tag>[^\s}}]+)       # Extract the tag name.
                                (?![}}])                    # Stop if /> is found
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
            //Name = tag.Name.Value;
            return tag.Attrs;
        }

    }
}