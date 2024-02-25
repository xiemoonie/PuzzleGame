using Godot;
using System;


    public class GrutaScene : LocationHolder
    {
    Sprite candle;
    Sprite puzzleGruta;
    Sprite backgroundOpen;
    Sprite background;
    Sprite metal;
    Sprite meltingPot;
  
    bool candleVisible;

    public override void _Ready()
    {
        candle = GetNode<Sprite>("Candle");
        puzzleGruta = GetNode<Sprite>("PuzzleGruta");
        backgroundOpen= GetNode<Sprite>("BackgroundOpen");
        background= GetNode<Sprite>("Background");
        metal= GetNode<Sprite>("BackgroundOpen/Metal");
        meltingPot= GetNode<Sprite>("BackgroundOpen/MeltingPot");
        GameLoader.LoadScene();
        int candleValue = WorldDictionary.checkObjectStatuScene(candle.Name);
        int puzzleValue = WorldDictionary.checkObjectStatuScene(puzzleGruta.Name);
        int backgroundOpenValue = WorldDictionary.checkObjectStatuScene(backgroundOpen.Name);
        int metalValue = WorldDictionary.checkObjectStatuScene(metal.Name);
        int meltingPotValue = WorldDictionary.checkObjectStatuScene(meltingPot.Name);
        if (candleValue != 0)
        {
            SceneManager(candle, candleValue);
        }
        if (puzzleValue != 0)
        {
            SceneManager(puzzleGruta, puzzleValue);
        }
        if (backgroundOpenValue != 0)
        {
            SceneManager(backgroundOpen, backgroundOpenValue);
        }
        if (metalValue != 0)
        {
            SceneManager(metal, metalValue);
        }
        if (meltingPotValue != 0)
        {
            SceneManager(meltingPot, meltingPotValue);
        }

    }

    public void OpenGruta()
    {
        background.Visible = false;
        backgroundOpen.Visible = true;
        WorldDictionary.setStateObject("BackgroundOpen", 3);
    }

    public void SceneManager(Sprite sprite, int state)
    {
        switch (state)
        {
            case 1: sprite.QueueFree(); break;
            case 2: puzzleGruta.Visible = true; break;
            case 3: 
            background.Visible = false;
            backgroundOpen.Visible = true;
            break;
        }
    }
}
