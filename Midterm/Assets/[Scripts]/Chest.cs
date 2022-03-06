using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    
    public GameObject player;
    public GameObject star;
    public Transform starPos;

    private PlayerBehaviour _playerScript;
    private Animator _animator;
    private bool _chestOpened;
    
    public List<GameObject> stars;

    void Start()
    {
        _playerScript = player.GetComponent<PlayerBehaviour>();
        _animator = GetComponent<Animator>();
        //creating = false;
        //cont = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        bool _chestOpened = _playerScript.openChest;
        if(_chestOpened)
        {
            StartCoroutine(StarsGenerator());    
            _animator.SetBool("isOpened", true);         
        }          
    }       

    IEnumerator StarsGenerator()
    {
        yield return new WaitForSeconds(0.8f);
        stars.Add(Instantiate(star, starPos));  
        //creating = false;       
    } 
}
