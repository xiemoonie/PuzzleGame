using Godot;
using System;

public class WorkingTableLocation : LocationHolder
{
    Sprite standClassic;
    bool standClassisVisibility;
    Sprite sextandUnfinished;
    bool sextantUnfinishedVisibility;
    Sprite finishedSextant;
    bool finishedSextantVisibility;
    Sprite sextant;
    bool sextantVisibility;
    string backPath;
    public override void _Ready()
    {
        backPath = base.backLocationPath;
        standClassic = GetNode<Sprite>("Stand");
        sextandUnfinished = GetNode<Sprite>("SectantUnfinished");
        finishedSextant = GetNode<Sprite>("FinishedSextant");
        sextant = GetNode<Sprite>("CompletedSextant");
    }

    public void WorkingTableToSave()
    {
        GD.Print("\n Working Table To Save");
       
        if (standClassic != null)
        {
            standClassisVisibility = standClassic.Visible;
        }
        if (sextandUnfinished != null)
        {
            sextantUnfinishedVisibility = sextandUnfinished.Visible;
        }
        if (finishedSextant != null)
        {
            finishedSextantVisibility = finishedSextant.Visible;
        }
        if (sextant != null)
        {
            sextantVisibility = sextant.Visible;
        }

    }

    public override Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "StandClassisVisibility", standClassisVisibility},
                { "SextantUnfinished", sextantUnfinishedVisibility},
                { "finishedSextantVisibility", finishedSextantVisibility},
                { "SextandVisibility", sextantVisibility},
                { "BackPath", backPath}

            };
    }
}
