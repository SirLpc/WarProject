
using UnityEngine;

public static class Utils
{

    public static Direction GetDirection(Transform me, Transform other)
    {
        Direction result;
        Vector3 forward = me.TransformDirection(Vector3.forward);
        Vector3 toOther = other.transform.position - me.position;
        float dirForward = Vector3.Dot(forward, toOther);

        Vector3 left = me.TransformDirection(Vector3.right);
        Vector3 toOtherL = other.position - me.position;
        float dirLeft = Vector3.Dot(left, toOtherL);

        if (Mathf.Abs(dirForward) > Mathf.Abs(dirLeft))
        {
            if (dirForward < 0)
            {
                result = Direction.BEHIND;
                //Debug.Log(string.Format("I am {0}, dir is {1}, I's behind us!", me.parent.name, dirForward));
            }
            else
            {
                result = Direction.AHEAD;
                //Debug.Log(string.Format("I am {0}, dir is {1}, I's before us!", me.parent.name, dirForward));
            }
        }
        else
        {
            if (dirLeft < 0)
            {
                result = Direction.LEFT;
            }
            else
            {
                result = Direction.RIGHT;
            }
        }

        return result;
    }

}
