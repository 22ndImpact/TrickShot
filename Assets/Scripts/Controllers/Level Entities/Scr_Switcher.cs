using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Switcher : MonoBehaviour
{
    //Object colour state
    public Scr_Orb.ObjectColour SwitcherColour;

    void OnTriggerEnter(Collider _collider)
    {
        Debug.Log("Trigger Enter");
        //If you collider with a player
        if (_collider.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            //Change the objects colour
            _collider.gameObject.GetComponent<Scr_Orb>().ChangeColour(SwitcherColour);
        }
        //If you collider with another level entity
        else if (_collider.gameObject.tag == "LevelEntity")
        {
            Debug.Log("Hit entity");
            //Change the objects colour
            _collider.gameObject.GetComponent<Scr_LevelEntity>().ChangeColour(SwitcherColour);
        }
    }
}
