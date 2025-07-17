using System.Text.Json;

namespace LocationWeatherReport.Services
{
    public class LocationStorageService
    {
        private const string FilePath = "locations.json";

        public List<string> LoadLocations()
        {
            if (!File.Exists(FilePath)) return new List<string>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }


        //Saves the City entered in Search
        public void SaveLocation(string city)
        {
            var locations = LoadLocations();
            //Check if City already exist, if not adds the location
            if (!locations.Contains(city, StringComparer.OrdinalIgnoreCase))
            {
                locations.Add(city);
                File.WriteAllText(FilePath, JsonSerializer.Serialize(locations));
            }
        }


        //Clears all Locations Saved
        public void DeleteLocation()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);

        }
    }
}
