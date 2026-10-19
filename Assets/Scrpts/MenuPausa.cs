using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private KeyCode teclaPausa = KeyCode.Escape;
    [SerializeField] private string nombreEscenaMenu = "Menu";

    private bool enPausa;

    private void Start()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(teclaPausa)) return;

        if (enPausa)
            Reanudar();
        else if (Time.timeScale > 0f)   // no pausar si hay un diálogo abierto
            Pausar();
    }

    public void Pausar()
    {
        enPausa = true;
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        enPausa = false;
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;   // importante: si no, el menú queda congelado
        SceneManager.LoadScene(nombreEscenaMenu);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego....");
        Application.Quit();
    }
}