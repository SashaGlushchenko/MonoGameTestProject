using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using TellurionTestProject.Systems;

namespace TellurionTestProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly GraphicsDeviceManager _graphicsDeviceManager;

        public FramesPerSecondCounter FpsCounter { get; } = new FramesPerSecondCounter();
        private World _world;

        public GameMain()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
            IsFixedTimeStep = true;

            _graphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            var font = Content.Load<SpriteFont>("FPS");
            var texture = Content.Load<Texture2D>("0x72_16x16DungeonTileset.v4");
            var tileset = new Tileset(texture, 16, 16);

            _world = new WorldBuilder()
                .AddSystem(new UnitGrowingSystem())
                .AddSystem(new SpriteRenderingSystem(GraphicsDevice, font))
                .AddSystem(new HudSystem(this, GraphicsDevice, font))
                .Build();

            var entityFactory = new EntityFactory(_world, tileset);

            InitialSetup(entityFactory);
        }

        private void InitialSetup(EntityFactory entityFactory)
        {
            var border = 100;
            var x = _graphicsDeviceManager.PreferredBackBufferWidth - border;
            var y = _graphicsDeviceManager.PreferredBackBufferHeight - border;
            var random = new Random();

            var buildingCount = random.Next(6, 10);

            var step = x / (buildingCount + 1);

            for (var i = 0; i <= buildingCount; i++)
            {
                entityFactory.SpawnBuilding(
                    random.Next( i == 0 ? border: i * step, (i * step + border)), 
                    random.Next(border, y),
                    i == 0 ? OwnerEnum.Player : (i == buildingCount ? OwnerEnum.AI : OwnerEnum.None)
                    );
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.Buttons.Back == ButtonState.Pressed
                || keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            FpsCounter.Update(gameTime);
            _world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            _world.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
