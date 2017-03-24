using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_MedalIndicator : MonoBehaviour
{
    public Color Fail;
    public Color Sufficient;
    public Color Exceptional;

    public void UpdateIndicator(int _OrbsFired, int _SufficientScore, int _ExceptionalScore)
    {
        if(_OrbsFired <= _ExceptionalScore -1)
        {
            GetComponent<Image>().color = Exceptional;
        }
        else if(_OrbsFired <= _SufficientScore -1)
        {
            GetComponent<Image>().color = Sufficient;
        }
        else
        {
            GetComponent<Image>().color = Fail;
        }
    }

    public void UpdateEndOfLevelIndicator(int _OrbsFired, int _SufficientScore, int _ExceptionalScore)
    {
        if (_OrbsFired <= _ExceptionalScore)
        {
            GetComponent<Image>().color = Exceptional;
        }
        else if (_OrbsFired <= _SufficientScore)
        {
            GetComponent<Image>().color = Sufficient;
        }
        else
        {
            GetComponent<Image>().color = Fail;
        }
    }
}
