using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChomperController))]
public class ChomperDeath : MonoBehaviour
{
    protected float deathCastRadius = 1.5f;
    protected Vector3 collBounds;
    protected CastType cast = CastType.box;

    protected ChomperController controller;
    protected GameObject gameManager;

    protected void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        controller = GetComponent<ChomperController>();
        AdjustCast();
    }

    private void AdjustCast()
    {
        var collider = GetComponent<Collider>();
        switch (collider)
        {
            case SphereCollider sp:
                cast = CastType.sphere;
                deathCastRadius = sp.radius;
                break;
            case CapsuleCollider cc:
                cast = CastType.sphere;
                deathCastRadius = cc.radius;
                break;
            case BoxCollider bc:
                cast = CastType.box;
                collBounds = bc.bounds.size / 2;
                break;
        }
    }

    /// <summary>
    /// if the players enters on my trigger I will throw a ray upwards
    /// and if the player, is upward, kill me, instead, I will kill him
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == controller.player.gameObject)
        {
            var ray = new Ray(transform.position, Vector3.up);
            RaycastHit hit;
            var intersected = cast == CastType.sphere ? Physics.SphereCast(ray, deathCastRadius, out hit) : Physics.BoxCast(ray.direction, collBounds, ray.direction, out hit);
            if (intersected)
            {
                Debug.Log("Cast to " + hit.collider.name);
                if(hit.collider == other)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            gameManager.SendMessage("RespawnPlayer");
        }
    }

    protected enum CastType { box, sphere};
}
