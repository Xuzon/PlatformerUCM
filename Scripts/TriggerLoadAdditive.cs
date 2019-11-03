using System;
using UnityEngine;

public class TriggerLoadAdditive : MonoBehaviour
{
    // TODO 1 - Añadir string público que será el nombre del nivel a cargar
    [SerializeField]
    protected GameManager.DoorColor levelToLoad;

    /// <summary>
    /// La comprobación del tipo de gameobject que entra en el trigger se hace por tag
    /// El valor del tag que nos interesa se guarda en esta variable
    /// </summary>
    private string m_PlayerTag = "Player";

	/// <summary>
	/// Detecta cuándo un GameObject entra en el trigger al cual está asignado este componente.
	/// En nuestro caso, realizamos la carga aditiva del nivel indicado en el atributo
	/// público "LevelToLoadName"
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