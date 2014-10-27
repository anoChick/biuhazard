using UnityEngine;
using System.Collections;

public class AlertMessage : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	 Vector3 p= transform.position;
   p = new Vector3(p.x,p.y+0.002f,p.z);
   transform.position=p;
   if(p.y>1.1){
    GameObject.Destroy(this.gameObject);
   }
	}
}
