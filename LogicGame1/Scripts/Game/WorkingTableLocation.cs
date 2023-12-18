using Godot;
using System;

public class WorkingTableLocation : LocationHolder
{
    Sprite standClassic;
    bool standClassisVisibility;
    Sprite sextandStand;
    bool sextantStandVisibility;
    Sprite sextandClomplited;
    bool sextandClomplitedVisibility;
    Sprite sextand;
    bool sextandVisibility;
    string backPath;
    public override void _Ready()
    {
        backPath = base.backLocationPath;
        standClassic = GetNode<Sprite>("Stand");
        sextandStand = GetNode<Sprite>("SectantUnfinished");
        sextandClomplited = GetNode<Sprite>("FinishedSextant");
        sextand = GetNode<Sprite>("CompletedSextant");
    }

    public void WorkingTableToSave()
    {
        GD.Print("\n Working Table To Save");
       
        if (standClassic != null)
        {
            standClassisVisibility = standClassic.Visible;
        }
        if (sextandStand != null)
        {
            sextantStandVisibility = sextandStand.Visible;
        }
        if (sextandClomplited != null)
        {
            sextandClomplitedVisibility = sextandClomplited.Visible;
        }
        if (sextand != null)
        {
            sextandVisibility = sextand.Visible;
        }

    }

    public override Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "StandClassisVisibility", standClassisVisibility},
                { "SextantStandVisibility", sextantStandVisibility},
                { "SextandClomplitedVisibility", sextandClomplitedVisibility},
                { "SextandVisibility", sextandVisibility},
                { "BackPath", backPath}

            };
    }
}
