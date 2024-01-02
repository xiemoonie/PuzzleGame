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
    private bool sextant;
    private bool locked;
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
        sextant = true;
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
       
        if (secretCompartiment != null)
        {
            secretCompartimentVisibility = secretCompartiment.Visible;
            if (secretCompartiment.GetChildCount() == 0)
            {
                sextant = false;
            }
        }
        var fungiScript = (AttachableObject)areaKnob;
        locked = fungiScript.unlockedSlide;

        positionWoodenPlank.x = woodenPlank.Position.x;
        positionWoodenPlank.y = woodenPlank.Position.y;

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
                { "Sextant", sextant},
                 {"Locked", locked},
                { "LeftPath", leftPath},
                { "RightPath", rightPath},
                { "BackPath", backPath}
            };
    }
}