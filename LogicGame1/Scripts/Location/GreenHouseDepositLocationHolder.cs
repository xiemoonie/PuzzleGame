using Godot;
using System;
using System.Runtime.InteropServices;

namespace LogicGame1.Scripts.Location
{
    public class GreenHouseDepositLocationHolder : LocationHolder
    {
        private Sprite screwdriver;
        private Sprite battery;
        private Sprite bucket;

        public override void _Ready()
        {
            screwdriver = GetNodeOrNull<Sprite>("Screwdriver");
            battery = GetNodeOrNull<Sprite>("Battery");
            bucket = GetNodeOrNull<Sprite>("Bucket");
            GameLoader.LoadScene();
            int screwdriverValue = WorldDictionary.checkObjectStatuScene(screwdriver.Name);
            int batteryValue = WorldDictionary.checkObjectStatuScene(battery.Name);
            int bucketValue = WorldDictionary.checkObjectStatuScene(bucket.Name);
            if (screwdriverValue != 0)
            {
                SceneManager(screwdriver, screwdriverValue);
            }
            if (batteryValue != 0)
            {
                SceneManager(battery, batteryValue);
            }
            if (bucketValue != 0)
            {
                SceneManager(bucket, bucketValue);
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