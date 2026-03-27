using Godot;
using System;

public partial class ButtonConsole : Node
{
	Node3D button1, button2, button3, button4;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		button1 = GetNode<Node3D>("Button1");
		button2 = GetNode<Node3D>("Button2");
		button3 = GetNode<Node3D>("Button3");
		button4 = GetNode<Node3D>("Button4");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void PressButton(StringName name)
	{
		switch (name)
		{
			case "Button1":
				switch (Main.currentWeather)
				{
					case Constants.WeatherSelection.Rain:

						break;
					case Constants.WeatherSelection.Snow:
						button1.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Rain;
						button2.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Storm:
						button1.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Rain;
						button3.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Clear:
						button1.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Rain;
						button4.Translate(new Vector3(0, 0.2f, 0));
						break;
				}
				break;
			case "Button2":
				switch (Main.currentWeather)
				{
					case Constants.WeatherSelection.Rain:
						button2.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Snow;
						button1.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Snow:
						break;
					case Constants.WeatherSelection.Storm:
						button2.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Snow;
						button3.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Clear:
						button2.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Snow;
						button4.Translate(new Vector3(0, 0.2f, 0));
						break;
				}
				break;
			case "Button3":
				switch (Main.currentWeather)
				{
					case Constants.WeatherSelection.Rain:
						button3.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Storm;
						button1.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Snow:
						button3.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Storm;
						button2.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Storm:
						break;
					case Constants.WeatherSelection.Clear:
						button3.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Storm;
						button4.Translate(new Vector3(0, 0.2f, 0));
						break;
				}
				break;
			case "Button4":
				switch (Main.currentWeather)
				{
					case Constants.WeatherSelection.Rain:
						button4.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Clear;
						button1.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Snow:
						button4.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Clear;
						button2.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Storm:
						button4.Translate(new Vector3(0, -0.2f, 0));
						Main.currentWeather = Constants.WeatherSelection.Clear;
						button3.Translate(new Vector3(0, 0.2f, 0));
						break;
					case Constants.WeatherSelection.Clear:
						break;
				}
				break;
		}



	}
}
