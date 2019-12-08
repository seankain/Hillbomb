using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    private long score;
    private PlayerControls player;
    private ObstacleGenerator obstacleGenerator;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        obstacleGenerator = FindObjectOfType<ObstacleGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
