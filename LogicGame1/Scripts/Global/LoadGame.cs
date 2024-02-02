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
        inventoryItem = ResourceLoader.Load<PackedScene>("res://Objects/Gui/InventoryItem.tscn");
        GameTemplate = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn");
        GameLoader.LoadScene();

        Control screenContent = GetNode<Control>("/root/Main/Screen");
        screenContent.removeAllChildren();

        string scene = WorldDictionary.getMainScene();
        WorldDictionary.setCurrentScene("GardenOne");


        GameWrapper gameWrapper = GameTemplate.Instance<GameWrapper>();
        var inventoryContainer = gameWrapper.GetNode<InventoryManager>("GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
        
        gameWrapper.LocationToLoad = scene;
        screenContent.AddChild(gameWrapper);
        inventoryContainer.getSprites();
        //gameWrapper.loadLocation(scene);
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
