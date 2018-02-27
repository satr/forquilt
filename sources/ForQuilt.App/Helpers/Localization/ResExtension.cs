//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Threading;
using System.Windows.Markup;
using ForQuilt.App.Properties;

namespace ForQuilt.App.Helpers.Localization
{
    class ResExtension: MarkupExtension
    {
        private string _name;

        public ResExtension(): this("Not defined")
        {
        }

        public ResExtension(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                return Resources.ResourceManager.GetString(_name ?? string.Empty, Thread.CurrentThread.CurrentUICulture) ?? ErrorMessage;
            }
            catch
            {
                return ErrorMessage;
            }
        }

        private string ErrorMessage
        {
            get { return string.Format("Resource not found: \"{0}\"", _name ?? string.Empty); }
        }
    }
}