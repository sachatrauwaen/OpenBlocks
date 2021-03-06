#region Copyright

// 
// Copyright (c) [YEAR]
// by Satrabel
// 

#endregion

#region Using Statements

using DotNetNuke.Web.Api;

#endregion

namespace Satrabel.OpenBlocks.TemplateEditor
{
    public class TemplateEditorRouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("OpenBlocks", "default", "{controller}/{action}", new[] {"Satrabel.OpenBlocks.TemplateEditor"});
        }
    }
} 
