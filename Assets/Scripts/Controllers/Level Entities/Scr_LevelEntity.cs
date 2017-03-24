using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LevelEntity : MonoBehaviour
{
    //Object colour state
    public Scr_Orb.ObjectColour EntityColour;

    //Component References
    MeshRenderer MR;
    Collider CO;

    //Colour Materials
    public Material mat_Neutural;
    public Material mat_AltOne;
    public Material mat_AltTwo;

    //The force the object expels when it is consumed
    public float ExplosionForce;

    //The current level this entity is in
    public Scr_Level CurrentLevel;

    void Awake()
    {
        MR = GetComponent<MeshRenderer>();
        CO = GetComponent<Collider>();

        FindCurrentLevel();
    }

    public void ChangeColour(Scr_Orb.ObjectColour _EntityColour)
    {
        EntityColour = _EntityColour;
        UpdateColour();
    }

    public void UpdateColour()
    {
        switch (EntityColour)
        {
            case Scr_Orb.ObjectColour.Neutural:
                MR.material = mat_Neutural;
                break;
            case Scr_Orb.ObjectColour.AltOne:
                MR.material = mat_AltOne;
                break;
            case Scr_Orb.ObjectColour.AltTwo:
                MR.material = mat_AltTwo;
                break;
        }
    }

    void OnCollisionEnter(Collision _collision)
    {
        //If you collider with a player
        if(_collision.transform.gameObject.tag == "Player")
        {
            //If the player is the same colour as you
            if(_collision.transform.gameObject.GetComponent<Scr_Orb>().OrbColour == EntityColour)
            {
                Rigidbody PLRB = _collision.transform.gameObject.GetComponent<Rigidbody>();

                PLRB.AddForce((PLRB.transform.position - _collision.contacts[0].point).normalized * ExplosionForce);

                Destroy(this.gameObject);
                //Prompts the level to update how many entities are left in the level
                CurrentLevel.UpdateEntities(-1);
            }
        }
        //If you collider with another level entity
        else if (_collision.transform.gameObject.tag == "LevelEntity")
        {
            //If the player is the same colour as you
            if (_collision.transform.gameObject.GetComponent<Scr_LevelEntity>().EntityColour == EntityColour)
            {
                Destroy(this.gameObject);
                //Prompts the level to update how many entities are left in the level
                CurrentLevel.UpdateEntities(-1);
            }
        }
    }

    void FindCurrentLevel()
    {
        CurrentLevel = FindObjectOfType<Scr_Level>();
    }
}
