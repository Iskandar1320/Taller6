using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator  transitionAnim;
    [SerializeField]
    private string sceneName;

    private void Start()
    {
        print("Escena #"+SceneManager.GetActiveScene().buildIndex);
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                if (sceneName != null)
                    transitionAnim.Play("MenuEnter");
                    
                break;
            case > 0:
                StartCoroutine(LoadScene());
                break;
        }
    }

    public IEnumerator LoadScene()
    {
        if (transitionAnim != null && SceneManager.GetActiveScene().buildIndex == 0)
        {
            transitionAnim.Play("EntreEscenas");
            yield return new WaitForSeconds(6f);
            SceneManager.LoadScene(sceneName);
        }
    }

    public IEnumerator EndScene()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0) 
        {
            
            transitionAnim.Play("MenuEnter");
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("PantallaInicio_1");
        }
    }
    
    /// <summary>
    ///   <para>Sirve para salir de la escena con el boton de exit y que se dispare la animación entre escenas.</para>
    /// </summary>
    /// <param name="OnClickExit()">Llama la corrutina EndScene() para cargar la escena del menu principal.</param>
    public void OnClickExit()
    {
        transitionAnim.Play("EntreEscenas", -1, 0f);
        StartCoroutine(EndScene());
    }
}