using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Orb : MonoBehaviour
{
    //State Based Materials
    public Material mat_Chargeing;
    public Material mat_Active;
    public Material mat_Static;

    //Trail and tether references
    

    //Component References
    MeshRenderer MR;
    Rigidbody RB;
    SphereCollider SC;
    LineRenderer tether;
    TrailRenderer trail;

    //The level anchor
    Transform anchor;

    //Used to determine how large the "hitbox" is for dragging
    public Vector2 hitBoundary;

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

    void Awake()
    {
        //Co6mponenet Referencing
        MR = GetComponent<MeshRenderer>();
        RB = GetComponent<Rigidbody>();
        SC = GetComponent<SphereCollider>();
        tether = GetComponent<LineRenderer>();
        trail = GetComponent<TrailRenderer>();
    }

	void Start ()
    {
        Initialize();
        UpdateState();
	}
	
	void Update ()
    {
        UpdateInput();
        
        if (orbState == OrbState.Charging)
        {
            FollowMouse();
            UpdateTether();
        }
	}

    void Initialize()
    {
        //Links the anchor to the one in the current level
        anchor = Scr_Mgr_Game.inst.Manager_GamePlay.currentLevel.Anchor;
        //Sets the parent object tio the current level
        transform.parent = Scr_Mgr_Game.inst.Manager_GamePlay.currentLevel.transform;
        //Sets the initial position of the orb to that of thge anchor
        transform.position = anchor.position;
    }

    void UpdateInput()
    {
        if(Input.GetMouseButtonDown(0) && orbState == OrbState.Static && MouseOnOrb())
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

    void ChangeState(OrbState _orbState)
    {
        orbState = _orbState;
        UpdateState();
    }

    void UpdateState()
    {
        switch(orbState)
        {
            case OrbState.Static:
                MR.material = mat_Static;
                RB.useGravity = false;
                SC.enabled = false;
                tether.enabled = false;
                trail.enabled = false;
                break;
            case OrbState.Charging:
                MR.material = mat_Chargeing;
                RB.useGravity = false;
                SC.enabled = false;
                tether.enabled = true;
                trail.enabled = false;
                break;
            case OrbState.Active:
                MR.material = mat_Active;
                RB.useGravity = true;
                SC.enabled = true;
                tether.enabled = false;
                trail.enabled = true;
                break;
        }
    }

    void UpdateTether()
    {
        tether.SetPosition(0, transform.position);
        tether.SetPosition(1, (transform.position + anchor.position) / 2);
        tether.SetPosition(2, anchor.position);
    }

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

        positionDelta = transform.position - anchor.position;

        //Apply force
        RB.AddForce(positionDelta * -1 * launchForceMultiplier);
        trail.Clear();

        Scr_Mgr_Game.inst.Manager_GamePlay.currentLevel.Reload();
    }
}
