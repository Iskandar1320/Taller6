using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    List<GameObject> _touchTiles = new List<GameObject>();
    public float xOffsset = 0;
    public float yOffsset = 0;
    private float nextYRotation = 90f;
    private GameObject clickedTile;
    void Start()
    {
        
    }

    // Update is called once per fra
    public void ClearTileList()
    {
        _touchTiles.Clear();
    }
    public Vector3 GetOffsetVec(Vector3 tilePos)
    {
        return new Vector3(tilePos.x + xOffsset, tilePos.y + yOffsset, 0);
        
    }
    public void RotateShip()
    {
        _touchTiles.Clear();
        transform.localEulerAngles += new Vector3(0, 0, nextYRotation);
        nextYRotation *= -1;
        float temp = xOffsset;
        xOffsset = yOffsset;
        yOffsset = temp;
        SetPosition(clickedTile.transform.position);
    }
    public void SetPosition(Vector3 newVec)
    {
        transform.localPosition = new Vector3(newVec.x + xOffsset, newVec.y + yOffsset, 0);
    }
    public void SetClickedTile(GameObject tile)
    {
        clickedTile = tile;
    }
}
