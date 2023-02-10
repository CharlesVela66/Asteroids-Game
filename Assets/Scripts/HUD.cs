using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// HUD
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    float elapsedSeconds = 0;

    bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            text.text = ((int)elapsedSeconds).ToString();
        }
    }
    public void StopGameTimer()
    {
        running= false;
    }
}
