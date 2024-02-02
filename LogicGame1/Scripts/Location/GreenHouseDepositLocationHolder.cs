using Godot;
using System;
using System.Runtime.InteropServices;

namespace LogicGame1.Scripts.Location
{
    public class GreenHouseDepositLocationHolder : LocationHolder
    {
        private Sprite screwdriver;
        private Sprite battery;

        public override void _Ready()
        {
            screwdriver = GetNodeOrNull<Sprite>("Screwdriver");
            battery = GetNodeOrNull<Sprite>("Battery");
            GameLoader.LoadScene();
            int screwdriverValue = WorldDictionary.checkObjectStatuScene(screwdriver.Name);
            int batteryValue = WorldDictionary.checkObjectStatuScene(battery.Name);
            if (screwdriverValue != 0)
            {
                SceneManager(screwdriver, screwdriverValue);
            }
            if (batteryValue != 0)
            {
                SceneManager(battery, batteryValue);
            }

        }
        public void SceneManager(Sprite sprite, int state)
        {
            switch (state)
            {
                case 1: sprite.QueueFree(); break;
                case 3: sprite.QueueFree(); break;
                case 5: sprite.Visible = false; break;
            }
        }
    }
}