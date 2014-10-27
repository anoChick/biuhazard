using UnityEngine;
using System.Collections;


public class UnigeScorerScript : MonoBehaviour {
private string secretKey = "ひよこは世界一可愛い生き物だ。";
private string unigeKey;
private string accessToken;
private string url;
private int score;
	void Start () {
    url="http://unige.jp/scorer";
    score=0;
    unigeKey="none";
    accessToken="null";
    SetUp();
	}
  internal void Recordpiyopiyo(int s){
    score = s;
    StartCoroutine(Post());
  }
	private void SetUp(){
    Application.ExternalCall( "SetUpRequest");
  }
  public void SetParams(string keys){
    JSONObject  json = new JSONObject(keys);
    unigeKey = (string)json.GetField("unigeKey").str;
    accessToken = (string)json.GetField("accessToken").str;
  }
  private void Receive(string result) {
    GameObject.Find("Result").GetComponent<ResultScript>().Piyo(result);

  }
  private IEnumerator Post() {
  string strToEncrypt = score+unigeKey+accessToken+secretKey;
  System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
  byte[] bytes = ue.GetBytes(strToEncrypt);
  System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
  byte[] hashBytes = md5.ComputeHash(bytes);
  string hashString = "";
  for (int i = 0; i < hashBytes.Length; i++)
  {
    hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
  }
    WWWForm wwwForm= new WWWForm();
    wwwForm.AddField("hash", hashString.PadLeft(32, '0'));
    wwwForm.AddField("score", score);
    wwwForm.AddField("unigeKey", unigeKey);
    wwwForm.AddField("accessToken", accessToken);
    WWW www= new WWW(url, wwwForm);
    yield return www;
    JSONObject  json = new JSONObject(www.text);
    Receive(www.text);
  }
  void Awake() {
    DontDestroyOnLoad(this);
  }
}
