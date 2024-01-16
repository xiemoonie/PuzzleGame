using Godot;
using LogicGame1.Scripts.Location;
using System;

public class LocationChangeButtonRight : TextureButton
{
    public string locationDestinationPath = "";
    public GameWrapper gameWrapper;

    public override void _Pressed()
    {
        base._Pressed();

        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        var sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");


        var som = sceneContainer.GetChild<Node>(0);
        var list = som.GetGroups();
        GD.Print($"Som is :{som?.Name}, list:{list.Count}");

        if (list.Count == 0)
        {
            som.AddToGroup("Garden", true);
        }

        sceneContainer.removeAllChildren();
      
        gameWrapper.loadLocation(WorldDictionary.getSceneRight());  
        gameWrapper.fetchLocation();


    }
}
