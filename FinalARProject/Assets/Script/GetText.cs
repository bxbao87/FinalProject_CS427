using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public class GetText : MonoBehaviour
{

    public GameObject TextObject;

    void Start(){
        string ReadFromFilePath = Application.streamingAssetsPath + "/Animal_Information/" + "wolf.txt";
        List<string> filelines = File.ReadAllLines(ReadFromFilePath).ToList();

        TextObject.GetComponent<Text>().text = ""; // reset text objet
        foreach(string line in filelines){
            string s = TextObject.GetComponent<Text>().text;
            TextObject.GetComponent<Text>().text = s + '\n' + line;
        }
    }

}
