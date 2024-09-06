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
        if (this.gameObject.name is "Boton ready" or "Boton girar")
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


        if (this.gameObject.name is "SaveBtn")
        {
            Physics.queriesHitTriggers = false;

            Physics.queriesHitTriggers = true;

        }
    }
}
