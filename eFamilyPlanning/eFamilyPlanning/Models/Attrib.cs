﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eFamilyPlanning.Models
{
    public partial class Attrib
    {
        public int EMID { get; set; }
        public string AttribName { get; set; }
        public string AttribValue { get; set; }

        public virtual AttribName AttribName1 { get; set; }
    }
}