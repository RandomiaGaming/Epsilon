using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace EpsilonEngine
{
    public sealed class Ground : GameObject
    {
        public Ground(Scene stage) : base(stage)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this, new Texture(Engine, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\Ground.png"));
            Collider collider = new Collider(this);
            collider.shape = new Rectangle(0, 0, 15, 15);
        }
    }
}
