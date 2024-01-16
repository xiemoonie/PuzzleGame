using Godot;
using System;

namespace LogicGame1.Scripts.Location
{
    public class GrutaScene : LocationHolder
    {
        Sprite candle;
        bool candleVisible;
       
        public override void _Ready()
        {
            candle = GetNode<Sprite>("Candle/Candle");
        }

        public void GrutaToSave()
        {
            if (candle != null)
            {
               candleVisible = true;
            }
        }
    }
}
