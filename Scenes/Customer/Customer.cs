using Godot;
using System;

public partial class Customer : CharacterBody3D
{
	public PathFollow3D pathFollow;
	private bool isAtStore = false;
	private bool waiting = false;
	private bool beenAtStore = false;
	public Constants.CustomerTypes customerType;
	public override void _Ready()
	{
		// customerType = (Constants.CustomerTypes) (GD.Randi() % Enum.GetValues(typeof(Constants.CustomerTypes)).Length);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (isAtStore)
		{
			// Customer is at the store, do nothing or perform some action
			isAtStore = false;
			waiting = true;
			beenAtStore = true;
			GetTree().CreateTimer(10).Timeout += () =>
			{
				waiting = false;
				switch (customerType)
				{
					case Constants.CustomerTypes.Bloblin:
						Constants.WeatherWants.Add(Constants.WeatherSelection.Storm);
						break;
					case Constants.CustomerTypes.Farmer:
						Constants.WeatherWants.Add(Constants.WeatherSelection.Rain);
						break;
				}
			};
			return;
		}
		else if (waiting){
			return;
		}
		else
		{
			// Move the customer along the path
			pathFollow.Progress += (float)delta * 3; // Adjust speed as needed

			// Check if the customer has reached the store (you can adjust the offset value as needed)
			if (pathFollow.ProgressRatio >= .47 && pathFollow.ProgressRatio <= .48 && !beenAtStore) // Assuming 1000 is the offset where the store is located
			{
				isAtStore = true;
			}
			if (pathFollow.ProgressRatio >= .98) // Assuming 1000 is the offset where the store is located
			{
				GetParent().QueueFree(); // Remove the customer from the scene
			}
		}




	}
}
