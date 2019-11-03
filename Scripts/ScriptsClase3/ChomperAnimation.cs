using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que implementa las animaciones de Chomp
/// </summary>
public class ChomperAnimation : MonoBehaviour
{
    private Animator m_Animator;
    protected AudioSource source;
    [SerializeField]
    protected AudioClip stepClip;
    [SerializeField]
    protected AudioClip gruntClip;
    [SerializeField]
    protected AudioClip endClip;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        m_Animator.SetTrigger("Attack");
    }

    public void UpdateForward(float ford)
    {
        m_Animator.SetFloat("Forward", ford);
    }

    public void PlayStep() => PlayIfExistsClip(stepClip);
    public void Grunt() => PlayIfExistsClip(gruntClip);
    public void AttackBegin() => PlayIfExistsClip(gruntClip);
    public void AttackEnd() => PlayIfExistsClip(endClip);

    protected void PlayIfExistsClip(AudioClip clip)
    {
        if(clip != null)
        {
            source?.PlayOneShot(clip);
        }
    }
}
