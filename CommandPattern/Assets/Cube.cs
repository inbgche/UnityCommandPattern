using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor {
	public Vector3 pos = new Vector3(0, 0, 0);
	public Transform _tr;

	public Actor(Transform tr)
	{
		_tr = tr;
	}

	public void MoveTo(Vector3 pos)
	{
		_tr.position = pos;
	}
}

public class Cube : MonoBehaviour 
{
	public enum eBtn{
		BtnM,
		BtnUndo,
		None
	}

	eBtn pressedBtn = eBtn.None;
	Stack<Command> stack = new Stack<Command>();
	Actor actor;

	// Use this for initialization
	void Start () {
		actor = new Actor(gameObject.transform);
	}

	// Update is called once per frame
	void Update () {
		bool isUndo = false;
		Command command = GetCommand(ref isUndo);
		if(command!=null)
		{
			if(!isUndo)
				command.Execute(actor, new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
			else
				command.Undo(actor);
		}
	}

	Command GetCommand(ref bool isUndo)
	{
		if(IsPressed(eBtn.BtnM)) 
		{ 
			Command cm = new CommandMove();
			stack.Push(cm); return cm;
		}
		if(IsPressed(eBtn.BtnUndo)) 
		{
			isUndo = true;
			return stack.Count > 0 ? stack.Pop() : null;
		}
		return null;
	}

	bool IsPressed(eBtn btn)
	{
		pressedBtn = eBtn.None;

		if(Input.GetKey("m"))
			pressedBtn = eBtn.BtnM;
		else if(Input.GetKey("u"))
			pressedBtn = eBtn.BtnUndo;

		return (btn == pressedBtn);
	}
}
