using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ships;
    bool _setupComplete = false;
    public bool _player1Turn = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  public void TileClicked(GameObject tile)
    {
        if (_setupComplete && _player1Turn)
        {
            //misile
        }
        else if (!_setupComplete) PlaceShip(tile);
    }
    public void PlaceShip(GameObject tile)
    {

    }
}
