using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptmovimentojogador;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public TMP_Text TextoTempoDeSobrevivencia;
    public TMP_Text TextoPontuacaoMaxima;
    private float tempoMaximoSalvo;
    private int quantidadeZumbisMortos;
    public TMP_Text TextoZumbisMortos;
    public TMP_Text MensagemChefe;

    // Start is called before the first frame update
    void Start()
    {
        scriptmovimentojogador = GameObject.FindWithTag("Jogador")
            .GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptmovimentojogador.statusDoJogador.Vida;
        AtualizarVidaJogador();
        Time.timeScale = 1;
        tempoMaximoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    

    
    public void AtualizarVidaJogador()
    {
        SliderVidaJogador.value = scriptmovimentojogador.statusDoJogador.Vida;
    }

    public void AtualizarQtdaZumbisMortos()
    {
        quantidadeZumbisMortos ++;
        TextoZumbisMortos.text = string.Format("x   {0}", quantidadeZumbisMortos);
    }


    public void GameOver ()
    {
        PainelGameOver.SetActive(true);
        Time.timeScale = 0;
        int minutos = (int)Time.timeSinceLevelLoad / 60;
        int segundos = (int) Time.timeSinceLevelLoad % 60;
        TextoTempoDeSobrevivencia.text = "You survived for " + minutos + " min and "+ segundos + " s.";
        AjustarPontuacaoMaxima(minutos, segundos);

    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if (Time.timeSinceLevelLoad > tempoMaximoSalvo)
        {
            tempoMaximoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("You're best time is {0} min and {1} s.", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoMaximoSalvo);
        }
        if(TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoMaximoSalvo/ 60;
            seg = (int)tempoMaximoSalvo % 60;
            TextoPontuacaoMaxima.text = string.Format("You're best time is {0} min and {1} s.", min, seg);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("gameproject");
    }

    public void AparicaoChefe()
    {

        StartCoroutine(DesaparecerTexto(2, MensagemChefe));

    }

    IEnumerator DesaparecerTexto (float tempoDeSumico, TMP_Text mensagemDeSumico)
    {
        mensagemDeSumico.gameObject.SetActive(true);
        Color corDeTexto = mensagemDeSumico.color;
        corDeTexto.a = 1;
        mensagemDeSumico.color = corDeTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (mensagemDeSumico.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corDeTexto.a = Mathf.Lerp(1, 0, contador);
            mensagemDeSumico.color = corDeTexto;
            if (mensagemDeSumico.color.a <= 0)
            {
                mensagemDeSumico.gameObject.SetActive(false);
            }
            yield return null;
        }

    }
}
