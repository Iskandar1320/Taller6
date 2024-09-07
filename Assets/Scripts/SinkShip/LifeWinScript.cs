using TMPro;
using UnityEngine;

namespace SinkShip
{
    public class LifeWinScript : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI _lifesBlue;
        [SerializeField] TextMeshProUGUI _lifesRed;
        [SerializeField] ShipsPlayerController _playerControllerBlue;
        [SerializeField] ShipsPlayerController _playerControllerRed;
        private int lifesRed;
        private int lifesBlue;

        // Start is called before the first frame update
        private void Awake()
        {
            lifesBlue = _playerControllerBlue.lifes;
            lifesRed = _playerControllerRed.lifes;
        }

        // Update is called once per frame
        void Update()
        {
            lifesBlue = _playerControllerBlue.lifes;
            lifesRed = _playerControllerRed.lifes;
            _lifesBlue.text = lifesBlue.ToString();
            _lifesRed.text = lifesRed.ToString();

        }
    }
}
