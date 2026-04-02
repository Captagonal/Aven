using Godot;
using System;

public partial class SnowMan : Customer
{
	public override void _Ready()
	{
		customerType = Constants.CustomerTypes.SnowMan;
		pathFollow = GetParent<PathFollow3D>();
	}
}
