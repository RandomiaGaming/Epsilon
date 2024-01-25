using System.Reflection;
using EpsilonEngine;

namespace Epsilon
{
    public sealed class PauseMenu : Canvas
    {
        public PauseMenu(Epsilon epsilon) : base(epsilon)
        {
            Texture pauseButtonTexture = new Texture(epsilon, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.UI_Textures.Pause.png"));

            Image pauseButton = new Image(this, pauseButtonTexture);
            pauseButton.LocalMinX = 0.9f;
            pauseButton.LocalMinY = 0.82222222222f;
            pauseButton.LocalMaxY = 1.0f;
            pauseButton.LocalMaxY = 1.0f;
        }
    }
}
