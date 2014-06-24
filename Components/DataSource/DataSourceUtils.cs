using Satrabel.OpenBlocks.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Satrabel.OpenBlocks.DataSource
{
    public class DataSourceUtils
    {
        public static Object GetModel(Dictionary<string, string> parameters)
        {
            var ModelParameters = new Dictionary<string, string>(parameters);
            try
            {
                string ModelType = "";
                if (parameters.ContainsKey("datatype"))
                {
                    ModelType = parameters["datatype"];
                    ModelParameters.Remove("datatype");
                }
                string ModelProvider = "";
                if (parameters.ContainsKey("dataprovider"))
                {
                    ModelProvider = parameters["dataprovider"];
                    ModelParameters.Remove("dataprovider");
                }
                string ModelConstructor = "";
                if (parameters.ContainsKey("datasource"))
                {
                    ModelConstructor = parameters["datasource"];
                    ModelParameters.Remove("datasource");
                }
                DataSourceProvider provider = null;
                if (!string.IsNullOrEmpty(ModelType))
                {
                    Object obj = Activator.CreateInstance(Type.GetType(ModelType));
                    provider = obj as DataSourceProvider;
                }
                else if (!string.IsNullOrEmpty(ModelProvider))
                {
                    provider = DataSourceProvider.Instance(ModelProvider);
                }
                object Model = null;
                if (provider != null)
                {
                    Model = provider.Invoke(ModelConstructor, ModelParameters);
                }
                return Model;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error when try to create model ({0})",  string.Join(";", parameters.Select(x => x.Key + "=" + x.Value).ToArray())), ex);
            }
            
        }
    }
}