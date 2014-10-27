using UnityEngine;
using System.Collections;

public class ResultScript : MonoBehaviour {
  public GUIText rank;
  public GUIText score;
	// Use this for initialization
	void Start () {
    int s = GameObject.Find("Score").GetComponent<TotalDieScript>().die;
	 GameObject.Find("UnigeScorer").GetComponent<UnigeScorerScript>().Recordpiyopiyo(s);
   score.text="死者数 : "+s.ToString("N0")+"人";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  internal void Piyo(string result){
    JSONObject  json = new JSONObject(result);
    int ra=(int)json.GetField("rank").n;
    rank.text=""+ ra+"位";
    
  }
}
