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
        public static readonly Color BackgroundColor = new Color(byte.MaxValue, (byte)150, byte.MaxValue, byte.MaxValue);
        public static readonly Point ViewportSize = new Point(256 * 2, 144 * 2);
        public static double AspectRatio => (double)ViewportSize.Y / (double)ViewportSize.X;
        #endregion
        #region Variables
        private Epsilon _epsilon = null;
        private RenderTarget2D _renderTarget = null;
        private SpriteBatch _stageSpriteBatch = null;
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

            _epsilon.GraphicsDevice.Clear(BackgroundColor);

            _stageSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            foreach (StageObject stageObject in _stageObjects)
            {
                foreach (DrawInstruction drawInstruction in stageObject.Render())
                {
                    Point texturePosition = stageObject.Position;
                    texturePosition = texturePosition + drawInstruction.Offset;
                    texturePosition = texturePosition - CameraPosition;
                    Texture2D drawInstructionTexture = drawInstruction.Texture;
                    texturePosition = new Point(texturePosition.X, ViewportSize.Y - texturePosition.Y);
                    texturePosition = new Point(texturePosition.X, texturePosition.Y - drawInstructionTexture.Height);
                    _stageSpriteBatch.Draw(drawInstructionTexture, new Rectangle(texturePosition.X, texturePosition.Y, drawInstructionTexture.Width, drawInstructionTexture.Height), new Rectangle(0, 0, drawInstructionTexture.Width, drawInstructionTexture.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                }
            }

            _stageSpriteBatch.End();

            _epsilon.GraphicsDevice.SetRenderTarget(null);

            return _renderTarget;
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
            if(_addStageObjectRequests is not null)
            {
                _stageObjects.AddRange(_addStageObjectRequests);
            }
            _addStageObjectRequests = new List<StageObject>();
        }
        #endregion

    }
}