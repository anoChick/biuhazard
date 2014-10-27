using UnityEngine;
using System.Collections;

public class AreaScript : MonoBehaviour {
  public string state;
  public int areaNo;
  public string nameJP;
  public string nameEN;
  public int population;
  public ArrayList adjoins;
  public GameObject audioBox;
  public AreaInfoScript areaInfo;
  public float baiuPoint ;
  public float diffRetio ;
  public float baiuPointNext;
  public float temperature;
  public float humidity;
  public float immune;
  public GameObject alertPrefab;
	// Use this for initialization
	void Start () {
    baiuPoint= 1f;
    diffRetio= 0.01f;
    baiuPointNext=0f;
    init();
	 state="default";
	}
  void init(){
    immune=0f;
    temperature=25f;
    humidity=50f;
    adjoins = new ArrayList();
    nameJP=NAME_JP[areaNo-1];
    population=POPULATION[areaNo-1];
    GameObject map = GameObject.Find("Map");
    for(int i=0;i<map.transform.childCount;i++){
      for(int j=0;j<7;j++){
        Transform child = map.transform.GetChild (i);
        if(child.gameObject.GetComponent<AreaScript>().areaNo == ADJOINS[areaNo-1,j]){
          adjoins.Add(child.gameObject);
        } 
      }
    }

  }
	
	// Update is called once per frame
	void Update () {
    //TIME_CORRECT



    float corB=baiuPoint;
    if(corB>100){
      corB=100;
    }
    switch(state){
      case "default":
        renderer.material.SetColor("_Color", new Color((corB/100f),1f-(corB/100f),0.3f,1f));
        break;
      case "hover":
        renderer.material.SetColor("_Color", new Color((corB/100f)/1.7f,(1f-(corB/100f))/1.7f,0.5f,1f));
        break;
      case "adjoins":
        renderer.material.SetColor("_Color", new Color((corB/100f)/1.3f,(1f-(corB/100f))/1.3f,0.3f,1f));
        break;     
    }
	
	}
  void PointVirus(){
    GameObject tv =GameObject.Find("TVirus");
    VirusScript vs=(VirusScript)tv.GetComponent<VirusScript>();
    if(vs.value>=1){
      vs.value--;
      audioBox.GetComponent<AudioScript>().Play("pion");
      baiuPoint+=20;
    }

  }
  public void Next(){

  }
  public void Diffusion(){
        temperature=25f*CORRECT[areaNo-1]*(1f+((Random.value)/10f)-0.05f);
        humidity = 50f*(temperature/25f)*(1f+(baiuPoint/500f));
        immune+=Mathf.Log10((population/10000f)*(population/10000f))/80000f;
      baiuPointNext-=(baiuPoint*diffRetio)-((baiuPoint/500f));
      baiuPoint-=immune;
      foreach(GameObject go in adjoins){
        go.GetComponent<AreaScript>().baiuPointNext+=(baiuPoint*diffRetio)/adjoins.Count;
      }
  }
  public void Done(){
    baiuPoint+=baiuPointNext;
    baiuPointNext=0;
    if(baiuPoint>100)baiuPoint=100;
    if(baiuPoint<0)baiuPoint=0;


    Happening();

  }
  private void Happening(){
    int die=0;
    if(baiuPoint>=100&&Random.value>0.80){
      string str=""+nameJP;
      if(Random.value>0.5){
        str+="で食中毒が発生。";
      }else{
        str+="で土石流が発生。";
      }
      die =(int)(Random.value*population/2f);
      population-=die;
      if(population<0)population=0;
      str+=die+"人が死亡した。";
      if(die!=0){ 
       GameObject msg=(GameObject)Instantiate(alertPrefab);
       msg.guiText.text=str;
     }
    }else if(baiuPoint>=50&&Random.value>0.90){
      string str=""+nameJP;
      if(Random.value>0.5){
        str+="で食中毒が発生。";
      }else{
        str+="で土石流が発生。";
      }
      die =(int)(Random.value*population/3f);
      population-=die;
      if(population<0)population=0;
      str+=die+"人が死亡した。";
      if(die!=0){ 
       GameObject msg=(GameObject)Instantiate(alertPrefab);
       msg.guiText.text=str;
     }
    }else if(baiuPoint>=10&&Random.value>0.95){
      string str=""+nameJP;
      if(Random.value>0.5){
        str+="で食中毒が発生。";
      }else{
        str+="で土石流が発生。";
      }
      die =(int)(Random.value*population/5f);
      population-=die;
      if(population<0)population=0;
      str+=die+"人が死亡した。";
      if(die!=0){ 
       GameObject msg=(GameObject)Instantiate(alertPrefab);
       msg.guiText.text=str;
     }
    }
      TotalDieScript tds=GameObject.Find("Score").GetComponent<TotalDieScript>();
      int d=tds.die;
      d+=die;
      tds.die=d;
  }
  void OnMouseEnter () {
    areaInfo.setTarget(this.gameObject);
    state="hover";
    foreach(GameObject go in adjoins){

      go.GetComponent<AreaScript>().state="adjoins";
    }
  }
  void OnMouseDown () {
    PointVirus();
    
  }
  void OnMouseExit () {
    state="default";
    foreach(GameObject go in adjoins){
      go.GetComponent<AreaScript>().state="default";
    }
  }
  private static int[] POPULATION ={5506000,1373000,1330000,2348000,1086000,1169000,2029000,2970000,2008000,2008000,7195000,6216000,13159000,9048000,2374000,1093000,1170000,806000,863000,2152000,2081000,3765000,7411000,1855000,1411000,2636000,8865000,5588000,1401000,1002000,589000,717000,1945000,2861000,1451000,785000,996000,1431000,764000,5072000,850000,1427000,1817000,1197000,1135000,1706000,1393000};

