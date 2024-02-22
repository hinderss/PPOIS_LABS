namespace Platform.Classes
{
    public class Resources
    {
        private string _resourceFolder;

        public Resources(string resourceFolder)
        {
            _resourceFolder = resourceFolder;
        }

        public string? GetResource(string id)
        {
            var resourcePath = Path.Combine(_resourceFolder, $"{id}.json");
            if (File.Exists(resourcePath))
            {
                var resourceJson = File.ReadAllText(resourcePath);

                return resourceJson;
            }
            else
            {
                return null;
            }
        } 
    }
}