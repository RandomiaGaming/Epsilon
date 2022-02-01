using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpsilonEngine
{
    public class Engine : Game
    {
        #region Constants
        public static readonly Color BackgroundColor = new Color(byte.MaxValue, byte.MaxValue, (byte)150, byte.MaxValue);
        public const bool ProfilerEnabled = true;
        public const string Name = "Epsilon";
        public const string VersionString = "1.0.0";
        public static string FullName => Name + " - " + VersionString;
        public const ushort VersionCode = 1;
        #endregion
        #region Variables
        private Updateable[] updatePump = new Updateable[0];
        private Updateable[] safeUpdatePump = new Updateable[0];
        private bool updatePumpDirty = false;
        private bool updatePumpInUse = false;
        #endregion
        #region Properties
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; } = null;
        public SpriteBatch MainSpriteBatch { get; private set; } = null;
        public Scene CurrentScene { get; private set; } = null;
        public TimeSpan TimeSinceStart { get; private set; } = new TimeSpan(0);
        public TimeSpan DeltaTime { get; private set; } = new TimeSpan(0);
        #endregion
        #region Constructors
        public Engine()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.Reach;
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            GraphicsDeviceManager.HardwareModeSwitch = true;
            GraphicsDeviceManager.IsFullScreen = false;
            GraphicsDeviceManager.PreferHalfPixelOffset = false;
            GraphicsDeviceManager.PreferredBackBufferFormat = SurfaceFormat.Color;
            GraphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight | DisplayOrientation.Portrait | DisplayOrientation.PortraitDown | DisplayOrientation.Unknown | DisplayOrientation.Default;
            GraphicsDeviceManager.ApplyChanges();

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

            MainSpriteBatch = new SpriteBatch(GraphicsDevice);
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
        protected sealed override void Update(GameTime gameTime)
        {
            DebugProfiler.UpdateStart();

            TimeSinceStart = gameTime.TotalGameTime;
            DeltaTime = gameTime.ElapsedGameTime;

            int safeUpdatePumpLength = safeUpdatePump.Length;
            for (int i = 0; i < safeUpdatePumpLength; i++)
            {
                safeUpdatePump[i].Update();
            }

            if (updatePumpDirty)
            {
                safeUpdatePump = updatePump;
                updatePumpDirty = false;
            }

            DebugProfiler.UpdateEnd();

            DebugProfiler.RenderStart();

            Texture2D stageRender = null;

            if (CurrentScene is not null)
            {
                stageRender = CurrentScene.Render();
            }

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            MainSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

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

                MainSpriteBatch.Draw(stageRender, new Microsoft.Xna.Framework.Rectangle(RenderX, RenderY, RenderWidth, RenderHeight), new Microsoft.Xna.Framework.Rectangle(0, 0, stageRender.Width, stageRender.Height), Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
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
            return $"EpsilonEngine.Engine({FullName})";
        }
        #endregion
        #region Methods
        public void ChangeScene(Scene scene)
        {
            if (CurrentScene is not null)
            {
                CurrentScene.OnDestroy();
            }
            CurrentScene = scene;
            if (CurrentScene is not null)
            {
                CurrentScene.Initialize();
            }
        }
        public void RegisterForUpdate(Updateable updateable)
        {
            if (updateable is null)
            {
                throw new Exception("updateable cannot be null.");
            }

            Updateable[] newUpdatePump = new Updateable[updatePump.Length + 1];
            Array.Copy(updatePump, 0, newUpdatePump, 0, updatePump.Length);
            newUpdatePump[updatePump.Length] = updateable;
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