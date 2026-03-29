using Godot;

public static class Constants
{
   public enum WeatherSelection
    {
        Rain,
        Snow,
        Clear,
        Storm
    }
    public enum TimeOfDaySelection
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    public static TimeOfDaySelection currentTime = TimeOfDaySelection.Morning;
}