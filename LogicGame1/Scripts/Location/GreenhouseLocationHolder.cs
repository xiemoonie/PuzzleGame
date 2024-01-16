using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace LogicGame1.Scripts.Location
{

    internal class GreenhouseLocationHolder : LocationHolder
    {

        private Sprite plate;
        bool visibilityPlate;

        public override void _Ready()
        {
            plate = GetNodeOrNull<Sprite>("Plate");
            GameLoader.LoadScene();
            int plateValue = WorldDictionary.checkObjectStatuScene(plate.Name);
            if (plateValue != 0)
            {
                SceneManager(plate, plateValue);
            }

        }
        public void SceneManager(Sprite sprite, int state)
        {
            switch (state)
            {
                case 1: sprite.QueueFree(); break;
                case 3: sprite.QueueFree(); break;
            }
        }
    }
}
