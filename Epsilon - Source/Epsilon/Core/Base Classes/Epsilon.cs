using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Epsilon
{
    public sealed class Epsilon : Game
    {
        #region Constants
        public static readonly Color BackgroundColor = new Color(byte.MaxValue, (byte)150, byte.MaxValue, byte.MaxValue);
        public const string ProductTitle = "Epsilon";
        public const string VersionString = "1.0.0";
        public const ushort VersionCode = 1;
        public const bool ProfilerEnabled = true;
        public string WindowTitle => ProductTitle + " - " + VersionString;
        #endregion
        #region Variables
        private GraphicsDeviceManager _graphicsDeviceManager = null;
        private SpriteBatch _spriteBatch = null;
        private Stage _currentStage = null;
        private TimeSpan _timeSinceStart = new TimeSpan(0);
        private TimeSpan _deltaTime = new TimeSpan(0);
        private bool _running = false;
        #endregion
        #region Properties
        public Stage CurrentStage
        {
            get
            {
                return _currentStage;
            }
            set
            {
                _currentStage = value;
            }
        }
        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return _graphicsDeviceManager;
            }
        }
        public TimeSpan TimeSinceStart
        {
            get
            {
                return _timeSinceStart;
            }
        }
        public TimeSpan DeltaTime
        {
            get
            {
                return _deltaTime;
            }
        }
        public bool Running
        {
            get
            {
                return _running;
            }
        }
        #endregion
        #region Constructors
        public Epsilon()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);

            _graphicsDeviceManager.GraphicsProfile = GraphicsProfile.Reach;
            _graphicsDeviceManager.HardwareModeSwitch = true;
            _graphicsDeviceManager.IsFullScreen = false;
            _graphicsDeviceManager.PreferHalfPixelOffset = false;
            _graphicsDeviceManager.PreferredBackBufferFormat = SurfaceFormat.Color;
            _graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight | DisplayOrientation.Portrait | DisplayOrientation.PortraitDown | DisplayOrientation.Unknown | DisplayOrientation.Default;
            _graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            _graphicsDeviceManager.ApplyChanges();

            _graphicsDeviceManager.PreferredBackBufferWidth = base.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2;
            _graphicsDeviceManager.PreferredBackBufferHeight = base.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2;
            _graphicsDeviceManager.ApplyChanges();

            base.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            base.Window.AllowAltF4 = true;
            base.Window.AllowUserResizing = true;
            base.Window.IsBorderless = false;
            base.Window.Position = new Point(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4);
            base.Window.Title = WindowTitle;

            base.InactiveSleepTime = new TimeSpan(10000000 * 3);
            base.TargetElapsedTime = new TimeSpan(10000000 / 60);
            base.MaxElapsedTime = new TimeSpan(10000000 / 60);
            base.IsFixedTimeStep = false;
            base.IsMouseVisible = true;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatch.Name = "Main SpriteBatch";
            _spriteBatch.Tag = null;
        }
        #endregion
        #region Overrides
        protected sealed override void Initialize()
        {
            _currentStage = new Stage(this);
            _currentStage.Initialize();

            _running = true;

            base.Initialize();
        }
        protected sealed override void Update(GameTime gameTime)
        {
            _timeSinceStart = gameTime.TotalGameTime;
            _deltaTime = gameTime.ElapsedGameTime;

            if (ProfilerEnabled)
            {
                DebugProfiler.AddSample(_deltaTime.Ticks);
                DebugProfiler.Print();
            }

            if (_currentStage is not null)
            {
                _currentStage.Update();
            }

            base.Update(gameTime);
        }
        protected sealed override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, null);

            if (_currentStage is not null)
            {
                Texture2D sceneRender = _currentStage.Render();
                _spriteBatch.Draw(sceneRender, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Rectangle(0, 0, sceneRender.Width, sceneRender.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public override string ToString()
        {
            return $"Epsilon.Epsilon()";
        }
        #endregion
    }
}