using Godot;
using System;

public class GardenSunLocation : LocationHolder
{
     Sprite SextantGardenBackground;
     Sprite Sun;
     bool sunVisibility;
     Sprite SextantView;
     Sprite SunCompleted;
     bool sunCompletedVisibility;
     Sprite Ruler;
     Label angles;
   
     Vector2 positionBackground;
     Vector2 positionSun;
     Vector2 positionSunCompleted;
     Sextant sext;
     string backPath;
     bool rulerBlocked = false;
     string degree;
     bool sextantVisibility;


    public override void _Ready()
    {
        base._Ready();
        SextantGardenBackground = GetNode<Sprite>("SextantGardenBackground");
        Sun = GetNode<Sprite>("Sun");
        SunCompleted = GetNode<Sprite>("SunCompleted");
        Ruler = GetNode<Sprite>("Ruler");
        angles = Ruler.GetNode<Label>("RichTextLabel");
        sext = GetNode<Sextant>("Right");
        SextantView = GetNode<Sprite>("SextantView");
        sextantVisibility = false;
        backPath = base.backLocationPath;
    }
    public void sextantToSave()
    {
        GD.Print("\n Sextant To Save");

        if (SunCompleted != null)
        {
            positionSunCompleted = SunCompleted.Position;
            sunCompletedVisibility = SunCompleted.Visible;
            if (SextantGardenBackground != null)
            {
                positionBackground = SextantGardenBackground.Position;
                sextantVisibility = true;

            }
        }  
        rulerBlocked = sext.locked;
        degree = sext.degrees.Text;
    }
    public override Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "PositionBackgroundX", positionBackground.x},
                { "PositionBackgroundY", positionBackground.y},
                { "SunVisibility",  sunVisibility},
                { "PositionSunCompletedX", positionSunCompleted.x},
                { "PositionSunCompletedY", positionSunCompleted.y},
                { "SunCompletedVisibility", sunCompletedVisibility},
                { "BackPath", backPath},
                { "Degree", degree},
                { "RulerBlocked", rulerBlocked},
                { "SextantVisibility", sextantVisibility}
            };
    }
}
