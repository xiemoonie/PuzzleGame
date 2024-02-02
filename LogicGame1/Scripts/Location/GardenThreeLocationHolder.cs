using Godot;
using System;


namespace LogicGame1.Scripts.Location
{
    public class GardenThreeLocationHolder : LocationHolder
    {
        private Sprite vision;
        private Sprite cloth;
            public override void _Ready()
            {
                base._Ready();
                GameLoader.LoadScene();
                vision = GetNode<Sprite>("Vision");
                cloth = GetNode<Sprite>("Cloth");
                int visionValue = WorldDictionary.checkObjectStatuScene(vision.Name);
                int clothValue = WorldDictionary.checkObjectStatuScene(cloth.Name);
                if (visionValue != 0)
                {
                    SceneManager(vision, visionValue);
                }
                if (clothValue != 0)
                {
                SceneManager(cloth, clothValue);
                }
        }

            public void SceneManager(Sprite sprite, int state)
            {
                switch (state)
                {
                    case 1: sprite.QueueFree(); break;
                }
            }
        }
    }