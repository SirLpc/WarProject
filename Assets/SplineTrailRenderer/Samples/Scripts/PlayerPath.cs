using UnityEngine;
using System.Collections;

public class PlayerPath : MonoBehaviour 
{
	public SplineTrailRenderer trailReference;
	public Vector3 trailOffset = new Vector3(0, 0.02f, 0);

	private bool playerSelected = false;
	
    //todo 删除之前的无用代码
	void Update_kill () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

			//if(Physics.Raycast(ray, out hit, float.MaxValue, LayerNameToIntMask(playerLayerName)))
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                var ln = LayerMask.LayerToName(hit.transform.gameObject.layer);
                if (!ln.Equals(Consts.PlayerLayerName) || hit.transform.root != this.transform)
                    return;

                playerSelected = true;
                MoveOnFloor();
                trailReference.Clear();
            }
		}
		else if(Input.GetMouseButtonUp(0))
		{
			playerSelected = false;
		}

		if(Input.GetMouseButton(0) && playerSelected)
		{
			MoveOnFloor();
		}
	}

    public void StartMove()
    {
        playerSelected = true;
        MoveOnFloorStart();
        trailReference.Clear();
    }

    public void InMove()
    {
        if (!playerSelected)
            return;
        MoveOnFloor();
    }

    public void EndMove()
    {
        playerSelected = false;
    }

    void MoveOnFloorStart()
    {
        trailReference.transform.position = GetComponentInChildren<PlayerMove>().transform.position + trailOffset;
    }

    void MoveOnFloor()
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, 
			Input.mousePosition.y, 0)), out hit, float.MaxValue, LayerNameToIntMask(Consts.GroundLayerName)))
		{
			trailReference.transform.position = hit.point + trailOffset;
		}
	}

	static int LayerNameToIntMask(string layerName)
	{
		int layer = LayerMask.NameToLayer(layerName);

		if(layer == 0)
			return int.MaxValue;

		return 1 << layer;
	}
}
