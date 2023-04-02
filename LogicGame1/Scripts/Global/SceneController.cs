using Godot;
using System;

public class SceneController : Node {
	private PackedScene GameTemplate;

	private Control ScreenContent;
	
	public override void _Ready() {
		base._Ready();

		ScreenContent = GetParent().GetNode<Control>("Screen");
		GameTemplate = ResourceLoader.Load<PackedScene>("res://Objects/Menu.tscn");
		goToMenu();

	}

	public void goToMenu()
    {
		ScreenContent.removeAllChildren();
		var gameWrapper = GameTemplate.Instance<Menu>();
		ScreenContent.AddChild(gameWrapper);
	}


}
