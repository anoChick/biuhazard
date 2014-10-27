using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {
  public AudioSource pion;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	 
	}
  public void Play (string str){
    switch(str){
      case "pion":
        pion.Play();
      break;
    }
  }
}
