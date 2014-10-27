using UnityEngine;
using System.Collections;

public class AreaInfoScript : MonoBehaviour {
  public GameObject targetArea;
  public GUIText areaName;
  public GUIText baiuPoint;
  public GUIText population;
  public GUIText temperature;
  public GUIText humidity;
  public GUIText immune;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if(targetArea==null){

    }else{
      areaName.text=targetArea.GetComponent<AreaScript>().nameJP;
      baiuPoint.text="梅雨指数 : "+targetArea.GetComponent<AreaScript>().baiuPoint;
      population.text="人口 : "+targetArea.GetComponent<AreaScript>().population+"人";
      temperature.text="気温 : "+targetArea.GetComponent<AreaScript>().temperature+"℃";
      humidity.text="湿度 : "+targetArea.GetComponent<AreaScript>().humidity+"％";
      immune.text="免疫 : "+targetArea.GetComponent<AreaScript>().immune;
    }
	 
	}
  public void setTarget(GameObject target){
    targetArea = target;
  }
}
