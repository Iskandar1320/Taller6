using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator  transitionAnim;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) transitionAnim.Play("MenuEnter");
    }

    public IEnumerator LoadScene(string sceneName) //Se llama desde OnClickExit()
    {
        transitionAnim.Play("EntreEscenas");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(sceneName);
        
    }

    public IEnumerator EndScene() //End Scene tiene delay por defecto para usarlo al final de los niveles
    {
        yield return new WaitForSeconds(3f);
        transitionAnim.Play("EntreEscenas", -1, 0f);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Menu");
    }
    
    /// <summary>
    ///   <para>Sirve para salir de la escena con el boton de exit y que se dispare la animación entre escenas. Llama la corrutina LoadScene() para cargar la escena del menu principal.</para>
    /// </summary>
    /// <param name="sceneName">Se indica la escena deseada en el Inspector</param>
    public void OnClickExit(string sceneName) //USAR PARA BOTONES, EN MENU PARA IR AL NIVEL, EN NIVEL PARA IR A MENU
    {
        StartCoroutine(LoadScene(sceneName));
    }

 
}