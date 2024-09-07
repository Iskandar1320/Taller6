using UnityEngine;

namespace Contar
{
    public class Pajaros : MonoBehaviour
    {   
        void Update()
        {
            if(transform.position.y < -6f)
            {
                Destroy(gameObject);
            }
        }
    }
}
