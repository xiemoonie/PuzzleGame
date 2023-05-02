using Godot;
using LogicGame1.Scripts.Location;
using System;
using Object = Godot.Object;

public class ClickOnDiaryPage : Area2D
{
    
    public override void _Ready()
    {

    }

    public override void _InputEvent(Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                DiaryLocationHolder book = this.GetParent<DiaryLocationHolder>();
                if (this.Name == "Right")
                {
                    book.goRight();
                }
                else if(this.Name == "Left")
                {
                    book.goLeft();
                }
            }
        }
    }
}
