﻿using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapForm.Config
{
    internal class TextAreaConfig : ControlConfig
    {
        public int Rows { get; set; }
        public bool ReadOnly { get; set; }
    }
}