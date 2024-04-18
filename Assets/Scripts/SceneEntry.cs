using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntry : MonoBehaviour
{
    public string NextSceneName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }

    [InspectorButton]
    public void LoadNextScene()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}
