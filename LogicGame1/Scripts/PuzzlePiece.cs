using Godot;
using System;

public class PuzzlePiece : Area2D
{
    private int x;
    int[,] puzzlePiece;
    private int y;
    private int key;
    private int counterKeys = 0;
    float timer = 0;
    bool used;
    public override void _PhysicsProcess(float delta)
    {
        if (timer > 0)
        {
            timer -= 2 * delta;
        }
        if (timer < 0)
        {
            unpressSprites();
            timer = 0;
        }
    }
    public void unpressSprites()
    {
        Godot.Collections.Array nodes = GetParent().GetParent().GetChildren();
        foreach (Node n in nodes)
        {
            if (n.GetType().ToString() == "Godot.Node2D")
            {
                Sprite s = n.GetNode<Sprite>("PuzzlePiece/PuzzlePiece");
                s.Visible = false;

            }

        }
    }

    public void setIDPuzzlePiece(int xIndex, int yIndex)
    {
        x = xIndex;
        y = yIndex;
        GD.Print("hey it is set x: " + x + " y: " + y + "  // " + GetParent().Name);
    }
    public void setKeyPuzzlePiece(int _key)
    {
        key = _key;
    }
    public int[] getIDPuzzlePiece()
    {
        return new int[] { this.x, this.y };
    }
    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
    {
        base._InputEvent(viewport, @event, shapeIdx);

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                Sprite sprite = GetNodeOrNull<Sprite>("PuzzlePiece");
                sprite.Visible = !sprite.Visible;
                var clickOnGruta = GetNode<PuzzleGruta>("/root/Main/Screen/GameWrapper/SceneContainer/GardenGruta/PuzzleGruta");

                if (key == 1)
                {
                    if (sprite.Visible)
                    {
                        clickOnGruta.pressedCounter++;
                    }
                    else
                    {
                        clickOnGruta.pressedCounter--;
                    }
                }
                else
                {
                    clickOnGruta.pressedCounter = 0;
                    timer = 2;
                }
            }

        }
    }
}
