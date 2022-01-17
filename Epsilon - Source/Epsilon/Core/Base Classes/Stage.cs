using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public sealed class Stage
    {
        #region Constants
        public static readonly Color BackgroundColor = new Color(byte.MaxValue, (byte)150, byte.MaxValue, byte.MaxValue);
        public static readonly Point ViewportSize = new Point(256, 144);
        public static double AspectRatio => (double)ViewportSize.Y / (double)ViewportSize.X;
        #endregion
        #region Variables
        private Epsilon _epsilon = null;
        private RenderTarget2D _renderTarget = null;
        private SpriteBatch _stageSpriteBatch = null;
        private Point _cameraPosition = new Point(0, 0);
        private List<StageObject> _stageObjects = new List<StageObject>();
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

            _stageSpriteBatch = new SpriteBatch(_epsilon.GraphicsDevice);
            _stageSpriteBatch.Name = "Stage SpriteBatch";
            _stageSpriteBatch.Tag = null;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"Epsilon.Stage()";
        }
        #endregion
        #region Methods
        public void Update()
        {
            foreach (StageObject stageObject in _stageObjects)
            {
                stageObject.Update();
            }
        }
        public Texture2D Render()
        {
            _epsilon.GraphicsDevice.SetRenderTarget(_renderTarget);

            _epsilon.GraphicsDevice.Clear(BackgroundColor);

            _stageSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            int stageObjectsCount = _stageObjects.Count;
            for (int i = 0; i < stageObjectsCount; i++)
            {
                StageObject stageObject = _stageObjects[i];
                Point gameObjectOffset = stageObject.Position - _cameraPosition;
                List<DrawInstruction> drawInstructions = stageObject.Render();
                int drawInstructionsCount = drawInstructions.Count;
                for (int i2 = 0; i2 < drawInstructionsCount; i2++)
                {
                    DrawInstruction drawInstruction = drawInstructions[i2];
                    Texture2D drawInstructionTexture = drawInstruction.Texture;
                    Point drawInstructionPosition = gameObjectOffset + drawInstruction.Offset;
                    _stageSpriteBatch.Draw(drawInstructionTexture, new Rectangle(drawInstructionPosition.X, drawInstructionPosition.Y, drawInstructionTexture.Width, drawInstructionTexture.Height), new Rectangle(0, 0, drawInstructionTexture.Width, drawInstructionTexture.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
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
            if (index < 0 || index >= _stageObjects.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _stageObjects[index];
        }
        public List<StageObject> GetGameObjects()
        {
            return new List<StageObject>(_stageObjects);
        }
        public int GetGameObjectCount()
        {
            return _stageObjects.Count;
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

            for (int i = 0; i < _stageObjects.Count; i++)
            {
                if (_stageObjects[i] == gameObject)
                {
                    _stageObjects.RemoveAt(i);
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

            foreach (StageObject _gameObject in _stageObjects)
            {
                if (_gameObject == gameObject)
                {
                    throw new Exception("GameObject was already added.");
                }
            }

            _stageObjects.Add(gameObject);
        }
        #endregion
        #endregion
    }
}