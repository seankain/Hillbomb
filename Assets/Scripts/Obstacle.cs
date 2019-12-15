using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<Sprite> ObstacleSprites;
    private SpriteRenderer _spriteRenderer;

    public void Kill() {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (ObstacleSprites != null && ObstacleSprites.Count > 0)
        {
            var index = Mathf.RoundToInt(Random.Range(0, ObstacleSprites.Count - 1));
            _spriteRenderer.sprite = ObstacleSprites[index];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
