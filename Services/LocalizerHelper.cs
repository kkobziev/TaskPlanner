using System;
using System.Globalization;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using TaskPlanner.Resources;

namespace TaskPlanner.Services
{
    public class LocalizerHelper : IStringLocalizer
    {
        private readonly System.Resources.ResourceManager _resourceManager;
        
        public LocalizerHelper()
        {
            _resourceManager = new System.Resources.ResourceManager(typeof(SharedResource));
        }
        
        public LocalizedString this[string name]
        {
            get
            {
                var value = _resourceManager.GetString(name, CultureInfo.CurrentUICulture);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }
        
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = _resourceManager.GetString(name, CultureInfo.CurrentUICulture);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, format == null);
            }
        }
        
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            // This method is required by IStringLocalizer interface but isn't used directly in our app
            // A proper implementation would scan all resources, but we're using direct key access
            return new List<LocalizedString>();
        }
    }
}
