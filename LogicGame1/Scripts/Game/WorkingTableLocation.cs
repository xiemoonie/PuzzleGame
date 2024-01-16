using Godot;
using System;

public class WorkingTableLocation : LocationHolder
{
    Sprite stand;
    Sprite sextantUnfinished;
    Sprite sextantFinished;
    Sprite sextant;
    public override void _Ready()
    {
        base._Ready();
        sextantUnfinished = GetNodeOrNull<Sprite>("SextantUnfinished");
        sextantFinished = GetNodeOrNull<Sprite>("SextantFinished");
        stand = GetNodeOrNull<Sprite>("Stand");
        sextant = GetNodeOrNull<Sprite>("SextantCompleted");

        GameLoader.LoadScene();

        int standValue = WorldDictionary.checkObjectStatuScene(stand.Name);
        int sextantUnfinishedValue = WorldDictionary.checkObjectStatuScene(sextantUnfinished.Name);
        int sextantFinishedValue = WorldDictionary.checkObjectStatuScene(sextantFinished.Name);
        if (standValue != 0)
        {
            GD.Print("the value of stand is:" +standValue);
            SceneManager(stand, standValue);
        }
        if (sextantUnfinishedValue != 0)
        {
            GD.Print("the value of sextantUnfinished" + sextantUnfinishedValue);
            SceneManager(sextantUnfinished, sextantUnfinishedValue);
        }
        if (sextantFinishedValue != 0)
        {
            GD.Print("sextant finished" + sextantFinishedValue);
            SceneManager(sextantFinished, sextantFinishedValue);
        }

    }
    public void SceneManager(Sprite sprite, int state)
    {
        switch (state)
        {
            case 2: sprite.Visible = true; break;
            case 1: sprite.Visible = false; break;
            case 3: sprite.QueueFree(); break;
            case 4: sprite.Visible = false; break;
        }
    }
}