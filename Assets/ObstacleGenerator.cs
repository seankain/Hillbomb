using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    private SpriteRenderer spriteRenderer;
    private List<GameObject> activeObstacles = new List<GameObject>();

    [SerializeField]
    private float SpawnProbabilityPerSecond = 1.0f;
    private PlayerControls player;
    private float elapsed = 0;
    private Camera cam;
    private Rect bounds;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerControls>();
        cam = Camera.main;
        bounds = new Rect();

        var bottomLeft = cam.ViewportToWorldPoint(Vector3.zero);
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 1));
        bounds.xMin = bottomLeft.x;
        bounds.yMin = bottomLeft.y;
        bounds.xMax = topRight.x;
        bounds.yMax = topRight.y;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1)
        {
            elapsed = 0;
            if (Random.Range(0, 1) < SpawnProbabilityPerSecond)
            {
                SpawnObstacle();
            }
        }
        foreach (var g in activeObstacles)
        {
            var oldPos = g.transform.position;
            oldPos += new Vector3(0, player.Speed * Time.deltaTime, 0);
            g.transform.position = oldPos;
        }
        RemovePassedObstacles();

    }

    private void RemovePassedObstacles()
    {
        if (activeObstacles.Count == 0) { return; }
        for (var i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if (activeObstacles[i].transform.position.y > bounds.yMax + 3)
            {
                var oldObj = activeObstacles[i];
                activeObstacles.Remove(oldObj);
                Destroy(oldObj);
            }
        }

    }

    private void SpawnObstacle()
    {
        var y = bounds.yMin - 1;
        var x = Random.Range(bounds.xMin, bounds.xMax);
        activeObstacles.Add(Instantiate<GameObject>(ObstaclePrefab, new Vector3(x, y, 0), Quaternion.identity, null));

    }
}
