using UnityEngine;

namespace Memoria
{
    public class MemoriaImagen : MonoBehaviour
    {
        [SerializeField]
        private GameObject image_unknown;
        [SerializeField]
        private ControllerMemoria gameController;

        [SerializeField] private AudioSource aS;
        private int _spriteId;
        public int spriteId
        {
            get { return _spriteId; }
        }

        private void OnMouseDown()
        {
            if (image_unknown.activeSelf && gameController.canOpen)
            {
                image_unknown.SetActive(false);
                aS.Play();
                gameController.ImageOpened(this);
            }
        }

        public void ChangeSprite(int id, Sprite image)
        {
            _spriteId = id;
            GetComponent<SpriteRenderer>().sprite = image;
        }
        public void Close()
        {
            image_unknown.SetActive(true);
        }
    }
}
