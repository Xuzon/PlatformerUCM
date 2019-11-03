using UnityEngine;

public class Item : MonoBehaviour
{
	/// <summary>
	/// Representa el color de la puerta que se activará cuando
	/// el jugador coja este item
	/// </summary>
	public GameManager.DoorColor m_DoorColorToActivate;

	/// <summary>
	/// GameManager presente en la escena. Necesario para activar la puerta que haga falta
	/// </summary>
	private GameObject m_GameManager = null;

	/// <summary>
	///  Inicializaciones necesarias para conseguir el Game Manager
	/// </summary>
	void Start()
	{
		// Buscamos el GameManager
		m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
	}

	/// <summary>
	/// En la función de entrada en el trigger, se comprueba que sea
	/// el player el que toca el item.
	/// </summary>
	/// <param name="other">
	/// Objeto que colisiona contra el item (debería ser el player) <see cref="Collider"/>
	/// </param>
	void OnTriggerEnter(Collider other)
	{
		ActivateDoor(other.gameObject);
	}

	/// <summary>
	/// Función que activa una puerta determinada
	/// </summary>
	public void ActivateDoor(GameObject other)
	{
		// Comprobación del tag del objeto con el que se detecta colisión
		if (other.tag == "Player")
		{

            // TODO 1 - Obtener componente GameManager del GameObject m_GameManager y guardarlo
            // en una variable local
            GameManager gmComp = m_GameManager.GetComponent<GameManager>(); ;

            // TODO 2 - Si el componente existe..
            if (gmComp)
            {
                gmComp.ActivateDoor(m_DoorColorToActivate, true);
                var spawn = new GameObject("Spawn Point");
                spawn.transform.position = this.transform.position + new Vector3(0,4,0);
                gmComp.SetCurrentSpawnPoint(spawn.transform);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("No existe componente GameManager dentro del Game Manager");
            }
        }
	}
}