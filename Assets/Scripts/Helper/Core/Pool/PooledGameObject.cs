using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Helper
{
    public interface IPooledGameObject
    {
        void ReturnToPool();
		void Activate();
    }

    [AddComponentMenu("Func/Pooled GameObject")]
    public class PooledGameObject : MonoBehaviour, IPooledGameObject
    {
    	public GameObjectPool Pool;

		public System.Action<PooledGameObject> onReturnToPoolCallback = null;

		[BoxGroup("AutoReturnToPool")]
		public bool enabledAutoReturnToPool = false;
		[BoxGroup("AutoReturnToPool")]
		public float time;

    	public virtual void ReturnToPool()
    	{
			if (onReturnToPoolCallback != null)
			{
				onReturnToPoolCallback(this);
				onReturnToPoolCallback = null;
			}

    		if (Pool)
    		{
    			Pool.AddObject(this);
    		}
    		else
    		{
    			Debug.LogWarning("[ObjectPool] PooledObject has not pool.");
				
    			Destroy(gameObject);
    		}
    	}

		public virtual void Activate()
		{
			gameObject.SetActive(true);
		}

		private Coroutine coroutine = null;
		private IEnumerator IWaitForSeconds()
		{
			yield return new WaitForSeconds(time);

			ReturnToPool();	
		}

		private void OnEnable()
		{
			if (enabledAutoReturnToPool)
			{
				if (coroutine != null)
					StopCoroutine(coroutine);
				coroutine = StartCoroutine(IWaitForSeconds());
			}
		}
    }
}
