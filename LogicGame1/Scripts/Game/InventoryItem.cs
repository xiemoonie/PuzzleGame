using Godot;
using System;
using Object = Godot.Object;

public class InventoryItem : Control
{
    public delegate void PictureOnScreen();

    public override void _Ready() {
        //  GetParent<ItemOnInventory>().itemAdded(this);
    }
    public event PictureOnScreen pictureOnScreenEvent;
    public void setTexture(Sprite s)
    {

        //GD.Print("some stuff happends");
        TextureRect item = GetNode<TextureRect>("Content/Texture");
        item.Texture = s.Texture;

        if (pictureOnScreenEvent != null) {
            pictureOnScreenEvent();
            //      GD.Print("chinga wut");
        } else {
            //  GD.Print("say wut");
        }


    }

}

    