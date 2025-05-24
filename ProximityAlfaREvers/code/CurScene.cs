using System;

namespace engine
{
    public static class CurScene
    {
        public static List<GameObject> gameObjectsOnScene = new List<GameObject>();
        public static List<short> exsistingLayers = new List<short>();
        public static void Render()
        {
            foreach (GameObject i in gameObjectsOnScene)
            {
                gameObjectsOnScene.FirstOrDefault(x => x.printLayer == 0);//hkjfhkjesdhkgjhjlglkjslkjsdflkgjlkdsjglkfjsdl
            }
        }
    }
}