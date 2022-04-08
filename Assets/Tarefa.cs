using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarefa : MonoBehaviour
{
    // Pega os waypoints e coloca eles em uma lista (ARRAY)
    public GameObject[] waypoints;
    int currentWP = 0;
    // Atribui��o das vari�veis que ir�o influenciar na movimenta��o
    float speed = 1.0f;
    float accuracy = 1.0f;
    float rotSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        // Dinamicamente ir� puxar todos os gameobjects da cena para a lista, com um requirimento : possuir a tag waypoint no gameobject
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }
    // Um update que ser� chamado ap�s a conclus�o dos 60 ticks do Update normal, o Late update tambem funciona como um update, por�m os seus 60 ticks s� funcionam
    // ap�s o update.
    void LateUpdate()
    {
        //Se a lista estiver vazia, retorne ela
        if (waypoints.Length == 0) return;
        // LookAtGoal � uma vari�vel do tipo vector 3, que ir� receber como valor a POSI��O do primeiro waypoint(currentWP)
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x,this.transform.position.y,waypoints[currentWP].transform.position.z);
        // Direction � uma vari�vel do tipo vector 3, que ir� receber como valor a DIRE��O do primeiro waypoint, a sua posi��o � dada atrav�s do LookAtGoal
        Vector3 direction = lookAtGoal - this.transform.position;
        // Esse m�todo rotation, faz a rota��o do nosso objeto atrav�s de um sphereLerp que baseado no time.deltatime ele ir� rotacionar at� a dire��o inserida no m�todo.
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);
        // Se o comprimento do vetor dire��o for menor que precis�o... = ele chegou no ponto
        if (direction.magnitude < accuracy)
        {
            // Como ele chega no ponto, ele deve atualizar o waypoint, neste caso incrementando 1 no index contador
            currentWP++;
            // Caso o index contador alcan�ar o limite da lista, ele deve zerar o index, fazendo com que recome�e
            if (currentWP >= waypoints.Length)
            {
                // reinicia o index contador
                currentWP = 0;
            }
        }
        // M�todo que ir� fazer a movimenta��o do nosso gameobject baseado em sua velocidade e baseado no tempo.
        // Neste caso ir� movimentar atrav�s do transform, utilizando o m�todo translate.
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}