using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

[RequireComponent(typeof(Navigator))]
public class Entity : MonoBehaviour
{
    private Navigator nav;
    private AnimationHook anim;
    private Enums.Operation? lastOperation;
    private MoodParticles moodParticles;

    public AudioSource Walking;
    public AudioSource Refusal;

    public Enums.Species Species;

    public int mood;
    public Enums.Mood moodLevel {
        get { return Helper.EnumHelper.GetMood(mood); }
    }

    public Navigator Navigate { get { return nav; } }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<AnimationHook>();
        moodParticles = GetComponentInChildren<MoodParticles>();
        moodParticles.SetMood(moodLevel);
        nav = GetComponent<Navigator>();
        nav.OnEnterRoom.AddListener(OnEnterRoom);
        nav.OnLeaveRoom.AddListener(OnLeaveRoom);
        nav.OnNavigatorArrived.AddListener(OnEnterNode);
        lastOperation = null;
        anim.StartWalk();
        transform.Find("conflict").gameObject.SetActive(false);
    }

    void OnLeaveRoom(Navigator nav, NavRoom room)
    {
        // room.Room.LeaveRoom(this);
    }
    void OnEnterRoom(Navigator nav, NavRoom room)
    {
        // room.Room.EnterRoom(this);
    }

    void OnEnterNode(Navigator nav, NavNode n)
    {
        if (n is NavNodeSlot)
        {
            var slt = n.GetComponent<Slot>();
            slt.OnEntityArrived(this);
            anim.StopWalk();
            Walking.Stop();
        }
    }

    public void ApplyTreatment(Enums.Operation op)
    {
        Debug.Log("Applied " + op.ToString());
        lastOperation = op;
        mood += 1;
        moodParticles.SetMood(moodLevel);
        if (moodLevel == Enums.Mood.ASCENDED) {
            ScoreKeeper.mainKeeper.AddToScore(Species);
            // TODO: Despawn
            Destroy(gameObject);
        }
        anim.StartWalk();
        Walking.Play();
    }

    public bool ApproachToken(NavNode slot)
    {
        var res = IsOperationAllowed(slot.GetComponent<Slot>().appliedTreatment);
        if (res is Reaction reaction) {
            StartCoroutine(ShowReaction(reaction));
            return false;
        } else {
            nav.SetDirection(slot);
            return true;
        }
    }

    public IEnumerator ShowReaction(Reaction reaction) {
        var speech = GetComponentInChildren<Speech>(true);
        if (reaction.room is Enums.Operation op) {
            speech.gameObject.SetActive(true);
            speech.SetRoomIcon(op, reaction.forbidden);
            yield return new WaitForSeconds(3f);
            speech.gameObject.SetActive(false);
        } else {
            // TODO: probably hotRoom == true
        }
    }

    private Reaction? IsOperationAllowed(Enums.Operation op) {
        // Has to undress first
        if (lastOperation == null && op != Enums.Operation.CHANGING_ROOM) {
            Reaction reaction;
            reaction.room = Enums.Operation.CHANGING_ROOM;
            reaction.hotRoom = false;
            reaction.forbidden = false;
            Refusal.Play();
            return reaction;
        }

        // Has to dress last
        // TODO: Check energy
        if (op == Enums.Operation.EXIT && lastOperation != Enums.Operation.CHANGING_ROOM) {
            Reaction reaction;
            reaction.room = Enums.Operation.CHANGING_ROOM;
            reaction.hotRoom = false;
            reaction.forbidden = false;
            Refusal.Play();
            return reaction;
        }

        // Cold bath only after hot spring or sauna
        if (op == Enums.Operation.COLD_BATH && !EnumHelper.IsOperationHot(lastOperation)) {
            Reaction reaction;
            reaction.room = null;
            reaction.hotRoom = true;
            reaction.forbidden = false;
            Refusal.Play();
            return reaction;
        }

        // No two hot things after each other
        if (EnumHelper.IsOperationHot(lastOperation) && EnumHelper.IsOperationHot(op)) {
            Reaction reaction;
            reaction.room = null;
            reaction.hotRoom = true;
            reaction.forbidden = true;
            Refusal.Play();
            return reaction;
        }

        return null;
    }

    public void SetConflict(Enums.Species? conflict) {
        var speech = GetComponentInChildren<Speech>(true);
        if (conflict is Enums.Species species) {
            transform.Find("conflict").gameObject.SetActive(true);
            speech.gameObject.SetActive(true);
            speech.SetSpirit(species, true);
        } else {
            speech.gameObject.SetActive(false);
            transform.Find("conflict").gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct Reaction {

        public Enums.Operation? room;
        // If hot is true then room should be null
        public bool hotRoom;
        public bool forbidden;

    }
}
