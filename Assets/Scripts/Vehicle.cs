using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MovingObstacle
{

    public Sprite LeftChassisSprite;
    public Sprite RightChassisSprite;
    public Sprite UpChassisSprite;
    public Sprite DownChassisSprite;

    public Sprite LeftPaintSprite;
    public Sprite RightPaintSprite;
    public Sprite UpPaintSprite;
    public Sprite DownPaintSprite;

    public Color PaintColor;
    private Sprite _currentChassisSprite;
    private Sprite _currentPaintSprite;
    private Vector3 _defaultDirection = Vector3.down;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _currentChassisSprite = DownChassisSprite;
        _currentPaintSprite = DownPaintSprite;
        if (Direction == Vector3.up)
        {
            _currentChassisSprite = UpChassisSprite;
            _currentPaintSprite = UpPaintSprite;
        }
        if (Direction == Vector3.left) {
            _currentChassisSprite = LeftChassisSprite;
            _currentPaintSprite = LeftPaintSprite;
        }
        if (Direction == Vector3.right) {
            _currentChassisSprite = RightChassisSprite;
            _currentPaintSprite = RightPaintSprite;
        }

        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in spriteRenderers)
        {
            if (sr.gameObject.name == "Paint")
            {
                spriteRenderer = sr;
                //spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Random.ColorHSV();
            }
        }

    }

    public override void UpdateTraits()
    {
        UpdateSprites();
        RefreshColor();
        Speed = Random.Range(MinSpeed, MaxSpeed);
    }

    private void UpdateSprites() {
        if (Direction == Vector3.up)
        {
            _currentChassisSprite = UpChassisSprite;
            _currentPaintSprite = UpPaintSprite;
        }
        if (Direction == Vector3.left)
        {
            _currentChassisSprite = LeftChassisSprite;
            _currentPaintSprite = LeftPaintSprite;
        }
        if (Direction == Vector3.right)
        {
            _currentChassisSprite = RightChassisSprite;
            _currentPaintSprite = RightPaintSprite;
        }
        if (Direction == Vector3.down)
        {
            _currentChassisSprite = DownChassisSprite;
            _currentPaintSprite = DownPaintSprite;
        }

        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in spriteRenderers)
        {
            if (sr.gameObject.name == "Paint")
            {
                sr.sprite = _currentPaintSprite;
            }
            else
            {
                sr.sprite = _currentChassisSprite;
            }
        }

    }

    private void RefreshColor() {

        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in spriteRenderers)
        {
            if (sr.gameObject.name == "Paint")
            {
                sr.color = Random.ColorHSV();
            }
        }

    }

}
