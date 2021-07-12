using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mine
{
    public class ElevatorArea : GameObject2D
    {
        public int currentFloor = -1;
        // [floorLevelWaiting]
        public Dictionary<int, List<Miner>> minersWaiting;
        // [floorLevelGoal]
        public Dictionary<int, List<Miner>> minersOn;
        [SerializeField]
        private GameObject2D objectElevator;
        [BoxGroup("entrance")]
        [SerializeField]
        private List<GameObject2D> entrances;
        [BoxGroup("entrance")]
        [SerializeField]
        private List<int> entranceFloorLevels;
        private Dictionary<int, GameObject2D> entrancesDict;

        public Vector2 direction;
        public float speed;
        private Vector2 Speed
        {
            get
            {
                return direction * speed  * Time.deltaTime;
            }
        }

        public int debug;

        private void Awake()
        {
            minersWaiting = new Dictionary<int, List<Miner>>();
            minersOn = new Dictionary<int, List<Miner>>();
            entrancesDict = new Dictionary<int, GameObject2D>();
        }

        private void Start()
        {
            for (int i = 0; i < entranceFloorLevels.Count; i++)
            {
                entrancesDict.Add(entranceFloorLevels[i], entrances[i]);
                entrances[i].Position = new Vector2(entrances[i].Position.x, SpaceManager.Instance.totalFloors[i].GoalElevator.Position.y);
                minersWaiting.Add(entranceFloorLevels[i], new List<Miner>());
                minersOn.Add(entranceFloorLevels[i], new List<Miner>());
            }

            currentFloor = entranceFloorLevels[0];
            objectElevator.Position = entrances[0].Position;
        }

        public void AddMinerWaiting(Miner miner, int floorLevelWaiting)
        {
            List<Miner> miners;
            if (!minersWaiting.TryGetValue(floorLevelWaiting, out miners))
            {
                Debug.LogError("something went wrong");
            }
            miners.Add(miner);
            Debug.Log("Miner wait for the elevator on " + floorLevelWaiting);
        }

        private void AddMinerOn(Miner miner, int floorLevelGoal)
        {
            List<Miner> miners;
            if (!minersOn.TryGetValue(floorLevelGoal, out miners))
            {
                Debug.LogError("something went wrong");
            }
            miners.Add(miner);
        }

        public void ArriveOnFloor(int floor)
        {
            for (int i = 0; i < minersOn[floor].Count; i++)
            {
                minersOn[floor][i].OnArriveAtGoalFloor();
                Debug.Log("Miner get off the elevator on " + floor);
            }
            minersOn[floor].Clear();

            for (int i = 0; i < minersWaiting[floor].Count; i++)
            {
                AddMinerOn(minersWaiting[floor][i], floor);
                Debug.Log("Miner get on the elevator on " + floor);
            }
            minersWaiting[floor].Clear();
        }
        
        // Action
        private IEnumerator CoroutineMoveTo(int floorLevel)
        {
            GameObject2D goal = entrancesDict[floorLevel];
            direction = objectElevator.GetDirectionY(goal.Position);
            while (objectElevator.GetDistanceY(goal.Position) > 0.1f)
            {
                objectElevator.Position += Speed;
                yield return null;
            }
            ArriveOnFloor(floorLevel);
        }

        [Button]
        public void Test()
        {
            StartCoroutine(CoroutineMoveTo(debug));
        }
        [Button]
        public void Test2()
        {
            objectElevator.Position = entrances[0].Position;
        }
    }
}