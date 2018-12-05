using UnityEngine;
using System.Collections;
using System;

public class DragCamera : MonoBehaviour
{
    private float bottomBoundY;
    private float topBound;
    private float bottomBound;
    private Vector3 pos;
    private Vector3 dragDelta;
    private Transform target;
    private SpriteRenderer spriteBounds;
    private Camera cam;

    private void Start()
    {
        cam = this.GetComponent<Camera>();

        float vertExtent = cam.orthographicSize;
        spriteBounds = GameObject.Find("background").GetComponentInChildren<SpriteRenderer>();
        bottomBound = (float)(vertExtent - spriteBounds.sprite.bounds.size.y / 2.0f);
        bottomBoundY = spriteBounds.sprite.bounds.size.y * spriteBounds.transform.localScale.y;
        topBound = (float)(spriteBounds.sprite.bounds.size.y / 2.0f - vertExtent);
    }

    void Update()
    {
        if (_State == State.None && Input.GetMouseButtonDown(0)) InitDrag();

        if (_State == State.Dragging && Input.GetMouseButton(0)) MoveCamera();

        if (_State == State.Dragging && Input.GetMouseButtonUp(0)) FinishDrag();

        if (cam.transform.position.y < -bottomBoundY)
        {
            Camera.main.transform.Translate(0, -bottomBound, 0);
        }
        else if (cam.transform.position.y > topBound)
        {
            Camera.main.transform.Translate(0, -topBound, 0);
        }

    }

    #region Calculations
    public State _State = State.None;
    public Vector3 _DragStartPos;

    private void InitDrag()
    {
        _DragStartPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, -Camera.main.transform.position.z));

        _State = State.Dragging;
    }

    private void MoveCamera()
    {
        Vector3 actualPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, -Camera.main.transform.position.z));
        dragDelta = actualPos - _DragStartPos;

        if (/*Math.Abs(dragDelta.x) < 0.00001f &&*/ Math.Abs(dragDelta.y) < 0.00001f) return;

        Camera.main.transform.Translate(-dragDelta);
        
    }

    private void FinishDrag()
    {
        _State = State.None;
    }
    #endregion

    public enum State
    {
        None,
        Dragging
    }
}

