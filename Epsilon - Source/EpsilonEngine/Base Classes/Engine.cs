using System;
namespace EpsilonEngine
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region Variables
        private Microsoft.Xna.Framework.GraphicsDeviceManager GraphicsDeviceManager = null;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch MainSpriteBatch = null;

        private PumpEvent[] updatePump = new PumpEvent[0];
        private PumpEvent[] safeUpdatePump = new PumpEvent[0];
        private bool updatePumpDirty = false;
        private bool updatePumpInUse = false;
        #endregion
        #region Properties
        public Color BackgroundColor { get; private set; } = Color.White;
        public Scene CurrentScene { get; private set; } = null;
        public float CurrentFPS { get; private set; } = 0f;
        public TimeSpan TimeSinceStart { get; private set; } = new TimeSpan(0);
        public TimeSpan DeltaTime { get; private set; } = new TimeSpan(0);
        #endregion
        #region Constructors
        public Game()
        {
            GraphicsDeviceManager = new Microsoft.Xna.Framework.GraphicsDeviceManager(this);
            GraphicsDeviceManager.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.Reach;
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            GraphicsDeviceManager.HardwareModeSwitch = true;
            GraphicsDeviceManager.IsFullScreen = false;
            GraphicsDeviceManager.PreferHalfPixelOffset = false;
            GraphicsDeviceManager.PreferredBackBufferFormat = Microsoft.Xna.Framework.Graphics.SurfaceFormat.Color;
            GraphicsDeviceManager.SupportedOrientations = Microsoft.Xna.Framework.DisplayOrientation.LandscapeLeft | Microsoft.Xna.Framework.DisplayOrientation.LandscapeRight | Microsoft.Xna.Framework.DisplayOrientation.Portrait | Microsoft.Xna.Framework.DisplayOrientation.PortraitDown | Microsoft.Xna.Framework.DisplayOrientation.Unknown | Microsoft.Xna.Framework.DisplayOrientation.Default;
            GraphicsDeviceManager.ApplyChanges();

            base.GraphicsDevice.BlendState = Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend;
            base.GraphicsDevice.DepthStencilState = Microsoft.Xna.Framework.Graphics.DepthStencilState.None;
            base.GraphicsDevice.RasterizerState = Microsoft.Xna.Framework.Graphics.RasterizerState.CullNone;

            base.Window.AllowAltF4 = true;
            base.Window.AllowUserResizing = true;
            base.Window.IsBorderless = false;
            base.Window.Position = new Point(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4).ToXNA();
            base.Window.Title = "Game";

            base.InactiveSleepTime = new TimeSpan(0);
            base.TargetElapsedTime = new TimeSpan(10000000 / 60);
            base.MaxElapsedTime = new TimeSpan(10000000 / 60);
            base.IsFixedTimeStep = false;
            base.IsMouseVisible = true;

            MainSpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            MainSpriteBatch.Name = "Main SpriteBatch";
            MainSpriteBatch.Tag = null;

            Window.ClientSizeChanged += WindowClientSizeChanged;
        }
        #endregion
        #region Overrides
        protected override void Initialize()
        {
            SetWindowed();
        }
        protected sealed override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            DebugProfiler.UpdateStart();

            TimeSinceStart = gameTime.TotalGameTime;
            DeltaTime = gameTime.ElapsedGameTime;
            CurrentFPS = 10000000f / DeltaTime.Ticks;

            if (CurrentScene is not null && !CurrentScene.GameObjectsInitialized)
            {
                CurrentScene.Initialize();
            }

            int updatePumpLength = safeUpdatePump.Length;
            for (int i = 0; i < updatePumpLength; i++)
            {
                safeUpdatePump[i].Invoke();
            }

            if (updatePumpDirty)
            {
                safeUpdatePump = updatePump;
                updatePumpDirty = false;
            }

            DebugProfiler.UpdateEnd();

            DebugProfiler.RenderStart();

            Microsoft.Xna.Framework.Graphics.Texture2D stageRender = null;

            if (CurrentScene is not null)
            {
                stageRender = CurrentScene.Render();
            }

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            MainSpriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            if (stageRender is not null)
            {
                double targetAspectRatio = CurrentScene.AspectRatio;
                Point viewPortSize = GetViewportSize();
                int viewPortWidth = viewPortSize.X;
                int viewPortHeight = viewPortSize.Y;
                double viewPortAspectRatio = viewPortWidth / (double)viewPortHeight;
                int RenderWidth;
                int RenderHeight;
                int RenderX;
                int RenderY;

                if (viewPortAspectRatio >= targetAspectRatio)
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

                MainSpriteBatch.Draw(stageRender, new Microsoft.Xna.Framework.Rectangle(RenderX, RenderY, RenderWidth, RenderHeight), new Microsoft.Xna.Framework.Rectangle(0, 0, stageRender.Width, stageRender.Height), Microsoft.Xna.Framework.Color.White, 0, new Microsoft.Xna.Framework.Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
            }

            //Render Canvas Here

            MainSpriteBatch.End();

            DebugProfiler.RenderEnd();

            DebugProfiler.FrameEnd();

            DebugProfiler.Print();

            DebugProfiler.FrameStart();
        }
        public override string ToString()
        {
            return $"EpsilonEngine.Engine(Game)";
        }
        #endregion
        #region Methods
        public void ChangeScene(Scene scene)
        {
            if (CurrentScene is not null)
            {
                CurrentScene.OnRemove();
            }
            CurrentScene = scene;
            if (CurrentScene is not null)
            {
                CurrentScene.Initialize();
            }
        }
        public void RegisterForUpdate(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new Exception("updateable cannot be null.");
            }

            PumpEvent[] newUpdatePump = new PumpEvent[updatePump.Length + 1];
            Array.Copy(updatePump, 0, newUpdatePump, 0, updatePump.Length);
            newUpdatePump[updatePump.Length] = pumpEvent;
            updatePump = newUpdatePump;

            if (!updatePumpInUse)
            {
                safeUpdatePump = newUpdatePump;
            }
            else
            {
                updatePumpDirty = true;
            }
        }
        #endregion
        #region Window Management
        private void WindowClientSizeChanged(object sender, EventArgs e)
        {
            Point viewportSize = GetViewportSize();
            if (GraphicsDeviceManager.PreferredBackBufferWidth != viewportSize.X || GraphicsDeviceManager.PreferredBackBufferHeight != viewportSize.Y)
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = viewportSize.X;
                GraphicsDeviceManager.PreferredBackBufferHeight = viewportSize.Y;
                GraphicsDeviceManager.ApplyChanges();
            }
        }
        public void SetWindowed()
        {
            GraphicsDeviceManager.IsFullScreen = false;
            GraphicsDeviceManager.PreferredBackBufferWidth = base.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2;
            GraphicsDeviceManager.PreferredBackBufferHeight = base.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2;
            GraphicsDeviceManager.ApplyChanges();
            Window.Position = new Point(base.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, base.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 4).ToXNA();
        }
        public void SetFullscreen()
        {
            GraphicsDeviceManager.IsFullScreen = true;
            GraphicsDeviceManager.PreferredBackBufferWidth = base.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = base.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            GraphicsDeviceManager.ApplyChanges();
        }
        public void ToggleFullscreen()
        {
            if (GraphicsDeviceManager.IsFullScreen)
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
            if (GraphicsDeviceManager.IsFullScreen)
            {
                return new Point(base.GraphicsDevice.Adapter.CurrentDisplayMode.Width, base.GraphicsDevice.Adapter.CurrentDisplayMode.Height);
            }
            else
            {
                return new Point(base.GraphicsDevice.Viewport.Width, base.GraphicsDevice.Viewport.Height);
            }
        }
        #endregion
    }
}