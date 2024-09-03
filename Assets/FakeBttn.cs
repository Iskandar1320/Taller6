using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBttn : MonoBehaviour
{
    GameManager gm;

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }
    public void OnMouseUpAsButton()
    {
        FakeBttn[] btns = FindObjectsOfType<FakeBttn>();

        Physics.queriesHitTriggers = false;

        if (this.CompareTag("BotonR"))
        {
        gm.NextShipCliked();

        }
        else if (this.CompareTag("BotonRotar"))
        {
         gm.RotateClicked();
        }
        this.gameObject.SetActive(false);
        foreach (FakeBttn script in btns)
        {
            script.gameObject.SetActive(false);
        }
        Physics.queriesHitTriggers = true;
    }
}
