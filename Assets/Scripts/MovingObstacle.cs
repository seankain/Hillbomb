using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    public float Speed = 0;
    public float MaxSpeed = 10;
    public float MinSpeed = 0;
    public Vector3 Direction = Vector3.zero;

    public virtual void UpdateTraits() { }

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(MinSpeed, MaxSpeed);
    }
}
