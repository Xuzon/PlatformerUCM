using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTravel : MonoBehaviour
{
    public Camera m_Camera;
    public Transform m_Target;
    public float m_MinDistanceToStop;
    public float m_TravelTime;
    public float m_TimeCameraStop;

    private Camera m_MainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Camera.gameObject.SetActive(false);
        GameObject cameraGo = GameObject.FindGameObjectWithTag("MainCamera");
        m_MainCamera = cameraGo.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Travel(other.gameObject));
        }
    }

    IEnumerator Travel(GameObject player)
    {
        Vector3 direction = Vector3.zero;
        Vector3 initialPosition = m_Camera.transform.position;
        SetCamera(true,player);
        float time = 0;
        do
        {
            time += Time.deltaTime;
            direction = m_Target.position - m_Camera.transform.position;
            var t = time / m_TravelTime;
            m_Camera.transform.position = Vector3.Lerp(initialPosition, m_Target.position, t);
            yield return new WaitForEndOfFrame();
        }
        while (direction.sqrMagnitude > m_MinDistanceToStop);
        yield return new WaitForSeconds(m_TimeCameraStop);
        SetCamera(false,player);
        Destroy(m_Camera.gameObject);
        Destroy(gameObject);
    }

    private void SetCamera(bool toSet, GameObject player)
    {
        m_MainCamera.gameObject.SetActive(!toSet);
        player.gameObject.SetActive(!toSet);
        m_Camera.gameObject.SetActive(toSet);
    }
}
