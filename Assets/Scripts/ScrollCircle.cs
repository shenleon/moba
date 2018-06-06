using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

/// <summary>
/// 圆形滚动区域
/// </summary>
public class ScrollCircle : ScrollRect
{
    public delegate void DragHandler(Vector2 forwardPostion, Vector2 position);

    /// <summary>
    /// 滚动区域半径
    /// </summary>
    protected float mRadius = 0f;
    /// <summary>
    /// 扩充区域
    /// </summary>
    public float m_range = 0.5f;
    /// <summary>
    /// 拖拽开始
    /// </summary>
    public DragHandler beginHandler;
    /// <summary>
    /// 拖拽中
    /// </summary>
    public DragHandler dragHandler;
    /// <summary>
    /// 拖拽结束
    /// </summary>
    public DragHandler endHandler;
    /// <summary>
    /// 标准方向
    /// </summary>
    protected Vector2 forwardPostion;


    protected override void Start()
    {
        base.Start();
        //计算摇杆块的半径
        mRadius = (transform as RectTransform).sizeDelta.x * this.m_range;
        this.forwardPostion = new Vector2(0, mRadius);
    }

    /// <summary>
    /// 添加拖拽监听
    /// </summary>
    public void AddBeginHandler(DragHandler handler)
    {
        this.beginHandler += handler;
    }

    /// <summary>
    /// 添加拖拽监听
    /// </summary>
    public void AddDragHandler(DragHandler handler)
    {
        this.dragHandler += handler;
    }

    /// <summary>
    /// 添加拖拽监听
    /// </summary>
    public void AddEndHandler(DragHandler handler)
    {
        this.endHandler += handler;
    }

    /// <summary>
    /// 拖动控制
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnDrag(eventData);
        Vector2 contentPostion = this.content.anchoredPosition;
        if (contentPostion.magnitude > mRadius)
        {
            contentPostion = contentPostion.normalized * mRadius;
            SetContentAnchoredPosition(contentPostion);
        }
        if (this.dragHandler != null)
        {
            this.dragHandler(this.forwardPostion, contentPostion);
        }
    }

    /// <summary>
    /// 拖拽开始
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        Vector2 contentPostion = this.content.anchoredPosition;
        if (this.beginHandler != null)
        {
            this.beginHandler(this.forwardPostion, contentPostion);
        }
    }

    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        Vector2 contentPostion = this.content.anchoredPosition;
        if (this.endHandler != null)
        {
            this.endHandler(this.forwardPostion, contentPostion);
        }
    }
}
