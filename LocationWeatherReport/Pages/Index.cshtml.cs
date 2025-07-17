using LocationWeatherReport.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LocationWeatherReport.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WeatherService _weatherService;
        private readonly LocationStorageService _locationStorageService;      

        [BindProperty]
        public string City { get; set; }

        public string WeatherDisplay { get; set; }
        public List<string> SavedLocations { get; set; } = new();

        public IndexModel(WeatherService weatherService, LocationStorageService locationStorageService)
        {
            _weatherService = weatherService;
            _locationStorageService = locationStorageService;
        }

        public void OnGet()
        {
            //On Load get the list of Locations Saved
            SavedLocations = _locationStorageService.LoadLocations();
        }
        

        //On Search, the location is saved and the weather is displayed in the Result
        public async Task OnPostAddLocationsAsync()
        {
            //Check for the city name is not null or empty
            if (!string.IsNullOrWhiteSpace(City))
            {
                try
                {
                    //Call the API to get the Weather
                    //TODO Task : Call By PostCode and Country code (The APi needs zip Code and country code as input)
                    var weather = await _weatherService.GetWeatherAsync(City);
                    //Display the Result with City name and Temperature
                    WeatherDisplay = $"{weather.Name}: {weather.Main.Temp}°C";
                    _locationStorageService.SaveLocation(City);
                }
                catch (Exception ex)
                {
                    WeatherDisplay = $"Error: {ex.Message}";
                }
            }
            //Save the Location
            SavedLocations = _locationStorageService.LoadLocations();
        }


        //Clears all the Locations
        public async Task OnPostClearAllLocationsAsync()
        {
            var locations =  _locationStorageService.LoadLocations();
            if (locations.Count != 0) 
                _locationStorageService.DeleteLocation();
        }
    }
}
