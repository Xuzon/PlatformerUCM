using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChomperController))]
public class ChomperAI : MonoBehaviour
{
    [SerializeField]
    protected float visionAngle = 45;
    [SerializeField]
    protected float angleKeepRotate = 10;
    [SerializeField]
    protected float visionDistance = 10;
    [SerializeField]
    protected float distanceToAttack = 1;
    [SerializeField]
    protected float upOffset = 0.5f;

    protected float sqrDistanceAttack = 1;
    protected float sqrVisionDistance = 100;
    protected ChomperController controller;
    protected ChomperAnimation animation;

    void Start()
    {
        sqrVisionDistance = visionDistance * visionDistance;
        sqrDistanceAttack = distanceToAttack * distanceToAttack;
        controller = GetComponent<ChomperController>();
        animation = GetComponent<ChomperAnimation>();
        controller.player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    void Update()
    {
        switch (controller.State)
        {
            case ChomperController.AIState.Patrol:
                CheckForChase();
                break;
            case ChomperController.AIState.Following:
                Chase();
                break;
        }
    }

    private void Chase()
    {
        float angle;
        if (!CanSeePlayer(out angle))
        {
            controller.State = ChomperController.AIState.Patrol;
        }
        else
        {
            //readjust rotation
            if(angle > angleKeepRotate)
            {
                controller.ReadjustRotation();
            }
            if((transform.position - controller.player.position).sqrMagnitude < sqrDistanceAttack)
            {
                animation.Attack();
            }
        }
    }

    private void CheckForChase()
    {
        if (CanSeePlayer(out float angle))
        {
            controller.State = ChomperController.AIState.Following;
        }
    }

    private bool CanSeePlayer(out float angle)
    {
        angle = Vector3.Angle(-transform.forward, controller.player.forward);
        var sqrDistance = (transform.position - controller.player.position).sqrMagnitude;
        var origin = transform.position + Vector3.up * upOffset;
        var dir = controller.player.transform.position - origin;
        dir.y = 0;
        var ray = new Ray(origin, dir);
        var isSomethingBetween = true;
        if(Physics.Raycast(ray, out RaycastHit hit, visionDistance))
        {
            if (hit.collider.gameObject == controller.player.gameObject)
            {
                isSomethingBetween = false;
            }
        }
        else
        {
            isSomethingBetween = false;
        }
        return angle < visionAngle && sqrDistance < sqrVisionDistance && !isSomethingBetween;
    }
}
