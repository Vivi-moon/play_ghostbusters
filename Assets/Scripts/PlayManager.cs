using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public bool Play;
    private Camera camera;
    private int count = 0;
    [SerializeField] private GameObject aim_play;
    public List<GameObject> aims;
    public float point = 0.0f;
    public float HP = 100.0f;
    [SerializeField] private ParticleSystem particleSystemGhost;
    [SerializeField] private ParticleSystem particleSystemGun;
    [SerializeField] private Gun gun;
    public int ghost;
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasGame;
    [SerializeField] private GameObject canvasWin;
    [SerializeField] private GameObject canvasLose;

    private Vector3[] possitions = new Vector3[7]
    {
        new Vector3(40, 0, 25),
        new Vector3(-9, 0, 37),
        new Vector3(-42, 0, 19),
        new Vector3(20, 0, 33),
        new Vector3(-32, 0, 22),
        new Vector3(23, 0, 20),
        new Vector3(-40, 0, 34)
        
    };

    void Start()
    {
        camera = Camera.main;
        GetComponent<AudioSource>().Play();
        GenerateAim();
    }

    void Update()
    {
        
        if (Play)
        {
            DetectObjectWithRaycast();
            StartCoroutine(GenerateAim());
        }
       if (point == 100f && HP>0)
        {
            Play = false;
            point = 0f;
            HP = 100f;
            canvasWin.SetActive(true);
            canvasGame.SetActive(false);
            aims.Clear();
            gun.gameObject.SetActive(false);
        }
        if (HP == 0f)
        {
            Play = false;
            point = 0f;
            HP = 100f;
            canvasLose.SetActive(true);
            canvasGame.SetActive(false);
            aims.Clear();
            gun.gameObject.SetActive(false);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void Close()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        canvasGame.SetActive(true);
        canvasMenu.SetActive(false);
        canvasWin.SetActive(false);
        canvasLose.SetActive(false);
        Play = true;
        Vector3 rotate = transform.eulerAngles;
        rotate.y = 180;
        gun.transform.rotation = Quaternion.Euler(rotate);
        gun.gameObject.SetActive(true);
    }

    IEnumerator GenerateAim()
    {
        yield return new WaitForSeconds(2f);
        if(aims.Count < 3) CreateAim();
    }

    public void CreateAim()
    {
       
        Vector3 pos = possitions[count%7];
        count++;
        GameObject go = Instantiate<GameObject>(aim_play, pos, Quaternion.identity);
        go.transform.SetParent (transform);
        aims.Add(go);
    }


    public void DetectObjectWithRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particleSystemGun.Play();
            gun.Shoot();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"{hit.collider.name} Detected",
                    hit.collider.gameObject);
                if (hit.collider.name == "Ghost(Clone)")
                {
                    
                    point += 5.0f;
                    Vector3 posDie = hit.collider.transform.position;
                    particleSystemGhost.transform.position = posDie;
                    particleSystemGhost.Play();
                    Destroy(hit.collider.gameObject);
                    aims.Remove(hit.collider.gameObject);
                }
            }
        }
    }
}
