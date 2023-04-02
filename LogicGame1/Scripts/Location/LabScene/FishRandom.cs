using Godot;
using System;

public class FishRandom : Sprite {
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private Vector2 targetPos;
    private float newPosCountdown = 2f;
    public override void _Ready() {
        targetPos = GlobalPosition;
    }

    public override void _Process(float delta) {
        base._Process(delta);
        newPosCountdown -= delta;
        if (newPosCountdown <= 0) {
            newPosCountdown = (float)GD.RandRange(1f,2f);
            
            moveToNewLocation();
        }

        var direction = GlobalPosition.DirectionTo(targetPos);
        var distance = GlobalPosition.DistanceTo(targetPos);
        GlobalPosition = GlobalPosition.MoveToward(targetPos, distance / 32f);


        this.FlipH = GlobalPosition.x < targetPos.x;
    }


    public void moveToNewLocation() {
        var area = GetParent<Area2D>();
        var shape = area.GetNode<CollisionShape2D>("Shape2D");
        var rectShape = shape.Shape as RectangleShape2D;
        
        
        targetPos = new Vector2(
             (float)GD.RandRange(shape.GlobalPosition.x - rectShape.Extents.x, shape.GlobalPosition.x + rectShape.Extents.x),
             (float)GD.RandRange(shape.GlobalPosition.y - rectShape.Extents.y, shape.GlobalPosition.y + rectShape.Extents.y));
        
         
    }
    
}
