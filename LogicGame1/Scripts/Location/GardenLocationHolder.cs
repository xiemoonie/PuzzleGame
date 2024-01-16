using Godot;

namespace LogicGame1.Scripts.Location
{
    public class GardenLocationHolder : LocationHolder
    {
        private Sprite coin;
        private Sprite fungi;
       
        public override void _Ready()
        {
            base._Ready();
            GameLoader.LoadScene();
            coin = GetNode<Sprite>("Coin");
            fungi = GetNode<Sprite>("Fungi");
            int coinValue = WorldDictionary.checkObjectStatuScene(coin.Name);
            int fungiValue = WorldDictionary.checkObjectStatuScene(fungi.Name);
            if (coinValue != 0)
            {
                SceneManager(coin, coinValue);
            }
            if (fungiValue != 0)
            {
                SceneManager(fungi, fungiValue);
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
