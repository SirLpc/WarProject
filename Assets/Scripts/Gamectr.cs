using UnityEngine;
using System.Collections;

public class Gamectr : MonoBehaviour
{
    private PlayerPath _currentPath = null;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Transform tr = PoolTotleMgr.Instance.Spawn(Consts.TroopRootPrefName);
            tr.GetComponent<PlayerCtr>().InitTroopAs((TroopType)i, Random.Range(9, 30));
            tr.transform.position = Vector3.forward*i*2  + Vector3.back * 4;
        }
    }
	
	private void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                var ln = LayerMask.LayerToName(hit.transform.gameObject.layer);
                if (!ln.Equals(Consts.PlayerLayerName))
                    return;
                _currentPath = hit.transform.GetComponentInParent<PlayerPath>();
                if (_currentPath == null)
                    return;
                _currentPath.StartMove();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_currentPath != null)
                _currentPath.EndMove();
        }

        if (Input.GetMouseButton(0))
        {
            if (_currentPath != null)
                _currentPath.InMove();
        }
    }
}
