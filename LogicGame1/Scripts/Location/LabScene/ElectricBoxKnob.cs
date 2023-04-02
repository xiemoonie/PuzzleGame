using Godot;
using System;

public class ElectricBoxKnob : Node2D {
    private ElectricBoxKnob knob1;
    private ElectricBoxKnob knob2;
    private ElectricBoxKnob knob3;
    private ElectricBoxKnob knob4;
    private string rotateMatrix;

    private Area2D clickArea;

    private float targetRotation = 0f;
    private float currentRotation = 0f;

    public void Setup(ElectricBoxKnob knob1, ElectricBoxKnob knob2, ElectricBoxKnob knob3, ElectricBoxKnob knob4, string rotateSetup) {
        this.knob1 = knob1;
        this.knob2 = knob2;
        this.knob3 = knob3;
        this.knob4 = knob4;
        this.rotateMatrix = rotateSetup;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        clickArea = GetNode<Area2D>("Area2D");
        clickArea.Connect("input_event", this, nameof(onInput));
    }

    public void onInput(Viewport node, InputEvent ev, int shapeIdx) {
        if (ev is InputEventMouseButton mouseEvent) {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {
                rotateAll();
            }
        }
    }

    private int getRotateForKnob(int rotIndex) {
        return rotateMatrix.Split(",")[rotIndex].Trim().ToInt();
    }

    public void rotateAll(Boolean quick = false) {
        knob1.rotate(getRotateForKnob(0), quick);
        knob2.rotate(getRotateForKnob(1), quick);
        knob3.rotate(getRotateForKnob(2), quick);
        knob4.rotate(getRotateForKnob(3), quick);
    }

    public void rotate(int rotateTimes = 1, Boolean quick = false) {
        var toAdd = 45f * rotateTimes;

        targetRotation += toAdd;
        
        if (quick) {
            currentRotation += toAdd;
        }
    }

    public override void _Process(float delta) {
        base._Process(delta);

        if (currentRotation < targetRotation) {
            currentRotation += Mathf.Max(Mathf.Min((targetRotation - currentRotation) / 10f, 50f),0.1f);

            if (currentRotation > targetRotation) currentRotation = targetRotation;
        }
        
        this.RotationDegrees = currentRotation;
    }
}