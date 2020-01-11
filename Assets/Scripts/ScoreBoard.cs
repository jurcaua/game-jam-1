using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update

    //public for debuging
    public float score;
    Dictionary<string, int> scoreDict;
    Text text;
    void Start()
    {
        scoreDict = new Dictionary<string, int>
        {
            {"Ring", 100},
            {"Necklace",200},
            {"Scrap",5}
        };
        score = 0;
        text = GetComponent<Text>();
    }

    void clearscore()
    {
        score = 0f;
    }

    public void handleCollision(GameObject obj)
    {
        //string vType = obj.title;
        //score += scoreDict[vType];
        score += (float)obj.GetComponent<Valuable>().price;
    }
    // Update is called once per frame
    void Update()
    {
        score -= Time.deltaTime;
        text.text = "Score: " + score.ToString("0.");
        if (score > 100)
            text.color = Color.blue;
        if (score > 500)
            text.color = Color.red;
    }
}
