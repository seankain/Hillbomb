using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnit : MonoBehaviour
{

    public List<Sprite> Tiles;
    public Sprite StraightRoad;
    public Sprite Intersection;

    public List<BoxCollider2D> SideWalkTriggers;
    public List<BoxCollider2D> RampTriggers;

    private SpriteRenderer _spriteRenderer;
    private bool _isIntersection = false;

    private List<MovingObstacleSpawn> IntersectionSpawns = new List<MovingObstacleSpawn>();
    private List<MovingObstacleSpawn> StraightRoadSpawns = new List<MovingObstacleSpawn>();

    public bool IsIntersection
    {
        get { return _isIntersection; }
        set { _isIntersection = value; SetRoadType(value); }

    }

    public bool PositionInBounds(Vector3 position)
    {
       return _spriteRenderer.bounds.Contains(position);
    }

    public MovingObstacleSpawn GetSpawnPosition()
    {
        var idx = 0;
        if (_isIntersection)
        {
            idx = Random.Range(0, IntersectionSpawns.Count);
            return IntersectionSpawns[idx];
        }
        idx = Random.Range(0, StraightRoadSpawns.Count);
        return StraightRoadSpawns[idx];
    }

    private void SetRoadType(bool isInterSection)
    {
        if (isInterSection)
        {
            _spriteRenderer.sprite = Intersection;
        }
        else
        {
            _spriteRenderer.sprite = StraightRoad;
        }
        ToggleTriggers(isInterSection);
    }

    private void ToggleTriggers(bool isIntersection)
    {
        foreach (var b in SideWalkTriggers)
        {
            b.enabled = !isIntersection;
        }
        foreach (var r in RampTriggers)
        {
            r.enabled = isIntersection;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = StraightRoad;
        SideWalkTriggers = new List<BoxCollider2D>();
        RampTriggers = new List<BoxCollider2D>();
        var triggers = GetComponentsInChildren<BoxCollider2D>();
        foreach(var trigger in triggers)
        {
            if(trigger.tag == "Curb")
            {
                SideWalkTriggers.Add(trigger);
            }
            else if(trigger.tag == "Ramp")
            {
                RampTriggers.Add(trigger);
            }
        }
        var movingObstacleSpawns = gameObject.GetComponentsInChildren<MovingObstacleSpawn>();
        foreach (var m in movingObstacleSpawns)
        {
            if (m.ForIntersection)
            {
                IntersectionSpawns.Add(m);
            }
            else
            {
                StraightRoadSpawns.Add(m);
            }
        }
        //Used random before, now will be a procedure, this should be removed
        //if (Tiles != null && Tiles.Count > 0)
        //{
        //    var index = Mathf.RoundToInt(Random.Range(0, Tiles.Count - 1));
        //    _spriteRenderer.sprite = Tiles[index];
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isIntersection)
        {
            PlayerOnRamp = true;
            PlayerOnSideWalk = false;
        }
        else {
            PlayerOnSideWalk = true;
            PlayerOnRamp = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isIntersection) {
            PlayerOnRamp = false;
        }
        else
        {
            PlayerOnSideWalk = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public bool PlayerOnSideWalk { get; set; }
    public bool PlayerOnRamp { get; set; }

}
