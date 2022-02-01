using System.Reflection;
namespace EpsilonEngine
{
    public sealed class Ground : GameObject
    {
        public Ground(Scene stage, PhysicsManager physicsManager) : base(stage)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this, new Texture(Scene.Engine, Assembly.GetCallingAssembly().GetManifestResourceStream("Epsilon.Epsilon.Textures.Item_Textures.Ground.png")));
            Collider collider = new Collider(this, physicsManager, 1);
            collider.shape = new Rectangle(0, 0, 15, 15);
        }
    }
}
