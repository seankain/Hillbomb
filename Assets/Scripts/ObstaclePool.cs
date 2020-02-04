using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public uint PoolSize = 5;
    public List<Obstacle> ObstaclePrefabs;
    [SerializeField]
    private Vector3 PointA;
    [SerializeField]
    private Vector3 PointB;
    [SerializeField]
    private float SpawnXMin;
    [SerializeField]
    private float SpawnXMax;
    private GroundGenerator groundGenerator;

    private List<Obstacle> activeObstacles = new List<Obstacle>();
    private PlayerControls player;
    private float spawnElapsed = 0;
    private float SpawnCooldown = 5f;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        groundGenerator = FindObjectOfType<GroundGenerator>();
    }

    private void SpawnObstacle()
    {
        var idx = (int)Random.Range(0, ObstaclePrefabs.Count - 1);
        //var spawn = groundGenerator.GetSpawnLocation();
        var pos = new Vector3(Random.Range(SpawnXMin, SpawnXMax), -5, 0);
       
        var g = Instantiate(ObstaclePrefabs[idx], pos, Quaternion.identity, null);
        var obstacle = g.GetComponent<Obstacle>();
        //obstacle.Direction = spawn.DestinationDirection;
        obstacle.UpdateTraits();
        activeObstacles.Add(obstacle);
    }

    private void UpdateObstacleTraits(Obstacle obstacle)
    {
       // var spawn = groundGenerator.GetSpawnLocation();
        var movingObstacle = obstacle.GetComponent<Obstacle>();
        var pos = new Vector3(Random.Range(SpawnXMin, SpawnXMax), Random.Range(-5,-10), 0);
        //var pos = spawn.transform.position;
        movingObstacle.transform.position = pos;
        movingObstacle.UpdateTraits();
    }

    // Update is called once per frame
    void Update()
    {
        spawnElapsed += Time.deltaTime;
        if (spawnElapsed >= SpawnCooldown && activeObstacles.Count < PoolSize)
        {
            spawnElapsed = 0;
            SpawnObstacle();
        }
        foreach (var obstacle in activeObstacles)
        {
            var oldPos = obstacle.transform.position;
            //Apply player speed to keep in sync with level
            oldPos += new Vector3(0, player.Speed * Time.deltaTime, 0);
            //Then apply obstacle speed with its direction
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
