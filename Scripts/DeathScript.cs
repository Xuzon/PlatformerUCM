using UnityEngine;

public class DeathScript : MonoBehaviour
{
    /// <summary>
    /// El jugador
    /// </summary>
    public GameObject m_Player = null;

    /// <summary>
    /// Game Manager para hacer respawn del jugador
    /// </summary>
    private GameObject m_GameManager = null;

    /// <summary>
    /// En la función Start hacemos una búsqueda del GameManager
    /// </summary>
    void Start()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }


    /// <summary>
    /// Si algo choca contra nosotros, comprobaremos si es el player
    /// </summary>
    /// <param name="other">
    /// Objeto que ha entrado en el trigger <see cref="Collider"/>
    /// </param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == m_Player)
        {
            m_GameManager.SendMessage("RespawnPlayer");
        }
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}