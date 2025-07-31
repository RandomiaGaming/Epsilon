using EpsilonEngine;
namespace DMCCR
{
    public sealed class Checkpoint : PhysicsObject
    {
        private Stage _stagePlayer;
        private TextureRenderer _textureRenderer = null;
        private Texture _checkPointLockedTexture = null;
        private Texture _checkPointUnlockedTexture = null;
        public Checkpoint(Stage stagePlayer, PhysicsLayer physicsLayer) : base(stagePlayer, false)
        {
            _stagePlayer = stagePlayer;
            _checkPointLockedTexture = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.Checkpoint Locked.png"));
            _checkPointUnlockedTexture = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.Checkpoint Unlocked.png"));
            _textureRenderer = new TextureRenderer(this, 1);
            _textureRenderer.Texture = _checkPointLockedTexture;
            SetColliderShape(new Rect[1] { new Rect(0, 0, 15, 31) });
            CollisionPhysicsLayers = physicsLayer;
            LogOverlaps = true;
        }
        protected override void Update()
        {
            if (Position == _stagePlayer.CheckPointPos)
            {
                _textureRenderer.Texture = _checkPointUnlockedTexture;
            }
            else
            {
                _textureRenderer.Texture = _checkPointLockedTexture;
            }
            foreach (PhysicsObject physicsObject in _overlaps)
            {
                if (physicsObject.GetType() == typeof(Player))
                {
                    _stagePlayer.CheckPointPos = Position;
                }
            }
        }
        public override string ToString()
        {
            return $"DMCCR.Checkpoint()";
        }
    }
}