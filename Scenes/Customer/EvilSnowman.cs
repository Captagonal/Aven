using Godot;
using System;

public partial class EvilSnowman : EvilCustomer
{
	public override void _Ready()
	{
		base._Ready();
		customerType = Constants.CustomerTypes.SnowMan; // Set the customer type to Bloblin for EvilBloblin
		health = 200; // Set the health of the EvilBloblin to 150
		speed = .3f; // Set the speed of the EvilBloblin to 0.75 (slower than regular customers)
	}
}
