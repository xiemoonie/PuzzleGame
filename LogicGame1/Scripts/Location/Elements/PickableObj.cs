using Godot;
using System;
using Object = Godot.Object;

class PickableObj : Area2D
{
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
    InventoryItem item;
    public override void _Ready()
    {
        item = itemScene.Instance<InventoryItem>();
    }
    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                Sprite some = (Sprite)this.GetParent();
                if (some != null)
                {  
                    WorldDictionary.setStateObject(Name,1);
                    GameSaver.SaveGameScene();
                    var objectToErase = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
                    objectToErase.pickedItem(item, some);
                    some.QueueFree();

                }
            }
        }
    }

}






