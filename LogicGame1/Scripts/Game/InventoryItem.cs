using Godot;
using System;
using Object = Godot.Object;

public class InventoryItem : Control
{
    public delegate void PictureOnScreen();

    public override void _Ready() {
        base._Ready();
    }
    public event PictureOnScreen pictureOnScreenEvent;
    public void setTexture(Sprite s)
    {
        TextureRect item = GetNode<TextureRect>("Content/Texture");
        item.Texture = s.Texture;

        if (pictureOnScreenEvent != null) {
            pictureOnScreenEvent();
        }
    }
}

    