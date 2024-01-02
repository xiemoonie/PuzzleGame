using Godot;
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
        GameSaver gameSaverScript = new GameSaver();
        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        GameWrapper myGameWrapper = GetNode<GameWrapper>("/root/Main/Screen/GameWrapper");
        var sceneContainer = myGameWrapper.GetNode<Control>("SceneContainer");
        string nameScene = sceneContainer.GetChild<Node>(0).Name;

        var som = sceneContainer.GetChild<Node>(0);
        var list = som.GetGroups();
        GD.Print($"Som is :{som?.Name}, list:{list.Count}");

        inventory.getSprites();
        gameSaverScript.SaveInventory(inventory);

        if (list.Count == 0)
        {
            som.AddToGroup("Garden", true);
        }
        var currentScene = sceneContainer.GetChild<Node>(0);
        switch (currentScene.Name)
        {
            case "GreenHouseThree":
                var greenHouseThree = sceneContainer.GetChild<GreenHousePotLocationHolder>(0);
                greenHouseThree.saveGreenHouseThree(); 
                break;
            case "WorkingTable":
                var workingTable = sceneContainer.GetChild<WorkingTableLocation>(0);
                workingTable.WorkingTableToSave(); 
                break;
            case "GardenSextant":
                var gardenSextant = sceneContainer.GetChild<GardenSunLocation>(0);
                gardenSextant.sextantToSave();
                break;
        }
       
        
        gameSaverScript.SaveParticularScene(inventory, myGameWrapper, nameScene);
        sceneContainer.removeAllChildren();
        SceneController myScript = inventory.GetNode<SceneController>("/root/Main/SceneController");
        GD.Print("Going to MENUUUU");
        myScript.goToMenu();


    }
}
