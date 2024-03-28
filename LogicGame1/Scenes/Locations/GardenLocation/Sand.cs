using Godot;
using System;

public class Sand : Area2D
{
    Area2D Bucket;
    [Export] string bucketSprite;
    public override void _Ready()
    {
        Bucket = GetParent().GetNode<Area2D>("BucketSand");
    }
    public void checkForBucket()
    {
        var s = GetNodeOrNull<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        if (s != null && s.Texture != null)
        {
            GD.Print("Check for bucket" + s.Texture.ResourcePath);
            if (s.Texture.ResourcePath == bucketSprite)
            {
                var inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
                inventory.eraseItem();
                GD.Print("Bucket found");
                Bucket.Visible = true;
            }
        }
    }
    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            checkForBucket();
        }
    }
}
