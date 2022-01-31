using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpsilonEngine
{
    public enum EpsilonState { Initialing, Updating, Drawing, Exiting, Exited };
    public class Engine : Game
    {
        #region Constants
        public static readonly Color BackgroundColor = new Color(byte.MaxValue, byte.MaxValue, (byte)150, byte.MaxValue);
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
        private Scene _currentStage = null;
        private Scene _newStageQue = null;
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
        public Scene CurrentStage
        {
            get
            {
                return _currentStage;
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
        public Engine()
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
            base.Window.Position = new Point(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4).ToXNA();
            base.Window.Title = FullName;

            base.InactiveSleepTime = new TimeSpan(0);
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
            Window.Position = new Point(base.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, base.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4).ToXNA();
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

            SetWindowed();
        }
        protected sealed override void Update(GameTime gameTime)
        {
            DebugProfiler.UpdateStart();

            _currentState = EpsilonState.Updating;

            _timeSinceStart = gameTime.TotalGameTime;
            _deltaTime = gameTime.ElapsedGameTime;

            SquashStageQue();

            if (_currentStage is not null)
            {
                _currentStage.InvokeUpdate();
            }

            DebugProfiler.UpdateEnd();

            DebugProfiler.RenderStart();

            _currentState = EpsilonState.Drawing;

            Texture2D stageRender = null;

            if (_currentStage is not null)
            {
                stageRender = _currentStage.InvokeRender();
            }

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            _mainSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            if (stageRender is not null)
            {
                double targetAspectRatio = _currentStage.AspectRatio;
                Point viewPortSize = GetViewportSize();
                int viewPortWidth = viewPortSize.X;
                int viewPortHeight = viewPortSize.Y;
                double viewPortAspectRatio = viewPortWidth / (double)viewPortHeight;
                int RenderWidth;
                int RenderHeight;
                int RenderX;
                int RenderY;

                if(viewPortAspectRatio >= targetAspectRatio)
                {
                    RenderWidth = (int)(viewPortHeight * targetAspectRatio);
                    RenderHeight = viewPortHeight;
                    RenderX = (viewPortWidth - RenderWidth) / 2;
                    RenderY = 0;
                }
                else
                {
                    RenderWidth = viewPortWidth;
                    RenderHeight = (int)(viewPortWidth / targetAspectRatio);
                    RenderX = 0;
                    RenderY = (viewPortHeight - RenderHeight) / 2;
                }

                _mainSpriteBatch.Draw(stageRender, new Microsoft.Xna.Framework.Rectangle(RenderX, RenderY, RenderWidth, RenderHeight), new Microsoft.Xna.Framework.Rectangle(0, 0, stageRender.Width, stageRender.Height), Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            }

            //Render Canvas Here

            _mainSpriteBatch.End();

            DebugProfiler.RenderEnd();

            DebugProfiler.FrameEnd();

            DebugProfiler.Print();

            DebugProfiler.FrameStart();
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
                    _currentStage.InvokeOnDestroy();
                }
                if (_newStageQue is not null)
                {
                    _newStageQue.InvokeInitialize();
                }
                _currentStage = _newStageQue;
            }
        }
        public void ChangeStage(Scene newStage)
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
                else if (newStage.Engine == this)
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