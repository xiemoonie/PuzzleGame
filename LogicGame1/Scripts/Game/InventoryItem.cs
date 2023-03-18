using Godot;
using System;
using Object = Godot.Object;

class InventoryItem : Control
{

    public void setItem(Sprite s)
    {
        TextureRect item= GetNode<TextureRect>("Content/Texture"); 
        item.Texture = s.Texture;

    }
}

    