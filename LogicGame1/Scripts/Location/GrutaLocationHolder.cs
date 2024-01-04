using Godot;
using System;


    public class GrutaLocationHolder : LocationHolder
    {
        Sprite candle;
        string backPath;
        bool candleVisible = false;
        public override void _Ready()
        {
        base._Ready();
        candle = GetNode<Sprite>("Candle");
    }

        public void GrutaToSave()
        {
        backPath = base.backLocationPath;
        candle = GetNodeOrNull<Sprite>("Candle");
    }

        public override Godot.Collections.Dictionary<string, object> Save()
        {
            return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "BackPath", backPath},
                { "Candle", candle}

            };
        }
    }

