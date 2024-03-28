using Godot;
using LogicGame1.Scripts.Location;
using System;
using System.Data.SqlTypes;
using System.Net.Sockets;

public class Rotate : Area2D
{
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
    private PackedScene game = (PackedScene)GD.Load("res://Scenes/Game.tscn");
    GameWrapper gameWrapper;
    [Export] public String pathKey = "";
    [Export] public String pathGuiKey = "";
    [Export] public String pathMetal = "";
    [Export] public String pathGuiMetal = "";
    [Export] public float xShape;
    bool shapedMarked = false;
    bool melted = false;
    Sprite Key;
    InventoryItem item;

    CollisionPolygon2D Up;
    CollisionPolygon2D Down;
    CollisionPolygon2D Right;
    CollisionPolygon2D Left;
    BucketKey bucket;
    bool drawnedKey = false;
    public override void _Ready()
    {
        gameWrapper = game.Instance<GameWrapper>();
        Key = GetParent().GetParent().GetNode<Sprite>("Key");
        item = itemScene.Instance<InventoryItem>();
        Up = GetParent().GetParent().GetNode<CollisionPolygon2D>("BackgroundOne/Up/AreaUp");
        Down = GetParent().GetParent().GetNode<CollisionPolygon2D>("BackgroundOne/Down/AreaDown");
        Left = GetParent().GetParent().GetNode<CollisionPolygon2D>("BackgroundOne/Left/AreaLeft");
        Right = GetParent().GetParent().GetNode<CollisionPolygon2D>("BackgroundOne/Right/AreaRight");
        bucket = GetParent<BucketKey>();
     
    }
    public bool checkForKey()
    {
        var s = GetNodeOrNull<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        if (s != null && s.Texture != null)
        {
            GD.Print("Check for key" + s.Texture.ResourcePath);
            if (s.Texture.ResourcePath == pathKey || s.Texture.ResourcePath == pathGuiKey)
            {
                return true;
            }
        }
        return false;

    }
    public bool checkForMetal()
    {
        var s = GetNodeOrNull<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        if (s != null && s.Texture != null)
        {
            GD.Print("Check for metal" + s.Texture.ResourcePath +" pathMetal " +pathMetal +" pathGuiMetal " +pathGuiMetal);
            if (s.Texture.ResourcePath == pathMetal || s.Texture.ResourcePath == pathGuiMetal)
            {
                return true;
            }
        }
        return false;

    }
    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                GD.Print("Clicking on Area");
                if (checkForKey() && !shapedMarked)
                {
                    GardenBucketSand bucketSand = GetParent().GetParent<GardenBucketSand>();
                    int[] rotation = bucketSand.getRotation();
                    bool[] flip = bucketSand.getFlip();
                    if (!(rotation[0] == -90 && rotation[1] == 90 && rotation[2] == 180 && rotation[3] == 0 &&
                        flip[0] == false && flip[1] == true && flip[2] == false && flip[3] == true))
                    {
                        if (this.Name == "Up")
                        {
                            bucketSand.rotateUp();
                        }
                        if (this.Name == "Down")
                        {
                            bucketSand.rotateDown();
                        }
                        if (this.Name == "Right")
                        {
                            bucketSand.rotateRight();
                        }
                        if (this.Name == "Left")
                        {
                            bucketSand.rotateLeft();
                        }
                        rotation = bucketSand.getRotation();
                        flip = bucketSand.getFlip();
                        if (rotation[0] == -90 && rotation[1] == 90 && rotation[2] == 180 && rotation[3] == 0 &&
                       flip[0] == false && flip[1] == true && flip[2] == false && flip[3] == true)
                        {    
                            bucket.finishedPuzzle = true;
                            GD.Print("Rotation finished");
                            var inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
                            inventory.eraseItem();
                            WorldDictionary.setStateObject("KeyShape", 3);
                            GameSaver.SaveGameScene();
                        }
                    }
                }
                if (checkForMetal() && bucket.finishedPuzzle && !melted)
                {
                    GD.Print("Melted key shape false");
                    var inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");
                    inventory.eraseItem();
                    Key.Visible = true;
                    melted = true;
                    drawnedKey = true;
                    Down.Disabled = true;
                    Up.Disabled = true;
                    Left.Disabled = true;
                    Right.Disabled = true;

                }
            }
        }
    }
}
