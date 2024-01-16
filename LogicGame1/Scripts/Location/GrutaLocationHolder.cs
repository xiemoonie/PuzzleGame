using Godot;
using System;


    public class GrutaLocationHolder : LocationHolder
    {
        Sprite candle;
        Sprite plate;
    public override void _Ready()
    {
        base._Ready();
        candle = GetNodeOrNull<Sprite>("Candle");
        GameLoader.LoadScene();
        int candleValue = WorldDictionary.checkObjectStatuScene(candle.Name);

        if (candleValue != 0)
        {
            SceneManager(candle, candleValue);
        }
    }

      
    public void SceneManager(Sprite sprite, int state)
    {
        switch (state)
        {
            case 1: candle.Visible = false; break;
        }
    }
}

       
    

