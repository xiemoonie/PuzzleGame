using Godot;
using System;

public class ClickOnGruta: Area2D
{
    Vector2 PositionMouse;
    Sprite PressedRock;
    private StreamTexture pressedRock;
    private int nodesGraph = 7;
    bool created = false;
    public override void _Ready()
    {
        pressedRock = ResourceLoader.Load<StreamTexture>("res://Images/Locations/MoonieDrawing/pressedGruta.png");
    }

    public void createPressedButtons()
    {
        Sprite _pressed;
        Vector2 InitialPositionPressed = new Vector2(450, 180);
        for (int i = 0; i < nodesGraph; i++)
        {
            InitialPositionPressed.y = 180;
            _pressed = new Sprite();
            _pressed.Scale = new Vector2(1f, 1f);
            _pressed.Texture = pressedRock;
         
            GetParent().GetParent().AddChild(_pressed);
            InitialPositionPressed += new Vector2(120, 0);
            _pressed.GlobalPosition = InitialPositionPressed;
            for (int j = 0; j < nodesGraph; j++)
            {
                _pressed = new Sprite();
                _pressed.Texture = pressedRock;
                GetParent().GetParent().AddChild(_pressed);
                InitialPositionPressed += new Vector2(0, 100);
                _pressed.GlobalPosition = InitialPositionPressed;
            }
        }
       

    }
    public void Pressed(Vector2 PositionMouse)
    {
        Sprite _pressed = new Sprite();
        _pressed.Texture = pressedRock;
        GetParent().GetParent().AddChild(_pressed);
        if (PositionMouse.x > 530 && PositionMouse.x < 630)
        {
            if (PositionMouse.y > 150 && PositionMouse.y < 200)
            {
                GD.Print("1 The position of the mouse is X:  " + PositionMouse.x + " Y :  " + PositionMouse.y);
                _pressed.Visible = true;
                _pressed.GlobalPosition = new Vector2(600, 180);
            }
        }
        if (PositionMouse.x > 630 && PositionMouse.x < 730)
        {
            GD.Print("2 The position of the mouse is X:  " + PositionMouse.x + " Y :  " + PositionMouse.y);
            if (PositionMouse.y > 200 && PositionMouse.y < 250)
            {
                _pressed.Visible = true;
                _pressed.GlobalPosition = new Vector2(700, 180);
            }
        }
    }

    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                PositionMouse = mouseEvent.GlobalPosition;
                //Pressed(PositionMouse);
                if (created == false)
                {
                    created = true;
                    createPressedButtons();
                }
            }

        }
    }
}
