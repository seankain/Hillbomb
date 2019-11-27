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
        //Seed();
    }


    //private void Seed()
    //{
    //    //for(var x = bounds.xMin -1 ; x < bounds.xMax + 1; x++)
    //    //{
    //    //for (var y = bounds.yMin - 1; y < bounds.yMax + 1; y++)
    //    //{
    //        PlaceGroundTile(new Vector3(0, 0, 0));
    //    //}

    //    //}
    //}

    //private void PlaceGroundTile(Vector3 location)
    //{
    //    straightCounter++;
    //    var g = Instantiate<GameObject>(GroundPrefab, location, Quaternion.identity, null);
    //    if (straightCounter == straightPerIntersection)
    //    {
    //        g.GetComponent<GroundUnit>().SetRoadType(true);
    //        straightCounter = 0;
    //    }
    //    groundPieces.Add(g);

    //}

    //private void GenerateRow()
    //{
    //    if (groundPieces.Last().transform.position.y > (bounds.yMin - SpawnOffset))
    //    {
    //        var y = bounds.yMin - (bounds.height + SpawnOffset);
    //        PlaceGroundTile(new Vector3(0, y, 0));
    //    }
    //}

    //private void RemoveRow()
    //{
    //    var y = bounds.yMax + (bounds.height + SpawnOffset);
    //    for (var i = groundPieces.Count - 1; i >= 0; i--)
    //    {
    //        if (groundPieces[i].transform.position.y > y)
    //        {
    //            var oldPiece = groundPieces[i];
    //            groundPieces.Remove(oldPiece);
    //            DestroyImmediate(oldPiece);

    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        Speed = player.Speed;
        //GenerateRow();
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
                   // gu.SetRoadType(true);
                    straightCounter = 0;
                }
                else
                {
                    gu.IsIntersection = false;
                }

            }
            g.transform.position = oldPos;

        }
        //RemoveRow();

    }
}
