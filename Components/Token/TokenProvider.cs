using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.ComponentModel;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Instrumentation;

namespace Satrabel.OpenBlocks.Token
{

    public abstract class TokenProvider
    {
        
        #region "Shared/Static Methods"

        public static List<TokenProvider> GetProviderList()
        {
            LoadProviders();
            return _providers;
        }

        public static TokenProvider Instance(string FriendlyName)
        {
            LoadProviders();
            return _providers.SingleOrDefault(p => p.FriendlyName == FriendlyName);
        }

        private static readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration("tokenEngine");

       
        #endregion

        #region "Provider configuration and setup"

        private static List<TokenProvider> _providers;

        private static readonly object _lock = new object();

        public List<TokenProvider> Providers
        {
            get
            {
                return _providers;
            }
        }


        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_providers == null)
            {
                lock (_lock)
                {
                    _providers = new List<TokenProvider>();
                    ComponentFactory.InstallComponents(new ProviderInstaller("tokenEngine", typeof(TokenProvider)));            
                    foreach (KeyValuePair<string, TokenProvider> comp in ComponentFactory.GetComponents<TokenProvider>())
                    {
                        var objProvider = (DotNetNuke.Framework.Providers.Provider)_providerConfiguration.Providers[comp.Key];
                        comp.Value.Name = comp.Key;
                        comp.Value.FriendlyName = objProvider.Attributes["FriendlyName"];
                        comp.Value.Configurator = objProvider.Attributes["Configurator"];
                        _providers.Add(comp.Value);
                    }
                }
            }
        }
        #endregion
        public abstract string Execute(Dictionary<string, string> parameters);
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string Configurator { get; set; }
    }
}