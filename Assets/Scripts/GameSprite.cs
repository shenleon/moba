using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSprite : MonoBehaviour
{
    /// <summary>
    /// 摇杆范围
    /// </summary>
    public ScrollCircle scroll;
    Vector3 fw;

    // Use this for initialization
    void Start()
    {
        scroll.AddBeginHandler(this.BeginDrag);
        scroll.AddDragHandler(this.OnDrag);
        scroll.AddEndHandler(this.EndDrag);
        fw = this.transform.forward;
    }

    bool state = false;

    void BeginDrag(Vector2 forwardPostion, Vector2 position)
    {
        this.state = true;
    }

    void OnDrag(Vector2 forwardPostion, Vector2 position)
    {
        if (this.state)
        {
            float cosVal = Vector2.Dot(forwardPostion, position) / (forwardPostion.magnitude * position.magnitude);
            float angle = (Mathf.Acos(cosVal) / Mathf.PI) * 180;
            if (position.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            else if (position.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, -angle, 0);
            }
        }
    }

    void EndDrag(Vector2 forwardPostion, Vector2 position)
    {
        this.state = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
