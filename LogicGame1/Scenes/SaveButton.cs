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

        switch (nameScene)
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
            case "GrutaScene":
                var grutaLocation = sceneContainer.GetChild<GrutaLocationHolder>(0);
                grutaLocation.GrutaToSave();
                break;
            case "GreenHouseOne":
                var greenHouseLocation = sceneContainer.GetChild<GreenhouseLocationHolder>(0);
                greenHouseLocation.GreenHouseToSave();
                break;
        }

        GD.Print("SAVING SCENE NAMED "+nameScene);
        gameSaverScript.SaveParticularScene(inventory, myGameWrapper, nameScene);
        sceneContainer.removeAllChildren();
        SceneController myScript = inventory.GetNode<SceneController>("/root/Main/SceneController");
        GD.Print("Going to MENUUUU");
        myScript.goToMenu();


    }
}
