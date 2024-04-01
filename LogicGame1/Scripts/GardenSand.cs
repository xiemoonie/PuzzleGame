using Godot;
using System;

public class GardenSand : LocationHolder
{
    Area2D candle;
    public override void _Ready()
    {
        candle = GetNode<Area2D>("BucketSand");
        GameLoader.LoadScene();
        int candleValue = WorldDictionary.checkObjectStatuScene(candle.Name);
        if (candleValue != 0)
        {
            SceneManager(candle, candleValue);
        }
    }
    public void SceneManager(Area2D sprite, int state)
    {
        switch (state)
        {
            case 1: sprite.QueueFree(); break;
            case 2: sprite.Visible = true; break;
            case 3: sprite.QueueFree(); break;
        }
    }
}