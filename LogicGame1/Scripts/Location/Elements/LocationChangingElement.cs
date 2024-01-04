using Godot;
using System;
using Object = Godot.Object;

public class LocationChangingElement : Area2D
{
    [Export] public string locationPath = "";
    public GameWrapper gameWrapper;
    Control sceneContainer;
    HFlowContainer inventoryContainer;
    bool result = false;
    GameSaver myGameSaver = new GameSaver();

    public override void _Ready()
    {
        gameWrapper = GetNode<GameWrapper>("/root/Main/Screen/GameWrapper");
        inventoryContainer = gameWrapper.GetNode<HFlowContainer>("GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
        sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");
    }


    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {

                var som = sceneContainer.GetChild<Node>(0);
                var list = som.GetGroups();
                GD.Print($"Som is :{som?.Name}, list:{list.Count}");

                if (list.Count == 0)
                {
                    som.AddToGroup("Garden", true);
                }

                bool result = false;
                string nameScene = som?.Name;

                switch (locationPath)
                {
                    case "res://Scenes/Locations/GardenLocation/GardenSextant.tscn":
                        if (nameScene == "GardenTwo")
                        {
                            myGameSaver.SaveParticularScene(inventoryContainer, gameWrapper, "GardenTwo");
                            sceneContainer.removeAllChildren();
                        }
                        result = myGameSaver.LoadGardenSextantScene(inventoryContainer, sceneContainer);
                        if (!result)
                        {
                            gameWrapper.loadLocation(locationPath);
                        }
                        gameWrapper.fetchLocation();
                        break;
                    case "res://Scenes/Locations/GardenLocation/WorkingTable.tscn":
                        if (nameScene == "GreenHouseOne")
                        {
                            myGameSaver.SaveParticularScene(inventoryContainer, gameWrapper, "GreenHouseOne");
                            sceneContainer.removeAllChildren();
                        }
                        result = myGameSaver.LoadWorkingTableScene(inventoryContainer, sceneContainer);
                        if (!result)
                        {
                            gameWrapper.loadLocation(locationPath);
                        }
                        gameWrapper.fetchLocation();
                        break;
                    case "res://Scenes/Locations/GardenLocation/GrutaScene.tscn":
                        if (nameScene == "GardenOne")
                        {
                            myGameSaver.SaveParticularScene(inventoryContainer, gameWrapper, "GardenOne");
                            sceneContainer.removeAllChildren();
                        }
                        result = myGameSaver.LoadGrutaScene(inventoryContainer, sceneContainer);
                        if (!result)
                        {
                            gameWrapper.loadLocation(locationPath);
                        }
                        gameWrapper.fetchLocation();
                        break;
                    case "res://Scenes/Locations/GardenLocation/GreenHouseOne.tscn":
                        if (nameScene == "GardenTwo")
                        {
                            myGameSaver.SaveParticularScene(inventoryContainer, gameWrapper, "GardenTwo");
                            sceneContainer.removeAllChildren();
                        }
                        result = myGameSaver.LoadGreenHouseOneScene(inventoryContainer, sceneContainer);
                        if (!result)
                        {
                            gameWrapper.loadLocation(locationPath);
                        }
                        gameWrapper.fetchLocation();
                        break;
                    default: this.getGameWrapperController().loadLocation(locationPath); break;
                }





            }
        }
    }
}
