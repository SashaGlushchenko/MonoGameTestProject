using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using TellurionTestProject.Components;

namespace TellurionTestProject.Systems
{
    public class SpriteRenderingSystem : EntityDrawSystem
    {
        private readonly SpriteFont _font;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Building> _buildingMapper;

        public SpriteRenderingSystem(GraphicsDevice graphicsDevice, SpriteFont font)
            : base(Aspect.All(typeof(Transform2)).One(typeof(Sprite)))
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _font = font;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _buildingMapper = mapperService.GetMapper<Building>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var sprite = _spriteMapper.Get(entity);
                var building = _buildingMapper.Get(entity);

                _spriteBatch.Draw(sprite, transform);

                if (building != null)
                {
                    _spriteBatch.DrawString(
                        _font,
                        building.UnitsCount.ToString(CultureInfo.InvariantCulture),
                        transform.WorldPosition + new Vector2(-10, -25),
                        Color.Black);
                }
            }

            _spriteBatch.End();
        }
    }
}
