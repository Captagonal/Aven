using System.Collections.Generic;
using System.Linq;
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

    public static int health = 5;

    public static int gpd = 12; //Guys Per Day

    public static int coins = 20;
    
    public static void TakeDamage(int damage, Node owner)
    {
        health -= damage;
        owner.GetNode<Label>("%Health").Text = $"Health: "; // Update the health label in the UI

        for (int i = 0; i < health; i++)
        {
            owner.GetNode<Label>("%Health").Text += "<3"; // Add a heart emoji for each remaining health point
        }
        if (health <= 0)
        {
            health = 0; // Ensure health doesn't go below 0
            GD.Print("Game Over! Your health has been depleted.");
          
        }
    }

    public static void SpendCoin(int coinsToSpend, Node owner){
        if (coins > 0)
        {
            coins-= coinsToSpend;
        }
            owner.GetNode<Label>("%Coins").Text = $"Coins: {coins}"; // Update the coins label in the UI
    }
}