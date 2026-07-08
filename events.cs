using UnityEngine;
using System.Collections.Generic;
	
	public class Event {
	
		public string name;
		public byte weight;

		public List<events.EventInstruction> instructions;
	}
	
public class events : MonoBehaviour {
	
		public List<Event> allEvents;
	
	[SerializeField]private dates Dates;

	public struct EventInstruction {
		public EventOpType type;
	/////////////////////////////
	//		stats for target if used for stats
	//		Social	         /
	//	elegance = 0    /
	//	grace = 2	    / 
	//	glamor = 4	    / 
	//	negotiation = 6  /
	//					    /
	//		physical	         /
	//	agality = 8	    / 
	//	athletics =10   / 
	//	strength = 12   /  
	//	craftsmanship = 14 / 
	//					    /
	//		intelligance      /
	//	stategy = 1     /  
	//	science = 3	    /  
	//	history = 5	    /  
	//	math = 7	    /  
	//	morality = 9    /
	//	theology = 11   /
	//	sin = 13	    /
	//	peity = 15	    /
	//					    /
	/////////////////////////////
		public byte target;
		public int values;
	}

	public enum EventOpType {
		typeOevent, /* 1 for hard coded events, 2 for random events, 3 for vm events, 4 for charector events, 5 for holidays, 6 for events in the market*/
		TextId,
	
		RequireAge,
		RequireMonth,
		RequireDay,
		
		RequireStat,
		CheckFlag,  /*if 0 can run if 1 can not run again*/

		AddStat,
		RemoveStat,
		AddFunds,
		RemoveFunds,
		SetFlag,

		RandomChance
	}

	public Event birthday = new Event {
		name = "Birthday",
		weight = 0,

		instructions = new List<EventInstruction> {
			new EventInstruction { type = EventOpType.TextId, values = 1 },
			new EventInstruction { type = EventOpType.RequireAge, values = 1 },
			new EventInstruction { type = EventOpType.RequireMonth, values = 1 }
		}
	};
	
	private void Awake(){ 
		allEvents = new List<Event>(){
			birthday,
        // harvestFestival,
        // churchFestival
		};
	}
}
