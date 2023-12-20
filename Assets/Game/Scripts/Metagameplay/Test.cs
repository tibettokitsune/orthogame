using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public int avgFrameRate;
    public Text display_Text;
    private void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
#endif
    }
    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
}
