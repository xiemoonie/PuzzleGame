using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace LogicGame1.Scripts.Location
{

    internal class GreenhouseLocationHolder : LocationHolder
    {

        private Sprite coin;
        Vector2 position;
        string leftPath;
        string rightPath;
        string backPath;

        public override void _Ready()
        {
            base._Ready();
            /*  coin = GetNode<Sprite>("Coin/Coin");
              if (coin != null)
              {
                  Vector2 position = coin.GetPosition();
              }*/
            leftPath = base.leftLocationPath;
            rightPath = base.rightLocationPath;
            backPath = base.backLocationPath;
        }
        public override Godot.Collections.Dictionary<string, object> Save()
        {
            return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "PosX", position.x + 20.0f},
                { "PosY", position.y - 20.0f},
                { "LeftPath", leftPath},
                { "RightPath", rightPath},
                { "BackPath", backPath}

            };
        }
    }
}
