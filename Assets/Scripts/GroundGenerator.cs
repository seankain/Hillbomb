using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;

public class GroundGenerator : MonoBehaviour
{
    public GameObject GroundPrefab;
    public float Speed = 1.0f;
    public Vector3 PointA;
    public Vector3 PointB;
    public List<GameObject> groundPieces = new List<GameObject>();
    private List<GroundUnit> groundUnits = new List<GroundUnit>();
    private Camera cam;
    private Rect bounds;
    private float elapsed = 0;
    private PixelPerfectCamera ppcam;
    private PlayerControls player;
    private int straightPerIntersection = 5;
    private int straightCounter = 0;
    //private GraphicRaycaster m_Raycaster;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        bounds = new Rect();

        var bottomLeft = cam.ViewportToWorldPoint(Vector3.zero);
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 1));
        bounds.xMin = bottomLeft.x;
        bounds.yMin = bottomLeft.y;
        bounds.xMax = topRight.x;
        bounds.yMax = topRight.y;
        ppcam = cam.GetComponent<PixelPerfectCamera>();
        player = FindObjectOfType<PlayerControls>();
    }

    public MovingObstacleSpawn GetSpawnLocation()
    {
        var yPos = float.PositiveInfinity;
        GameObject lowestPiece = null;
        //Get the piece most recently at the bottom
        foreach (var g in groundPieces)
        {
            if (g.transform.position.y < yPos)
            {
                yPos = g.transform.position.y;
                lowestPiece = g;
            }
        }
        return lowestPiece.GetComponent<GroundUnit>().GetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Speed = player.Speed;
        foreach (var g in groundPieces)
        {
            var oldPos = g.transform.position;
            oldPos += new Vector3(0, Speed * Time.deltaTime, 0);
            if (oldPos.y >= PointA.y)
            {
                straightCounter++;
                oldPos.y = PointB.y;
                var gu = g.GetComponent<GroundUnit>();
                if (straightCounter == straightPerIntersection)
                {
                    gu.IsIntersection = true;
                    straightCounter = 0;
                }
                else
                {
                    gu.IsIntersection = false;
                }

            }
            g.transform.position = oldPos;

        }

    }
}
