using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using TellurionTestProject.Components;


namespace TellurionTestProject
{
    public class EntityFactory
    {
        private readonly Tileset _tileset;
        private readonly World _world;

        public EntityFactory(World world, Tileset tileset)
        {
            _world = world;
            _tileset = tileset;
        }

        public void SpawnBuilding(float x, float y, OwnerEnum owner)
        {
            var entity = _world.CreateEntity();
            entity.Attach(new Sprite(_tileset.GetTile(232)));
            entity.Attach(new Transform2(x, y));
            entity.Attach(new Building{Level = 4, Owner = owner});
        }
    }
}
