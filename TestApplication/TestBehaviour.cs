using HellFireEngine;
using Microsoft.Xna.Framework;

namespace TestApplication
{
    public class TestBehaviour : MonoBehaviour
    {
        public string FunnyText { get; set; } = string.Empty;

        public override void Update(GameTime gameTime)
        {
            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                SceneManager.Stop();
            base.Update(gameTime);
        }
    }
}
