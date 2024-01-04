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

        private Sprite plateSprite;
        bool visibilityPlate;
        string leftPath;
        string rightPath;
        string backPath;


        public override void _Ready()
        {
            base._Ready();
            visibilityPlate = true;
            leftPath = base.leftLocationPath;
            rightPath = base.rightLocationPath;
            backPath = base.backLocationPath;
        }

        public void GreenHouseToSave()
        {
            plateSprite = GetNodeOrNull<Sprite>("Plate");
            if (plateSprite == null)
                {
                    visibilityPlate = false;
                }   
        }
        public override Godot.Collections.Dictionary<string, object> Save()
        {
            return new Godot.Collections.Dictionary<string, object>()
            {
                { "Filename", this.Filename},
                { "Parent", GetParent().GetParent()},
                { "VisibilityPlate", visibilityPlate},
                { "LeftPath", leftPath},
                { "RightPath", rightPath},
                { "BackPath", backPath}

            };
        }
    }
}
