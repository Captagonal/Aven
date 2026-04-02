using Godot;
using System;

public partial class EvilHorse : EvilCustomer
{
	public override void _Ready()
	{
		base._Ready();
		customerType = Constants.CustomerTypes.Farmer; // Set the customer type to Bloblin for EvilBloblin
		health = 75; // Set the health of the EvilBloblin to 150
		speed = 1.25f; // Set the speed of the EvilBloblin to 0.75 (slower than regular customers)
	}
}
