﻿using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class AppSettingsPage : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string NavIdentifier
        {
            get
            {
                return "navsys";
            }
        }
    }
}