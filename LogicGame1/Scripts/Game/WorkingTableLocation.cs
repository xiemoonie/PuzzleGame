using Godot;
using System;

public class WorkingTableLocation : LocationHolder
{
    Sprite stand;
    Sprite sextantUnfinished;
    Sprite sextantFinished;
    Sprite sextant;
    Sprite candle;
    Sprite plate;
    Sprite flame;
    Sprite meltingPotCompleted;
    Sprite meltingPotMelted;
    Sprite meltingPotEmpty;
    Sprite spoonMelted;


    public override void _Ready()
    {
        base._Ready();
        sextantUnfinished = GetNodeOrNull<Sprite>("SextantUnfinished");
        sextantFinished = GetNodeOrNull<Sprite>("SextantFinished");
        stand = GetNodeOrNull<Sprite>("Stand");
        sextant = GetNodeOrNull<Sprite>("SextantCompleted");

        plate = GetNodeOrNull<Sprite>("Plate");
        candle = GetNodeOrNull<Sprite>("Candle");
        flame = GetNodeOrNull<Sprite>("Flame");

        meltingPotCompleted = GetNodeOrNull<Sprite>("MeltingPotCompleted");
        meltingPotMelted = GetNodeOrNull<Sprite>("MeltingPotMelted");
        meltingPotEmpty = GetNodeOrNull<Sprite>("MeltingPotEmpty");
        spoonMelted = GetNodeOrNull<Sprite>("SpoonMelted");

        GameLoader.LoadScene();

        int standValue = WorldDictionary.checkObjectStatuScene(stand.Name);
        int sextantUnfinishedValue = WorldDictionary.checkObjectStatuScene(sextantUnfinished.Name);
        int sextantFinishedValue = WorldDictionary.checkObjectStatuScene(sextantFinished.Name);

        int plateValue = WorldDictionary.checkObjectStatuScene(plate.Name);
        int candleValue = WorldDictionary.checkObjectStatuScene(candle.Name);
        int flameValue = WorldDictionary.checkObjectStatuScene(flame.Name);

        int meltingPotCompletedValue = WorldDictionary.checkObjectStatuScene(meltingPotCompleted.Name);
        int meltingPotMeltedValue = WorldDictionary.checkObjectStatuScene(meltingPotMelted.Name);
        int meltingPotEmptyValue = WorldDictionary.checkObjectStatuScene(meltingPotEmpty.Name);
        int spoonMeltedValue = WorldDictionary.checkObjectStatuScene(spoonMelted.Name);

        if (meltingPotEmptyValue != 0)
        {
            GD.Print("melting pot empty value" + meltingPotEmptyValue);
            SceneManager(meltingPotEmpty, meltingPotEmptyValue);
        }
        if (spoonMeltedValue != 0)
        {
            GD.Print("spoon melted value" + spoonMeltedValue);
            SceneManager(spoonMelted, spoonMeltedValue);
        }
        if (meltingPotMeltedValue!= 0)
        {
            GD.Print("melting pot melted value" + meltingPotMeltedValue);
            SceneManager(meltingPotMelted, meltingPotMeltedValue);
        }
        if (meltingPotCompletedValue!= 0)
        {
            GD.Print("melting pot completed value" + meltingPotCompletedValue);
            SceneManager(meltingPotCompleted, meltingPotCompletedValue);
        }
        if (standValue != 0)
        {
            GD.Print("stand value" +standValue);
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
        if (plateValue != 0)
        {
            GD.Print("plate value" +plateValue);
            SceneManager(plate, plateValue);
        }
        if (candleValue != 0)
        {
            GD.Print("candle value" + candleValue);
            SceneManager(candle, candleValue);
        }
        if (flameValue != 0)
        {
            GD.Print("flame value" + flameValue);
            SceneManager(flame, flameValue);
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