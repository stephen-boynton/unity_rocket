using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
    int loadingTime = 3;
	// Use this for initialization
	void Start () {
        Invoke("LoadLevel", loadingTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadLevel() {
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = thisScene + 1;
        SceneManager.LoadScene(nextScene);
    }
}
