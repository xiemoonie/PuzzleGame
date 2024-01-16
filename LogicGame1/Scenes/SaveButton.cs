using Godot;
using LogicGame1.Scripts.Location;
using System;
using System.IO;

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
        
        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        GameWrapper myGameWrapper = GetNode<GameWrapper>("/root/Main/Screen/GameWrapper");
        var sceneContainer = myGameWrapper.GetNode<Control>("SceneContainer");
        string nameScene = sceneContainer.GetChild<Node>(0).Name;

        var som = sceneContainer.GetChild<Node>(0);
        var list = som.GetGroups();
        GD.Print($"Som is :{som?.Name}, list:{list.Count}");

        inventory.getSprites();
        

        if (list.Count == 0)
        {
            som.AddToGroup("Garden", true);
        }
        inventory.getSprites();
        GameSaver.SaveGameScene();
        GameSaver.SaveGameInvenotry();
        sceneContainer.removeAllChildren();
        SceneController myScript = inventory.GetNode<SceneController>("/root/Main/SceneController");
        GD.Print("\n Going to Menu from SaveButton Script");
        myScript.goToMenu();


    }
}
