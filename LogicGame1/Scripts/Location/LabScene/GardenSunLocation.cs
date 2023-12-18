using Godot;
using System;

public class GardenSunLocation : LocationHolder
{
     Sprite SextantGardenBackground;
     Sprite Sun;
     bool sunVisibility;
     Sprite SextantView;
     bool sextantViewVisibility;
     Sprite SunCompleted;
     bool sunCompletedVisibility;
     Sprite Ruler;
     Label angles;
     string anglesRuler;
   
     Vector2 positionBackground;
     Vector2 positionSun;
     Vector2 positionSunCompleted;
     Sextant sext;
     string backPath;

    public override void _Ready()
    {
        base._Ready();
        SextantGardenBackground = GetNode<Sprite>("SextantGardenBackground");
        Sun = GetNode<Sprite>("Sun");
        SunCompleted = GetNode<Sprite>("SunCompleted");
        SextantView = GetNode<Sprite>("SextantView");
        Ruler = GetNode<Sprite>("Ruler");
        angles = Ruler.GetNode<Label>("RichTextLabel");
        sext = GetNode<Sextant>("Right");

        backPath = base.backLocationPath;
    }
    public void sextantToSave()
    {
        GD.Print("\n Sextant To Save");
      
            if (SextantGardenBackground
            
            != null)
            {
                positionBackground = SextantGardenBackground.GetPosition();
            }
            if (Sun != null)
            {
                positionSun = Sun.GetPosition();
                sunVisibility = Sun.IsVisible();
            }
            if (SunCompleted != null)
            {
                positionSunCompleted = SunCompleted.GetPosition();
                sunCompletedVisibility = SunCompleted.IsVisible();
            }
            if (SextantView != null)
            {
                GD.Print("\n Not null and inside sextant Visibility");
                sextantViewVisibility = SextantView.IsVisible();
            }
            if (Ruler != null)
            {
                anglesRuler = angles.Text.ToString();
            }
       
       
    }
    public override Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "PositionBackgroundX", positionBackground.x},
                { "PositionBackgroundY", positionBackground.y},
                { "PositionSunX",  positionSun.x},
                { "PositionSunY",  positionSun.y},
                { "SunVisibility",  sunVisibility},
                { "PositionSunCompletedX", positionSunCompleted.x},
                { "PositionSunCompletedY", positionSunCompleted.y},
                { "SunCompletedVisibility", sunCompletedVisibility},
                { "SextantViewVisibility",  sextantViewVisibility},
                { "AnglesRuler",  anglesRuler}, 
                { "BackPath", backPath}

            };
    }
}
