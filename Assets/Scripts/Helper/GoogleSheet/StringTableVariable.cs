using System;
using Sirenix.OdinInspector;

namespace Helper
{
    [Serializable]
    public class StringTableVariable
    {
    	[HideLabel]
    	[VerticalGroup("Key")]
    	[PropertyOrder(1)]
    	public string key;

    	[HideLabel]
    	[VerticalGroup("Value")]
    	[PropertyOrder(2)]
    	public string value;
    }
}
