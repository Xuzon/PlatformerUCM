using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperController : MonoBehaviour
{
    public bool run;
    public float speed;
    public float runSpeed;
    public float rotationTime;
    public float timeToAccel;
    
    [SerializeField]
    protected List<Transform> wayPoints = new List<Transform>();
    [SerializeField]
    protected float distanceToStop = 1.5f;

    protected float sqrDistanceToStop = 2.25f;
    protected Transform currentWayPoint;
    protected IEnumerator currentRotationRoutine;

    private ChomperAnimation chomperAnimation;
    private BoxCollider _boxCollider;
    private float runTime;
    private Transform lastLook;

    public Transform player { get; set; }
    protected AIState state = AIState.Patrol;
    //if the state is changed then, rotate to the new objective
    public AIState State 
    { 
        get { return state; } 
        set
        {
            state = value;
            Debug.Log("New AI State: " + value);
            var following = value == AIState.Following;
            var toRotate = following ? player : currentWayPoint;
            sqrDistanceToStop = following ? 0 : distanceToStop * distanceToStop;
            run = following;
            //on constructor is called and the currentwaypoint will be null
            if (toRotate != null)
            {
                StartRotation(toRotate);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        chomperAnimation = GetComponent<ChomperAnimation>();
        sqrDistanceToStop = distanceToStop * distanceToStop;
        State = AIState.Patrol;
        ChooseWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(State == AIState.Patrol)
        {
            if ((currentWayPoint.position - transform.position).sqrMagnitude < sqrDistanceToStop)
            {
                ChooseWayPoint();
            }
        }

        float velocity = CalculateVelocity(Time.deltaTime);
        //Debug.Log("Velocity " + velocity);
        chomperAnimation.UpdateForward(velocity / runSpeed);
        //La velocidad depende de si está corriendo o no
        if (Physics.BoxCast(transform.position, _boxCollider.size, transform.forward, out RaycastHit hit, transform.rotation, 0.1f))
        {
            if (hit.collider.gameObject == player.gameObject)
            {
                transform.position += transform.forward * velocity * Time.deltaTime;
            }
        }else
        {
            transform.position += transform.forward * velocity * Time.deltaTime;
        }
    }

    private void ChooseWayPoint()
    {
        var index = currentWayPoint == null ? -1 : wayPoints.IndexOf(currentWayPoint);
        index++;
        if (index >= wayPoints.Count)
        {
            index = 0;
        }
        currentWayPoint = wayPoints[index];
        StartRotation(currentWayPoint);
    }

    private void StartRotation(Transform toRotate)
    {
        lastLook = toRotate;
        if (currentRotationRoutine != null)
        {
            StopCoroutine(currentRotationRoutine);
        }
        currentRotationRoutine = Rotate(toRotate);
        StartCoroutine(currentRotationRoutine);
    }

    private float CalculateVelocity(float time)
    {
        if (run)
        {
            runTime += time;
            if (runTime > timeToAccel)
                runTime = timeToAccel;
            return Mathf.Lerp(speed, runSpeed, runTime / timeToAccel);
        }
        else
        {
            runTime -= time;
            if (runTime < 0)
                runTime = 0f;
            return Mathf.Lerp(speed, runSpeed, runTime / timeToAccel);

        }
    }

    public void ReadjustRotation()
    {
        if(lastLook != null)
        {
            StartRotation(lastLook);
        }
    }

    IEnumerator Rotate(Transform toLook)
    {
        float time = 0;
        var dir = (toLook.position - transform.position);
        //we don't want to the AI to rotate on the Y axis
        dir.y = 0;
        var newRotation = Quaternion.LookRotation(dir, Vector3.up);
        Quaternion originalRotation = transform.rotation;
        while (time < rotationTime)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(originalRotation, newRotation, time / rotationTime);
            //removed new WaitForEndOfFrame because it generates garbage and we don't need to do
            //any special stuff here
            yield return null;
        }
    }

    public enum AIState { Patrol, Following };
}
