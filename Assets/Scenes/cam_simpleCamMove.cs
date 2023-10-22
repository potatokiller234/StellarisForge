using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_simpleCamMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    public float shiftSpeedMulti = 2;
    public float scroll_speed = 50;
    public float scroll_scrolllimit_close = 5;
    public float scroll_scrolllimit_far = 50;
    public float scroll_scrolllimit_far_smallGridDisabled = 100;

    private float normalSpeed;
    private float sprintSpeed;



    public Camera camera_player;


    public GridDrawer gridDrawer;

    void Start()
    {
        normalSpeed = speed;
        sprintSpeed = speed * shiftSpeedMulti;
    }
    // Update is called once per frame
    void Update()
    {
        if (UI_mouseManager._UI_mouseManager.isInGridArea)
        {
            moveCam();
        }
    }

    private void moveCam()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0, 0, 1) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += new Vector3(0, 0, -1) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
        }



        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && camera_player.orthographicSize > scroll_scrolllimit_close)
        {
            if (camera_player.orthographicSize <= scroll_scrolllimit_far_smallGridDisabled)
            {
                gridDrawer.grid_linerend_inside_upDown.gameObject.SetActive(true);
                gridDrawer.grid_linerend_inside_leftRight.gameObject.SetActive(true);

            }
            camera_player.orthographicSize += -1 * Time.deltaTime * scroll_speed;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && camera_player.orthographicSize < scroll_scrolllimit_far)
        {
            //If it is 100 far away get disabled
            if (camera_player.orthographicSize >= scroll_scrolllimit_far_smallGridDisabled)
            {
                gridDrawer.grid_linerend_inside_upDown.gameObject.SetActive(false);
                gridDrawer.grid_linerend_inside_leftRight.gameObject.SetActive(false);
            }
            camera_player.orthographicSize += 1 * Time.deltaTime * scroll_speed;
        }
        if (camera_player.orthographicSize < scroll_scrolllimit_close)
        {
            camera_player.orthographicSize = scroll_scrolllimit_close;
        }
        else if (camera_player.orthographicSize > scroll_scrolllimit_far)
        {
            camera_player.orthographicSize = scroll_scrolllimit_far;
        }
    }
}
