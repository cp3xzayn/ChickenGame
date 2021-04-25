using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadToTitle : MonoBehaviour
{
    public void OnClickToTitile()
    {
        FadeManager.Instance.LoadScene("Title", 1.0f);
    }
}
