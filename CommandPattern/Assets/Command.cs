using UnityEngine;
using System.Collections;

public class Command {
	public virtual void Execute(Actor actor, Vector3 newPos){}
	public virtual void Undo(Actor actor){}
}

public class CommandMove : Command {

	Vector3 prevPos;
	public override void Execute(Actor actor, Vector3 newPos)
	{
		prevPos = actor._tr.position;
		actor.MoveTo(newPos);
		Debug.Log(newPos);
	}

	public override void Undo(Actor actor)
	{
		actor.MoveTo(prevPos);
	}
}

