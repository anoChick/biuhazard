using UnityEngine;
using System.Collections;

public class TotalDieScript : MonoBehaviour {
  public int die;
	// Use this for initialization
	void Start () {
	 die=0;
	}
	
	// Update is called once per frame
	void Update () {

	 guiText.text="死者数 : "+die.ToString("N0")+"人";
	}
  void Awake() {
    DontDestroyOnLoad(this);
    gameObject.guiText.text="";
  }
  
}
