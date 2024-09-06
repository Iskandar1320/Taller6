using UnityEngine;

namespace RadarMadness
{
    public class FakeBttn : MonoBehaviour
    {
        GameManager _gm;

        // Start is called before the first frame update
        private void Start()
        {
            _gm = FindAnyObjectByType<GameManager>();
        }
        public void OnMouseUpAsButton()
        {
            if (this.gameObject.name is "Boton ready" or "Boton girar")
            {

                var btns = FindObjectsOfType<FakeBttn>();

                Physics.queriesHitTriggers = false;

                if (this.CompareTag("BotonR"))
                {
                    _gm.NextShipClicked();

                }
                else if (this.CompareTag("BotonRotar"))
                {
                    _gm.RotateClicked();
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

}
