namespace ClimaTempoDesafioAPI.Models
{
    public class ReponseAPIWeather
    {
        public Location location { get; set; }
        public Current current { get; set; }
        public Forecast forecast { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string tz_id { get; set; }
        public float localtime_epoch { get; set; }
        public string localtime { get; set; }
    }

    public class Current
    {
        public float last_updated_epoch { get; set; }
        public string last_updated { get; set; }
        public float temp_c { get; set; }
        public float temp_f { get; set; }
        public float is_day { get; set; }
        public ConditionAPI condition { get; set; }
        public float wind_mph { get; set; }
        public float wind_kph { get; set; }
        public float wind_degree { get; set; }
        public string wind_dir { get; set; }
        public float pressure_mb { get; set; }
        public float pressure_in { get; set; }
        public float precip_mm { get; set; }
        public float precip_in { get; set; }
        public float humidity { get; set; }
        public float cloud { get; set; }
        public float feelslike_c { get; set; }
        public float feelslike_f { get; set; }
        public float windchill_c { get; set; }
        public float windchill_f { get; set; }
        public float heatindex_c { get; set; }
        public float heatindex_f { get; set; }
        public float dewpoint_c { get; set; }
        public float dewpoint_f { get; set; }
        public float vis_km { get; set; }
        public float vis_miles { get; set; }
        public float uv { get; set; }
        public float gust_mph { get; set; }
        public float gust_kph { get; set; }
        public float will_it_rain { get; set; }
        public float chance_of_rain { get; set; }
        public float will_it_snow { get; set; }
        public float chance_of_snow { get; set; }
        public float wetbulb_c { get; set; }
        public float wetbulb_f { get; set; }
        public float short_rad { get; set; }
        public float diff_rad { get; set; }
        public float dni { get; set; }
        public float gti { get; set; }
    }

    public class ConditionAPI
    {
        public string text { get; set; }
        public string icon { get; set; }
        public float code { get; set; }
    }

    public class Forecast
    {
        public Forecastday[] forecastday { get; set; }
    }

    public class Forecastday
    {
        public string date { get; set; }
        public float date_epoch { get; set; }
        public Day day { get; set; }
        public Astro astro { get; set; }
        public Hour[] hour { get; set; }
    }

    public class Day
    {
        public float maxtemp_c { get; set; }
        public float maxtemp_f { get; set; }
        public float mintemp_c { get; set; }
        public float mintemp_f { get; set; }
        public float avgtemp_c { get; set; }
        public float avgtemp_f { get; set; }
        public float maxwind_mph { get; set; }
        public float maxwind_kph { get; set; }
        public float totalprecip_mm { get; set; }
        public float totalprecip_in { get; set; }
        public float totalsnow_cm { get; set; }
        public float avgvis_km { get; set; }
        public float avgvis_miles { get; set; }
        public float avghumidity { get; set; }
        public float daily_will_it_rain { get; set; }
        public float daily_chance_of_rain { get; set; }
        public float daily_will_it_snow { get; set; }
        public float daily_chance_of_snow { get; set; }
        public Condition1 condition { get; set; }
        public float uv { get; set; }
        public float avgwetbulb_c { get; set; }
        public float avgwetbulb_f { get; set; }
        public float maxwetbulb_c { get; set; }
        public float maxwetbulb_f { get; set; }
    }

    public class Condition1
    {
        public string text { get; set; }
        public string icon { get; set; }
        public float code { get; set; }
    }

    public class Astro
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        public float moon_illumination { get; set; }
        public float is_moon_up { get; set; }
        public float is_sun_up { get; set; }
    }

    public class Hour
    {
        public float time_epoch { get; set; }
        public string time { get; set; }
        public float temp_c { get; set; }
        public float temp_f { get; set; }
        public float is_day { get; set; }
        public Condition2 condition { get; set; }
        public float wind_mph { get; set; }
        public float wind_kph { get; set; }
        public float wind_degree { get; set; }
        public string wind_dir { get; set; }
        public float pressure_mb { get; set; }
        public float pressure_in { get; set; }
        public float precip_mm { get; set; }
        public float precip_in { get; set; }
        public float snow_cm { get; set; }
        public float humidity { get; set; }
        public float cloud { get; set; }
        public float feelslike_c { get; set; }
        public float feelslike_f { get; set; }
        public float windchill_c { get; set; }
        public float windchill_f { get; set; }
        public float heatindex_c { get; set; }
        public float heatindex_f { get; set; }
        public float dewpoint_c { get; set; }
        public float dewpoint_f { get; set; }
        public float will_it_rain { get; set; }
        public float chance_of_rain { get; set; }
        public float will_it_snow { get; set; }
        public float chance_of_snow { get; set; }
        public float vis_km { get; set; }
        public float vis_miles { get; set; }
        public float gust_mph { get; set; }
        public float gust_kph { get; set; }
        public float uv { get; set; }
        public float wetbulb_c { get; set; }
        public float wetbulb_f { get; set; }
        public float short_rad { get; set; }
        public float diff_rad { get; set; }
        public float dni { get; set; }
        public float gti { get; set; }
    }

    public class Condition2
    {
        public string text { get; set; }
        public string icon { get; set; }
        public float code { get; set; }
    }

}
