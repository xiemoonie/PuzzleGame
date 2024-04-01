using Godot;
using System;

public class Sand : Area2D
{
    Area2D Bucket;
    InventoryManager inventory;
    [Export] string bucketSprite;
    public override void _Ready()
    {
        Bucket = GetParent().GetNode<Area2D>("BucketSand");
        inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
    }
    public void checkForBucket()
    {
        var s = GetNodeOrNull<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        if (s != null && s.Texture != null)
        {
            GD.Print("Check for bucket" + s.Texture.ResourcePath);
            if (inventory.onlyOneSelected())
            {
                if (s.Texture.ResourcePath == bucketSprite)
                {

                    inventory.eraseItem();
                    GD.Print("Bucket found");
                    Bucket.Visible = true;
                    WorldDictionary.setStateObject(Bucket.Name, 2);
                    GameSaver.SaveGameScene();
                }
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
