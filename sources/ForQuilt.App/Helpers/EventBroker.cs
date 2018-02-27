//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace ForQuilt.App.Helpers
{
    class EventBroker
    {
        public event EventHandler OnDoIt;

        public virtual void DoIt()
        {
            var handler = OnDoIt;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnApplicationExit;
        public event EventHandler OnImportImageFromCameraViewExit;
        public event EventHandler<ActionResultEventArgs> OnActionResult;

        private readonly IDictionary<EventHandler, bool> _eventInProcessList = new Dictionary<EventHandler, bool>();
        private static readonly Object SyncRoot = new Object();
        private static volatile EventBroker _instance;

        public static EventBroker Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new EventBroker();
                        }
                    }
                }
                return _instance;
            }
        }

        private void FireEvent(EventHandler eventHandler)
        {
            try
            {
                if (eventHandler == null)
                {
                    return;
                }
                lock (_eventInProcessList)
                {
                    if (!_eventInProcessList.ContainsKey(eventHandler))
                    {
                        _eventInProcessList.Add(eventHandler, false);
                    }
                    if (_eventInProcessList[eventHandler])
                    {
                        return;
                    }
                    _eventInProcessList[eventHandler] = true;
                }
                eventHandler(this, EventArgs.Empty);
            }
            finally
            {
                lock (_eventInProcessList)
                {
                    if (eventHandler != null && _eventInProcessList.ContainsKey(eventHandler))
                    {
                        _eventInProcessList[eventHandler] = false;
                    }
                }
            }
        }

        public void ApplicationExit()
        {
            FireEvent(OnApplicationExit);
        }

        public void ImportImageFromCameraViewExit()
        {
            FireEvent(OnImportImageFromCameraViewExit);
        }

        public void ActionResult(string actionName, string format, params object[] args)
        {
            if (OnActionResult != null)
            {
                OnActionResult(null, new ActionResultEventArgs(actionName, format, args));
            }
        }
    }

    internal class ActionResultEventArgs : EventArgs
    {
        public string ActionName { get; private set; }
        public string Message { get; private set; }

        public ActionResultEventArgs(string actionName, string format, params object[] args)
        {
            ActionName = actionName;
            Message = string.Format(format, args);
        }
    }
}
