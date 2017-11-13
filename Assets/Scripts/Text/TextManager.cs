using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextManager : MonoBehaviour {
    [SerializeField]
    private string interactionFileName, objectFileName, language = "EN";
    private string interactionDictionaryName = "INTERACTIONS", objectDictionaryName = "OBJECTS";
    private Dictionary<string, Dictionary<string, string>> textDictionnary;

    private static TextManager instance;
    private IEnumerator LoadInteractionText()
    {
        Dictionary<string,string> obj = new Dictionary<string, string>();
        string filePath = "Text/" + language + "/" + interactionFileName;

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        string[] textLines = targetFile.text.Split('\n');
        Debug.Log(textLines[0] + " " + textLines[1  ]);
        foreach(string line in textLines)
        {
            if(line != "")
            {
                string[] split = line.Split(':');
                obj.Add(split[0], split[1]);
            } 
        }
        Debug.Log(obj);
        textDictionnary.Add(interactionDictionaryName, obj);
        yield return null;
    }

    private IEnumerator LoadObjectsText()
    {
        Dictionary<string, string> obj = new Dictionary<string, string>();
        string filePath = "Text/" + language + "/" + objectFileName;

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        string[] textLines = targetFile.text.Split('\n');
        Debug.Log(textLines[0] + " " + textLines[1]);
        foreach (string line in textLines)
        {
            if (line != "")
            {
                string[] split = line.Split(':');
                obj.Add(split[0], split[1]);
            }
        }
        textDictionnary.Add(objectDictionaryName, obj);
        yield return null;
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        textDictionnary = new Dictionary<string, Dictionary<string, string>>();
        StartCoroutine(LoadInteractionText());
        StartCoroutine(LoadObjectsText());
    }

    public string GetInteraction(string key)
    {
        string name = "NAME_NOT_FOUND";
        if(textDictionnary.ContainsKey(interactionDictionaryName))
        {
            if (textDictionnary[interactionDictionaryName].ContainsKey(key))
            {
                name = textDictionnary[interactionDictionaryName][key];
            }      
        }
        return name;
    }

    public string GetObject(string key)
    {
        string name = "NAME_NOT_FOUND";
        if (textDictionnary.ContainsKey(objectDictionaryName))
        {
            if (textDictionnary[objectDictionaryName].ContainsKey(key))
            {
                return textDictionnary[objectDictionaryName][key];
            }
        }
        return name;
    }

    public static TextManager GetInstance()
    {
        return instance;
    }
}
