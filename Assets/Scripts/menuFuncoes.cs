using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuFuncoes : MonoBehaviour
{
    [SerializeField] private String scene;
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelMenuOpcoes;
    
    public void Jogar()
    {
        SceneManager.LoadScene(scene);
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
