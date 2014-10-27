using UnityEngine;
using System.Collections;

public class GameMasterScript : MonoBehaviour {
  public GameObject date;
  public GameObject dateLimit;
  private int time;
  public GameObject virus;

	// Use this for initialization
	void Start () {
    time=0;
	
	}
	
	// Update is called once per frame
	void Update () {
    time+=4;
    if(time>=126660){
      Application.LoadLevel("Result");
    }
    if(time%30==0){
      Debug.Log(time);
      updateClock();
      Step();
    }


	
	}

  void Step(){
    virus.GetComponent<VirusScript>().Charge(2);
    for(var i=0;i<47;i++){
      GameObject area =GameObject.Find("Map").transform.GetChild(i).gameObject;
      area.GetComponent<AreaScript>().Diffusion();

    }
    

    float totalBaiu=0;
    for(var i=0;i<47;i++){
      GameObject area =GameObject.Find("Map").transform.GetChild(i).gameObject;
      area.GetComponent<AreaScript>().Done();
      totalBaiu+=area.GetComponent<AreaScript>().baiuPoint;
    }
  }


  void updateClock(){
    int mo=6+(6+(time/(24*60*2)%44))/30;
    int da=(6+(time/(24*60*2)%44))%30;
    int ho=0+(time/(60*2)%24);
    int limit = 44;
    date.guiText.text=""+mo+"月"+da+"日"+ho+"時";
    dateLimit.guiText.text="あと"+(limit-((time/(24*60*2)%44)))+"日";
  }
}
