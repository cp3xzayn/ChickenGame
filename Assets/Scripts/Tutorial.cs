using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] m_tutorialUI = null;

    private void Update()
    {
        TutorialSetActive();
    }

    /// <summary>
    /// TutorialのUIの表示非表示を決定する
    /// </summary>
    void TutorialSetActive()
    {
        foreach (var item in m_tutorialUI)
        {
            if (PlayerSetting.IsTutorial)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }
}
