using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Timed : Timer
{
	// Called when the node enters the scene tree for the first time.
	int counter = -1;
	WorldEnvironment environment;
	public List<Node3D> customers = new List<Node3D>();
	Path3D path;
	PackedScene[] customerScenes = new PackedScene[]
	{
		GD.Load<PackedScene>("res://Scenes/Customer/Bloblin.tscn"),
		GD.Load<PackedScene>("res://Scenes/Customer/Farmer.tscn"),
		GD.Load<PackedScene>("res://Scenes/kids.tscn"),
		GD.Load<PackedScene>("res://Scenes/Customer/snow_man.tscn")
	};
	PackedScene[] EvilcustomerScenes = new PackedScene[]
	{
		GD.Load<PackedScene>("res://Scenes/Customer/evil_bloblin.tscn"),
		GD.Load<PackedScene>("res://Scenes/Customer/evil_horse.tscn"),
		GD.Load<PackedScene>("res://Scenes/Customer/evil_kids.tscn"),
			GD.Load<PackedScene>("res://Scenes/Customer/evil_snowman.tscn")
		
	};
	public override void _Ready()
	{
		Timeout += _on_timer_timeout;
		environment = GetNode<WorldEnvironment>("../WorldEnvironment");
		path = GetNode<Path3D>("../Path3D");
		_on_timer_timeout();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	float time;
	int gpd = Constants.gpd;
	public override void _Process(double delta)
	{
		time += (float)delta;
		if (time >= (WaitTime * 3 - 50) / gpd)
		{
			if (customers.Count == 0)
			{
				return;
			}

			GD.Print("Spawning customer at time: " + time);
			time = 0;
			PathFollow3D newPathFollow = new PathFollow3D();
			newPathFollow.Name = "PathFollow" + customers.Count;
			path.AddChild(newPathFollow);
			newPathFollow.ProgressRatio = 0;
			newPathFollow.AddChild(customers.FirstOrDefault());
			customers.RemoveAt(0);
		}
	}

	public void _on_timer_timeout()
	{
		counter++;
		counter %= 6;
		ProceduralSkyMaterial proceduralSkyMaterial = environment.Environment.Sky.SkyMaterial as ProceduralSkyMaterial;
		// GD.Print("Time of day changed to: " + counter);
		switch (counter)
		{
			case 0:
				GD.Print("CLEAR");
				gpd = Constants.gpd;
				Constants.currentTime = Constants.TimeOfDaySelection.Morning;
				proceduralSkyMaterial.SkyTopColor = new Color(0.8f, 0.6f, 0.4f);
				for (int i = 0; i < gpd; i++)
				{
					Customer newCustomer;
					int customerType = (int)(GD.Randi() % Enum.GetValues(typeof(Constants.CustomerTypes)).Length);
					newCustomer = customerScenes[customerType].Instantiate<Customer>();
					newCustomer.Name = "Customer" + i;
					newCustomer.Rotation = new Vector3(0, Mathf.DegToRad(-90f), 0);
					customers.Add(newCustomer);
					GD.Print("Added customer to list: " + customers.LastOrDefault().Name);
				}

				break;
			case 1:
				Constants.currentTime = Constants.TimeOfDaySelection.Afternoon;
				proceduralSkyMaterial.SkyTopColor = new Color(0.4f, 0.6f, 0.8f);
				break;
			case 2:
				Constants.currentTime = Constants.TimeOfDaySelection.Evening;
				proceduralSkyMaterial.SkyTopColor = new Color(0.2f, 0.2f, 0.4f);
				break;
			case 3:
				GD.Print("Time of day changed to: night");
				switch (Main.currentWeather)
				{
					case Constants.WeatherSelection.Rain:
						GD.Print("Raining");
						break;
					case Constants.WeatherSelection.Storm:
						GD.Print("Storming");
						break;
					case Constants.WeatherSelection.Clear:
						GD.Print("Clear");
						break;
					case Constants.WeatherSelection.Snow:
						GD.Print("Snowing");
						break;
				}
				for (int i = 0; i < Constants.WeatherWants.Count; i++)
				{
					if (Constants.WeatherWants[i] == Main.currentWeather)
					{
						Constants.WeatherWants.RemoveAt(i);
					}
				}
				GD.Print("Weather wants count: " + Constants.WeatherWants.Count);
				gpd = Constants.WeatherWants.Count;
				foreach (Constants.WeatherSelection weather in Constants.WeatherWants)
				{
					EvilCustomer newCustomer = EvilcustomerScenes[0].Instantiate<EvilCustomer>();
					switch (weather)
					{
						case Constants.WeatherSelection.Rain:
							newCustomer = EvilcustomerScenes[1].Instantiate<EvilCustomer>();
							break;
						case Constants.WeatherSelection.Storm:
							newCustomer = EvilcustomerScenes[0].Instantiate<EvilCustomer>();
							break;
						case Constants.WeatherSelection.Clear:
							newCustomer = EvilcustomerScenes[2].Instantiate<EvilCustomer>();
							break;
						case Constants.WeatherSelection.Snow:
							GD.Print("Customer wants snow");
							break;
					}
					newCustomer.Name = "Customer" + Time.GetTicksMsec();
					newCustomer.Rotation = new Vector3(0, Mathf.DegToRad(-90f), 0);
					customers.Add(newCustomer);
				}
				for (int i = 0; i < gpd; i++)
				{
					EvilCustomer newCustomer;
					// int customerType = (int)(GD.Randi() % Enum.GetValues(typeof(Constants.CustomerTypes)).Length);
					newCustomer = EvilcustomerScenes[0].Instantiate<EvilCustomer>();
					
					GD.Print("Added customer to list: " + customers.LastOrDefault().Name);
				}
				Constants.WeatherWants.Clear();
				Constants.currentTime = Constants.TimeOfDaySelection.Night;
				proceduralSkyMaterial.SkyTopColor = new Color(0.05f, 0.05f, 0.1f);

				break;
			case 4:
				Constants.currentTime = Constants.TimeOfDaySelection.Night;
				proceduralSkyMaterial.SkyTopColor = new Color(0.05f, 0.05f, 0.1f);

				break;
			case 5:
				Constants.currentTime = Constants.TimeOfDaySelection.Night;
				proceduralSkyMaterial.SkyTopColor = new Color(0.05f, 0.05f, 0.1f);
				break;
		}
	}
}
