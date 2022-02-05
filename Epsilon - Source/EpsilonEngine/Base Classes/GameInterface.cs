using System;
namespace EpsilonEngine
{
    internal sealed class GameInterface : Microsoft.Xna.Framework.Game
    {
        #region Properties
        public Game Game { get; private set; } = null;
        public Microsoft.Xna.Framework.GraphicsDeviceManager GraphicsDeviceManager { get; private set; } = null;
        public Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch { get; private set; } = null;
        #endregion
        #region Constructors
        public GameInterface(Game game)
        {
            if (game is null)
            {
                throw new Exception("game cannot be null.");
            }

            Game = game;

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

            SpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            SpriteBatch.Name = "Main SpriteBatch";
            SpriteBatch.Tag = null;
        }
        #endregion
        #region Overrides
        protected sealed override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.InvokeUpdate();
        }
        protected sealed override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.InvokeRender();
        }
        public sealed override string ToString()
        {
            return $"EpsilonEngine.GameInterface(Game)";
        }
        #endregion
        /* #region Window Management
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
         #endregion*/
    }
}