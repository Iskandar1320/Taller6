using UnityEngine;

namespace Pong
{
    public class Laterales : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D pelotaGolpe)
        {
            if(pelotaGolpe.gameObject.GetComponent<ControladorPelota>() != null)
            {
                pelotaGolpe.gameObject.GetComponent<ControladorPelota>().GolpeLateral();
            }
        }
    }
}
