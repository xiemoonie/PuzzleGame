using Godot;
using System;
using System.Threading;
using System.Timers;
using Object = Godot.Object;


class ItemSelected : TextureRect
{
    bool selected = false;
    Vector2 positionMouse;
    Vector2 mousePosition;
    InventoryItem inventory;
    public override void _Ready()
    {

    }
    public override void _Process(float delta)
    {
        
    }
    public override void _GuiInput(InputEvent _event)
    {
        base._GuiInput(_event);
        if (_event is InputEventMouseButton mouseEvent)
        {
            var inventoryManager = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
            inventory = GetOwner<InventoryItem>();
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                if (!selected)
                {
                    inventoryManager.selectedItem(inventory);
                    selected = true;
                }
                else
                {
                    inventoryManager.unselectedItem(inventory);
                    selected = false;
                }
            }
            
        }
       
     }
}


