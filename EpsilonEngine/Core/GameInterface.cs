namespace EpsilonEngine
{
    internal sealed class GameInterface : Microsoft.Xna.Framework.Game
    {
        #region Internal Variables
        internal Game Game = null;
        internal Microsoft.Xna.Framework.GraphicsDeviceManager XNAGraphicsDeviceManager = null;
        internal Microsoft.Xna.Framework.Graphics.GraphicsDevice XNAGraphicsDevice = null;
        internal Microsoft.Xna.Framework.GameWindow XNAGameWindow = null;
        #endregion
        #region Constructors
        internal GameInterface(Game game)
        {
            Game = game;

            XNAGraphicsDeviceManager = new Microsoft.Xna.Framework.GraphicsDeviceManager(this);
            XNAGraphicsDeviceManager.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.Reach;
            XNAGraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            XNAGraphicsDeviceManager.HardwareModeSwitch = false;
            XNAGraphicsDeviceManager.IsFullScreen = false;
            XNAGraphicsDeviceManager.PreferHalfPixelOffset = false;
            XNAGraphicsDeviceManager.PreferredBackBufferFormat = Microsoft.Xna.Framework.Graphics.SurfaceFormat.Color;
            XNAGraphicsDeviceManager.SupportedOrientations = Microsoft.Xna.Framework.DisplayOrientation.LandscapeLeft | Microsoft.Xna.Framework.DisplayOrientation.LandscapeRight | Microsoft.Xna.Framework.DisplayOrientation.Portrait | Microsoft.Xna.Framework.DisplayOrientation.PortraitDown | Microsoft.Xna.Framework.DisplayOrientation.Unknown | Microsoft.Xna.Framework.DisplayOrientation.Default;
            XNAGraphicsDeviceManager.ApplyChanges();

            XNAGraphicsDevice = GraphicsDevice;

            XNAGraphicsDevice.BlendState = Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend;
            XNAGraphicsDevice.DepthStencilState = Microsoft.Xna.Framework.Graphics.DepthStencilState.None;
            XNAGraphicsDevice.RasterizerState = Microsoft.Xna.Framework.Graphics.RasterizerState.CullNone;

            XNAGameWindow = Window;

            XNAGameWindow.AllowAltF4 = true;
            XNAGameWindow.AllowUserResizing = true;
            XNAGameWindow.IsBorderless = false;
            XNAGameWindow.Position = new Microsoft.Xna.Framework.Point(XNAGraphicsDevice.Adapter.CurrentDisplayMode.Width / 4, XNAGraphicsDevice.Adapter.CurrentDisplayMode.Height / 4);
            XNAGameWindow.Title = "EpsilonEngine";

            InactiveSleepTime = new System.TimeSpan(0);
            TargetElapsedTime = new System.TimeSpan(10000000 / 60);
            MaxElapsedTime = new System.TimeSpan(10000000 / 60);
            IsFixedTimeStep = false;
            IsMouseVisible = true;

            XNAGameWindow.ClientSizeChanged += ResizeCallback;
        }
        #endregion
        #region Private Methods
        private void ResizeCallback(object sender, System.EventArgs e)
        {
            Game.ResizeCallback();
        }
        #endregion
        #region Override Methods
        protected sealed override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Game.UpdateCallback();
        }
        public sealed override string ToString()
        {
            return $"EpsilonEngine.GameInterface()";
        }
        protected sealed override void OnExiting(object sender, System.EventArgs args)
        {
            Game.ExitCallback();
        }
        #endregion
    }
}