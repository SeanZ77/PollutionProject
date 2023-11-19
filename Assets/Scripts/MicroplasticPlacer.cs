using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape { 
    sphere, fiber, film
}

public class MicroplasticPlacer : MonoBehaviour
{
    public TextAsset csvFile;
    public int spawnAmount;
    public GameObject prefab;
    public Dictionary<Shape, Sprite> spriteDictionary = new Dictionary<Shape, Sprite>();
    public Dictionary<Shape, Color> colorDictionary = new Dictionary<Shape, Color>();
    public Sprite sphere, fiber, film;
    public Color sphereColor, fiberColor, filmColor;
    public float maxHeight, maxWidth;
    public Dictionary<Shape, List<GameObject>> plasticDictionary = new Dictionary<Shape, List<GameObject>>();
    public bool showSpheres = true, showFibers = true, showFilms = true;

    private void Awake()
    {
        spriteDictionary.Add(Shape.sphere, sphere);
        spriteDictionary.Add(Shape.fiber, fiber);
        spriteDictionary.Add(Shape.film, film);
        colorDictionary.Add(Shape.sphere, sphereColor);
        colorDictionary.Add(Shape.fiber, fiberColor);
        colorDictionary.Add(Shape.film, filmColor);
        plasticDictionary.Add(Shape.sphere, new List<GameObject>());
        plasticDictionary.Add(Shape.fiber, new List<GameObject>());
        plasticDictionary.Add(Shape.film, new List<GameObject>());

        List<Dictionary<string, object>> data = CSVReaderTest.Read(csvFile);
       
        for (int i = 0; i < spawnAmount; i++)
        {
            float verticalVelocity = float.Parse(data[i]["0"].ToString());
            float verticalPosition = 1000 - float.Parse(data[i]["1"].ToString());
            Shape shape = stringToShape(data[i]["2"].ToString());
            float x = i * maxWidth / spawnAmount;
            float y = verticalPosition / 1000 * maxHeight;
            GameObject p = spawnPlastic(new Vector3(x, y) + transform.position, verticalVelocity, verticalPosition, shape);
            plasticDictionary[shape].Add(p); 
        }
    }

    private void Update()
    {
        foreach(GameObject p in plasticDictionary[Shape.sphere]) {
            p.SetActive(showSpheres);
        }
        foreach (GameObject p in plasticDictionary[Shape.fiber])
        {
            p.SetActive(showFibers);
        }
        foreach (GameObject p in plasticDictionary[Shape.film])
        {
            p.SetActive(showFilms);
        }
    }

    GameObject spawnPlastic(Vector2 position, float verticalVelocity, float verticalPosition, Shape shape) {
        GameObject gameObject = Instantiate(prefab, position, Quaternion.identity);
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteDictionary[shape];
        spriteRenderer.color = colorDictionary[shape];
        PlasticDisplay plasticDisplay = gameObject.GetComponent<PlasticDisplay>();
        plasticDisplay.SetValues(verticalVelocity.ToString(), verticalPosition.ToString(), shape.ToString());
        return gameObject; 
    }

    Shape stringToShape(string s) {
        if (s.Equals("sphere")) {
            return Shape.sphere;
        }
        if (s.Equals("fiber"))
        {
            return Shape.fiber;
        }
        if (s.Equals("film"))
        {
            return Shape.film;
        }
        return Shape.sphere;
    }

    public void toggleSphere()
    {
        toggleVisibility(Shape.sphere);
    }

    public void toggleFiber()
    {
        toggleVisibility(Shape.fiber);
    }

    public void toggleFilm()
    {
        toggleVisibility(Shape.film);
    }

    public void toggleVisibility(Shape shape) {
        if (shape == Shape.sphere) {
            showSpheres = !showSpheres;
        }
        if (shape == Shape.fiber)
        {
            showFibers = !showFibers; 
        }
        if (shape == Shape.film)
        {
            showFilms = !showFilms;
        }
    }
}