  private static string[] NAME_JP ={"北海道","青森県","岩手県","宮城県","秋田県","山形県","福島県","茨城県","栃木県","群馬県",
"埼玉県","千葉県","東京都","神奈川県","新潟県","富山県","石川県","福井県","山梨県","長野県",
"岐阜県","静岡県","愛知県","三重県","滋賀県","京都府","大阪府","兵庫県","奈良県","和歌山県",
"鳥取県","島根県","岡山県","広島県","山口県","徳島県","香川県","愛媛県","高知県","福岡県",
"佐賀県","長崎県","熊本県","大分県","宮崎県","鹿児島県","沖縄県"};
 private static float[] TIME_CORRECT={
  0.8f,0.8f,0.8f,0.8f,0.8f,0.8f,
  0.85f,0.9f,0.4f,0.95f,0.96f,0.98f,
  0.1f,0.1f,0.1f,0.1f,0.96f,0.90f,
  0.88f,0.8f,0.8f,0.8f,0.8f,0.8f,};
 private static float[] CORRECT = {0.8f,0.8f,0.8f,0.8f,0.8f,0.8f,0.9f,1f,0.95f,1f,
1.2f,1.0f,1.0f,1.0f,1.0f,0.9f,0.9f,1.0f,1.0f,0.95f,
1.0f,1.0f,1.0f,1.0f,1.0f,1.0f,1.0f,1.0f,1.0f,1.0f,
1.1f,1.1f,1.1f,1.1f,1.1f,1.15f,1.15f,1.15f,1.15f,1.2f,
1.2f,1.2f,1.2f,1.2f,1.2f,1.2f,1.2f};
 private static int[,] ADJOINS= {



    {2,0,0,0,0,0,0},
    {1,3,5,0,0,0,0},
    {2,4,5,0,0,0,0},
    {3,5,6,7,0,0,0},
    {2,3,4,6,0,0,0},
    {4,5,7,15,0,0,0},
    {4,6,8,9,10,15,0},
    {7,9,11,12,0,0,0},
    {7,8,10,11,0,0,0},
    {7,9,11,15,20,0,0},
    {8,9,10,12,13,19,20},
    {8,11,13,0,0,0,0},
    {11,12,14,19,0,0,0},
    {13,19,22,0,0,0,0},
    {6,7,10,16,20,0,0},
    {15,17,20,21,0,0,0},
    {16,18,21,0,0,0,0},
    {17,21,25,26,0,0,0},
    {11,13,14,20,22,0,0},
    {10,15,16,19,21,22,23},
    {16,17,18,20,23,24,25},
    {14,19,20,23,0,0,0},
    {20,21,22,24,0,0,0},
    {21,23,25,26,29,30,0},
    {18,21,24,26,0,0,0},
    {18,25,24,27,28,29,0},
    {26,28,29,30,0,0,0},
    {26,27,31,33,36,0,0},
    {24,26,27,30,0,0,0},
    {24,27,29,0,0,0,0},
    {28,32,33,34,0,0,0},
    {31,34,35,0,0,0,0},
    {28,31,34,37,0,0,0},
    {31,32,33,35,38,0,0},
    {32,34,40,0,0,0,0},
    {28,37,38,39,0,0,0},
    {33,36,38,0,0,0,0},
    {34,36,37,38,0,0,0},
    {36,38,0,0,0,0,0},
    {35,41,43,44,0,0,0},
    {40,42,0,0,0,0,0},
    {41,0,0,0,0,0,0},
    {40,44,45,46,0,0,0},
    {40,43,45,0,0,0,0},
    {43,44,46,0,0,0,0},
    {43,45,47,0,0,0,0},
    {46,0,0,0,0,0,0}
  };

}
