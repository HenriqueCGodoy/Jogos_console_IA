using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuFuncoes : MonoBehaviour
{
    [SerializeField] private String AiVsAiScene;
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelMenuOpcoes;
    
    public void AiVsAi()
    {
        SceneManager.LoadScene(AiVsAiScene);
    }

    public void AbrirOpcoes()
    {
        painelMenuPrincipal.SetActive(false);
        painelMenuOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelMenuPrincipal.SetActive(true);
        painelMenuOpcoes.SetActive(false);
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
