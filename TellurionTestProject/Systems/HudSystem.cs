using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;

namespace TellurionTestProject.Systems
{
    public class HudSystem : DrawSystem
    {
        private readonly GameMain _game;
        private readonly SpriteFont _font;
        private readonly SpriteBatch _spriteBatch;

        public HudSystem(GameMain game,
            GraphicsDevice graphicsDevice,
            SpriteFont font)
        {
            _game = game;
            _font = font;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.FpsCounter.Draw(gameTime);
            var fps = $"FPS: {_game.FpsCounter.FramesPerSecond}";

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, fps, new Vector2(16, 16), Color.White);
            _spriteBatch.End();
        }
    }
}
