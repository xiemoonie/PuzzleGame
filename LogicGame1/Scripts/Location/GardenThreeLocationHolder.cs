using Godot;
using System;


namespace LogicGame1.Scripts.Location
{
    public class GardenThreeLocationHolder : LocationHolder
    {
        private Sprite vision;
            public override void _Ready()
            {
                base._Ready();
                GameLoader.LoadScene();
                vision = GetNode<Sprite>("Vision");
                int visionValue = WorldDictionary.checkObjectStatuScene(vision.Name);
                if (visionValue != 0)
                {
                    SceneManager(vision, visionValue);
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