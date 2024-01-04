using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace LogicGame1.Scripts.Location
{
    public class GardenLocationHolder : LocationHolder
    {
        private Sprite coin;
        private Sprite fungi;
        Vector2 positionCoin;
        Vector2 positionFungi;
        string leftPath;
        string rightPath;

        public override void _Ready()
        {
            base._Ready();
           coin = GetNode<Sprite>("Coin");
           fungi = GetNode<Sprite>("Fungi");
            if (coin != null)
            {
                positionCoin = coin.Position;
            }
            if (fungi != null)
            {
                positionFungi = fungi.Position;
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
                { "Fungi", fungi},
                { "PosXcoin", positionCoin.x},
                { "PosYcoin", positionCoin.y},
                { "PosXfungi", positionFungi.x},
                { "PosYfungi", positionFungi.y},
                { "LeftPath", leftPath},
                { "RightPath", rightPath}

            };
        }
    }
}
