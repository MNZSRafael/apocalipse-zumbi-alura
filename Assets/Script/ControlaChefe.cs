using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    public AudioClip MorteZumbi;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;
    private ControlaInterface scriptTeste;
    public GameObject KitMedico;
    public Slider sliderVidaChefe;
    public Image SliderImage;
    public Color CorMaxima, CorMinima;
    public GameObject SangueZumbi;



    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        agente.speed = statusChefe.Velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();
        scriptTeste = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        sliderVidaChefe.maxValue = statusChefe.VidaInicial;
        


    }
    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath == true)
        {
            bool estouPertoDoJogador = agente.remainingDistance <= agente.stoppingDistance;
            if (estouPertoDoJogador)
            {
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            }
            else
            {
                animacaoChefe.Atacar(false);
            }
        }
    }

    void AtacaJ ()
    {
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusChefe.Vida -= dano;
        AtualizaSlider();
            if (statusChefe.Vida <= 0)
            {
            Morrer();
            }
    }
    public void ParticulaSangueZumbi(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(SangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        ControlaAudio.instancia.PlayOneShot(MorteZumbi);
        scriptTeste.AtualizarQtdaZumbisMortos();
        this.enabled = false;
        agente.enabled = false;
        Instantiate(KitMedico, transform.position,Quaternion.identity);
        Destroy(gameObject, 2);
    }

    public void AtualizaSlider ()
    {
        sliderVidaChefe.value = statusChefe.Vida;
        float porcetagemDaVida = (float)statusChefe.Vida / statusChefe.VidaInicial;
        Color corDaVida = Color.Lerp(CorMinima, CorMaxima, porcetagemDaVida);
        SliderImage.color = corDaVida;

    }

}
