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
        private Microsoft.Xna.Framework.Graphics.RenderTarget2D renderTarget = null;
        private Point _cameraPosition = new Point(0, 0);
        private List<GameObject> _gameObjects = new List<GameObject>();
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

            renderTarget = new RenderTarget2D(epsilon.GraphicsDevice, ViewportSize.X, ViewportSize.Y);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"Epsilon.Stage({_name})";
        }
        #endregion
        #region Methods
        private Texture2D renderTexture;
        public void Initialize()
        {
            renderTexture = new Texture2D(_epsilon.GraphicsDevice, 10, 10);
            Color[] textureBuffer = new Color[renderTexture.Width * renderTexture.Height];
            int i = 0;
            for (int y = renderTexture.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < renderTexture.Width; x++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        textureBuffer[i] = new Color(255, 255, 255, 255);
                    }
                    else
                    {
                        textureBuffer[i] = new Color(0, 0, 0, 255);
                    }
                    i++;
                }
            }
            renderTexture.SetData(textureBuffer);
        }
        public void Update()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Update();
            }
        }
        public Texture2D Render()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Render();
            }

            return renderTexture;
        }
        #endregion
        #region GameObject Management
        public GameObject GetGameObject(int index)
        {
            if (index < 0 || index >= _gameObjects.Count)
            {
                throw new Exception("index was out of range.");
            }

            return _gameObjects[index];
        }
        public List<GameObject> GetGameObjects()
        {
            return new List<GameObject>(_gameObjects);
        }
        public int GetGameObjectCount()
        {
            return _gameObjects.Count;
        }
        #region Internal Methods
        internal void RemoveGameObject(GameObject gameObject)
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
        internal void AddGameObject(GameObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("GameObject was null.");
            }

            if (gameObject.Scene != this)
            {
                throw new Exception("GameObject belongs to a different Scene.");
            }

            foreach (GameObject _gameObject in _gameObjects)
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