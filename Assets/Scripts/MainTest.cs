using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainTest : MonoBehaviour
{

	public TextAsset csvFile;

	void Awake()
	{

		List<Dictionary<string, object>> data = CSVReaderTest.Read(csvFile);

		for (var i = 0; i < data.Count; i++)
		{
			print(data[i]["Name"] + " " +
				   data[i]["Description"] + " " +
				   data[i]["Website"] + " " +
				    data[i]["Latitude"] + " " + data[i]["Longitude"]);
		}

	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}
}