using Godot;
using System;

public class GreenHousePotLocationHolder : LocationHolder
{

    private Sprite knob;
    private Sprite woodenPlank;
    Area2D areaKnob;
    bool knobVisibility;
    Vector2 positionAreaKnob;
    Vector2 positionWoodenPlank;
    private Sprite secretCompartiment;
    bool secretCompartimentVisibility;
    string leftPath;
    string rightPath;
    string backPath;

    public override void _Ready()
    {
        base._Ready();
        areaKnob = GetNode<Area2D>("Area2D");
        woodenPlank = GetNode<Sprite>("WoodenPlank");
        secretCompartiment = GetNode<Sprite>("SecretCompartiment");
        
        leftPath = base.leftLocationPath;
        rightPath = base.rightLocationPath;
        backPath = base.backLocationPath;
    }

    public void saveGreenHouseThree()
    {

        if (areaKnob != null)
        {
            knob = areaKnob.GetNode<Sprite>("Knob");
            knobVisibility = knob.Visible;
            positionAreaKnob = areaKnob.Position;
        }
        if (woodenPlank != null)
        {
            positionWoodenPlank = woodenPlank.Position;
        }
        if (secretCompartiment != null)
        {
            secretCompartimentVisibility = secretCompartiment.Visible;
        }
    }
    public override Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "KnobVisibility", knobVisibility},
                { "AreaKnobPosX",positionAreaKnob.x},
                { "AreaKnobPosY", positionAreaKnob.y},
                { "WoodenPlankPosX", positionWoodenPlank.x},
                { "WoodenPlankPosY", positionWoodenPlank.y},
                { "SecretCompartimentVisibility", secretCompartimentVisibility},
                { "LeftPath", leftPath},
                { "RightPath", rightPath},
                { "BackPath", backPath}

            };
    }
}