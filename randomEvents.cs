using UnityEngine;
using System.Collections.Generic;

	/*this is used by ScheduleEvent(e) to store events in the list monthEvents so they can fire on the day, 
	it stores day for obvious  reasons and stores eventData so it knows what to fire*/
	class ScheduledEvent {
		public byte day;
		public Event eventData;
	}

public class randomEvents : MonoBehaviour{
	
	[SerializeField]private dates Dates;
	[SerializeField]private Stats stats;
	[SerializeField]private schedule Schedule;
	[SerializeField] private events Events;
	
	//the int is for the day it happens on and the scheduleevent is well... what do you think?
	private List<ScheduledEvent> monthEvents = new List<ScheduledEvent>();
	

    //runs at the start of the month to generate events to run for month
    public void generateEvents() {
		monthEvents.Clear();
		//pulls every event from the allEvents list and checks it for the month
		foreach(Event e in Events.allEvents) {
			if(CheckConditions(e))
				ScheduleEvent(e);
		}
		
		monthEvents.Sort((a, b) => a.day.CompareTo(b.day));
	} 
	
	private bool CheckConditions(Event e) {
		//this looks at the eventInstruction in the Event that stores EventOpType, target, and values for the eventoptype
		//so eventop type says which enum and then i store the value of it and even target if need be
		foreach(events.EventInstruction i in e.instructions){
			//target mostly for stats
			ushort statForCheck = changerReqStat(i.target);

			switch(i.type){
				case events.EventOpType.RequireAge: 
					if(stats.age != i.values) 
						return false;
					break;
				case events.EventOpType.RequireMonth:
					if(Schedule.todaysmonth != i.values) 
						return false;
					break;
				case events.EventOpType.RequireDay:
					if(Schedule.todaysday  != i.values) 
						return false;
					break;
				case events.EventOpType.RequireStat:
					if(statForCheck <= i.values)
						return false;
					break;
				case events.EventOpType.CheckFlag:
					if(0 != i.values)
						return false;
					break;
				default:
					return false;
			}
		}
		return true;		
    }
	
	private ushort changerReqStat(ushort ReqStat) {
		switch(ReqStat) {
			case 0: return stats.elegance; break;
			case 2: return stats.grace; break;
			case 4: return stats.glamor; break;
			case 6: return stats.negotiation; break;
				
			case 8: return stats.agality; break;
			case 10: return stats.athletics; break;
			case 12: return stats.strength; break;
			case 14: return stats.craftsmanship; break;
				
			case 1: return stats.stategy; break;
			case 3: return stats.science; break;
			case 5: return stats.history; break;
			case 7: return stats.math;break;
				
			case 9: return stats.morality;break;
			case 11: return stats.theology;break;
			case 13: return stats.sin;break;
			case 15: return stats.peity;break;
			
			default:
			return 0;
			break;
		}
	}
	
	private void ScheduleEvent(Event e) {
		
		byte daytoadd = 1, count;
		
		// Find the event's preferred day
		foreach (events.EventInstruction i in e.instructions) {
			if (i.type == events.EventOpType.RequireDay) {
				daytoadd = (byte)i.values;
				break;
			}
		}
		// Hard-coded events always stay on their assigned day
		if ((int)events.EventOpType.typeOevent == 1) {
			monthEvents.Add(new ScheduledEvent {
				day = daytoadd,
				eventData = e
			});
        return;
		}
		// Number of days in the current month
		byte maxDays = (byte)Schedule.monthmatrix[Schedule.yearType, Schedule.todaysmonth - 1];
		while (true) {
			count = 0;
			foreach (ScheduledEvent scheduled in monthEvents) {
				if (scheduled.day == daytoadd)
					count++;
			}
			if (count < 3) {
				monthEvents.Add(new ScheduledEvent {
					day = daytoadd,
					eventData = e
				});
            return;
			}
			// Try the next day
			daytoadd++;

			// Wrap back to day 1 if we've gone past the end of the month
			if (daytoadd > maxDays)
				daytoadd = 1;
			if (daytoadd == 1) {
				Debug.LogWarning("Month is full.");
				return;
			}
		}
	}
}
