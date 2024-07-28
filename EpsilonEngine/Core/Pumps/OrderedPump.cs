namespace EpsilonEngine
{
    public sealed class OrderedPump
    {
        #region Public Variables
        public int EventCount => _pumpEvents.Count;
        #endregion
        #region Private Variables
        private System.Collections.Generic.List<PumpEvent> _pumpEvents = new System.Collections.Generic.List<PumpEvent>();
        private System.Collections.Generic.List<int> _invokeOrder = new System.Collections.Generic.List<int>();
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
        public void RegisterPumpEvent(PumpEvent pumpEvent, int invokePriority)
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
            _pumpEventCacheDirty = true;
            _pumpEmpty = false;
            for (int i = 0; i < _pumpEvents.Count; i++)
            {
                if (invokePriority > _invokeOrder[i])
                {
                    _invokeOrder.Insert(i, invokePriority);
                    _pumpEvents.Insert(i, pumpEvent);
                    return;
                }
            }
            _invokeOrder.Add(invokePriority);
            _pumpEvents.Add(pumpEvent);
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
                    _invokeOrder.RemoveAt(i);
                    _pumpEvents.RemoveAt(i);
                    _pumpEventCacheDirty = true;
                    if (_pumpEvents.Count is 0)
                    {
                        _pumpEmpty = true;
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
        #region Internal Methods
        internal void RegisterPumpEventUnsafe(PumpEvent pumpEvent, int invokePriority)
        {
            _pumpEventCacheDirty = true;
            _pumpEmpty = false;
            for (int i = 0; i < _pumpEvents.Count; i++)
            {
                if (invokePriority > _invokeOrder[i])
                {
                    _invokeOrder.Insert(i, invokePriority);
                    _pumpEvents.Insert(i, pumpEvent);
                    return;
                }
            }
            _invokeOrder.Add(invokePriority);
            _pumpEvents.Add(pumpEvent);
        }
        internal bool UnregisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            for (int i = 0; i < _pumpEvents.Count; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    _invokeOrder.RemoveAt(i);
                    _pumpEvents.RemoveAt(i);
                    _pumpEventCacheDirty = true;
                    if (_pumpEvents.Count is 0)
                    {
                        _pumpEmpty = true;
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.OrderedPump({_pumpEvents.Count})";
        }
        #endregion
    }
}