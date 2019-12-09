using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    private long score;
    private PlayerControls player;
    private ObstacleGenerator obstacleGenerator;
    public GameObject GameOverText;
    public GameObject SlappyText;
    private bool playerStartedGrind = false;
    private bool playerDied = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        obstacleGenerator = FindObjectOfType<ObstacleGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Dead && !playerDied)
        {
            playerDied = true;
            obstacleGenerator.AddText(GameOverText);
        }
        else
        {
            if (!player.Dead && playerDied)
            {
                playerDied = false;
            }
        }
        if (player.Grinding && !playerStartedGrind)
        {
            playerStartedGrind = true;
            obstacleGenerator.AddText(SlappyText);
        }
        else
        {
            if (!player.Grinding && playerStartedGrind) {
                playerStartedGrind = false;
            }
        }
    }
}
