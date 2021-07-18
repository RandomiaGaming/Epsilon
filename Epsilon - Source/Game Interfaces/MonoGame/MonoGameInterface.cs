using System;
using System.Diagnostics;
using System.Collections.Generic;
public sealed class MonoGameInterface : Microsoft.Xna.Framework.Game
{
    public readonly DontMelt.DontMeltGame dmGame = null;

    private DontMelt.Texture epsilonFrameBuffer;
    private Microsoft.Xna.Framework.Graphics.Texture2D frameBuffer;
    private Microsoft.Xna.Framework.Color[] frameColorBuffer;
    private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

    private int lastScrollWheelValue = 0;
    public MonoGameInterface()
    {
        dmGame = new DontMelt.DontMeltGame();

        _ = new Microsoft.Xna.Framework.GraphicsDeviceManager(this)
        {
            SynchronizeWithVerticalRetrace = false
        };
         
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        Window.IsBorderless = false;
        Window.Title = "Don't Melt! - 1.0.0";
        IsMouseVisible = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = new TimeSpan(10000000 / 60);
    }
    private DontMelt.InputPacket CreateInputPacket()
    {
        Microsoft.Xna.Framework.Input.KeyboardState keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        List<DontMelt.KeyboardButton> pressedKeyboardButtons = new List<DontMelt.KeyboardButton>();
        foreach (Microsoft.Xna.Framework.Input.Keys key in keyboardState.GetPressedKeys())
        {
            switch (key)
            {
                case Microsoft.Xna.Framework.Input.Keys.A:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.A);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.B:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.B);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.C:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.C);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.D:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.D);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.E:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.E);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.G:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.G);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.H:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.H);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.I:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.I);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.J:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.J);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.K:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.K);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.L:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.L);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.M:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.M);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.N:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.N);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.O:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.O);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.P:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.P);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Q:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Q);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.R:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.R);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.S:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.S);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.T:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.T);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.U:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.U);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.V:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.V);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.W:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.W);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.X:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.X);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Y:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Y);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Z:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Z);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad0:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad0);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad1:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad1);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad2:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad2);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad3:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad3);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad4:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad4);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad5:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad5);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad6:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad6);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad7:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad7);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad8:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad8);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumPad9:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPad9);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.OemPlus:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPadPlus);
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Plus);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.OemMinus:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPadMinus);
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Minus);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.NumLock:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumLock);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.LeftShift:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.LeftShift);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.RightShift:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.RightShift);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.LeftControl:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.LeftControl);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.RightControl:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.RightControl);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.LeftAlt:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.LeftAlt);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.RightAlt:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.RightAlt);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.LeftWindows:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.LeftWindows);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.RightWindows:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.RightWindows);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F1:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F1);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F2:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F2);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F3:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F3);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F4:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F4);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F5:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F5);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F6:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F6);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F7:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F7);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F8:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F8);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F9:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F9);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F10:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F10);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F11:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F11);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.F12:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.F12);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Back:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Backspace);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Delete:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Delete);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Scroll:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.ScrollLock);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Escape:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Escape);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Tab:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Tab);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.OemTilde:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Tilde);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Space:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Space);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.PrintScreen:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.PrintScreen);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Insert:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Insert);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Home:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Home);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.PageUp:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.PageUp);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.PageDown:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.PageDown);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.End:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.End);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.OemBackslash:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Backslash);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Divide:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Slash);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.OemComma:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Comma);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.OemPeriod:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Period);
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.NumPadPoint);
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Help:
                    pressedKeyboardButtons.Add(DontMelt.KeyboardButton.Help);
                    break;
            }
        }

        DontMelt.KeyboardState dmKeyboardState = new DontMelt.KeyboardState(keyboardState.CapsLock, false, keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift | Microsoft.Xna.Framework.Input.Keys.RightShift), keyboardState.NumLock, pressedKeyboardButtons);



        Microsoft.Xna.Framework.Input.MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

        DontMelt.Point position = new DontMelt.Point(mouseState.X, mouseState.Y);

        int scrollWheelValue = mouseState.ScrollWheelValue - lastScrollWheelValue;

        bool rightMouseButton = mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        bool leftMouseButton = mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        bool middleMouseButton = mouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;

        List<DontMelt.MouseButton> pressedMouseButtons = new List<DontMelt.MouseButton>();

        if (mouseState.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
        {
            pressedMouseButtons.Add(DontMelt.MouseButton.Button0);
        }
        if (mouseState.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
        {
            pressedMouseButtons.Add(DontMelt.MouseButton.Button1);
        }

        DontMelt.MouseState dmMouseState = new DontMelt.MouseState(position, scrollWheelValue, rightMouseButton, leftMouseButton, middleMouseButton, pressedMouseButtons);

        DontMelt.InputPacket iPacket = new DontMelt.InputPacket(dmKeyboardState, dmMouseState);

        lastScrollWheelValue = scrollWheelValue;
        return iPacket;
    }
    protected override void Initialize()
    {
        spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
        base.Initialize();
    }
    protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
    {
        DontMelt.InputPacket iPacket = CreateInputPacket();

        DontMelt.TickReturnPacket rPacket = dmGame.Tick(new DontMelt.TickInputPacket(iPacket, new DontMelt.Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)));

        if (rPacket.exceptions is not null && rPacket.exceptions.Count > 0)
        {
            Process.GetCurrentProcess().Kill();
        }
        epsilonFrameBuffer = rPacket.frameBuffer;
        base.Update(gameTime);
        Console.WriteLine(gameTime.ElapsedGameTime.Ticks);
    }
    protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
    {
        if (epsilonFrameBuffer is not null)
        {
            if (frameBuffer is null || frameBuffer.Width != epsilonFrameBuffer.width || frameBuffer.Height != epsilonFrameBuffer.height)
            {
                frameBuffer = new Microsoft.Xna.Framework.Graphics.Texture2D(GraphicsDevice, epsilonFrameBuffer.width, epsilonFrameBuffer.height);
            }
            if (frameColorBuffer is null || frameColorBuffer.Length != epsilonFrameBuffer.width * epsilonFrameBuffer.height)
            {
                frameColorBuffer = new Microsoft.Xna.Framework.Color[epsilonFrameBuffer.width * epsilonFrameBuffer.height];
            }

            int i = 0;
            for (int y = epsilonFrameBuffer.height - 1; y >= 0; y--)
            {
                for (int x = 0; x < epsilonFrameBuffer.width; x++)
                {
                    DontMelt.Color pixelColor = epsilonFrameBuffer.GetPixelUnsafe(x, y);
                    frameColorBuffer[i] = new Microsoft.Xna.Framework.Color(pixelColor.r, pixelColor.g, pixelColor.b, byte.MaxValue);
                    i++;
                }
            }
            frameBuffer.SetData(frameColorBuffer);
            spriteBatch.Begin(samplerState: Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp);
            spriteBatch.Draw(frameBuffer, new Microsoft.Xna.Framework.Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Microsoft.Xna.Framework.Color.White);
            spriteBatch.End();
        }
        else
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
        }
        base.Draw(gameTime);
    }
}