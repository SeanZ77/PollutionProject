using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequestTest : MonoBehaviour
{
    public TextAsset csvFile;
    public int requestCount = 100;

    public string materials = "";
    public string predictedMaterials = "";
    public string accuracyResults = "";
    public string generalOutput = "INTENDED MATERIAL" + "\t" + "PREDICTED MATERIAL" + "\t" + "ACCURATE?" + "\n";

    public int amountRight = 0;
    public float accuracy;
    public string analysisOutput = "MATERIAL" + "\t" + "AMOUNT TESTED" + "\t" + "AMOUNT RIGHT" + "\t" + "ACCURACY" + "\n";

    public Dictionary<string, int> dataDictionary = new Dictionary<string, int>();
    public Dictionary<string, int> accuracyDictionary = new Dictionary<string, int>();

    private void Start()
    {
        GetAllPredictions();
    }

    public void GetAllPredictions()
    {
        List<Dictionary<string, object>> data = CSVReaderTest.Read(csvFile);
        print(data);

        //get 100 or so random choices
        List<Dictionary<string, object>> experimentData = new List<Dictionary<string, object>>();
        for (int i = 0; i < requestCount; i++)
        {
            //repeat this until you get something of value (no blank materials)
            bool validMaterial = false;
            Dictionary<string, object> lineToAdd = null;
            while (validMaterial == false)
            {
                lineToAdd = data[Random.RandomRange(0, data.Count)];
                validMaterial = lineToAdd["material"].ToString() != "";
            }
            experimentData.Add(lineToAdd);

            if (dataDictionary.ContainsKey(experimentData[i]["material"].ToString()))
            {
                dataDictionary[experimentData[i]["material"].ToString()]++;
            }
            else
            {
                dataDictionary.TryAdd(experimentData[i]["material"].ToString(), 1);
            }
        }
        //for every choice, get the prediction
        foreach (Dictionary<string, object> d in experimentData)
        {
            GetPrediction(float.Parse(d["latitude"].ToString()), float.Parse(d["longitude"].ToString()), d["material"].ToString());
        }
    }

    public void GetPrediction(float latitude, float longitude, string material)
    {
        StartCoroutine(SendGetRequest("https://oceanpollutionflask.bigphan.repl.co/" + latitude + "/" + longitude, material));
    }

    private IEnumerator SendGetRequest(string url, string material) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success) {
                print("expected " + material + ", received " + webRequest.downloadHandler.text);

                RequestInfo rI = RequestInfo.createFromJson(webRequest.downloadHandler.text);

                materials += "\n" + material;
                generalOutput += material;
                predictedMaterials += "\n" + rI.material;
                generalOutput += "\t" + rI.material;

                int previousAmountRightForThisDebris = 0;
                if (accuracyDictionary.ContainsKey(material))
                {
                    accuracyDictionary.TryGetValue(material, out previousAmountRightForThisDebris);
                }
                else
                {
                    accuracyDictionary.TryAdd(material, 0);
                }

                if (material == rI.material)
                {
                    accuracyResults += "\n1";
                    generalOutput += "\t" + "1" + "\n";
                    amountRight++;
                    accuracyDictionary[material]++;
                }
                else {
                    accuracyResults += "\n0";
                    generalOutput += "\t" + "0" + "\n";
                }
                accuracy = (float)amountRight / requestCount;
            }
        }
    }

    public void printAccuracyDictionary() {
        /*
        foreach (KeyValuePair<string, int> kp in dataDictionary)
        {
            print(kp.Key + " " + kp.Value);
        }

        print("-------");
        */


        foreach (KeyValuePair<string, int> kp in accuracyDictionary) {
            int amountTested, amountRight;
            float accuracy;

            string material = kp.Key;
            amountTested = dataDictionary[material];
            amountRight = kp.Value;
            accuracy = (float)amountRight / amountTested;

            print(kp.Key + " " + dataDictionary[kp.Key] + " " + kp.Value);

            analysisOutput += material + "\t" + amountTested + "\t" + amountRight + "\t" + accuracy + "\n";

        }

        analysisOutput += accuracy;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            printAccuracyDictionary();
        }

    }
}
