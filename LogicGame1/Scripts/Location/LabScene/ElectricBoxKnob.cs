using Godot;
using System;

public class ElectricBoxKnob : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private Area2D clickArea;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        clickArea = GetNode<Area2D>("Area2D");
        clickArea.Connect("input_event", this, nameof(onInput));
    }
    
    public void onInput(Viewport Node, InputEvent ev, int shapeIdx) {
        if (ev is InputEventMouseButton mouseEvent) {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {
                rotate();
            }
        }
    }

    public void rotate() {
        this.RotationDegrees += 45;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
