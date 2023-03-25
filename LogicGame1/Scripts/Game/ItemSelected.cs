using Godot;
using System;
using Object = Godot.Object;

class ItemSelected : TextureRect
{
    public override void _Ready()
    {
     
    }
    
    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                
                var s = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer");
                InventoryItem some = this.GetOwner<InventoryItem>();
                s.selectedItem(some);
                GD.Print("clicked on item");
          
            }

           

        }
    }
    

}

