using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstaclePool : MonoBehaviour
{
    public uint PoolSize = 5;
    public List<MovingObstacle> MovingObstaclePrefabs;
    [SerializeField]
    private Vector3 PointA;
    [SerializeField]
    private Vector3 PointB;
    private GroundGenerator groundGenerator;

    private List<MovingObstacle> activeObstacles = new List<MovingObstacle>();
    private PlayerControls player;
    private float spawnElapsed = 0;
    private float SpawnCooldown = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        groundGenerator = FindObjectOfType<GroundGenerator>();
    }

    private void SpawnObstacle()
    {
        var idx = (int)Random.Range(0, MovingObstaclePrefabs.Count-1);
        var spawn = groundGenerator.GetSpawnLocation();
        var g = Instantiate(MovingObstaclePrefabs[idx],spawn.transform.position,Quaternion.identity,null);
        var moving = g.GetComponent<MovingObstacle>();
        moving.Direction = spawn.DestinationDirection;
        moving.UpdateTraits();
        activeObstacles.Add(moving);
    }

    private void UpdateObstacleTraits(MovingObstacle obstacle)
    {
        var spawn = groundGenerator.GetSpawnLocation();
        var movingObstacle = obstacle.GetComponent<MovingObstacle>();
        var pos = spawn.transform.position;

        //movingObstacle.transform.SetPositionAndRotation(spawn.transform.position, Quaternion.identity);
        //movingObstacle.gameObject.transform.position = spawn.transform.position;
        //movingObstacle.transform.position = new Vector3(0,0,0);
        movingObstacle.transform.position = pos;
        movingObstacle.Direction = spawn.DestinationDirection;
        //Debug.Log($"{movingObstacle.Direction} {movingObstacle.transform.position}");
        movingObstacle.UpdateTraits();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeObstacles.Count < PoolSize)
        {
            SpawnObstacle();
        }
        foreach (var obstacle in activeObstacles)
        {
            var oldPos = obstacle.transform.position;
            //Apply player speed to keep in sync with level
            oldPos += new Vector3(0, player.Speed * Time.deltaTime, 0);
            //Then apply obstacle speed with its direction
            oldPos += obstacle.Direction * (obstacle.Speed * Time.deltaTime);
            if (oldPos.y >= PointA.y)
            {
                oldPos.y = PointB.y;
                UpdateObstacleTraits(obstacle);
            }
            else
            {
                obstacle.transform.position = oldPos;
            }

        }
    }
}
