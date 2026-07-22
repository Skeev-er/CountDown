using UnityEngine;

public class S_Camera : MonoBehaviour
{
    private TargetJoint2D joint;
    private bool updateJointTarget = false;

    void Start()
    {
        joint = GetComponent<TargetJoint2D>();
    }

    private void Update()
    {
        // Actively follow the mouse
        if (updateJointTarget)
        {
            joint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
