using EpsilonEngine;
namespace DMCCR
{
    public sealed class GoalGate : PhysicsObject
    {
        private Stage _stagePlayer;

        public GoalGate(Stage stagePlayer, PhysicsLayer physicsLayer) : base(stagePlayer, false)
        {
            _stagePlayer = stagePlayer;

            Texture goalGateTexture = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.GoalGate.png"));

            TextureRenderer textureRenderer = new TextureRenderer(this, 1);

            textureRenderer.Texture = goalGateTexture;

            SetColliderShape(new Rect[1] { new Rect(0, 0, 15, 31) });

            CollisionPhysicsLayers = physicsLayer;

            LogOverlaps = true;
        }
        public override string ToString()
        {
            return $"DMCCR.GoalGate()";
        }
    }
}