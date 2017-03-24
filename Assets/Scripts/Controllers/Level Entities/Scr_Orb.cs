using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Orb : MonoBehaviour
{
    #region Variables
    //A reference to the current level the orb is in
    Scr_Level CurrentLevel;

    //Component References
    MeshRenderer MR;
    Rigidbody RB;
    SphereCollider SC;
    LineRenderer tether;
    TrailRenderer trail;

    //Colour Materials
    public Material mat_Neutural;
    public Gradient gra_Neutural;
    public Material mat_AltOne;
    public Gradient gra_AltOne;
    public Material mat_AltTwo;
    public Gradient gra_AltTwo;

    //The level anchor
    Transform anchor;

    //Used to determine how large the "hitbox" is for dragging
    public Vector2 hitBoundary;

    //How close the ball has to come to the anchor after being fired in order to dissappear
    public Vector2 tetherBoundary;

    //Used to maintain the orbs relative position to the mouse when draggin
    Vector3 mouseDragOffset;
    
    //The distance between the orb and the anchor
    public Vector3 positionDelta;

    //Adjusts the force the orb is launched with
    public float launchForceMultiplier;

    //Used to track the state of the orb
    public OrbState orbState;
    public  enum OrbState
    {
        Static,
        Charging,
        Active,
        OrbStateCount
    }

    public ObjectColour OrbColour;
    public enum ObjectColour
    {
        Neutural,
        AltOne,
        AltTwo,
        OrbColourCount
    }

    #endregion

    void Awake()
    {
        //Componenet Referencing
        MR = GetComponent<MeshRenderer>();
        RB = GetComponent<Rigidbody>();
        SC = GetComponent<SphereCollider>();
        tether = GetComponent<LineRenderer>();
        trail = GetComponent<TrailRenderer>();
    }

	void Start ()
    {
        UpdateState();
	}
	
	void Update ()
    {
        UpdateInput();
        UpdateTether();

        if (orbState == OrbState.Charging)
        {
            FollowMouse();
        }
	}

    public void Initialize(Transform _anchor, Transform _currentLevel)
    {
        //Links the anchor to the one in the current level
        anchor = _anchor;
        //Sets the parent object tio the current level
        transform.parent = _currentLevel;
        //Sets the initial position of the orb to that of thge anchor
        transform.position = anchor.position;
        //sets a script reference to the currentLevel
        CurrentLevel = _currentLevel.gameObject.GetComponent<Scr_Level>();
    }

    void UpdateInput()
    {
        #region Debug Input
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeColour(ObjectColour.Neutural);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeColour(ObjectColour.AltOne);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeColour(ObjectColour.AltTwo);
        }
        #endregion

        if (Input.GetMouseButtonDown(0) && orbState == OrbState.Static && MouseOnOrb())
        {
            ChangeState(OrbState.Charging);
        }

        if(Input.GetMouseButtonUp(0) && orbState == OrbState.Charging)
        {
            LaunchOrb();
        }
    }

    void FollowMouse()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseDragOffset;
        newPosition.z = 0;
        transform.position = newPosition;

        
    }
    
    //Changes the orbs state based on given parameter and Updates the state.
    void ChangeState(OrbState _orbState)
    {
        orbState = _orbState;
        UpdateState();
    }

    // Changes the properties of the orb based on its current state (Static, Charging, Active).
    void UpdateState()
    {
        switch(orbState)
        {
            case OrbState.Static:
                RB.useGravity = false;
                SC.enabled = false;
                tether.enabled = false;
                trail.enabled = false;
                break;
            case OrbState.Charging:
                RB.useGravity = false;
                SC.enabled = false;
                tether.enabled = true;
                trail.enabled = false;
                break;
            case OrbState.Active:
                RB.useGravity = true;
                SC.enabled = true;
                //This tether deactivation will be shifted to another part in code
                tether.enabled = false;
                trail.enabled = true;
                break;
        }
    }

    //Updates the properties of the tether connecting the anchor and orb
    void UpdateTether()
    {
        tether.SetPosition(0, transform.position);
        //This needs to change to adjust based on length of tether
        tether.SetPosition(1, (transform.position + anchor.position) / 2);
        tether.SetPosition(2, anchor.position);
    }

    //Returns true if the mouse is currently inside the Drag Boundary
    bool MouseOnOrb()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x + -hitBoundary.x &&
            mousePosition.x < transform.position.x +  hitBoundary.x  &&
            mousePosition.y > transform.position.y + -hitBoundary.y &&
            mousePosition.y < transform.position.y +  hitBoundary.y)
        {
            mouseDragOffset = transform.position - mousePosition;
            return true;
        }
        return false;
    }

    void LaunchOrb()
    {
        ChangeState(OrbState.Active);

        positionDelta = anchor.position - transform.position;

        //Apply force
        RB.AddForce(positionDelta * launchForceMultiplier);
        trail.Clear();

        CurrentLevel.UpdateOrbsFired(1);

        CurrentLevel.Reload();
    }

    public void ChangeColour(ObjectColour _OrbColour)
    {
        OrbColour = _OrbColour;
        UpdateColour();
    }

    public void UpdateColour()
    {
        switch (OrbColour)
        {
            case ObjectColour.Neutural:
                MR.material = mat_Neutural;
                trail.colorGradient = gra_Neutural;
                break;
            case ObjectColour.AltOne:
                MR.material = mat_AltOne;
                trail.colorGradient = gra_AltOne;
                break;
            case ObjectColour.AltTwo:
                MR.material = mat_AltTwo;
                trail.colorGradient = gra_AltTwo;
                break;
        }
    }
}

/*
 Colour codes
 000 56C4C5
 001 FCC777
 002 BD7CB4
*/
