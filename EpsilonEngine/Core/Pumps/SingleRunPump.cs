namespace EpsilonEngine
{
    public sealed class SingleRunPump
    {
        #region Public Variables
        public int EventCount => _pumpEvents.Count;
        #endregion
        #region Private Variables
        private System.Collections.Generic.List<PumpEvent> _pumpEvents = new System.Collections.Generic.List<PumpEvent>();
        private bool _pumpEmpty = true;
        #endregion
        #region Public Methods
        public void Invoke()
        {
            if (_pumpEmpty)
            {
                return;
            }
            for (int i = 0; i < _pumpEvents.Count; i++)
            {
                _pumpEvents[i].Invoke();
            }
            _pumpEvents = new System.Collections.Generic.List<PumpEvent>();
            _pumpEmpty = true;
        }
        public void RegisterPumpEvent(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new System.Exception("pumpEvent cannot be null.");
            }
            for (int i = 0; i < _pumpEvents.Count; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    throw new System.Exception("pumpEvent has already been added to this pump.");
                }
            }
            _pumpEvents.Add(pumpEvent);
            _pumpEmpty = false;
        }
        #endregion
        #region Internal Methods
        public void RegisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEvents.Add(pumpEvent);
            _pumpEmpty = true;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SingleRunPump({_pumpEvents.Count})";
        }
        #endregion
    }
}