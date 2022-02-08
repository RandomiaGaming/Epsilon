using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class Ground : GameObject
    {
        public Ground(StagePlayer stagePlayer) : base(stagePlayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ground.png"));
        }
        public override string ToString()
        {
            return $"Epsilon.Ground()";
        }
    }
}
