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
        public List<List<Miner>> minersWaiting;
        // [floorLevelGoal]
        public List<List<Miner>> minersOn;
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
            minersWaiting = new List<List<Miner>>();
            minersOn = new List<List<Miner>>();
            entrancesDict = new Dictionary<int, GameObject2D>();
        }

        private void Start()
        {
            for (int i = 0; i < entranceFloorLevels.Count; i++)
            {
                entrancesDict.Add(entranceFloorLevels[i], entrances[i]);
                entrances[i].Position = new Vector2(entrances[i].Position.x, SpaceManager.Instance.totalFloors[i].GoalElevator.Position.y);
            }

            currentFloor = entranceFloorLevels[0];
            objectElevator.Position = entrances[0].Position;
            Debug.Log("@@@ " + objectElevator.Position.y + " " + entrances[0].Position.y);
        }

        public void AddMinerWaiting(Miner miner, int floorLevelWaiting)
        {
            while (minersWaiting.Count < floorLevelWaiting + 1)
            {
                minersWaiting.Add(new List<Miner>());
            }

            minersWaiting[floorLevelWaiting].Add(miner);
        }

        public void AddMinerOn(Miner miner, int floorLevelGoal)
        {
            while (minersOn.Count < floorLevelGoal + 1)
            {
                minersOn.Add(new List<Miner>());
            }

            minersOn[floorLevelGoal].Add(miner);
        }

        public void ArriveOnFloor(int floor)
        {
            for (int i = 0; i < minersOn[floor].Count; i++)
            {
                minersOn[floor][i].OnArriveAtGoalFloor();
            }
            minersOn[floor].Clear();

            for (int i = 0; i < minersWaiting[floor].Count; i++)
            {
                minersOn[floor].Add(minersWaiting[floor][i]);
            }
            minersWaiting[floor].Clear();
        }
        
        // Action
        private IEnumerator CoroutineMoveTo(int floorLevel)
        {
            GameObject2D goal = entrancesDict[floorLevel - 1];
            Debug.Log("@@@1 target:" + goal.Position.y + " from:" + objectElevator.Position.y);
            direction = objectElevator.GetDirectionY(goal.Position);
            Debug.Log("@@@2 :" + direction.y);
            while (objectElevator.GetDistanceY(goal.Position) > 0.1f)
            {
                objectElevator.Position += Speed;
                yield return null;
            }
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