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

    public bool IsIntersection
    {
        get { return _isIntersection; }
        set { _isIntersection = value; SetRoadType(value); }

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
