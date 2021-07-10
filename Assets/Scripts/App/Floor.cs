using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class Floor : Space
    {
        [SerializeField]
        private Transform Spawner;
        [SerializeField]
        private GameObject mineralArea;

        private Mineral[] minerals;

        private void Awake()
        {
            minerals = mineralArea.GetComponentsInChildren<Mineral>();
        }
    }
}
