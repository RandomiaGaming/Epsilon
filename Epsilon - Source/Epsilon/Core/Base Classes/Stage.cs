using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public sealed class Stage
    {
        #region Constants
        public static readonly Point ViewportSize = new Point(256, 144);
        public static double AspectRatio => ViewportSize.Y / (double)ViewportSize.X;
        #endregion
        #region Variables
        private RenderTarget2D _renderTarget = null;
        private SpriteBatch _stageSpriteBatch = null;
        private Point _cameraPosition = new Point(0, 0);
        private List<StageObject> _gameObjects = new List<StageObject>();
        private Epsilon _epsilon = null;
        public string _name = "Unnamed Stage";
        #endregion
        #region Properties
        public Epsilon Epsilon
        {
            get
            {
                return _epsilon;
            }
        }
        public string Name
        {
            get
            {
                return _name;
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

            _stageSpriteBatch = new SpriteBatch(_epsilon.GraphicsDevice);
            _stageSpriteBatch.Name = "Stage SpriteBatch";
            _stageSpriteBatch.Tag = null;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"Epsilon.Stage({_name})";
        }
        #endregion
        #region Methods
        public void Initialize()
        {
            _gameObjects.Add(new StageObject(this));
        }
        public void Update()
        {
            foreach (StageObject gameObject in _gameObjects)
            {
                gameObject.Update();
            }
        }
        public Texture2D Render()
        {
            _epsilon.GraphicsDevice.SetRenderTarget(_renderTarget);

            _stageSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, null);

            foreach (StageObject gameObject in _gameObjects)
            {
                Point gameObjectOffset = gameObject.Position - _cameraPosition;
                List<DrawInstruction> gameObjectRender = gameObject.Render();
                foreach (DrawInstruction drawInstruction in gameObjectRender)
                {
                    Point spritePosition = gameObjectOffset + drawInstruction.Offset;
                    _stageSpriteBatch.Draw(drawInstruction.Texture, new Rectangle(spritePosition.X, spritePosition.Y, drawInstruction.Texture.Width, drawInstruction.Texture.Height), new Rectangle(0, 0, drawInstruction.Texture.Width, drawInstruction.Texture.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                }
            }

            _stageSpriteBatch.End();

            _epsilon.GraphicsDevice.SetRenderTarget(null);

            return _renderTarget;
        }
        #endregion
        #region GameObject Management
        public StageObject GetGameObject(int index)
        {
            if (index < 0 || index >= _gameObjects.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _gameObjects[index];
        }
        public List<StageObject> GetGameObjects()
        {
            return new List<StageObject>(_gameObjects);
        }
        public int GetGameObjectCount()
        {
            return _gameObjects.Count;
        }
        #region Internal Methods
        internal void RemoveGameObject(StageObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("GameObject was null.");
            }

            if (gameObject.Scene != this)
            {
                throw new Exception("GameObject belongs on a different Scene.");
            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] == gameObject)
                {
                    _gameObjects.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("GameObject not found.");
        }
        internal void AddGameObject(StageObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("GameObject was null.");
            }

            if (gameObject.Scene != this)
            {
                throw new Exception("GameObject belongs to a different Scene.");
            }

            foreach (StageObject _gameObject in _gameObjects)
            {
                if (_gameObject == gameObject)
                {
                    throw new Exception("GameObject was already added.");
                }
            }

            _gameObjects.Add(gameObject);

            gameObject.Initialize();
        }
        #endregion
        #endregion
    }
}