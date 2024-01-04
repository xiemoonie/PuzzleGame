using Godot;
using System;


namespace LogicGame1.Scripts.Location
{
    public class GardenThreeLocationHolder : LocationHolder
    {
        private Sprite vision;
        Vector2 position;
        string leftPath;
        string rightPath;


        public override void _Ready()
        {
            base._Ready();
            vision = GetNodeOrNull<Sprite>("Vision");
            if (vision != null)
             {
                 position = vision.Position;
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
                { "Vision", vision},
                { "PosXvision", position.x},
                { "PosYvision", position.y },
                { "LeftPath", leftPath},
                { "RightPath", rightPath}

            };
        }
    }
}