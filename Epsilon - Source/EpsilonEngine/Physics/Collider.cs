using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace EpsilonEngine
{
    public sealed class Collider : Component
    {
        public Rectangle shape = new Rectangle(Point.Zero, new Point(16, 16));
        private PhysicsManager _physicsManager = null;
        private Texture _theGreatPixel = null;
        public Collider(GameObject gameObject) : base(gameObject)
        {
            _theGreatPixel = new Texture(Engine, 8, 8);
            _theGreatPixel.SetData(new Color[] { Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black });
            _theGreatPixel.Apply();
        }
        protected override void Initialize()
        {
            _physicsManager = Scene.GetSceneManager<PhysicsManager>();

            if (_physicsManager is null)
            {
                throw new Exception("Cannot use physics components in a scene without a physics manager.");
            }

            _physicsManager._managedColliders.Add(this);
        }
        public Rectangle GetWorldShape()
        {
            return new Rectangle(shape.X + GameObject.Position.X, shape.Y + GameObject.Position.Y, shape.Width, shape.Height);
        }
        protected override void Render()
        {
            Scene.DrawTexture(_theGreatPixel, GetWorldShape().Location, Color.White);
        }
    }
}