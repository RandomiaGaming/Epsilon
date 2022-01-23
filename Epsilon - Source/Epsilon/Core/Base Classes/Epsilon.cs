using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Epsilon
{
    public enum EpsilonState { Initialing, Updating, Drawing, Exiting, Exited };
    public sealed class Epsilon : Game
    {
        #region Constants
        public static readonly Color BackgroundColor = new Color(byte.MaxValue, (byte)150, byte.MaxValue, byte.MaxValue);
        public const bool ProfilerEnabled = true;
        public const string Name = "Epsilon";
        public const string VersionString = "1.0.0";
        public static string FullName
        {
            get
            {
                return Name + " - " + VersionString;
            }
        }
        public const ushort VersionCode = 1;
        #endregion
        #region Variables
        private GraphicsDeviceManager _graphicsDeviceManager = null;
        private SpriteBatch _mainSpriteBatch = null;
        private Stage _currentStage = null;
        private Stage _newStageQue = null;
        private InputManager _inputManager = null;
        private TimeSpan _timeSinceStart = new TimeSpan(0);
        private TimeSpan _deltaTime = new TimeSpan(0);
        private EpsilonState _currentState = EpsilonState.Initialing;
        #endregion
        #region Properties
        public EpsilonState CurrentState
        {
            get
            {
                return _currentState;
            }
        }
        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return _graphicsDeviceManager;
            }
        }
        public SpriteBatch MainSpriteBatch
        {
            get
            {
                return _mainSpriteBatch;
            }
        }
        public Stage CurrentStage
        {
            get
            {
                return _currentStage;
            }
        }
        public InputManager InputManager
        {
            get
            {
                return _inputManager;
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
        #endregion
        #region Constructors
        public Epsilon()
        {
            _currentState = EpsilonState.Initialing;

            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.GraphicsProfile = GraphicsProfile.Reach;
            _graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            _graphicsDeviceManager.HardwareModeSwitch = true;
            _graphicsDeviceManager.IsFullScreen = false;
            _graphicsDeviceManager.PreferHalfPixelOffset = false;
            _graphicsDeviceManager.PreferredBackBufferFormat = SurfaceFormat.Color;
            _graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight | DisplayOrientation.Portrait | DisplayOrientation.PortraitDown | DisplayOrientation.Unknown | DisplayOrientation.Default;
            _graphicsDeviceManager.ApplyChanges();

            base.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            base.GraphicsDevice.DepthStencilState = DepthStencilState.None;
            base.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            base.Window.AllowAltF4 = true;
            base.Window.AllowUserResizing = true;
            base.Window.IsBorderless = false;
            base.Window.Position = new Point(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4);
            base.Window.Title = FullName;

            base.InactiveSleepTime = new TimeSpan(10000000 * 3);
            base.TargetElapsedTime = new TimeSpan(10000000 / 60);
            base.MaxElapsedTime = new TimeSpan(10000000 / 60);
            base.IsFixedTimeStep = false;
            base.IsMouseVisible = true;

            _mainSpriteBatch = new SpriteBatch(GraphicsDevice);
            _mainSpriteBatch.Name = "Main SpriteBatch";
            _mainSpriteBatch.Tag = null;

            Window.ClientSizeChanged += WindowClientSizeChanged;
        }
        #endregion
        #region Window Management
        private void WindowClientSizeChanged(object sender, EventArgs e)
        {
            Point viewportSize = GetViewportSize();
            if (_graphicsDeviceManager.PreferredBackBufferWidth != viewportSize.X || _graphicsDeviceManager.PreferredBackBufferHeight != viewportSize.Y)
            {
                _graphicsDeviceManager.PreferredBackBufferWidth = viewportSize.X;
                _graphicsDeviceManager.PreferredBackBufferHeight = viewportSize.Y;
                _graphicsDeviceManager.ApplyChanges();
            }
        }
        public void SetWindowed()
        {
            _graphicsDeviceManager.IsFullScreen = false;
            _graphicsDeviceManager.PreferredBackBufferWidth = base.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2;
            _graphicsDeviceManager.PreferredBackBufferHeight = base.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2;
            _graphicsDeviceManager.ApplyChanges();
            Window.Position = new Point(base.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, base.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4);
        }
        public void SetFullscreen()
        {
            _graphicsDeviceManager.IsFullScreen = true;
            _graphicsDeviceManager.PreferredBackBufferWidth = base.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphicsDeviceManager.PreferredBackBufferHeight = base.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphicsDeviceManager.ApplyChanges();
        }
        public void ToggleFullscreen()
        {
            if (_graphicsDeviceManager.IsFullScreen)
            {
                SetWindowed();
            }
            else
            {
                SetFullscreen();
            }
        }
        public Point GetViewportSize()
        {
            if (_graphicsDeviceManager.IsFullScreen)
            {
                return new Point(base.GraphicsDevice.Adapter.CurrentDisplayMode.Width, base.GraphicsDevice.Adapter.CurrentDisplayMode.Height);
            }
            else
            {
                return new Point(base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height);
            }
        }
        #endregion
        #region Overrides
        protected override void Initialize()
        {
            _currentState = EpsilonState.Initialing;

            _inputManager = new InputManager(this);

            SetWindowed();
        }
        protected sealed override void Update(GameTime gameTime)
        {
            _currentState = EpsilonState.Updating;

            _timeSinceStart = gameTime.TotalGameTime;
            _deltaTime = gameTime.ElapsedGameTime;

            if (ProfilerEnabled)
            {
                DebugProfiler.AddSample(_deltaTime.Ticks);
                DebugProfiler.Print();
            }

            SquashStageQue();

            if (_inputManager is not null)
            {
                _inputManager.Update();
            }

            if (_currentStage is not null)
            {
                _currentStage.Update();
            }
        }
        protected sealed override void Draw(GameTime gameTime)
        {
            _currentState = EpsilonState.Drawing;

            Texture2D stageRender = null;

            if (_currentStage is not null)
            {
                stageRender = _currentStage.Draw();
            }

            GraphicsDevice.Clear(BackgroundColor);

            _mainSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            if (stageRender is not null)
            {
                _mainSpriteBatch.Draw(stageRender, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Rectangle(0, 0, stageRender.Width, stageRender.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            }

            //Render Canvas Here

            _mainSpriteBatch.End();

            base.Draw(gameTime);
        }
        public override string ToString()
        {
            return $"Epsilon.Epsilon({FullName})";
        }
        protected override void EndRun()
        {
            _currentState = EpsilonState.Exiting;
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            this.Dispose();
            _currentState = EpsilonState.Exited;
        }
        #endregion
        #region Methods
        private void SquashStageQue()
        {
            if (_newStageQue != _currentStage)
            {
                if (_currentStage is not null)
                {
                    _currentStage.OnRemove();
                }
                if (_newStageQue is not null)
                {
                    _newStageQue.Initialize();
                }
                _currentStage = _newStageQue;
            }
        }
        public void ChangeStage(Stage newStage)
        {
            if (_currentState == EpsilonState.Initialing)
            {
                _newStageQue = newStage;
                _currentStage = _newStageQue;
            }
            else if (_currentState == EpsilonState.Updating)
            {
                if (newStage is null)
                {
                    _newStageQue = null;
                }
                else if (newStage.Epsilon == this)
                {
                    _newStageQue = newStage;
                }
                else
                {
                    throw new Exception("newStage belongs to a different Epsilon.");
                }
            }
            else if (_currentState == EpsilonState.Drawing)
            {
                throw new Exception("Cannot set stage during drawing.");
            }
            else if (_currentState == (EpsilonState.Exited | EpsilonState.Exited))
            {
                throw new Exception("Cannot set stage because game has exited.");
            }
        }
        #endregion
    }
}