namespace ClimaTempoDesafioAPI.Models
{
    public class ClimaTempo
    {
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public float Temp_c { get; set; }
        public float Humidity { get; set; }
        public Condition? Condition { get; set; }

        public float ForecastMaxtemp_c { get; set; }
        public float ForecastMintemp_c { get; set; }

    }


    public class Condition
    {
        public string Text { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Code { get; set; }
    }

}
