using Godot;
using System;

namespace LogicGame1.Scripts.Location
{
    public class GreenHouseDepositLocationHolder : LocationHolder
    {
        private Sprite screwdriver;
        private Sprite sack;
        Vector2 positionSack;
        string leftPath;
        string rightPath;
        string backPath;

        public override void _Ready()
        {
            base._Ready();
            screwdriver = GetNode<Sprite>("Screwdriver/Screwdriver");
            sack = GetNode<Sprite>("Sack/Sack");
            if (sack != null)
            {
                positionSack = sack.Position;
            }
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
                { "Screwdriver",screwdriver},
                { "Sack", sack},
                { "PosXSack", positionSack.x},
                { "PosYSack", positionSack.y},
                { "LeftPath", leftPath},
                { "RightPath", rightPath},
                { "BackPath", backPath }
            };
        }
    }
}