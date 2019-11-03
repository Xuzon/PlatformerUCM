using UnityEngine;
using System.Collections;

public class SuperJumpPowerUp : MonoBehaviour
{
	// TODO 1 - Atributo público de tipo float que representará la duración del power up
	/// <summary>
	/// Atributo que indica el tiempo que durará el powerUp
	/// </summary>
	public float m_duration;
	/// <summary>
	/// Atributo que indica la altura máxima de salto que alcanzará
	/// el jugador cuando el power up esté activo
	/// </summary>
	public float m_SuperJumpHeight = 4.0f;

    /// <summary>
    /// Cuando el jugador toca el ítem, este debe otorgar la habilidad de super-salto al jugador
    /// durante un tiempo determinado
    /// </summary>
    /// <param name="other">
    /// Objeto que chica contra el item <see cref="Collider"/>
    /// </param>
    IEnumerator OnTriggerEnter(Collider other)
	{
        var trail = other.GetComponent<TrailRenderer>();
        if (other.CompareTag("Player"))
        {
            other.SendMessage("SetJumpHeight", m_SuperJumpHeight);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GUIManager.Instance.StartPowerUpTimer(m_duration);
            trail.enabled = true;
        }
        yield return new WaitForSeconds(m_duration);
        other.SendMessage("RestoreJumpHeight");
        trail.enabled = false;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}