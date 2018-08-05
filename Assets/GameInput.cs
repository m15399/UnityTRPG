using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput {

	public Vector2 mousePositionWorld;

	Game game;

	Vector2 mousePosition;
	Vector2 prevMousePosition;
	GameObject mouseTarget = null;
	GameObject prevMouseTarget = null;
	GameObject initialMouseDownTarget = null;

	bool mouseMovedOffInitialMouseDownTarget = false;
	bool prevMouseDown = false;
	bool prevDragging = false;
	bool prevHoveringObject = false;

	public GameInput(Game game){
		this.game = game;
	}

	public void UpdateInput () {
//		Camera.main.transform.position += new Vector3(.5f * Time.deltaTime, 0, 0);

		// Update mouse position
		//
		mousePosition = Input.mousePosition;
		mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePosition);
		Vector2 prevMousePositionWorld = Camera.main.ScreenToWorldPoint(prevMousePosition);
		Vector2 mouseMovementWorld = prevMousePositionWorld - mousePositionWorld;

		// Find mouse target
		//
		mouseTarget = null;
		{
			Collider2D mouseCollider = Physics2D.OverlapPoint(mousePositionWorld);
			if (mouseCollider)
				mouseTarget = mouseCollider.gameObject;
		}

		// Mouse logic state
		//
		bool haveTarget = mouseTarget != null;

		bool mouseDown = Input.GetMouseButton(0);
		bool mouseDownThisFrame = mouseDown && !prevMouseDown;
		bool mouseUpThisFrame = !mouseDown && prevMouseDown;
		bool mouseChilling = !mouseDown && !mouseUpThisFrame;

		bool hoveringObject = mouseChilling && haveTarget;
		bool hoveringSameObject = hoveringObject && prevHoveringObject && prevMouseTarget == mouseTarget;
		bool hoveringNewObject = hoveringObject && !hoveringSameObject;
		bool stoppedHoveringPrevTarget = prevHoveringObject && !hoveringSameObject;

		bool dragging = mouseDown && prevMouseDown && (mouseMovementWorld.magnitude > .015f || prevDragging);
		bool clicking = !mouseDown && prevMouseDown && !dragging && !prevDragging;

		// Track initial mouse target (and whether we've ever moved off it)
		//
		if (mouseDownThisFrame){
			initialMouseDownTarget = mouseTarget;
			mouseMovedOffInitialMouseDownTarget = false;
		}
		if (mouseTarget != initialMouseDownTarget){
			mouseMovedOffInitialMouseDownTarget = true;
		}

		bool haveClickTarget = haveTarget && !mouseMovedOffInitialMouseDownTarget;
		bool clickingObject = clicking && haveClickTarget;
		bool clickingNothing = clicking && !haveClickTarget;

//		Utils.DebugText("Mouse target", mouseTarget);
//		Utils.DebugText("Dragging", dragging);
//		Utils.DebugText("Hovering", hoveringObject);
//		Utils.DebugText("Have click target", haveClickTarget);

		// Hover targets
		//
		if (stoppedHoveringPrevTarget){
			prevMouseTarget.SendMessage("DoStopHover", null, SendMessageOptions.DontRequireReceiver);
		}
		if (hoveringNewObject){
			mouseTarget.SendMessage("DoStartHover", null, SendMessageOptions.DontRequireReceiver);
		}

		// Click targets
		//
		if (clickingObject){
			mouseTarget.SendMessage("DoClick", null, SendMessageOptions.DontRequireReceiver);
		}
		if (clickingNothing){
			game.ClickedOnNothing();
		}

		// Drag camera
		//
		if (dragging){
			if (initialMouseDownTarget == null){
				Vector3 cameraMove = mouseMovementWorld;
				Camera.main.transform.position += cameraMove;
			}
		}

		// Update prev values
		//
		if (!mouseDown){
			initialMouseDownTarget = null;
		}
		prevMousePosition = mousePosition;
		prevMouseTarget = mouseTarget;
		prevHoveringObject = hoveringObject;
		prevMouseDown = mouseDown;
		prevDragging = dragging;
	}
}
