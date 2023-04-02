using Godot;
using System;
using Object = Godot.Object;

public class ClickMoveAndBackElement : Area2D
{
 
    [Export] private Vector2 shiftPos = new Vector2(0,0);
    [Export] private NodePath nodeToEnable;
    private Vector2 originPos;
    private bool shouldShift = false;
    private float shift = 0f;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        originPos = GetParent<Node2D>().GlobalPosition;
    }

    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {
                shouldShift = !shouldShift;
            }
        }
    }

    public override void _Process(float delta) {
        base._Process(delta);
        if (shouldShift && shift < 1) {
            shift += delta * 6;
        } else if (!shouldShift && shift > 0) {
            shift -= delta * 6;
        }

        shift = Mathf.Clamp(shift, 0, 1);
        if (HasNode(nodeToEnable)) {
            GetNode<Node2D>(nodeToEnable).Visible = shift > 0.9f;
        }

        GetParent<Node2D>().GlobalPosition = originPos + shiftPos * shift;
    }
}
