using Godot;
using System;
using Object = Godot.Object;

public class AquariumGlass : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx) {
        base._InputEvent(viewport, @event, shapeIdx);
        
        if (@event is InputEventMouseButton mouseEvent) {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {
                foreach (var child in this.GetChildren()) {
                    if (child is FishRandom fishRandom) {
                        fishRandom.moveToNewLocation();
                    }
                }
            }
        }
    }
}
