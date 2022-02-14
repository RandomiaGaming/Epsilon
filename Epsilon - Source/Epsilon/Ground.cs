using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class Ground : PhysicsObject
    {
        public Ground(StagePlayer stagePlayer, PhysicsLayer physicsLayer, Texture groundTexture) : base(stagePlayer, physicsLayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = groundTexture;
        }
        public override string ToString()
        {
            return $"Epsilon.Ground()";
        }
    }
}
