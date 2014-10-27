using UnityEngine;
using System.Collections;

public class VirusScript : MonoBehaviour {
  public int energy;
  public int value;
	// Use this for initialization
	void Start () {
	 energy=0;
   value=1;
   transform.Find("VirusLabel").gameObject.guiText.text="✕ "+value;
	}
	
	// Update is called once per frame
	void Update () {
  transform.Find("VirusLabel").gameObject.guiText.text="✕ "+value;

   Vector3 ls=transform.Find("Gauge/gaugeinr").localScale;
   ls= new Vector3((((float)energy)/100f), ls.y, ls.z);
   transform.Find("Gauge/gaugeinr").localScale=ls;
   Vector3 lp=transform.Find("Gauge/gaugeinr").localPosition;
   lp= new Vector3((((float)energy)/100f)/2f-0.5f, lp.y, lp.z);
   transform.Find("Gauge/gaugeinr").localPosition=lp;
	}

  public void Charge(int power){
    energy+=power;
    if(energy>=100){
      energy-=100;
      value++;
    }
  }
}
