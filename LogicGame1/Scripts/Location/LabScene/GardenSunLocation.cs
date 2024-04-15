using Godot;
using System;

public class GardenSunLocation : LocationHolder
{
     Sprite sextantGardenBackground;
     Sprite Sun;
     Sprite SextantView;
     Sprite SunCompleted;
     Sprite Ruler;
     Label angles;
    Sextant sextant;
   
     Vector2 positionBackground;
     Vector2 positionSun;
    public override void _Ready()
    {
        base._Ready();

        sextantGardenBackground = GetNodeOrNull<Sprite>("SextantGardenBackground");
        Sun = GetNodeOrNull<Sprite>("Sun");
        SunCompleted = GetNodeOrNull<Sprite>("SunCompleted");
        Ruler = GetNodeOrNull<Sprite>("Ruler");
        angles = Ruler.GetNodeOrNull<Label>("RichTextLabel");
        SextantView = GetNodeOrNull<Sprite>("SextantView");
        sextant= GetNodeOrNull<Sextant>("Right");
        positionBackground.x = 2475;
        positionBackground.y = 0;
        positionSun.x = 960;
        positionSun.y = 300;

        int backgroundValue = WorldDictionary.checkObjectStatuScene(sextantGardenBackground.Name);
        if (backgroundValue != 0)
        {
            GD.Print("background value:" + backgroundValue);
            if (backgroundValue == 3)
                sextantGardenBackground.Position = positionBackground;
            SunCompleted.Visible = true;
            SunCompleted.Position = positionSun;
            Sun.Visible = false;
            SextantView.Visible = true;
            angles.Text = 70.ToString();
            sextant.locked = true;
            Ruler.Visible = true;

        }
    }
}


