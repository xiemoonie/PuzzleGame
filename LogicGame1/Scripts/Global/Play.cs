using Godot;
using System;

public class Play : TextureButton
{
    private PackedScene GameTemplate;


    private Control ScreenContent;
    public override void _Ready()
    {


    }


    public override void _Pressed()
    {
        base._Pressed();
        GD.Print("play game...");
       // GameTemplate = ResourceLoader.Load<PackedScene>("res://Scenes/Main.tscn");

       // var gameWrapper = GameTemplate.Instance<Node>();

        ScreenContent = GetNode<Control>("/root/Main/Screen");
        ScreenContent.removeAllChildren();

        GameTemplate = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn");

        var gameWrapperGame = GameTemplate.Instance<GameWrapper>();
        gameWrapperGame.LocationToLoad = "res://Scenes/Locations/Location1/GardenOne.tscn";
        ScreenContent.AddChild(gameWrapperGame);


    }

}
 
