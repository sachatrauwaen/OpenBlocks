using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;

namespace Satrabel.OpenBlocks.Token
{

    public abstract class TokenConfigurator : UserControl
    {
        public int PortalId { get; set; }
        public string LocalResourceFile { get; set; }

        public abstract string getToken();
        public abstract void LoadSettings(Hashtable settings);
        public abstract Hashtable SaveSettings();


    }
}