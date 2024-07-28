namespace EpsilonEngine
{
    public sealed class ParticleSystem : Component
    {
        private class Particle
        {
            public int positionX;
            public int positionY;
            public double subPixelX;
            public double subPixelY;
            public double velocityX;
            public double velocityY;
            public long deathTime;
            public byte brightness;
            public Particle(int positionX, int positionY, double velocityX, double velocityY, long deathTime, byte brightness)
            {
                this.positionX = positionX;
                this.positionY = positionY;
                subPixelX = 0;
                subPixelY = 0;
                this.velocityX = velocityX;
                this.velocityY = velocityY;
                this.deathTime = deathTime;
                this.brightness = brightness;
            }
        }
        private System.Collections.Generic.List<Particle> _particles = new System.Collections.Generic.List<Particle>();
        private Texture _particleTexture = null;
        public double particleSpeed = 0.1f;
        public int particleLifeTime = 100;
        public double EmissionRate = 0;
        private double _timer = 0;
        private Microsoft.Xna.Framework.Vector2 _XNAPositionCache = Microsoft.Xna.Framework.Vector2.Zero;
        public ParticleSystem(GameObject gameObject, Texture particleTexture, int renderPriority) : base(gameObject, 0, renderPriority)
        {
            if (particleTexture is null)
            {
                throw new System.Exception("particleTexture cannot be null.");
            }
            _particleTexture = particleTexture;
        }
        public override string ToString()
        {
            return $"EpsilonEngine.ParticleSystem()";
        }
        protected override void Update()
        {
            _timer += EmissionRate;
            if (_timer < 0.5f)
            {
            }
            while (_timer >= 1)
            {
                _timer--;
                double rot = RandomnessHelper.NextDouble(0, MathHelper.Tau);
                _particles.Add(new Particle(0, 0, (double)System.Math.Cos(rot) * particleSpeed, (double)System.Math.Sin(rot) * particleSpeed, particleLifeTime, (byte)RandomnessHelper.NextInt(175, 255)));
            }
            for (int i = 0; i < _particles.Count; i++)
            {
                Particle particle = _particles[i];
                if (particle.deathTime <= 0)
                {
                    _particles.RemoveAt(i);
                    i--;
                }
                else
                {
                    particle.subPixelX += particle.velocityX;
                    particle.subPixelY += particle.velocityY;
                    int moveX = (int)particle.subPixelX;
                    int moveY = (int)particle.subPixelY;
                    particle.positionX += moveX;
                    particle.positionY += moveY;
                    particle.subPixelX -= (double)moveX;
                    particle.subPixelY -= (double)moveY;
                }
                particle.deathTime--;
            }
        }
        protected override void Render()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                Particle particle = _particles[i];
                int positionX = particle.positionX - Scene.CameraPositionX + GameObject.PositionX;
                int positionY = Scene.RenderHeight - particle.positionY + Scene.CameraPositionY - GameObject.PositionY - _particleTexture.Height;
                if (positionX < -_particleTexture.Width || positionY + _particleTexture.Height < 0 || positionX > Scene.RenderWidth || positionY > Scene.RenderHeight)
                {
                    return;
                }
                _XNAPositionCache.X = positionX;
                _XNAPositionCache.Y = positionY;
                Scene.XNASpriteBatch.Draw(_particleTexture._xnaTexture, _XNAPositionCache, new Microsoft.Xna.Framework.Color(particle.brightness, particle.brightness, particle.brightness, byte.MaxValue));
            }
        }
    }
}