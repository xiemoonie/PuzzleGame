using Godot;
using System;
using System.Runtime.InteropServices;

namespace LogicGame1.Scripts.Location
{
    public class GreenHouseDepositLocationHolder : LocationHolder
    {
        private Sprite screwdriver;

        public override void _Ready()
        {
            screwdriver = GetNodeOrNull<Sprite>("Screwdriver");
            GameLoader.LoadScene();
            int screwdriverValue = WorldDictionary.checkObjectStatuScene(screwdriver.Name);
            if (screwdriverValue != 0)
            {
                SceneManager(screwdriver, screwdriverValue);
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