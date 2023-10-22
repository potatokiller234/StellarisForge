using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_gridSelect : MonoBehaviour
{
    [Header("Select")]
    public GameObject defaultCenter;
    public GameObject defaultPlayerLook;
    public Transform spawnedHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    Debug.LogWarning("Started New Tester");
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    Physics.Raycast(ray, out hit);
        //  //  Debug.Log(hit.transform.position);
          
        //    StartCoroutine(tester(hit));
        //}
    }

    /// <summary>
    /// Uses boxCollider shenanigans to highlight an area similar to the windows blue box, then spawns another fake cube so the user knows what is being selected
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    IEnumerator tester(RaycastHit hit)
    {
        GameObject center = Instantiate(defaultCenter,
            new Vector3(hit.point.x, 0, hit.point.z),
            Quaternion.identity,
            spawnedHolder);
        BoxCollider coll = center.GetComponent<BoxCollider>();
        GameObject playerLook = Instantiate(defaultPlayerLook,Vector3.zero,Quaternion.identity,spawnedHolder);

        while (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 diff = camPos - center.transform.position;
            
            coll.size = MathfAbs(diff);
            coll.center = (diff / 2);
            playerLook.transform.position = center.transform.position + coll.center;
            playerLook.transform.localScale = new Vector3(coll.size.x,2,coll.size.z);

            yield return new WaitForEndOfFrame();
        }
        Debug.LogError("DONE");
        killChildren();
    }

    private Vector3 MathfAbs(Vector3 input)
    {
        return new Vector3(Mathf.Abs(input.x), Mathf.Abs(input.y), Mathf.Abs(input.z));
    }
    public void killChildren()
    {
        for (int x = spawnedHolder.childCount - 1; x >= 0; x--)
        {
            Debug.Log(x);
            Destroy(spawnedHolder.transform.GetChild(x).gameObject);
        }
    }
}
