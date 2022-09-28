using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuantityRequest : MonoBehaviour
{
    public TextAsset csvFile;
    public int requestCount = 100;
    public string predictedMaterials = "";
    public string intendedQuantities = "";
    public string predictedQuantities = "";
    public string accuracyResults = "";
    public string generalOutput = "INTENDED QUANTITY" + "\t" + "PREDICTED QUANTITY" + "\t" + "PREDICTED MATERIAL" + "\t" + "ACCURACY" + "\t" + "OVERESTIMATION" + "\n";

    public int amountRight = 0;
    public float accuracy;
    public string analysisOutput = "MATERIAL" + "\t" + "ACCURACY" + "\n";

    public Dictionary<string, int> dataDictionary = new Dictionary<string, int>();
    public Dictionary<string, float> accuracyDictionary = new Dictionary<string, float>();

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
            GetPrediction(float.Parse(d["latitude"].ToString()), float.Parse(d["longitude"].ToString()), d["quantity"].ToString());
        }
    }

    public void GetPrediction(float latitude, float longitude, string quantity)
    {
        StartCoroutine(SendGetRequest("https://oceanpollutionflask.bigphan.repl.co/" + latitude + "/" + longitude, quantity));
    }

    private IEnumerator SendGetRequest(string url, string quantity)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {

                RequestInfo rI = RequestInfo.createFromJson(webRequest.downloadHandler.text);

                intendedQuantities += quantity + "\n";
                generalOutput += quantity;

                predictedQuantities += rI.quantity + "\n";
                generalOutput += "\t" + rI.quantity;

                predictedMaterials += rI.material + "\n";
                generalOutput += "\t" + rI.material;

                //calculate the accuracy 
                //compare your predicted quantity by your initial

                bool overestimate = rI.quantity > int.Parse(quantity);
                float accuracy = 1 - (Mathf.Abs(int.Parse(quantity) - rI.quantity * 1.0f) / Mathf.Max(int.Parse(quantity), rI.quantity));
                print("Initial Quantity: " + quantity + ", Predicted Quantity: " + rI.quantity.ToString() + ", Accuracy of: " + accuracy.ToString());

                generalOutput += "\t" + accuracy + "\t" + overestimate;

                float previousCalculatedAccuracy = 0;

                if (accuracyDictionary.ContainsKey(rI.material))
                {
                    accuracyDictionary.TryGetValue(rI.material, out previousCalculatedAccuracy);
                }
                else
                {
                    accuracyDictionary.TryAdd(rI.material, 0);
                }

                float updatedAccuracy = (previousCalculatedAccuracy + accuracy) / 2;
                accuracyDictionary[rI.material] = updatedAccuracy;
                generalOutput += "\n";
              
            }
        }
    }

    public void printAccuracyDictionary()
    {
        /*
        foreach (KeyValuePair<string, int> kp in dataDictionary)
        {
            print(kp.Key + " " + kp.Value);
        }

        print("-------");
        */


        foreach (KeyValuePair<string, float> kp in accuracyDictionary)
        {

            string material = kp.Key;
            float accuracy = accuracyDictionary[material];

            print(kp.Key + " " + dataDictionary[kp.Key] + " " + kp.Value);

            analysisOutput += material + "\t" + accuracy + "\n";

        }

        analysisOutput += accuracy;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            printAccuracyDictionary();
        }

    }
}
