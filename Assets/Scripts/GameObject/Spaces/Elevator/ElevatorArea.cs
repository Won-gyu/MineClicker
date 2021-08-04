using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Mine
{
    public class ElevatorWaitInfo
    {
        public int goal;
        public Miner minerWaiting;
    }
    public class ElevatorArea : GameObject2D
    {
        [SerializeField]
        private List<Elevator> elevators;

        [BoxGroup("entrance")]
        [SerializeField]
        private Transform entranceArea;
        [BoxGroup("entrance")]
        [SerializeField]
        private List<int> entranceFloorLevels;
        public List<int> EntranceFloorLevels
        {
            get
            {
                return entranceFloorLevels;
            }
        }
        private Dictionary<int, GameObject2D> entrancesDict;

        // [floorLevelWaiting]
        public Dictionary<int, List<ElevatorWaitInfo>> waitingInfosDict;

        private void Awake()
        {
            entrancesDict = new Dictionary<int, GameObject2D>();
            waitingInfosDict = new Dictionary<int, List<ElevatorWaitInfo>>();
        }

        private void Start()
        {
            for (int i = 0; i < entranceFloorLevels.Count; i++)
            {
                GameObject2D go = CreateEntrance().GetComponent<GameObject2D>();
                entrancesDict.Add(entranceFloorLevels[i], go);
                go.Position = new Vector2(go.Position.x, SpaceManager.Instance.GetFloor(entranceFloorLevels[i]).GoalElevator.Position.y);
                waitingInfosDict.Add(entranceFloorLevels[i], new List<ElevatorWaitInfo>());
            }

            for (int i = 0; i < elevators.Count; i++)
            {
                elevators[i].Init(this, i);
            }
        }

        private GameObject CreateEntrance()
        {
            GameObject prefab = AppAssetBundleUtils.LoadCommonAsset<GameObject>("Elevator Entrance");
            var go = Instantiate(prefab) as GameObject;
            go.name = prefab.name;
            go.transform.SetParent(entranceArea, false);
            return go;
        }

        public GameObject2D GetEntrance(int floorLevel)
        {
            return entrancesDict[floorLevel];
        }

        public List<ElevatorWaitInfo> GetWaitInfos(int floorLevel)
        {
            return waitingInfosDict[floorLevel];
        }

        public void AddMinerWaiting(Miner miner, int floorLevelWaiting, int floorLevelGoal)
        {
            List<ElevatorWaitInfo> waitInfos;
            if (!waitingInfosDict.TryGetValue(floorLevelWaiting, out waitInfos))
            {
                Debug.LogError("something went wrong");
            }
            waitInfos.Add(new ElevatorWaitInfo
            {
                minerWaiting = miner,
                goal = floorLevelGoal
            });
            // Debug.Log("Miner wait for the elevator on " + floorLevelWaiting);
        }
    }
}