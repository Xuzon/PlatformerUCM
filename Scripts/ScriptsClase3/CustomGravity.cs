using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    [SerializeField]
    protected float gravityAcceleration = 9.8f;
    [SerializeField]
    protected Vector3 gravityVector = Vector3.down;
    [SerializeField]
    protected float minGravityCheck = 0.5f;
    [SerializeField]
    protected LayerMask gravityMask;

    
    private float currentSpeed;
    private Collider coll;

    protected void Start()
    {
        coll = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        Gravity(Time.fixedDeltaTime);
    }

    private void Gravity(float delta)
    {
        var speed = currentSpeed * delta;
        var castDistance = Mathf.Max(speed, minGravityCheck);
        var ray = new Ray(transform.position, gravityVector);
        if (Physics.Raycast(ray, out RaycastHit hit, castDistance, gravityMask))
        {
            if (currentSpeed > 0)
            {
                var y = hit.point.y + coll.bounds.extents.y / 2;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
            currentSpeed = 0;
        }
        else
        {
            currentSpeed += gravityAcceleration * delta;
            transform.position += gravityVector * speed;
        }
    }
}
