using System.Reflection;
using EpsilonEngine;
namespace Epsilon
{
    public sealed class Ground : GameObject
    {
        public Ground(StagePlayer stagePlayer, PhysicsManager physicsManager) : base(stagePlayer)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this);
            textureRenderer.Texture = new Texture(Game, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ground.png"));
            Collider collider = new Collider(this, physicsManager, 0);
            collider.Rect = new Rectangle(0, 0, 15, 15);
        }
        public override string ToString()
        {
            return $"Epsilon.Ground()";
        }
    }
}
