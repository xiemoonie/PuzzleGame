using Godot;

namespace LogicGame1.Scripts.Location
{
    public class GardenLocationHolder : LocationHolder
    {
        private Sprite fungi;
        private Sprite gum;
       
        public override void _Ready()
        {
            base._Ready();
            GameLoader.LoadScene();
            fungi = GetNode<Sprite>("Fungi");
            gum = GetNode<Sprite>("Gum");

            int fungiValue = WorldDictionary.checkObjectStatuScene(fungi.Name);
            int gumValue = WorldDictionary.checkObjectStatuScene(gum.Name);
          
            if (fungiValue != 0)
            {
                SceneManager(fungi, fungiValue);
            }
            if (gumValue != 0)
            {
                SceneManager(gum, gumValue);
            }
        }
        public void SceneManager(Sprite sprite, int state)
        {
            switch (state)
            {
                case 1: sprite.QueueFree(); break;
                case 3: sprite.QueueFree(); break;
                case 5: sprite.QueueFree(); break;
            }
        }
    }
}
