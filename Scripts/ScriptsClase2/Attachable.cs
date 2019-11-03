using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachable : MonoBehaviour
{
    [SerializeField]
    private bool isAttachable = true;
    private bool isAttached;

    public bool IsAttachable
    {
        get { return isAttachable; }
        set { isAttachable = value; }
    }

    public bool IsAttached
    {
        get { return isAttached; }
        set { isAttached = value; }
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
