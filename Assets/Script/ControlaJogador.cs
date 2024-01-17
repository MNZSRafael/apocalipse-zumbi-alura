using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{   
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TelaGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentaJogador meuMovimentoJogador;
    private AnimacaoPersonagem minhaAnimacaoJogador;
    public Status statusDoJogador;

    private void Start()
    {
        minhaAnimacaoJogador = GetComponent<AnimacaoPersonagem>();
        meuMovimentoJogador = GetComponent<MovimentaJogador>();
        statusDoJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
     float eixoX = Input.GetAxis ("Horizontal");
     float eixoY = Input.GetAxis ("Vertical");

     direcao = new Vector3 (eixoX, 0, eixoY);

        minhaAnimacaoJogador.Movimentar(direcao.magnitude);
        
    }

    void FixedUpdate ()

    {
        meuMovimentoJogador.Movimentar(direcao, statusDoJogador.Velocidade);
        meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano)
    {
        statusDoJogador.Vida -= dano;
        scriptControlaInterface.AtualizarVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if ( statusDoJogador.Vida <=0)
        {
            Morrer();
        }
       
    }
    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }

    public void CurarVida (int quantidadeDeCura)
    {
        statusDoJogador.Vida += quantidadeDeCura;
        if(statusDoJogador.Vida > statusDoJogador.VidaInicial)
        {
            statusDoJogador.Vida = statusDoJogador.VidaInicial;
        }
        scriptControlaInterface.AtualizarVidaJogador();

    }
   
 }
   
    