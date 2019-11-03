using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GUIItem : MonoBehaviour
{

	public GameManager.DoorColor m_DoorColor;
	private GameManager m_gameManager;
	private GameObject m_doorGameObject;

	private Image m_Image;

	void Awake()
	{
		GameObject m_GameManagerGo = GameObject.FindGameObjectWithTag("GameManager");
		m_gameManager = m_GameManagerGo.GetComponent<GameManager>();

		m_Image = this.GetComponent<Image>();
	}

	// Use this for initialization
	void Start ()
	{
		m_doorGameObject = m_gameManager.GetDoorGameObject(m_DoorColor);
	}

	// Update is called once per frame
	void Update () {
        if(m_doorGameObject == null || !m_doorGameObject.activeInHierarchy)
        {
            return;
        }
        switch (m_DoorColor)
        {
            case GameManager.DoorColor.GREEN:
                m_Image.color = Color.green;
                break;
            case GameManager.DoorColor.RED:
                m_Image.color = Color.red;
                break;
            case GameManager.DoorColor.BLUE:
                m_Image.color = Color.blue;
                break;
            case GameManager.DoorColor.YELLOW:
                m_Image.color = Color.yellow;
                break;
        }
        this.enabled = false;
    }
}
