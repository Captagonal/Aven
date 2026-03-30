using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Cauldron : Node3D
{
	public enum PotionIngredients
	{
		water,
		eye,
		slime,
		flower,
	}
	List<PotionIngredients> ingredients = new List<PotionIngredients>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ingredients.Add(PotionIngredients.water);
		ingredients.Add(PotionIngredients.eye);
		ingredients.Add(PotionIngredients.eye);
		ingredients.Add(PotionIngredients.eye);
		ingredients.Add(PotionIngredients.slime);
		ingredients.Add(PotionIngredients.flower);
		ingredients.Add(PotionIngredients.slime);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void pushButton(Node3D node)
	{
		int damage = 0;
		int knockback = 0;
		GD.Print("Button Pressed, number 1 ingredient is: " + getNumber1Ingredient());
		Node3D button = node.GetParent<Node3D>();
		button.Position += new Vector3(0, -0.1f, -0.1f);
		GetTree().CreateTimer(2).Timeout += () =>
			{
				button.Position += new Vector3(0, 0.1f, 0.1f);
			};
		if (!ingredients.Contains(PotionIngredients.water)){
			//Needs Water
			ingredients.Clear();
			return;
		}
		foreach (PotionIngredients ingredient in ingredients){
			switch (ingredient)
			{
				case PotionIngredients.water:
					
					break;
				case PotionIngredients.eye:
					damage += 10;
					break;
				case PotionIngredients.slime:
					knockback += 1;
					break;
				case PotionIngredients.flower:
					knockback += 2;
					break;
			}
		}
		Potion potion = GD.Load<PackedScene>("res://Scenes/Potion.tscn").Instantiate<Potion>();
				GetParent().AddChild(potion);
				potion.GlobalPosition = GlobalPosition + new Vector3(0, 1, 2);
		switch (getNumber1Ingredient()){
			case PotionIngredients.water:
				break;
			case PotionIngredients.eye:
				potion.ChangeColour(Colors.Red);
				break;
			case PotionIngredients.slime:
				potion.ChangeColour(Colors.Green);
				break;
			case PotionIngredients.flower:
				potion.ChangeColour(Colors.Purple);
				break;
		}
				potion.damage = damage;
				potion.knockback = knockback;
				potion.Scale = new Vector3(0.38f, 0.38f, 0.38f);


		// ingredients.Clear(); // empty the cauldron
		
	}


	private PotionIngredients getNumber1Ingredient(){
		return ingredients.GroupBy(value => value).OrderByDescending(group => group.Count()).Select(group => group.Key).FirstOrDefault();
		
	}
}
