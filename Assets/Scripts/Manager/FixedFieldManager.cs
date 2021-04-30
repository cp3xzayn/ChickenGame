using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFieldManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_fixedFields = null;

    /// <summary>
    /// ランダムで固定のオブジェクトのIndexを返すメソッド
    /// </summary>
    /// <returns></returns>
    int FixedFieldIndex()
    {
        int index = Random.Range(0, m_fixedFields.Length);
        return index;
    }

    /// <summary>
    /// ランダムで決まった固定のオブジェクトを返す。
    /// </summary>
    /// <returns></returns>
    public GameObject SelectedFixedField()
    {
        return m_fixedFields[FixedFieldIndex()];
    } 
}
