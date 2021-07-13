using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class MinerBody : MonoBehaviour
    {
        [SerializeField]
        private Transform faceArea;
        [SerializeField]
        private Transform headArea;
        [SerializeField]
        private List<SpriteRenderer> spriteSkins;


        public void SetRandomCostume()
        {
            foreach(Transform child in faceArea.transform)
            {
                Destroy(child.gameObject);
            }
            CreateCostume(MinerManager.Instance.MinerData.prefabFaces[Random.Range(0, MinerManager.Instance.MinerData.prefabFaces.Count)], faceArea);
            foreach(Transform child in headArea.transform)
            {
                Destroy(child.gameObject);
            }
            CreateCostume(MinerManager.Instance.MinerData.prefabHeads[Random.Range(0, MinerManager.Instance.MinerData.prefabHeads.Count)], headArea);

            Color color = MinerManager.Instance.MinerData.colorSkins[Random.Range(0, MinerManager.Instance.MinerData.colorSkins.Count)];
            for (int i = 0; i < spriteSkins.Count; i++)
            {
                spriteSkins[i].color = color;
            }
        }
        

        public void CreateCostume(GameObject costume, Transform area)
        {
            var go = Instantiate(costume) as GameObject;
            go.name = costume.name;
            go.transform.SetParent(area, false);
        }
    }
}