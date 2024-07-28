using EpsilonEngine;
namespace DMCCR
{
    public sealed class FPSCounter : SceneManager
    {
        private Texture Font0;
        private Texture Font1;
        private Texture Font2;
        private Texture Font3;
        private Texture Font4;
        private Texture Font5;
        private Texture Font6;
        private Texture Font7;
        private Texture Font8;
        private Texture Font9;
        public FPSCounter(Scene scene) : base(scene, 0, int.MaxValue)
        {
            Font0 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.0.png"));
            Font1 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.1.png"));
            Font2 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.2.png"));
            Font3 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.3.png"));
            Font4 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.4.png"));
            Font5 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.5.png"));
            Font6 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.6.png"));
            Font7 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.7.png"));
            Font8 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.8.png"));
            Font9 = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.9.png"));
        }
        protected override void Render()
        {
            string currentFPSString = Game.CurrentFPS.ToString();
            char[] currentFPSChars = currentFPSString.ToCharArray();
            int currentFPSCharsLength = currentFPSChars.Length;
            int offset = 0;
            for (int i = 0; i < currentFPSCharsLength; i++)
            {
                char c = currentFPSChars[i];
                if (c == '0')
                {
                    Scene.DrawTextureScreenSpace(Font0, new Point(offset, 0), Color.White);
                }
                else if (c == '1')
                {
                    Scene.DrawTextureScreenSpace(Font1, new Point(offset, 0), Color.White);
                }
                else if (c == '2')
                {
                    Scene.DrawTextureScreenSpace(Font2, new Point(offset, 0), Color.White);
                }
                else if (c == '3')
                {
                    Scene.DrawTextureScreenSpace(Font3, new Point(offset, 0), Color.White);
                }
                else if (c == '4')
                {
                    Scene.DrawTextureScreenSpace(Font4, new Point(offset, 0), Color.White);
                }
                else if (c == '5')
                {
                    Scene.DrawTextureScreenSpace(Font5, new Point(offset, 0), Color.White);
                }
                else if (c == '6')
                {
                    Scene.DrawTextureScreenSpace(Font6, new Point(offset, 0), Color.White);
                }
                else if (c == '7')
                {
                    Scene.DrawTextureScreenSpace(Font7, new Point(offset, 0), Color.White);
                }
                else if (c == '8')
                {
                    Scene.DrawTextureScreenSpace(Font8, new Point(offset, 0), Color.White);
                }
                else
                {
                    Scene.DrawTextureScreenSpace(Font9, new Point(offset, 0), Color.White);
                }
                offset += 11;
            }
        }
    }
}