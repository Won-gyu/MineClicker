using System;
using System.Collections;
using System.Collections.Generic;

namespace Helper
{
    ///Used for events
    public class EventData
    {
        public string name { get; private set; }
        public object value { get { return GetValue(); } }
        virtual protected object GetValue() { return null; }
        public EventData(string name) {
            this.name = name;
        }
    }

    ///Used for events with a value
    public class EventData<T> : EventData
    {
        new public T value { get; private set; }
        protected override object GetValue() { return value; }
        public EventData(string name, T value) : base(name) {
            this.value = value;
        }
    }

	public static class MessageDispatcher
	{
		public delegate void EventDelegate(EventData eventData);

		private static Dictionary<string, EventDelegate> delegates = new Dictionary<string, EventDelegate>();

		public static void Subscribe(string eventName, EventDelegate del)
		{
			EventDelegate tempDel;
			if (delegates.TryGetValue(eventName, out tempDel))
				delegates[eventName] = tempDel += del;
			else
				delegates[eventName] = del;
		}

		public static void UnSubscribe(string eventName, EventDelegate del)
		{
			EventDelegate tempDel;
			if (delegates.TryGetValue(eventName, out tempDel))
			{
				tempDel -= del;
				if (tempDel == null)
					delegates.Remove(eventName);
				else
					delegates[eventName] = tempDel;
			}
		}

		public static void Dispatch(string eventName, EventData eventData = null)
		{
			EventDelegate del;

			if (delegates.TryGetValue(eventName, out del))
				del.Invoke(eventData);
		}
	}
}
