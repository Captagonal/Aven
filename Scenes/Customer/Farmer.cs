using Godot;
using System;

public partial class Farmer : Customer
{
	
	public override void _Ready()
	{
		customerType = Constants.CustomerTypes.Farmer;
		pathFollow = GetParent<PathFollow3D>();
	}
}
