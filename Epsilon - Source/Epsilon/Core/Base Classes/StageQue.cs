namespace Epsilon
{
    public sealed class StageQue
    {
        private Stage _quedStage = null;
        private Stage _currentStage = null;
        private bool _queClear = true;
        public StageQue(Stage initialValue)
        {
            _quedStage = initialValue;
            _currentStage = initialValue;
            _queClear = true;
        }
        public Stage CurrentStage
        {
            get
            {
                return _currentStage;
            }
        }
        public Stage QuedStage
        {
            get
            {
                return _quedStage;
            }
            set
            {
                if (_currentStage == value)
                {
                    return;
                }
                _quedStage = value;
                _queClear = false;
            }
        }
        public bool QueClear
        {
            get
            {
                return _queClear;
            }
        }
        public void SquashQue()
        {
            if (!_queClear)
            {
                if (_currentStage is not null)
                {
                    _currentStage.OnRemove();
                }
                if (_quedStage is not null)
                {
                    _quedStage.OnAdd();
                }
                _currentStage = _quedStage;
                _queClear = true;
            }
        }
        public override string ToString()
        {
            if (_currentStage is null)
            {
                return $"Epsilon.StageQue(null)";
            }
            return $"Epsilon.StageQue()";
        }
    }
}
