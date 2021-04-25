using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadToGame : MonoBehaviour
{
    public void OnClickStart()
    {
        FadeManager.Instance.LoadScene("GameScene", 1.0f);
    }
}
