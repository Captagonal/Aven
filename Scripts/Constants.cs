using System.Collections.Generic;
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

    public enum CustomerTypes{
        Bloblin,
        Farmer
    }

    public static List<WeatherSelection> WeatherWants = new List<WeatherSelection>();

    public static TimeOfDaySelection currentTime = TimeOfDaySelection.Morning;

    public static int health = 4;

    public static int gpd = 12; //Guys Per Day

    public static int coins = 5;
}