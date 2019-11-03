using System;
using UnityEngine;

public class TriggerLoadAdditive : MonoBehaviour
{
    // TODO 1 - A�adir string p�blico que ser� el nombre del nivel a cargar
    [SerializeField]
    protected GameManager.DoorColor levelToLoad;

    /// <summary>
    /// La comprobaci�n del tipo de gameobject que entra en el trigger se hace por tag
    /// El valor del tag que nos interesa se guarda en esta variable
    /// </summary>
    private string m_PlayerTag = "Player";

	/// <summary>
	/// Detecta cu�ndo un GameObject entra en el trigger al cual est� asignado este componente.
	/// En nuestro caso, realizamos la carga aditiva del nivel indicado en el atributo
	/// p�blico "LevelToLoadName"
	/// </summary>
	void OnTriggerEnter(Collider other)
	{
        LoadLevelAdditive(other.gameObject);
    }

    private void LoadLevelAdditive(GameObject gameObject)
    {
        if(gameObject.CompareTag(m_PlayerTag))
        {
            var go = GameObject.FindGameObjectWithTag("GameManager");
            go.GetComponent<GameManager>().TriggerLoadAdditive(levelToLoad);
        }
    }

}