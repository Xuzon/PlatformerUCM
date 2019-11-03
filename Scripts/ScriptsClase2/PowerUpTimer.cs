using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PowerUpTimer : MonoBehaviour
{

	public float TotalTime;

	private float m_RemainingTime;
	private Image m_Image;

	// Use this for initialization
	void Start ()
	{
		m_Image = GetComponent<Image>();
	}

	void OnEnable()
	{
		// Al activarlo reseteamos el tiempo total que dura el powerup
		m_RemainingTime = TotalTime;
	}

	// Update is called once per frame
	void Update ()
	{
		// TODO 1 - Comprobamos si se ha acabado el tiempo
        if (m_RemainingTime <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            var t = Mathf.InverseLerp(0, TotalTime, m_RemainingTime);
            m_Image.fillAmount = t;
            m_RemainingTime -= Time.deltaTime;
        }
	}
}
