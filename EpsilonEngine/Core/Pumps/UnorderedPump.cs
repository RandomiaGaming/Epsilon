namespace EpsilonEngine
{
    public sealed class UnorderedPump
    {
        #region Public Variables
        public int EventCount => _pumpEvents.Count;
        #endregion
        #region Private Variables
        private System.Collections.Generic.List<PumpEvent> _pumpEvents = new System.Collections.Generic.List<PumpEvent>();
        private PumpEvent[] _pumpEventCache = new PumpEvent[0];
        private bool _pumpEventCacheDirty = false;
        private bool _pumpEmpty = true;
        #endregion
        #region Public Methods
        public void Invoke()
        {
            if (_pumpEmpty)
            {
                return;
            }
            if (_pumpEventCacheDirty)
            {
                _pumpEventCache = _pumpEvents.ToArray();
                _pumpEventCacheDirty = false;
            }
            foreach (PumpEvent pumpEvent in _pumpEventCache)
            {
                pumpEvent.Invoke();
            }
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
            _pumpEventCacheDirty = true;
            _pumpEmpty = false;
        }
        public bool UnregisterPumpEvent(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new System.Exception("pumpEvent cannot be null.");
            }
            for (int i = 0; i < _pumpEvents.Count; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    _pumpEvents.RemoveAt(i);
                    _pumpEventCacheDirty = true;
                    if (_pumpEvents.Count is 0)
                    {
                        _pumpEmpty = true;
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Internal Methods
        internal void RegisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEvents.Add(pumpEvent);
            _pumpEventCacheDirty = true;
            _pumpEmpty = false;
        }
        internal void UnregisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            for (int i = 0; i < _pumpEvents.Count; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    _pumpEvents.RemoveAt(i);
                    _pumpEventCacheDirty = true;
                    if (_pumpEvents.Count is 0)
                    {
                        _pumpEmpty = true;
                    }
                    return;
                }
            }
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.UnorderedPump({_pumpEvents.Count})";
        }
        #endregion
    }
}