using Godot;
using System;

namespace LogicGame1.Scripts.Location
{
    public class GrutaScene : LocationHolder
    {
        Sprite candle;
        Sprite puzzleGruta;
        bool candleVisible;
       
        public override void _Ready()
        {
            GD.Print("Ready functrion*******************************************");
            candle = GetNode<Sprite>("Candle/Candle");
            puzzleGruta = GetNode<Sprite>("PuzzleGruta");
            GameLoader.LoadScene();
            int candleValue = WorldDictionary.checkObjectStatuScene(candle.Name);
            int puzzleValue = WorldDictionary.checkObjectStatuScene(puzzleGruta.Name);
            if (candleValue != 0)
            {
                SceneManager(candle, candleValue);
            }
            if (puzzleValue != 0)
            {
                GD.Print("VALUEN OF PUZZle");
                SceneManager(puzzleGruta, puzzleValue);
            }

        }

        public void SceneManager(Sprite sprite, int state)
        {
            switch (state)
            {
                case 1: sprite.QueueFree(); break;
                case 2: puzzleGruta.Visible = true; break;
                case 3: sprite.QueueFree(); break;
            }
        }
    }
}
