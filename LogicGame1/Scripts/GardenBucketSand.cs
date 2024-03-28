using Godot;
using System;

public class GardenBucketSand : LocationHolder
{
    Sprite Up;
    Sprite Down;
    Sprite Left;
    Sprite Right;

    bool MirrorUp = false;
    bool MirrorDown = false;
    bool MirrorLeft = false;
    bool MirrorRight = false;

    public override void _Ready()
    {
        Up = GetNode<Sprite>("BackgroundOne/Up/Up"); 
        Down = GetNode<Sprite>("BackgroundOne/Down/Down");
        Left = GetNode<Sprite>("BackgroundOne/Left/Left");
        Right = GetNode<Sprite>("BackgroundOne/Right/Right");

        Up.RotationDegrees = -90;
        Down.RotationDegrees = -90;
        Left.RotationDegrees = -90;
        Right.RotationDegrees = -90;

    }
    public void rotateUp()
    {
        if (!Up.Visible)
        {
            Up.Visible = true;   
        }
        else
        {
            int rotation = (int)Up.RotationDegrees;
            if (MirrorUp)
            {
                Up.FlipV = Up.FlipV;
                GD.Print("Rotating Up MirrorUp" + rotation);
                if (Up.RotationDegrees == 0)
                {
                    MirrorUp = false;
                }
                rotate(rotation, Up);
            }
            else
            {
                Up.FlipV = !Up.FlipV;
                MirrorUp = true; 
            }
        }
    }
    public void rotateDown()
    {
        if (!Down.Visible)
        {
            Down.Visible = true;
        }
        else
        {
            int rotation = (int)Down.RotationDegrees;
            if (MirrorDown)
            {
                Down.FlipV = Down.FlipV;
                GD.Print("Rotating Up MirrorUp" + rotation);
                if (Down.RotationDegrees == 0)
                {
                    MirrorDown = false;
                }
                rotate(rotation, Down);
            }
            else
            {
                Down.FlipV = !Down.FlipV;
                MirrorDown = true;
            }
        }
    }
    public void rotateLeft()
    {
        if (!Left.Visible)
        {
            Left.Visible = true;
        }
        else
        {
            int rotation = (int)Left.RotationDegrees;
            if (MirrorLeft)
            {
                Left.FlipV = Left.FlipV;
                GD.Print("Rotating Up MirrorUp" + rotation);
                if (Left.RotationDegrees == 0)
                {
                    MirrorLeft = false;
                }
                rotate(rotation, Left);
            }
            else
            {
                Left.FlipV = !Left.FlipV;
                MirrorLeft = true;
            }
        }
    }
    public void rotateRight()
    {
        if (!Right.Visible)
        {
            Right.Visible = true;
        }
        else
        {
            int rotation = (int)Right.RotationDegrees;
            if (MirrorRight)
            {
                Right.FlipV = Right.FlipV;
                GD.Print("Rotating Up MirrorUp" + rotation);
                if (Right.RotationDegrees == 0)
                {
                    MirrorRight = false;
                }
                rotate(rotation, Right);
            }
            else
            {
                Right.FlipV = !Right.FlipV;
                MirrorRight = true;
            }
        }
    }
    public int[] getRotation()
    {
        int[] rotation = new int[4];
        rotation[0] = (int)Up.RotationDegrees;
        rotation[1] = (int)Down.RotationDegrees;
        rotation[2] = (int)Left.RotationDegrees;
        rotation[3] = (int)Right.RotationDegrees;
        return rotation;
    }
    public bool[] getFlip()
    {
        bool[] flip = new bool[4];
        flip[0] = Up.FlipV;
        flip[1] = Down.FlipV;
        flip[2] = Left.FlipV;
        flip[3] = Right.FlipV;
        return flip;
    }

    public void rotate(int rotation, Sprite shape)
    {
        switch (rotation)
        {
            case 90: shape.RotationDegrees = 0; break;
            case 0: shape.RotationDegrees = -90; break;
            case -90: shape.RotationDegrees = 180; break;
            case 180: shape.RotationDegrees = 90; break;
        }
    }
}
