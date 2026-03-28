using Godot;
using System;

public partial class Weather : SubViewport
{
	// Called when the node enters the scene tree for the first time.
	private TextureRect Rain, Snow, Clear, Storm;
	public override void _Ready()
	{
		Rain = GetNode<TextureRect>("Rain");
		Snow = GetNode<TextureRect>("Snow");
		Clear = GetNode<TextureRect>("Clear");
		Storm = GetNode<TextureRect>("Storm");

		switch (Main.currentWeather)
		{
			case Constants.WeatherSelection.Rain:
				Rain.Visible = true;
				break;
			case Constants.WeatherSelection.Snow:
				Snow.Visible = true;
				break;
			case Constants.WeatherSelection.Storm:
				Storm.Visible = true;
				break;
			case Constants.WeatherSelection.Clear:
				Clear.Visible = true;
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		switch (Main.currentWeather)
		{
			case Constants.WeatherSelection.Rain:
				Rain.Visible = true;
				Snow.Visible = false;
				Clear.Visible = false;
				Storm.Visible = false;
				break;
			case Constants.WeatherSelection.Snow:
				Rain.Visible = false;
				Snow.Visible = true;
				Clear.Visible = false;
				Storm.Visible = false;
				break;
			case Constants.WeatherSelection.Storm:
				Rain.Visible = false;
				Snow.Visible = false;
				Clear.Visible = false;
				Storm.Visible = true;
				break;
			case Constants.WeatherSelection.Clear:
				Rain.Visible = false;
				Snow.Visible = false;
				Clear.Visible = true;
				Storm.Visible = false;
				break;
		}
	}
}
