using System;
using EpsilonEngine;

namespace Epsilon
{
    public sealed class TestCanvas : Canvas
    {
        public TestCanvas(Epsilon epsilon) : base(epsilon)
        {
            Texture pixelTexture = new Texture(Game, 1, 1, new Color[1] { Color.White });

            Element mainPanel = new Element(this);
            mainPanel.LocalMinX = 0f;
            mainPanel.LocalMinY = 0f;
            mainPanel.LocalMaxX = 1f;
            mainPanel.LocalMaxY = 1f;


            Element navbar = new Element(this, mainPanel);
            navbar.LocalMinX = 0f;
            navbar.LocalMinY = 0.9f;
            navbar.LocalMaxX = 1f;
            navbar.LocalMaxY = 1f;

            Image navbarImage = new Image(navbar);
            navbarImage.Color = Color.SoftRed;
            navbarImage.Texture = pixelTexture;

            Element pauseButton = new Element(this, navbar);
            pauseButton.LocalMinX = 0.9f;
            pauseButton.LocalMinY = 0f;
            pauseButton.LocalMaxX = 1f;
            pauseButton.LocalMaxY = 1f;

            Image pauseButtonImage = new Image(pauseButton);
            pauseButtonImage.Color = Color.SoftYellow;
            pauseButtonImage.Texture = pixelTexture;
        }
    }
}
