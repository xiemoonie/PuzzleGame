using Godot;
using System;

public class LocationChangeButtonLeft : TextureButton
{
    public string locationDestinationPath = "";
    public GameWrapper gameWrapper;
    public override void _Ready()
    {
        }
    public override void _Pressed()
    {
        base._Pressed();
        GameSaver myGameSaver = new GameSaver();
        InventoryManager inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        //var inventoryContainer = gameWrapper.GetNode<HFlowContainer>("GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        GameWrapper myGameWrapper = GetNode<GameWrapper>("/root/Main/Screen/GameWrapper");
        var sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");

        var som = sceneContainer.GetChild<Node>(0);
        var list = som.GetGroups();
        GD.Print($"Som is :{som?.Name}, list:{list.Count}");

        if (list.Count == 0)
        {
            som.AddToGroup("Garden", true);
        }

        bool result = false;
        string nameScene = som?.Name;
        // myGameSaver.SaveInventory(inventoryContainer);
        // inventoryContainer.removeAllChildren();
        // myGameSaver.LoadInventory(inventoryContainer);
        switch (nameScene)
        {
            case "GardenOne":
                myGameSaver.SaveParticularScene(inventory, myGameWrapper, "GardenOne");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGardenTwoScene(inventory, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); } else { GD.Print("Not default scene"); };
                break;
            case "GardenTwo":
                myGameSaver.SaveParticularScene(inventory, myGameWrapper, "GardenTwo");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGardenThreeScene(inventory, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); } else { GD.Print("Not default scene"); };
                break;
            case "GardenThree":
                myGameSaver.SaveParticularScene(inventory, myGameWrapper, "GardenThree");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGardenOneScene(inventory, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); } else { GD.Print("Not default scene"); };
                break;
            case "GreenHouseOne":
                myGameSaver.SaveParticularScene(inventory, myGameWrapper, "GreenHouseOne");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGreenHouseThreeScene(inventory, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); } else { GD.Print("Not default scene"); };

                break;
            case "GreenHouseTwo":
                myGameSaver.SaveParticularScene(inventory, myGameWrapper, "GreenHouseTwo");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGreenHouseOneScene(inventory, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); } else { GD.Print("Not default scene"); };

                break;
            case "GreenHouseThree":
                var greenHouseThree = sceneContainer.GetChild<GreenHousePotLocationHolder>(0);
                greenHouseThree.saveGreenHouseThree();
                myGameSaver.SaveParticularScene(inventory, myGameWrapper, "GreenHouseThree");
                sceneContainer.removeAllChildren();
                result = myGameSaver.LoadGreenHouseTwoScene(inventory, sceneContainer);
                if (!result) { gameWrapper.loadLocation(locationDestinationPath); } else { GD.Print("Not default scene"); };

                break;
        }
        myGameWrapper.fetchLocation();


    }
}



