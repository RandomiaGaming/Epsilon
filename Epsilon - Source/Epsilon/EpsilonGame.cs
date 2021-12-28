using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace EpsilonCore
{
    public sealed class EpsilonGame : Microsoft.Xna.Framework.Game
    {
        //Average of 9800 tpf
        public Microsoft.Xna.Framework.Color backgroundColor = new Microsoft.Xna.Framework.Color(255, 255, 155, 255);
        private Texture2D thatOneTexture;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

        RenderTarget2D renderTarget;

        private Profiler profiler = new Profiler();

        GraphicsDeviceManager graphics;
        public EpsilonGame()
        {
            graphics = new Microsoft.Xna.Framework.GraphicsDeviceManager(this)
            {
                SynchronizeWithVerticalRetrace = false
            };

            Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;
            Window.IsBorderless = false;
            Window.Title = "Epsilon - RandomiaGaming - 1.0";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            TargetElapsedTime = new TimeSpan(10000000 / 60);
        }
        
        protected override void Initialize()
        {
            base.Initialize();

            profiler = new Profiler();
            profiler.Reset();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            renderTarget = new RenderTarget2D(GraphicsDevice, 64, 64, false, SurfaceFormat.Color, DepthFormat.None);

            thatOneTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(GraphicsDevice, 32, 32);
            Microsoft.Xna.Framework.Color[] textureBuffer = new Microsoft.Xna.Framework.Color[32 * 32];
            int i = 0;
            for (int y = 32 - 1; y >= 0; y--)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        textureBuffer[i] = new Microsoft.Xna.Framework.Color(255, 255, 255, 255);
                    }
                    else
                    {
                        textureBuffer[i] = new Microsoft.Xna.Framework.Color(0, 0, 0, 255);
                    }
                    i++;
                }
            }
            thatOneTexture.SetData(textureBuffer);
        }
        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            profiler.AddSample(gameTime.ElapsedGameTime.Ticks);
            profiler.PrintValue();
            base.Update(gameTime);
        }
        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Small Rendering
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(backgroundColor);
            spriteBatch.Begin(samplerState: Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp);
            spriteBatch.Draw(thatOneTexture, new Microsoft.Xna.Framework.Rectangle(0, 0, 32, 32), new Microsoft.Xna.Framework.Rectangle(0, 0, thatOneTexture.Width, thatOneTexture.Height), Microsoft.Xna.Framework.Color.White, 0, new Microsoft.Xna.Framework.Vector2(0, 0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            //Big Rendering
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, new Microsoft.Xna.Framework.Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Microsoft.Xna.Framework.Rectangle(0, 0, renderTarget.Width, renderTarget.Height), Microsoft.Xna.Framework.Color.White, 57.0f, new Microsoft.Xna.Framework.Vector2(renderTarget.Width / 2.0f, renderTarget.Height / 2.0f), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}