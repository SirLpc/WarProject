using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
	public float speed;
	public SplineTrailRenderer trailReference;
	public Animation anim;

	private float distance = 0;
	private Vector3 lastPosition;
    private bool _pause;

    public void Init()
    {
        _pause = false;
        lastPosition = transform.position;
    }

    public void CancelMove()
    {
        trailReference.Clear();
    }

    public void PauseMove(bool pause)
    {
        _pause = pause;
    }

    public void ChangSpeed(float targetSpeed)
    {
       speed = targetSpeed;
    }

    private void Start()
	{
		lastPosition = transform.position;
	}

 	private void FixedUpdate ()
    {
        if (_pause)
            return;

        MovePlayerFixedUpdate();
    }

    private void MovePlayerFixedUpdate()
    {
        float length = trailReference.spline.Length();

        distance = Mathf.Clamp(distance + speed * Time.deltaTime, 0, length - 0.1f);
        trailReference.maxLength = Mathf.Max(length - distance, 0);

        Vector3 forward = trailReference.spline.FindTangentFromDistance(distance);
        Vector3 position = trailReference.spline.FindPositionFromDistance(distance);

        if (forward != Vector3.zero)
        {
            if (anim != null)
            {
                if (lastPosition == position)
                {
                    anim.CrossFade("Idle");
                }
                else
                {
                    anim.CrossFade("Walk");
                }
            }
            forward = new Vector3(forward.x, 0, forward.z);
            transform.forward = forward;
            transform.position = lastPosition = position;
        }
    }
}
