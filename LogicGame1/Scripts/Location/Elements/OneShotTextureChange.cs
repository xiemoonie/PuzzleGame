using Godot;
using System;
using Object = Godot.Object;

public class OneShotTextureChange : Area2D {
    [Export] private Texture newTexture;

    public override void _Ready() {
        base._Ready();
      
    }

    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx) {
        base._InputEvent(viewport, @event, shapeIdx);
        if (@event is InputEventMouseButton mouseEvent) {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left) {
                this.GetParent<Sprite>().Texture = newTexture;
                this.Visible = false;
            }
        }
    }
}
