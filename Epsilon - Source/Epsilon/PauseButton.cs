using EpsilonEngine;
using System.Reflection;
namespace Epsilon
{
    public sealed class PauseButton : Element
    {
        public PauseButton(TestCanvas testCanvas, float minX, float minY, float maxX, float maxY) : base(testCanvas)
        {
            Image image = new Image(this);
            image.Texture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.UI_Textures.Font.png"));
            LocalMinX = minX;
            LocalMinY = minY;
            LocalMaxX = maxX;
            LocalMaxY = maxY;
        }
    }
}
