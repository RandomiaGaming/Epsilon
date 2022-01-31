using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace EpsilonEngine
{
    public sealed class ParticleSystem : Component
    {
        private class Particle
        {
            public int positionX;
            public int positionY;

            public float subPixelX;
            public float subPixelY;

            public float velocityX;
            public float velocityY;

            public int lifetime;

            public Color color;
            public Particle(int positionX, int positionY, float velocityX, float velocityY, Color color, int lifetime)
            {
                this.positionX = positionX;
                this.positionY = positionY;

                this.subPixelX = 0;
                this.subPixelY = 0;

                this.velocityX = velocityX;
                this.velocityY = velocityY;

                this.color = color;

                this.lifetime = lifetime;
            }
        }
        private List<Particle> _particles = new List<Particle>();
        private Texture _particleTexture = null;
        public float EmissionRate = 0;
        private float _timer = 0;
        public bool UseWorldSpace = false;

        public ParticleSystem(GameObject gameObject, Texture particleTexture) : base(gameObject)
        {
            if(particleTexture is null)
            {
                throw new Exception("particleTexture cannot be null.");
            }

            _particleTexture = particleTexture;
        }
        protected override void Update()
        {
            _timer += EmissionRate;
            while(_timer >= 1)
            {
                _timer--;
                double rot = RandomnessHelper.NextDouble(0, Math.PI * 2);
                _particles.Add(new Particle(0, 0, (float)Math.Cos(rot) * 0.1f, (float)Math.Sin(rot) * 0.1f, new Color((byte)RandomnessHelper.NextInt(0, 255), (byte)RandomnessHelper.NextInt(0, 255), (byte)RandomnessHelper.NextInt(0, 255), (byte)255), 1000));
            }
            for (int i = 0; i < _particles.Count; i++)
            {
                Particle particle = _particles[i];
                if (particle.lifetime <= 0)
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

                    particle.subPixelX -= (float)moveX;
                    particle.subPixelY -= (float)moveY;
                }
                particle.lifetime--;
            }
        }
        protected override void Render()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                Particle particle = _particles[i];
                if (UseWorldSpace)
                {
                    Scene.DrawTexture(_particleTexture, new Point(particle.positionX, particle.positionY), particle.color);
                }
                else
                {
                    GameObject.DrawTexture(_particleTexture, new Point(particle.positionX, particle.positionY), particle.color);
                }
            }
        }
    }
}
