using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Timed : Timer
{
	// Called when the node enters the scene tree for the first time.
	int counter = -1;
	WorldEnvironment environment;
	public List<Customer> customers = new List<Customer>();
	PathFollow3D pathFollow;
	Path3D path;

	public override void _Ready()
	{
		Timeout += _on_timer_timeout;
		environment = GetNode<WorldEnvironment>("../WorldEnvironment");
		path = GetNode<Path3D>("../Path3D");
		pathFollow = path.GetNode<PathFollow3D>("PathFollow3D");
		_on_timer_timeout();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	float time;
	int gpd = 12;
	public override void _Process(double delta)
	{
		time += (float)delta;
		if (time >= (WaitTime*3)/gpd)
		{
			GD.Print("Spawning customer at time: " + time);
			time = 0;
			PathFollow3D newPathFollow = pathFollow.Duplicate() as PathFollow3D;
			path.AddChild(newPathFollow);
			newPathFollow.ProgressRatio = 0;
			if (customers.Count == 0)
			{
				GD.Print("No customers to spawn");
				return;
			}
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
				Constants.currentTime = Constants.TimeOfDaySelection.Morning;
				proceduralSkyMaterial.SkyTopColor = new Color(0.8f, 0.6f, 0.4f);
				for (int i = 0; i < gpd; i++)
				{
					Customer newCustomer = GD.Load<PackedScene>("res://Scenes/Customer/Customer.tscn").Instantiate<Customer>();
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
