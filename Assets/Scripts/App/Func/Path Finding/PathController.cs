using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class PathController : MonoBehaviour
    {
        public List<int> pathes;

        public int PopPath()
        {
            int path = pathes[pathes.Count - 1];
            pathes.RemoveAt(pathes.Count - 1);
            return path;
        }

        public void SetPathes(List<int> pathes)
        {
            this.pathes = new List<int>(pathes);
            this.pathes.Reverse();
        }
    }
}