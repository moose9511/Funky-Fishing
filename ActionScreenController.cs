using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionScreenController : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public static Animation screen;

    private void Start()
    {
        screen = Text.GetComponent<Animation>();
    }
    public static void PlayAnimation()
    {
        screen.Play();
    }

    public void CaughtSomethingScreen()
    {
        Text.text = "You Caught Something";
    }

    public void CustomizeScreen(string sentence)
    {
        Text.text = sentence;
    }
}
