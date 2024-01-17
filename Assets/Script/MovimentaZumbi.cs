using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaZumbi : MonoBehaviour, IMatavel

{
    public GameObject Jogador;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusDoInimigo;
    public AudioClip MorteZumbi;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoes = 4;
    private float porcentagemGerarKitMedico = 0.15f;
    public GameObject KitMedicoPrefab;
    private ControlaInterface scriptControlaInterface;
    [HideInInspector]
    public GeradordeZumbis meuGerador;
    public GameObject SangueZumbi;


    //public int ataque= 30;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        AleatorizarZumbis();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        statusDoInimigo = GetComponent<Status>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }
    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }

        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;
            movimentaInimigo.Movimentar(direcao, statusDoInimigo.Velocidade);
            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
    }

    void AtacaJ()

    {
        int dano = Random.Range(12, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);

    }

    void AleatorizarZumbis()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusDoInimigo.Vida -= dano;
        if (statusDoInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangueZumbi (Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(SangueZumbi,posicao, rotacao);
    }

    public void Morrer()
    {
        Destroy(gameObject,3);
        animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(MorteZumbi);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQtdaZumbisMortos();
        meuGerador.DiminuirQtdaZumbiJogo();
    }

    void VerificarGeracaoKitMedico (float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position,Quaternion.identity);
        }
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoes + Random.Range(-1f,2f);

        }
        bool ficouPerto = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if (ficouPerto == false)
        {
         direcao = posicaoAleatoria - transform.position;
         movimentaInimigo.Movimentar(direcao, statusDoInimigo.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }
}
