using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace TellurionTestProject.Systems
{
    public class SpriteRenderingSystem : EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;

        public SpriteRenderingSystem(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(Transform2)).One(typeof(Sprite)))
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var sprite = _spriteMapper.Get(entity);
                
                _spriteBatch.Draw(sprite, transform);
            }

            _spriteBatch.End();
        }
    }
}
