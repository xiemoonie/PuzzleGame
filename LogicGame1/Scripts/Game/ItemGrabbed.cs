using Godot;
using System;
using Object = Godot.Object;

class ItemGrabbed : TextureRect
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
                TextureRect item = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer/InventoryItem/Content/Texture/SelectedItem");
                TextureRect textureItem = GetNode <TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/InventoryContainer/InventoryItem/Content/Texture");
                TextureRect itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
                itemToDisplay.Texture = textureItem.Texture;
                item.Visible = true;
               
            }
            
        }
    }

}

