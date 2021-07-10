using UnityEngine;

namespace Helper
{
    /// http://wiki.unity3d.com/index.php/Singleton
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
    	private static T instance;
    	private static object mutex = new object();
    	private static bool applicationIsQuitting = false;

    	public static T Instance
    	{
    		get
    		{
    			if (applicationIsQuitting)
    			{
    				return null;
    			}

    			lock (mutex)
    			{
    				if (instance == null)
    				{
    					var founds = FindObjectsOfType(typeof(T));
    					if (founds.Length > 1)
    					{
    						return null;
    					}
    					else if (founds.Length > 0)
    					{
    						instance = (T)founds[0];

    						DontDestroyOnLoad(instance.gameObject);
    					}
    					else
    					{
    						GameObject singleton = new GameObject();
    						instance = singleton.AddComponent<T>();
    						singleton.name = "(Singleton) " + typeof(T).ToString();

    						DontDestroyOnLoad(singleton);
    					}
    				}

    				return instance;
    			}
    		}
    	}

    	protected virtual void OnDestroy()
    	{
    	}

    	protected virtual void OnApplicationQuit()
    	{
    		applicationIsQuitting = true;
    	}
    }
}
