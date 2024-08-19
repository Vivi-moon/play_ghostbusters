using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Animator Anim;
    private int coefficient_x = 2;
    private int coefficient_y = 3;
    private int checkMiss = 0;
    private float checkHight;
    private PlayManager playManager;

    
    void Start()
    {
        playManager = GameObject.FindAnyObjectByType<PlayManager>();
        Anim = this.GetComponent<Animator>();
        gameObject.transform.Rotate(new Vector3(0,180,0));
        checkHight = gameObject.transform.position.z;
        if (gameObject.transform.position.x >0)
        { 
            coefficient_x = Random.Range(-3, -1);
        }
        else
        {
            coefficient_x = Random.Range(1, 3);
        }
        
    }

    void Update()
    {

        if (playManager.Play)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * coefficient_x, Time.deltaTime * coefficient_y, 0);
        }
        if (gameObject.transform.position.y >= 30 && playManager.Play == false || playManager.Play == false && checkMiss == -1)
        {
            StartCoroutine(AudioDelay());
            Destroy(gameObject);
        }
        if (gameObject.transform.position.y >= 30 + (checkHight/2))
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(AudioDelay());
            choosePossition(playManager.ghost % 3);
            
            playManager.ghost++;
            coefficient_x = 0;
            coefficient_y = 20;
            checkMiss=1;
            
        }
        if (gameObject.transform.position.y >= 10 && checkMiss>0)
        {
            coefficient_y = 0;
            checkMiss = -1;
            gameObject.name = "Ghost_delete";
            Anim.Play("attack");
            
            StartCoroutine(DeleteTimeAim());
            

        }
           
    }

    private void choosePossition( int count)
    {
        switch (count)
        {
            case 0:
                gameObject.transform.position = new Vector3(0, 6, -15);
                break;
            case 1:
                gameObject.transform.position = new Vector3(-8, 6, -15);
                gameObject.transform.Rotate(new Vector3(0, -30, 20));
                break;
            case 2:
                gameObject.transform.position = new Vector3(7, 6, -15);
                gameObject.transform.Rotate(new Vector3(0, 30, -20));
                break;
            default:
                gameObject.transform.position = new Vector3(0, 6, -15);
                break;
        }
    }

    IEnumerator AudioDelay()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator DeleteTimeAim()
    {
        yield return new WaitForSeconds(0.6f);
        coefficient_y -= 30;
        playManager.HP -= 20;
        yield return new WaitForSeconds(0.6f);
        playManager.aims.Remove(gameObject);
        Destroy(gameObject);

    }

}
