using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.ComponentModel;
using System.Reflection;
using System.Globalization;
using DotNetNuke.Framework.Providers;

namespace Satrabel.DataSource
{

    public abstract class DataSourceProvider
    {      
        #region "Shared/Static Methods"
        public static List<DataSourceProvider> GetProviderList()
        {
            LoadProviders();
            return _providers;
        }
        public static DataSourceProvider Instance(string FriendlyName)
        {
            LoadProviders();
            return _providers.SingleOrDefault(p => p.FriendlyName == FriendlyName);
        }      

        #endregion

        public virtual Dictionary<Type, string[]> GetSafeTypeMemebers()
        {
            return null;
        }
        
        public DataSourceProvider ()
		{		
			Extend(this.GetType());			
		}

        public Dictionary<string, Type> getModelTypes()
        {
            Dictionary<string, Type> dic = new Dictionary<string, Type>();
            
            foreach (var item in Methods.Select(m => m.ReturnType))
            {
                dic.Add(item.Name, item);    
            }
            return dic;
        }
        private readonly Dictionary<string, IList<MethodInfo>> _methods = new Dictionary<string, IList<MethodInfo>>();
		public IEnumerable<MethodInfo> Methods
		{
			get { return _methods.Values.SelectMany(m => m); }
		}
		/// <summary>
		/// In this C# implementation, we can't use mixins. So we grab all the static
		/// methods from the specified type and use them instead.
		/// </summary>
		/// <param name="type"></param>
		public void Extend(Type type)
		{
			// From what I can tell, calls to Extend should replace existing filters. So be it.
			var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
			var methodNames = type.GetMethods(BindingFlags.Public | BindingFlags.Static).Select(m => m.Name);

			foreach (var methodName in methodNames)
				_methods.Remove(methodName);

			foreach (MethodInfo methodInfo in methods)
			{
				var name = methodInfo.Name;
				if (!_methods.ContainsKey(name))
					_methods[name] = new List<MethodInfo>();

				_methods[name].Add(methodInfo);
			} // foreach
		}
		public bool RespondTo(string method)
		{
			return _methods.ContainsKey(method);
		}
        public object Invoke(string method, Dictionary<string, string> parameters)
		{
            List<object> argsLst = new List<object>(parameters.Count);
            if (!RespondTo(method)) {
                throw new Exception(string.Format("Method {0} dous not exist in provider {1}",method, FriendlyName));
            }
			// First, try to find a method with the same number of arguments.
            var methodInfo = _methods[method].FirstOrDefault(m => m.GetParameters().Length == parameters.Count);

			// If we failed to do so, try one with max numbers of arguments, hoping
			// that those not explicitly specified will be taken care of
			// by default values
			if (methodInfo == null)
				methodInfo = _methods[method].OrderByDescending(m => m.GetParameters().Length).First();

			ParameterInfo[] parameterInfos = methodInfo.GetParameters();
			
			// Add in any default parameters - .NET won't do this for us.
            if (parameterInfos.Length > parameters.Count)
            {
                for (int i = parameters.Count; i < parameterInfos.Length; ++i)
                {
                    if ((parameterInfos[i].Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.HasDefault)
                        throw new Exception("error "+ method +" "+ parameterInfos[i].Name);
                    argsLst.Add(parameterInfos[i].DefaultValue);
                }
            }
            for (int i = 0; i < parameters.Count; ++i)
            {
                
                string key = parameters.Keys.SingleOrDefault(p=> p.Equals( parameterInfos[i].Name, StringComparison.InvariantCultureIgnoreCase));
                if (string.IsNullOrEmpty(key)) {
                    throw new System.ArgumentException(string.Format("Parameter {0} missing", parameterInfos[i].Name));
                }
                object par = parameters[key];
                if (parameterInfos[i].ParameterType == par.GetType())
                {
                    argsLst.Add(par);
                }
                else
                {
                    argsLst.Add(Convert.ChangeType(par, parameterInfos[i].ParameterType, CultureInfo.InvariantCulture));
                }
            }


			try
			{
				return methodInfo.Invoke(null, argsLst.ToArray());
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
		public object Invoke(string method, params object[] args)
		{
            List<object> argsLst = new List<object>(args);
			// First, try to find a method with the same number of arguments.
            var methodInfo = _methods[method].FirstOrDefault(m => m.GetParameters().Length == args.Length);

			// If we failed to do so, try one with max numbers of arguments, hoping
			// that those not explicitly specified will be taken care of
			// by default values
			if (methodInfo == null)
				methodInfo = _methods[method].OrderByDescending(m => m.GetParameters().Length).First();

			ParameterInfo[] parameterInfos = methodInfo.GetParameters();

			
			// Add in any default parameters - .NET won't do this for us.
            if (parameterInfos.Length > args.Length)
            {
                for (int i = args.Length; i < parameterInfos.Length; ++i)
                {
                    if ((parameterInfos[i].Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.HasDefault)
                        throw new Exception("error " + method + " " + parameterInfos[i].Name);
                    argsLst.Add(parameterInfos[i].DefaultValue);
                }
            }
            for (int i = 0; i < args.Length; ++i)
            {
                if (parameterInfos[i].ParameterType != args[i].GetType())
                {
                    argsLst[i] = Convert.ChangeType(args[i], parameterInfos[i].ParameterType, CultureInfo.InvariantCulture);
                }
            }


			try
			{
				return methodInfo.Invoke(null, argsLst.ToArray());
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string Configurator { get; set; }
        #region "Provider configuration and setup"

        private static readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration("dataEngine");

        private static List<DataSourceProvider> _providers;

        private static readonly object _lock = new object();

        public List<DataSourceProvider> Providers
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
                    _providers = new List<DataSourceProvider>();

                    ComponentFactory.InstallComponents(new ProviderInstaller("dataEngine", typeof(DataSourceProvider)));
                    foreach (KeyValuePair<string, DataSourceProvider> comp in ComponentFactory.GetComponents<DataSourceProvider>())
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
    }
}