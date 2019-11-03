using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class StickyFloor : MonoBehaviour
{

    private Vector3 m_EnterScale = Vector3.one;
    public Transform m_globalParent = null; //Por defecto el padre global será la raiz de la escena pero podría ser que no fuera así.
    public Transform m_transformToAttach;
    [SerializeField]
    protected float maxAngle = 45;
    [SerializeField]
    protected float slipForce = 5;
    [SerializeField]
    protected float slipDuration = 2;
    protected List<Transform> stickedTransforms = new List<Transform>();

    void Start()
    {
        if (m_transformToAttach == null)
            m_transformToAttach = transform;
    }

    private void Update()
    {
        CheckStickedPlayer();
    }

    private void CheckStickedPlayer()
    {
        if(CanStick() || stickedTransforms.Count == 0)
        {
            return;
        }
        Debug.Log("Time to slip");
        foreach(var t in stickedTransforms)
        {
            RemoveAttached(t, t.GetComponent<Attachable>());
            var force = Vector3.Dot(transform.forward, Vector3.down) > Vector3.Dot(-transform.forward, Vector3.down) ?
                transform.forward : -transform.forward;
            StartCoroutine(Force( t, force, slipDuration));
        }
        stickedTransforms.Clear();
    }

    protected IEnumerator Force(Transform t, Vector3 force, float seconds)
    {
        float time = 0;
        var wait = new WaitForFixedUpdate();
        while(time < seconds)
        {
            time += Time.fixedDeltaTime;
            t.Translate(force * Time.fixedDeltaTime);
            yield return wait;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var attachable = other.GetComponent<Attachable>();
        if (attachable && attachable.IsAttachable && CanStick())
        {
            m_EnterScale = other.transform.localScale;
            other.transform.parent = m_transformToAttach;
            attachable.IsAttached = true;
            stickedTransforms.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var attachable = other.GetComponent<Attachable>();
        if (attachable && attachable.IsAttached)
        {
            RemoveAttached(other.transform, attachable);
        }
    }

    private void RemoveAttached(Transform other, Attachable attachable)
    {
        other.parent = m_globalParent;
        other.localScale = m_EnterScale;
        attachable.IsAttached = false;
    }

    protected bool CanStick()
    {
        return Vector3.Angle(transform.up, Vector3.up) < maxAngle;
    }
}
