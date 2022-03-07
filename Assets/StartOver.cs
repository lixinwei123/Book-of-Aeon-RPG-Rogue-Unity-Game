using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startOver;
    void Start()
    {
        startOver.onClick.AddListener(Listener);
    }

    // Update is called once per frame
    public void Listener()
    {
        Debug.Log("restart game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Update()
    {
        
    }
}
