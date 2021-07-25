using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public interface IGameObjectPool
    {
        void CreatePool();
        void ClearPool();
        GameObject GetObject();
    }

    [AddComponentMenu("Func/GameObjectPool")]
    public class GameObjectPool : MonoBehaviour, IGameObjectPool
    {
        public GameObject prefab;
        public int poolSize;

        public List<PooledGameObject> AvaliableObjects = new List<PooledGameObject>();

        private void Awake()
        {
            if (prefab != null)
                CreatePool();
        }

        public void CreatePool()
        {
            while (AvaliableObjects.Count < poolSize)
                AddObject(CreateObject());
        }

        public virtual void ClearPool()
        {
            for (int i = 0; i < AvaliableObjects.Count; ++i)
                DestroyImmediate(AvaliableObjects[i].gameObject);
            AvaliableObjects.Clear();
        }

        public GameObject GetObject()
        {
            if (AvaliableObjects.Count == 0)
                AddObject(CreateObject());

            var po = AvaliableObjects[AvaliableObjects.Count - 1];
            po.Activate();

            AvaliableObjects.RemoveAt(AvaliableObjects.Count - 1);
            
            return po.gameObject;
        }

        public void AddObject(PooledGameObject po)
        {
            if (AvaliableObjects.Contains(po))
            {
                Debug.LogError("AddObject Twise Error");
            }
            else
            {
                po.gameObject.SetActive(false);
                po.transform.SetParent(transform, false);
                AvaliableObjects.Add(po);
            }
        }

        protected PooledGameObject CreateObject()
        {
            var go = Instantiate(prefab) as GameObject;
            go.name = prefab.name;
            var po = go.GetComponent<PooledGameObject>();
            po.Pool = this;
            return po;
        }
    }
}
