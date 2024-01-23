using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;

    [SerializeField] private float _turnSpeed = 80.0f;

    [SerializeField] private Animation _anim; 


    private readonly float initHp = 100.0f;

    public float currHp;

    private Image hpBar;

    public delegate void PlayerDieHandler();

    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();

        currHp = initHp;

        _anim = GetComponent<Animation>();

        _anim.Play("Idle");

        _turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        _turnSpeed = 200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");


        Debug.Log("h=" + h);
        Debug.Log("v=" + v);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(moveDir.normalized * _moveSpeed * Time.deltaTime);

        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * r);

        PlayerAnim(h, v);
    }

    private void PlayerAnim(float h, float v)
    {
        if(v >= 0.1f)
        {
            _anim.CrossFade("RunF", 0.25f);
        }
        else if(v <= -0.1f)
        {
            _anim.CrossFade("RunB", 0.25f);
        }
        else if(h >= 0.1f)
        {
            _anim.CrossFade("RunR", 0.25f);
        }
        else if(h <= -0.1f)
        {
            _anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            _anim.CrossFade("Idle", 0.25f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currHp >= 0.0f && other.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            DisplayHealth();

            Debug.Log($"Player hp = {currHp/initHp}");

            if(currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Die !");

        //GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        //
        //foreach(GameObject monster in monsters)
        //{
        //    monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //}

        OnPlayerDie();
    }

    void DisplayHealth()
    {
        hpBar.fillAmount = currHp / initHp;
    }
}
