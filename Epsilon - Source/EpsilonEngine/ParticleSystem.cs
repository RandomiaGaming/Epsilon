using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace EpsilonEngine
{
    public sealed class ParticleSystem : GameObject
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
        private Texture2D _particleTexture = null;
        public ParticleSystem(Scene stage) : base(stage)
        {
            _particleTexture = Texture2D.FromFile(stage.Engine.GraphicsDevice, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\Player.png");
        }
        protected override void Update()
        {
            for (int i = 0; i < 1; i++)
            {
                double rot = RandomnessHelper.NextDouble() * Math.PI * 2.0;
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
            Console.WriteLine(_particles.Count);
        }
        protected override void Render()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                Particle particle = _particles[i];
                base.DrawTexture(_particleTexture, new Point(particle.positionX, particle.positionY), particle.color);
            }
        }
    }
}
