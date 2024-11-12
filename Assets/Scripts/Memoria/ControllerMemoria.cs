using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Memoria
{
    public class ControllerMemoria : MonoBehaviour
    {
        private const int columns = 2;
        private const int rows = 6;
        private const float Xspace = 1.5f;
        private const float Yspace = -1f;

        [SerializeField] private MemoriaImagen _startObject;
        [SerializeField] private Sprite[] _images;

        private GameObject primaryGroup;
        private GameObject secondaryGroup;

        private int currentPlayer = 1;
        private int scorePlayer1 = 0;
        private int scorePlayer2 = 0;
        private SceneTransitions _sceneTransitions;
        bool ganador;
        private int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 }; // Lista de ubicaciones de clones
        [SerializeField] private TextMeshProUGUI scoreTextPlayer1;
        [SerializeField] private TextMeshProUGUI scoreTextPlayer2;
        [SerializeField] private GameObject panelwin;
        [SerializeField] private Image colorpanel;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private SpriteRenderer _mesa;
        [SerializeField] private Animator animator;
        
        bool allPrimaryChildrenAtTargetPosition;  // Para verificar si todos los hijos de primaryGroup están en su lugar
        bool allSecondaryChildrenAtTargetPosition;  // Para verificar si todos los hijos de secondaryGroup están en su lugar
        private bool escalame;
        private int totalClones; // Total de clones creados
        private int clonesAssignedToGroups; // Clones que ya han sido movidos y asignados
        private int timex = 7;

        [SerializeField] private AudioSource winAudio;
        private void Start()
        {
            _sceneTransitions = FindObjectOfType<SceneTransitions>();
            locations = Randomizer(locations);

            primaryGroup = new GameObject("PrimaryGroup");
            secondaryGroup = new GameObject("SecondaryGroup");

            float totalWidth = (columns - 1) * Xspace;
            float totalHeight = (rows - 1) * Mathf.Abs(Yspace);
            Vector3 startPosition = _startObject.transform.position;
            Vector3 offset = new Vector3(-totalWidth / 2, totalHeight / 2, 0);

            _startObject.transform.SetParent(primaryGroup.transform);

            totalClones = columns * rows; // Calcula el total de clones esperados

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    MemoriaImagen gameImage;
                    if (i == 0 && j == 0)
                    {
                        gameImage = _startObject;
                    }
                    else
                    {
                        gameImage = Instantiate(_startObject) as MemoriaImagen;
                    }

                    int index = j * columns + i;
                    int id = locations[index];
                    gameImage.ChangeSprite(id, _images[id]);

                    float positionX = Xspace * i;
                    float positionY = Yspace * j;
                    Vector3 targetPosition = startPosition + offset + new Vector3(positionX, positionY, 0);

                    gameImage.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutBounce)
                        .OnComplete(() => AssignCloneToGroup(gameImage));
                }
            }

            UpdateTurnText();
        }
        private void AssignCloneToGroup(MemoriaImagen clone)
        {
            if (clone.transform.position.x == -.75f)
            {
                clone.transform.SetParent(primaryGroup.transform);
            }
            else if (clone.transform.position.x == .75f)
            {
                clone.transform.SetParent(secondaryGroup.transform);
            }

            clonesAssignedToGroups++; // Incrementa el contador de clones asignados

            // Si todos los clones han sido asignados, verifica las posiciones y aplica la escala
            
        }
        private void ApplyScaleIfAllAtTargetPosition()
        {
            float aspectRatio = (float)Screen.height / Screen.width;

            if (allPrimaryChildrenAtTargetPosition && allSecondaryChildrenAtTargetPosition && Mathf.Approximately(aspectRatio, 1.5f) && !escalame)
            {
                escalame = true;

                // Escala con animación a ambos grupos
                primaryGroup.transform.DOScale(Vector3.one * 0.5f, 0.5f).SetEase(Ease.OutBounce);
                secondaryGroup.transform.DOScale(Vector3.one * 0.5f, 0.5f).SetEase(Ease.OutBounce);

                RepositionAndScaleChildren(primaryGroup, new Vector3(-0.75f, 0f, 0f), Vector3.one * 0.5f, new Vector3(0, -1f, 0));
                RepositionAndScaleChildren(secondaryGroup, new Vector3(0.75f, 0f, 0f), Vector3.one * 0.5f, new Vector3(0, -1f, 0));
            }
        }


        private void Update()
        {

            if (clonesAssignedToGroups == totalClones)
            {
                ApplyScaleIfAllAtTargetPosition();
            }
            Parenter();
            
            if (!ganador)
            {
                if (scorePlayer1 + scorePlayer2 == 6)
                {
                    ganador = true;
                    Winner();
                }
            }

            print("primary "+allPrimaryChildrenAtTargetPosition + "secon " + allSecondaryChildrenAtTargetPosition);
            if ( Time.deltaTime>= timex)
            {
                Porfavor();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        bool Parenter()
        {
            if (!allPrimaryChildrenAtTargetPosition == !allSecondaryChildrenAtTargetPosition)
            {
                foreach (Transform child in primaryGroup.transform)
                {
                    if (child.position.x != 0.75f)
                    {
                        child.SetParent(primaryGroup.transform);
                        allPrimaryChildrenAtTargetPosition = true;
                    }
                }

                foreach (Transform child in secondaryGroup.transform)
                {
                    if (child.position.x != -0.75f)
                    {
                        child.SetParent(secondaryGroup.transform);
                        allSecondaryChildrenAtTargetPosition = true;
                    }
                }
            }

           

            return allPrimaryChildrenAtTargetPosition && allSecondaryChildrenAtTargetPosition;
        }
        private void RepositionAndScaleChildren(GameObject group, Vector3 basePosition, Vector3 fixedScale, Vector3 incrementalPosition)
        {
            int index = 0;
            foreach (Transform child in group.transform)
            {
                // Calcula la nueva posición sumando incrementalPosition por el índice actual
                Vector3 newPosition = basePosition + (incrementalPosition * index);

                // Aplica la posición y la escala fija usando DOTween para la animación
                child.DOMove(newPosition, 0.5f).SetEase(Ease.OutBounce);
                child.DOScale(fixedScale, 0.5f).SetEase(Ease.OutBack);

                index++;
            }
        }
        private int[] Randomizer(int[] locations)
        {
            int[] array = locations.Clone() as int[];
            for (int i = 0; i < array.Length; i++)
            {
                int newArray = array[i];
                int j = Random.Range(i, array.Length);
                array[i] = array[j];
                array[j] = newArray;
            }
            return array;
        }

        private void Porfavor()
        {
            float aspectRatio = (float) Screen.height / Screen.width;
            if (allPrimaryChildrenAtTargetPosition && Mathf.Approximately(aspectRatio, 1.5f) && !escalame)
            {
                escalame = true;
        
                // Aplica la misma escala con animación a ambos grupos
                primaryGroup.transform.DOScale(Vector3.one * 0.5f, 0.5f).SetEase(Ease.OutBounce);
                secondaryGroup.transform.DOScale(Vector3.one * 0.5f, 0.5f).SetEase(Ease.OutBounce)
                    .OnComplete(() => DOTween.KillAll());

                // Reposiciona y escala los elementos en cada grupo
                RepositionAndScaleChildren(primaryGroup, new Vector3(-0.75f, 0f, 0f), Vector3.one * 0.5f, new Vector3(0, -1f, 0));
                RepositionAndScaleChildren(secondaryGroup, new Vector3(0.75f, 0f, 0f), Vector3.one * 0.5f, new Vector3(0, -1f, 0));
            }
        }

        private MemoriaImagen firstOpen;
        private MemoriaImagen secondOpen;

        public bool canOpen => secondOpen == null;

        public void ImageOpened(MemoriaImagen startObject)
        {
            if (firstOpen == null)
            {
                firstOpen = startObject;
            }
            else
            {
                secondOpen = startObject;
                StartCoroutine(CheckGuessed());
            }
        }

        private IEnumerator CheckGuessed()
        {
            if (firstOpen.spriteId == secondOpen.spriteId)
            {
                if (currentPlayer == 1)
                {
                    scorePlayer1++;
                    scoreTextPlayer1.text = scorePlayer1.ToString();
                    AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.correctGood);
                }
                else
                {
                    scorePlayer2++;
                    scoreTextPlayer2.text = scorePlayer2.ToString();
                    AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.correctGood);
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                firstOpen.Close();
                secondOpen.Close();

                currentPlayer = currentPlayer == 1 ? 2 : 1;
                UpdateTurnText();
            }

            firstOpen = null;
            secondOpen = null;
        }

        private void UpdateTurnText()
        {
            _mesa.color = currentPlayer == 1 ? new Color32(161, 28, 28, 255) : new Color32(28, 39, 161, 255);
        }

        private void Winner()
        {
            if (scorePlayer1 + scorePlayer2 == 6)
            {
                if (scorePlayer1 > scorePlayer2)
                {
                    _textMeshPro.text = "Player 1 Wins";
                    colorpanel.color = new Color32(161, 28, 28, 233);
                    AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.Winning);
                    StartCoroutine(_sceneTransitions.EndScene());
                    //winAudio.Play();


                }
                else if (scorePlayer2 > scorePlayer1)
                {
                    _textMeshPro.text = "Player 2 Wins";
                    colorpanel.color = new Color32(28, 39, 161, 233);
                    //winAudio.Play();
                    AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.Winning);
                    StartCoroutine(_sceneTransitions.EndScene());
                }
                else
                {
                    _textMeshPro.text = "Empate";
                    colorpanel.color = new Color32(117, 5, 226, 233);
                    //winAudio.Play();
                    AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.Winning);
                    StartCoroutine(_sceneTransitions.EndScene());
                }
                panelwin.SetActive(true);
                StartCoroutine(RestartGame());
            }
        }

        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(0);
        }
    }
}
