using Godot;
using System;

public class ElectricBoxKnobNonWorking : Node2D {
    private Area2D clickArea;

    private const float ADD_ROTATION_AMOUNT = 45f;
    
    private float targetRotation = 0f;

    private float addRotation = 0f;
    private bool shouldIncrease = false;

    public void Setup(int rotation) {
        this.targetRotation = rotation * 45f;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        clickArea = GetNode<Area2D>("Area2D");
        clickArea.Connect("input_event", this, nameof(onInput));
    }

    public void onInput(Viewport node, InputEvent ev, int shapeIdx) {
        if (ev is InputEventMouseButton mouseEvent) {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {
                shouldIncrease = true;
            }
        }
    }

    public override void _Process(float delta) {
        base._Process(delta);

        if (shouldIncrease) {
            addRotation += delta * 10f;
            if (addRotation >= 1f) {
                shouldIncrease = false;
            }
        } else if(!shouldIncrease && addRotation > 0) {
            addRotation -= delta * 3f;
        }
        addRotation = Mathf.Clamp(addRotation, 0f, 1f);
        
        
        this.RotationDegrees = targetRotation + addRotation * ADD_ROTATION_AMOUNT;
    }
}