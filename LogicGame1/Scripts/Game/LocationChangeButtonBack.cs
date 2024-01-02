using Godot;
using System;

public class LocationChangeButtonBack : TextureButton
{
    public string locationDestinationPath = "";
    public GameWrapper gameWrapper;

    public override void _Pressed()
    {
        base._Pressed();
        //	gameWrapper.loadLocation(locationDestinationPath);

        GameSaver myGameSaver = new GameSaver();
        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        gameWrapper = GetNode<GameWrapper>("/root/Main/Screen/GameWrapper");
        var inventoryContainer = gameWrapper.GetNode<HFlowContainer>("GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        var sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");

        var sceneChilds = sceneContainer.GetChild<Node>(0);
        var list = sceneChilds.GetGroups();
        GD.Print($"Scene childs is :{sceneChilds?.Name}, list:{list.Count}");

        if (list.Count == 0)
        {
            sceneChilds.AddToGroup("Garden", true);
        }

        bool result = false;
        string nameScene = sceneChilds?.Name;
      
        switch (nameScene)
        {
            case "WorkingTable":
                var workingTable = sceneContainer.GetChild<WorkingTableLocation>(0);
                workingTable.WorkingTableToSave();
                myGameSaver.SaveParticularScene(inventory, gameWrapper, "WorkingTable");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGreenHouseOneScene(inventoryContainer, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); } 
                break;
            case "GreenHouseOne":
                myGameSaver.SaveParticularScene(inventory, gameWrapper, "GreenHouseOne");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGardenTwoScene(inventoryContainer, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); }
                break;
            case "GreenHouseThree":
                var greenHouseThree = sceneContainer.GetChild<GreenHousePotLocationHolder>(0);
                greenHouseThree.saveGreenHouseThree();
                myGameSaver.SaveParticularScene(inventory, gameWrapper, "GreenHouseThree");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGardenTwoScene(inventoryContainer, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); }
                break;
            case "Diary":
                myGameSaver.SaveParticularScene(inventory, gameWrapper, "Diary");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGardenTwoScene(inventoryContainer, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); }
                break;
            case "GardenSextant":
                var gardenSextant = sceneContainer.GetChild<GardenSunLocation>(0);
                gardenSextant.sextantToSave();
                myGameSaver.SaveParticularScene(inventory, gameWrapper, "GardenSextant");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGardenTwoScene(inventoryContainer, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); }
                break;
        }

        gameWrapper.fetchLocation();

    }
}
