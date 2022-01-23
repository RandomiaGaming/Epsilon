using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public enum StageState { Initializing, Updating, Drawing, Removed };
    public sealed class Stage
    {
        #region Constants
        public static readonly Point ViewportSize = new Point(256, 144);
        public static double AspectRatio => (double)ViewportSize.Y / (double)ViewportSize.X;
        #endregion
        #region Variables
        private Epsilon _epsilon = null;
        private RenderTarget2D _renderTarget = null;
        private SpriteBatch _spriteBatch = null;
        private BasicEffect _basicEffect = null;
        private Point _cameraPosition = new Point(0, 0);
        private StageState _currentState = StageState.Initializing;
        private List<StageObject> _stageObjects = new List<StageObject>();
        private List<StageObject> _addStageObjectRequests = new List<StageObject>();
        private List<StageObject> _removeStageObjectRequests = new List<StageObject>();
        #endregion
        #region Properties
        public Epsilon Epsilon
        {
            get
            {
                return _epsilon;
            }
        }
        public Point CameraPosition
        {
            get
            {
                return _cameraPosition;
            }
            set
            {
                _cameraPosition = value;
            }
        }
        #endregion
        #region Constructors
        public Stage(Epsilon epsilon)
        {
            if (epsilon is null)
            {
                throw new Exception("epsilon cannot be null.");
            }

            _epsilon = epsilon;

            _renderTarget = new RenderTarget2D(_epsilon.GraphicsDevice, ViewportSize.X, ViewportSize.Y, false, SurfaceFormat.Color, DepthFormat.None);

            _spriteBatch = new SpriteBatch(_epsilon.GraphicsDevice);
            _spriteBatch.Name = "Stage SpriteBatch";
            _spriteBatch.Tag = null;

            _basicEffect = new BasicEffect(epsilon.GraphicsDevice);
            _basicEffect.LightingEnabled = false;
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.World = Matrix.CreateOrthographic(ViewportSize.X, ViewportSize.Y, 0, float.PositiveInfinity);
            _basicEffect.FogEnabled = false;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"Epsilon.Stage()";
        }
        #endregion
        #region Methods
        public void Initialize()
        {

        }
        public void Update()
        {
            SquashStageObjectQue();

            foreach (StageObject stageObject in _stageObjects)
            {
                stageObject.Update();
            }
        }
        public Texture2D Draw()
        {
            _epsilon.GraphicsDevice.SetRenderTarget(_renderTarget);

            _epsilon.GraphicsDevice.Clear(Color.Transparent);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            foreach (StageObject stageObject in _stageObjects)
            {
                stageObject.Render();
            }

            _spriteBatch.End();

            _epsilon.GraphicsDevice.SetRenderTarget(null);

            return _renderTarget;
        }
        public void DrawTexture(Texture2D texture, Point position, Color color)
        {
            position = new Point(position.X, position.Y);
            position = position - _cameraPosition;
            position = new Point(position.X, ViewportSize.Y - position.Y);
            position = new Point(position.X, position.Y - texture.Height);

            if (position.X + texture.Width < 0 || position.Y + texture.Height < 0 || position.X > ViewportSize.X || position.Y > ViewportSize.Y)
            {
                return;
            }

            _spriteBatch.Draw(texture, new Rectangle(position.X, position.Y, texture.Width, texture.Height), new Rectangle(0, 0, texture.Width, texture.Height), color, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }
        public void DrawBox(Rectangle rectangle, Color color)
        {
            Point min = new Point(rectangle.X - (ViewportSize.X / 2), rectangle.Y - (ViewportSize.Y / 2));
            min = min - CameraPosition;
            Point max = min + new Point(rectangle.Width, rectangle.Height);
            max = max - CameraPosition;

            if (min.X < 0 || min.Y < 0 || max.X > ViewportSize.X || max.Y > ViewportSize.Y)
            {
                return;
            }

            _basicEffect.CurrentTechnique.Passes[0].Apply();
            VertexPositionColor[] tris = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(min.X, min.Y, 0), color),
                new VertexPositionColor(new Vector3(min.X, max.Y, 0), color),
                new VertexPositionColor(new Vector3(max.X, max.Y, 0), color),

                new VertexPositionColor(new Vector3(min.X, min.Y, 0), color),
                new VertexPositionColor(new Vector3(max.X, max.Y, 0), color),
                new VertexPositionColor(new Vector3(max.X, min.Y, 0), color)
            };
            Epsilon.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, tris, 0, 2);
        }
        public void OnRemove()
        {

        }
        #endregion
        #region StageObject Management
        public StageObject GetStageObject(int index)
        {
            if (index < 0 || index >= _stageObjects.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _stageObjects[index];
        }
        public List<StageObject> GetStageObjects()
        {
            return new List<StageObject>(_stageObjects);
        }
        public int GetStageObjectCount()
        {
            return _stageObjects.Count;
        }
        public void RemoveStageObject(StageObject stageObject)
        {
            if (stageObject is null)
            {
                throw new Exception("StageObject was null.");
            }

            if (stageObject.Stage != this)
            {
                throw new Exception("StageObject belongs on a different Stage.");
            }

            for (int i = 0; i < _stageObjects.Count; i++)
            {
                if (_stageObjects[i] == stageObject)
                {
                    _removeStageObjectRequests.Add(stageObject);
                    return;
                }
            }
            throw new Exception("StageObject not found.");
        }
        public void AddStageObject(StageObject stageObject)
        {
            if (stageObject is null)
            {
                throw new Exception("StageObject was null.");
            }

            if (stageObject.Stage != this)
            {
                throw new Exception("StageObject belongs to a different Stage.");
            }

            foreach (StageObject _stageObject in _stageObjects)
            {
                if (_stageObject == stageObject)
                {
                    throw new Exception("StageObject was already added.");
                }
            }

            _addStageObjectRequests.Add(stageObject);
        }
        private void SquashStageObjectQue()
        {
            if (_stageObjects is null)
            {
                _stageObjects = new List<StageObject>();
            }
            else
            {
                if (_removeStageObjectRequests is not null)
                {
                    foreach (StageObject removeStageObjectRequest in _removeStageObjectRequests)
                    {
                        _stageObjects.Remove(removeStageObjectRequest);
                    }
                }
                _removeStageObjectRequests = new List<StageObject>();
            }
            if (_addStageObjectRequests is not null)
            {
                _stageObjects.AddRange(_addStageObjectRequests);
            }
            _addStageObjectRequests = new List<StageObject>();
        }
        #endregion
    }
}