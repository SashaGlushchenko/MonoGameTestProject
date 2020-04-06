using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using TellurionTestProject.Components;

namespace TellurionTestProject.Systems
{
    public class UnitGrowingSystem: EntityUpdateSystem
    {
        const float Timer = 1;
        private float _timer;
        private ComponentMapper<Building> _buildingMapper;

        public UnitGrowingSystem()
            : base(Aspect.All(typeof(Building)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _buildingMapper = mapperService.GetMapper<Building>();
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _timer -= elapsed;

            if (_timer <= 0)
            {
                _timer = Timer;
                foreach (var entity in ActiveEntities)
                {
                    var building = _buildingMapper.Get(entity);

                    if (building.Owner != OwnerEnum.None
                        && building.UnitsCount < building.Level * 20)
                    {
                        building.UnitsCount += this.UnitGrowthRate(building.Level);
                    }
                }
            }
        }

        private double UnitGrowthRate(int buildingLevel)
            => buildingLevel switch
            {
                1 => 1,
                2 => 1.25,
                3 => 1.5,
                4 => 2,
                _ => 1
            };
    }
}
