#region Copyright

// 
// Copyright (c) [YEAR]
// by [OWNER]
// 

#endregion

#region Using Statements

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using System.Web.Hosting;
using System.Web;
using System.IO;
using DotNetNuke.Security;
using System.Collections.Generic;
using Satrabel.OpenBlocks.Token;

#endregion

namespace Satrabel.OpenBlocks.TemplateEditor
{
    [AllowAnonymous]
    public class WidgetController : DnnApiController
    {

        [HttpGet]        
        public HttpResponseMessage MyResponse(string provider)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Hello World! " + provider);
        }

       
        public class UpdateFileDTO
        {
            public string Filename { get; set; }
            public string Content { get; set; }
        }

        public class OpenFileDTO
        {
            public string Filename { get; set; }
        }
    }
}
