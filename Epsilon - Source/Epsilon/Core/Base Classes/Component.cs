using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Epsilon
{
    public abstract class Component
    {
        #region Variables
        private StageObject _stageObject = null;
        public string _name = "Unnamed Component";
        #endregion
        #region Properties
        public Epsilon Epsilon
        {
            get
            {
                return _stageObject.Epsilon;
            }
        }
        public Stage Stage
        {
            get
            {
                return _stageObject.Stage;
            }
        }
        public StageObject StageObject
        {
            get
            {
                return _stageObject;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        #endregion
        #region Constructors
        public Component(StageObject gameObject)
        {
            if (gameObject is null)
            {
                throw new Exception("gameObject cannot be null.");
            }

            _stageObject = gameObject;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.Component({_name})";
        }
        #endregion
        #region Methods
        public void Update()
        {
            OnUpdate();
        }
        public void Render()
        {
            OnRender();
        }
        #endregion
        #region Overridables
        protected virtual void OnUpdate()
        {

        }
        protected virtual void OnRender()
        {

        }
        #endregion
    }
}