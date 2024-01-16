using Godot;
using LogicGame1.Scripts.Location;
using System;

public class LocationChangeButtonBack : TextureButton
{
    public string locationDestinationPath = "";
    public GameWrapper gameWrapper;

    public override void _Pressed()
    {
        base._Pressed();

        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        var inventoryContainer = gameWrapper.GetNode<HFlowContainer>("GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        var sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");
     

        var sceneChilds = sceneContainer.GetChild<Node>(0);
        var list = sceneChilds.GetGroups();
        GD.Print($"Scene childs is :{sceneChilds?.Name}, list:{list.Count}");

        if (list.Count == 0)
        {
            sceneChilds.AddToGroup("Garden", true);
        }

        //myGameSaver.SaveParticularScene(inventory, gameWrapper, WorldDictionary.getCurrentScene());
        sceneContainer.removeAllChildren();
        gameWrapper.loadLocation(WorldDictionary.getSceneBack());
        gameWrapper.fetchLocation();

    }
}
