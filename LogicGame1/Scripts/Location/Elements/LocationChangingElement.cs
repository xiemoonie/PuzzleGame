using Godot;
using System;
using Object = Godot.Object;

public class LocationChangingElement : Area2D {
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


    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx) {
        base._InputEvent(viewport, @event, shapeIdx);
        
        if (@event is InputEventMouseButton mouseEvent) {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {

                switch (locationPath)
                {
                    case "res://Scenes/Locations/GardenLocation/GardenSextant.tscn":
                        sceneContainer.removeAllChildren();
                        result = myGameSaver.LoadGardenSextantScene(inventoryContainer, sceneContainer);
                        if (!result)
                        {
                            this.getGameWrapperController().loadLocation(locationPath);
                            GD.Print("\n Use default scene");
                        }
                        else
                        {
                            gameWrapper.fetchLocation();
                        }
                        break;
                    case "res://Scenes/Locations/GardenLocation/WorkingTable.tscn":
                        sceneContainer.removeAllChildren();
                        result = myGameSaver.LoadWorkingTableScene(inventoryContainer, sceneContainer);
                        if (!result)
                        {
                            this.getGameWrapperController().loadLocation(locationPath);
                            GD.Print("\n Use default scene");
                        }
                        else
                        {
                            gameWrapper.fetchLocation();
                        }
                        break;
                    default: this.getGameWrapperController().loadLocation(locationPath);  break;
                }

               
               
                    
                
            }
        }
    }
}
