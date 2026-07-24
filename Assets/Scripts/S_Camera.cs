using UnityEngine;

public class S_Camera : MonoBehaviour
{
    private TargetJoint2D joint;
    private bool updateJointTarget = false;
    public bool changeZoom = false;
    private Camera cam;
    private float t = 0;
    private bool zoomedIn = false;

    void Start()
    {
        joint = GetComponent<TargetJoint2D>();
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Actively follow the mouse
        if (updateJointTarget)
        {
            joint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Triggered by S_Sniper when aiming mode changes
        if (changeZoom)
        {
            t += Time.deltaTime * 2;

            if (!zoomedIn)
            {
                cam.orthographicSize = Mathf.Lerp(7, 2.5f, t);
            }
            else
            {
                cam.orthographicSize = Mathf.Lerp(2.5f, 7, t);
            }

            if (t >= 1)
            {
                changeZoom = false;
                zoomedIn = !zoomedIn;
                t = 0;
            }
        }
    }

    public void Aiming(bool aiming)
    {
        // If aiming, follow the mouse slowly
        if (aiming)
        {
            updateJointTarget = true;
            joint.maxForce = 50;
        }
        // Else, move back to the middle of the level quickly
        else
        {
            updateJointTarget = false;
            joint.target = new Vector2(0, 0);
            joint.maxForce = 1000;
        }
    }
}
