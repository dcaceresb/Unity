using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


    public Button StartButtons;
	// Use this for initialization
	void Start () {

        StartButtons.onClick.AddListener( delegate { LoadScene(); } );

	}

    private void LoadScene() {
          SceneManager.LoadScene("SampleScene");
    }

    void update() { }
}
