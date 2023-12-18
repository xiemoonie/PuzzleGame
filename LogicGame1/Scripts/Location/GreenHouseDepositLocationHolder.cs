using Godot;
using System;



namespace LogicGame1.Scripts.Location
{
    public class GreenHouseDepositLocationHolder : LocationHolder
    {
        private Sprite coin;
        Vector2 position;
        string leftPath;
        string rightPath;

        public override void _Ready()
        {
            base._Ready();
            /* coin = GetNode<Sprite>("Coin/Coin");
             if (coin != null)
             {
                 Vector2 position = coin.GetPosition();
             }*/
            leftPath = base.leftLocationPath;
            rightPath = base.rightLocationPath;
        }

        public override Godot.Collections.Dictionary<string, object> Save()
        {
            return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
               // { "GardenThing", coin},
                { "PosX", position.x + 20.0f},
                { "PosY", position.y - 20.0f},
                { "LeftPath", leftPath},
                { "RightPath", rightPath}

            };
        }
    }
}