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
                var sceneContainer = gameWrapper.GetNode<Control>("SceneContainer");
                var som = sceneContainer.GetChild<Node>(0);
                var list = som.GetGroups();

                if (list.Count == 0)
                {
                    som.AddToGroup("Garden", true);
                }
                GD.Print($"String to send to function:{Name}");
              
                sceneContainer.removeAllChildren();

                gameWrapper.loadLocation(WorldDictionary.getSceneZoom(this.Name));
                gameWrapper.fetchLocation();


            }
        }
    }
}
