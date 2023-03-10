using Godot;
using System;

public class SceneController : Node {
    private PackedScene GameTemplate;

    private Control ScreenContent;
    
    public override void _Ready() {
        base._Ready();
        ScreenContent = GetParent().GetNode<Control>("Screen");
        
        GameTemplate = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn");
        
        
        goToGame();
    }

    public void goToGame() {
        var gameWrapper = GameTemplate.Instance<GameWrapper>();
        ScreenContent.removeAllChildren();

        gameWrapper.LocationToLoad = "res://Scenes/Locations/Location1/Top.tscn";
        ScreenContent.AddChild(gameWrapper);
    }
}