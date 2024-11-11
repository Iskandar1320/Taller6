using UnityEngine;

namespace Pong
{
    public class Laterales : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D leftCollider;
        [SerializeField] private BoxCollider2D rightCollider;
        [SerializeField] private GameObject bg;

        void Start()
        {
            AdjustCollidersForAspect();
        }

        void AdjustCollidersForAspect()
        {
            // Detecta el aspecto de la pantalla
            float aspectRatio = (float) Screen.height/ Screen.width;
            print(aspectRatio);
            // Si el dispositivo es 16:9 (~1.77551f) ajustamos los colliders
            if (Mathf.Approximately(aspectRatio, 1.77551f))
            {
                leftCollider.offset = new Vector2(.31f, 0);
                rightCollider.offset = new Vector2(-.31f, 0);
            }
            if (Mathf.Approximately(aspectRatio, 1.5f))
            {
                leftCollider.offset = new Vector2(.84f, 0);
                rightCollider.offset = new Vector2(-.84f, 0);
                bg.transform.localScale = new Vector3(.35f, .25f, 1);
            }
        }
    }
}    
    
