using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private GameObject DialogoMar;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [Header("Diálogo con la misión completa (opcional)")]
    [SerializeField] private SistemaPuntos sistemaPuntos;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLinesMisionCompleta;

    private string[] lineasActuales;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    void Update()
    {
        if (Time.timeScale == 0f && !didDialogueStart) return;
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == lineasActuales[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lineasActuales[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        bool usarDialogoFinal = sistemaPuntos != null
                                && sistemaPuntos.MisionCumplida
                                && dialogueLinesMisionCompleta.Length > 0;

        lineasActuales = usarDialogoFinal ? dialogueLinesMisionCompleta : dialogueLines;

        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        DialogoMar.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < lineasActuales.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            DialogoMar.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in lineasActuales[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogoMar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogoMar.SetActive(false);
        }
    }
}