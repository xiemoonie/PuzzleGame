using Godot;
using System;

public class LoadGame : TextureButton
{
    private PackedScene GameTemplate;
    private PackedScene inventoryItem;

    public override void _Ready()
    {
        
    }

    public override void _Pressed()
    {
        base._Pressed();
        GD.Print("Load Game");
        GameSaver myGameSaver = new GameSaver();

        Control screenContent = GetNode<Control>("/root/Main/Screen");
        screenContent.removeAllChildren();


        inventoryItem = ResourceLoader.Load<PackedScene>("res://Objects/Gui/InventoryItem.tscn");
        GameTemplate = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn");

        var gameWrapper = GameTemplate.Instance<GameWrapper>();
      //  gameWrapper.LocationToLoad = "res://Scenes/Locations/Location1/Top4.tscn";

        screenContent.AddChild(gameWrapper);
        var sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");
        var inventoryContainer = gameWrapper.GetNode<HFlowContainer>("GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");

        myGameSaver.LoadGardenOneScene(inventoryContainer, sceneContainer);
       gameWrapper.fetchLocation();
    }
}
