using Godot;
using System;
using Object = Godot.Object;

public class DraggableObject : Area2D {
    private bool isDraged = false;
    private Vector2 mouseStartPos;
    private Vector2 objectStartPos;
    
    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx) {
        base._InputEvent(viewport, @event, shapeIdx);
        if (@event is InputEventMouseButton mouseEvent) {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {
                if (!isDraged) {
                    isDraged = true;
                    mouseStartPos = mouseEvent.Position;
                    objectStartPos = GetParent<Node2D>().GlobalPosition;
                }
            }
            
           
        }

        if (@event is InputEventMouseMotion mouseMotion) {
            if (isDraged) {
                GetParent<Node2D>().GlobalPosition = mouseMotion.Position - mouseStartPos + objectStartPos;
            }
        } 
    }

    // This _Input is here to be called everytime (so just in case mouse would be outside object to always stop dragging)
    // But _InputEvent will be valid only if mouse is above object (hopefully :D)
    public override void _Input(InputEvent @event) {
        base._Input(@event);
        if (@event is InputEventMouseButton mouseEvent) {
            if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left && isDraged) {
                isDraged = false;
            }
        }
    }
}
