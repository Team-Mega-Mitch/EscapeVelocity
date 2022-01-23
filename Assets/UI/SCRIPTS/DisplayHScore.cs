using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHScore : MonoBehaviour
{
    public Text ValueToUpdate;
    void Start()
    {
        ValueToUpdate.text = PlayerPrefs.GetString("HighScore", "0").ToString();
    }
}
