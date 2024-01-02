using Godot;
using System;

public class ClickMoveObject : Sprite
{
    private bool shouldShift = false;
    [Export] private float shift = 0f;
    private Vector2 originPos;

    public override void _Ready()
    {
        originPos = this.GlobalPosition;
    }
    public override void _Process(float delta)
    {
        base._Process(delta);
       

        if (this.Visible == true && this.GlobalPosition.y <= 750)
        {
            originPos.y += shift;
            this.GlobalPosition = originPos;
            GD.Print("clicked on item");
        }
      

    }
}
