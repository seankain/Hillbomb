using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    private long score = 0;
    [SerializeField]
    private float GrindScoreMultiplier = 2f;
    private float currentScoreMultiplier = 1.0f;
    private PlayerControls player;
    private ObstacleGenerator obstacleGenerator;
    public GameObject GameOverText;
    public GameObject SlappyText;
    public Text ScoreText;
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
            score = 0;
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
            currentScoreMultiplier += GrindScoreMultiplier;
            //obstacleGenerator.AddText(SlappyText);
        }
        else
        {
            if (!player.Grinding && playerStartedGrind) {
                playerStartedGrind = false;
                currentScoreMultiplier -= GrindScoreMultiplier;
            }
        }
        score += (long)(currentScoreMultiplier * player.Speed);
        ScoreText.text = score.ToString("D8");
    }
}
