using Godot;
using System;


namespace LogicGame1.Scripts.Location
{
    public class GardenTwoLocationHolder : LocationHolder
    {
        private Sprite coin;
        Vector2 positionCoin;
        string leftPath;
        string rightPath;

        public override void _Ready()
        {
            base._Ready();
            coin = GetNode<Sprite>("Coin");
            if (coin != null)
            {
                positionCoin = coin.Position;
            }
            leftPath = base.leftLocationPath;
            rightPath = base.rightLocationPath;

        }

        public override Godot.Collections.Dictionary<string, object> Save()
        {
            return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "Coin", coin},
                { "PosXcoin", positionCoin.x},
                { "PosYcoin", positionCoin.y},
                { "LeftPath", leftPath},
                { "RightPath", rightPath}


            };
        }
    }
}
