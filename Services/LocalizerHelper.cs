using System;
using System.Globalization;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Resources;
using TaskPlanner.Resources;

namespace TaskPlanner.Services
{
    public class LocalizerHelper : IStringLocalizer
    {
        private readonly System.Resources.ResourceManager _resourceManager;
        
        public LocalizerHelper()
        {
            try
            {
                _resourceManager = new System.Resources.ResourceManager(
                    "TaskPlanner.Resources.SharedResource", 
                    typeof(SharedResource).Assembly);
                
                // Debug check - retrieve a known key to validate resource manager is working
                var testKey = _resourceManager.GetString("TaskName", CultureInfo.CurrentUICulture);
                Debug.WriteLine($"Resource test - TaskName: {testKey ?? "NOT FOUND"}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing ResourceManager: {ex.Message}");
                // Fallback to a basic resource manager to prevent crashing
                _resourceManager = new ResourceManager(typeof(SharedResource));
            }
        }
        
        public LocalizedString this[string name]
        {
            get
            {
                string value = null;
                try
                {
                    value = _resourceManager.GetString(name, CultureInfo.CurrentUICulture);
                    Debug.WriteLine($"Localizing '{name}' => '{value}' (CurrentUICulture: {CultureInfo.CurrentUICulture.Name})");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error getting resource '{name}': {ex.Message}");
                }
                
                // If resource not found, return the key as the value
                return new LocalizedString(name, value ?? name, value == null);
            }
        }
        
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string format = null;
                try
                {
                    format = _resourceManager.GetString(name, CultureInfo.CurrentUICulture);
                }
                catch
                {
                    // If there's an error, we'll use the key as format
                }
                
                string value;
                try
                {
                    value = string.Format(format ?? name, arguments);
                }
                catch
                {
                    // If format fails, just return the format string or key
                    value = format ?? name;
                }
                
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
