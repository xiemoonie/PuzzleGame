using Godot;
using System;

public class SaveButton : TextureButton
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Pressed()
    {
        base._Pressed();
        
        GD.Print("click saving...");
        GameSaver gameSaverScript = new GameSaver();
        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        GameWrapper myGameWrapper = GetNode<GameWrapper>("/root/Main/Screen/GameWrapper");
        var sceneContainer = myGameWrapper.GetNode<Control>("SceneContainer");
        string nameScene = sceneContainer.GetChild<Node>(0).Name;

        var som = sceneContainer.GetChild<Node>(0);
        var list = som.GetGroups();
        GD.Print($"Som is :{som?.Name}, list:{list.Count}");

        if (list.Count == 0)
        {
            som.AddToGroup("Garden", true);
        }

        inventory.getSprites();
        gameSaverScript.SaveParticularScene(inventory, myGameWrapper, nameScene);
        sceneContainer.removeAllChildren();
        gameSaverScript.SaveInventory(inventory);
        
    }


}
