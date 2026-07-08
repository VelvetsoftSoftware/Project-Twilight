using UnityEngine;

public class jobs : MonoBehaviour{
	
	[SerializeField]private Stats stats;
	[SerializeField]private schedule Schedule;
	
	public class Activity{
		//positive for gaining money, negative for loosing money
		public float proficiency;
		private readonly uint packedStatIdentifier, packedStats;
		private readonly byte activityType;
		public AnimationClip jobAnimation, failureAnimation;

		public Activity(uint packedStats, float proficiency, uint packedStatIdentifier, byte activityType){
			this.packedStats = packedStats;
			this.proficiency = proficiency;
			this.packedStatIdentifier = packedStatIdentifier;
			this.activityType = activityType;
		}

		// Public properties to retrieve the values
		public uint PackedStats => packedStats;
		public float Proficiency => proficiency;
		public uint PackedStatIdentifier => packedStatIdentifier;
		public byte ActivityType => activityType;
	}
	
	/*
	age	money==== increase 1-4 decrease 1-4 stress
	0000 0000 0000 00 00 00 00  00 00 00 00  0000
	0000 0000 0000 0000  0000   0000  0000   0000
	*/
	
	/*
	will be an identifier for type of activity
	0000 = jobs
	0001 = vacation
	0010 = education
	*/
	
	/*jobs*/
	
	/*age 10*/ /*pm1 inn, church, scribe, armorer. 
	pm2 housework, babysitter, church, farm, inn, restaurant. 
	pm3 babysitter, housecleaner.
	pm4 housework, babbysitter, farm, church*/
	
	public Activity chapelCleaner = new Activity(
		/*pm4 2 gold down: charm, sensitivity, sin. up: pride, morals*/
		/*pm2 1 gold down: sin. up: morality, faith*/
		/*pm1 1 gold up:morals*/
	
		/*age, cost(money made), peity, morality, null, null, sin, null, null, null, stress*/
		0x00150402,
		/*proficiency*/ 0.0f,
		/*pack stats identify*/ 0xF900D000,
		/*type of activity*/ 0x00000000
	);
	
	public Activity houseWork = new Activity(
	/*pm2 up cleaning, cooking, temporment. down: sensitivity*/
	/*pm4 up: stamina, sens, temper, popularity, */
	/*pm3(simular to maid) Raises Stamina, Vitality, Morality and Attitude Lowers Pride and Sense*/
	
	/*age, cost(money made), strength, null, null, null, elegance, grace, null, null, stress*/
	0xA0010502,
	/*proficiency*/ 0.0f,
	/*pack stats identify*/ 0x02000003, 
	/*type of activity*/0b0000
	);
	
	public Activity babySitter = new Activity(
	/*age, cost(money made), strength, null, null, null, elegance, grace, null, null, stress*/
	0xA0010502,
	/*proficiency*/ 0.0f,
	/*pack stats identify*/ 0x0200000C, 
	/*type of activity*/0x0000
	);
	
//	public Activity chimneySweeper = new Activity(
//	/*age*/ 10,/*cost*/ 2, /*strength*/ 1, 
//	/*null*/ 0, /*null*/ 0, /*null*/ 0, 
//	/*fatige*/ 2, /*elegance*/ 1, /*grace*/ 1, 
//	/*null*/ 0, /*null*/ 0, /*proficiency*/ 0,
//	/*pack stats identify*/ 0x0200000C, /*type of activity*/0x0000
//	);

	public Activity maid = new Activity(   
	/*pm1 age 12, money 8, grace +1, fatigue +1 */
	/*pm2 n/a*/
	/*pm3 age ?, wage 12 elegance +, charm +, attitude -, pride -*/
	/*pm 4 age 11, has to have 100-150 to elegance too, charm+, morals +, elegance +*/

	/*age, cost(money made), strength, null, null, null, elegance, grace, null, null, stress*/
	0xA0210502,
	/*proficiency*/ 0.0f,
	/*pack stats identify*/ 0x002000C, 
	/*type of activity*/0b0000
	);
	
	/*rest & vacation*/
	
//	public Activity freetime = new Activity(
//	/*age*/ 10,/*cost*/ 0, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*null*/ 0, 
//	/*fatige*/ 0, /*fatige remove*/ 1, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*proficiency*/ 0,
//	/*pack stats identify*/ 0x00000000, /*type of activity*/0x0001
//	);
	
//	public Activity oceanVacation = new Activity(
//	/*age*/ 10,/*cost*/ 0, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*null*/ 0, 
//	/*fatige*/ 0, /*fatige remove*/ 1, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*proficiency*/ 0,
//	/*pack stats identify*/ 0x00000000, /*type of activity*/0x0001
//	);
	
//	public Activity mountainVacation = new Activity(
//	/*age*/ 10,/*cost*/ 0, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*null*/ 0, 
//	/*fatige*/ 0, /*fatige remove*/ 1, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*proficiency*/ 0,
//	/*pack stats identify*/ 0x00000000, /*type of activity*/0x0001
//	);

	/*education*/
	
//	public Activity theolgyEd = new Activity(
//	/*age*/ 10,/*cost*/ 0, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*null*/ 0, 
//	/*fatige*/ 0, /*fatige remove*/ 1, /*null*/ 0, 
//	/*null*/ 0, /*null*/ 0, /*proficiency*/ 0,
//	/*pack stats identify*/ 0x00000000, /*type of activity*/0x0010
//	);
	
	
	public void SelectJob(Activity job, string jobName) {
		
	}
}
