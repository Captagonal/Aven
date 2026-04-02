using Godot;
using System;

public partial class EvilKids : EvilCustomer
{
	public override void _Ready()
	{
		base._Ready();
		customerType = Constants.CustomerTypes.Kids; // Set the customer type to Bloblin for EvilBloblin
		health = 50; // Set the health of the EvilBloblin to 150
		speed = 1.6f; // Set the speed of the EvilBloblin to 0.75 (slower than regular customers)
	}
}
