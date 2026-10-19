using System.Collections;
using TMPro;
using UnityEngine;

public class SistemaPuntos : MonoBehaviour
{
    [Header("Contador")]
    [SerializeField] private TextMeshProUGUI textoCantidadPuntos;
    [SerializeField] private int cantidadPuntos;
    [SerializeField] private int cantidadMaximaDePuntos;

    [Header("Misión")]
    [SerializeField] private TextMeshProUGUI textoObjetivo;
    [SerializeField] private GameObject panelMisionCumplida;
    [SerializeField] private float segundosPanel = 3f;

    public bool MisionCumplida { get; private set; }

    private void Start()
    {
        if (textoObjetivo != null)
            textoObjetivo.text = $"Misión: Recolecta {cantidadMaximaDePuntos} puntos verdes";

        if (panelMisionCumplida != null)
            panelMisionCumplida.SetActive(false);

        ActualizarTexto();
    }

    private void OnEnable()
    {
        PuntosVerdes.PuntosRecolectado += SumarPuntos;
    }

    private void OnDisable()
    {
        PuntosVerdes.PuntosRecolectado -= SumarPuntos;
    }

    private void SumarPuntos()
    {
        if (cantidadPuntos + 1 > cantidadMaximaDePuntos) { return; }

        cantidadPuntos += 1;
        ActualizarTexto();

        if (cantidadPuntos >= cantidadMaximaDePuntos)
        {
            CompletarMision();
        }
    }

    private void ActualizarTexto()
    {
        textoCantidadPuntos.text = $"{cantidadPuntos:D2} / {cantidadMaximaDePuntos:D2}";
    }

    private void CompletarMision()
    {
        MisionCumplida = true;

        if (textoObjetivo != null)
            textoObjetivo.text = "Misión cumplida: habla con el sabio";

        if (panelMisionCumplida != null)
            StartCoroutine(MostrarPanelTemporal());
    }

    private IEnumerator MostrarPanelTemporal()
    {
        panelMisionCumplida.SetActive(true);
        yield return new WaitForSecondsRealtime(segundosPanel);
        panelMisionCumplida.SetActive(false);
    }
}