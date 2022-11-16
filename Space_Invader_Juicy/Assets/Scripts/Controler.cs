using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Controler : MonoBehaviour
{
    public float speed;
    public float life = 10;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        float positionX = transform.position.x + (Input.GetAxis("Horizontal") * speed);
        positionX = Mathf.Clamp(positionX,-10,10);
        transform.position = new Vector3(positionX, transform.position.y ,transform.position.z);
    }

    public void Hit()
    {
        life--;

        if (life <= 0)
        {
            GameManager.Instance?.Defeat();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ennemy"))
        {
            GameManager.Instance.Defeat();
            gameObject.SetActive(false);
        }
    }
}
