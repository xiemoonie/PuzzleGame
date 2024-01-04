using Godot;
using System;
using System.IO;

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

        screenContent.AddChild(gameWrapper);
        var sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");
        var inventoryContainer = gameWrapper.GetNode<HFlowContainer>("GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");

        myGameSaver.LoadGardenOneScene(inventoryContainer, sceneContainer);
        myGameSaver.LoadInventory(inventoryContainer);

        gameWrapper.fetchLocation();
  
    }
    public void EraseFiles()
    {
        string[] filePaths = System.IO.Directory.GetFiles("C:\\Users\\carod\\AppData\\Roaming\\Godot\\app_userdata\\LogicGame1");

        foreach (string filePath in filePaths)
        {
            var name = new FileInfo(filePath).Name;
            name = name.ToLower();
            if (name != "logs")
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
