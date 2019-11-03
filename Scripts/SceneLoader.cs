using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    protected string levelName = "victory_world";

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
        }
    }
}
