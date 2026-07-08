using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

/*
made by Velvetsoft
1)add more jobs, vacations, and education
2)add in working store fronts and item usage
3)add in VN aspects
4)random sickness that can spawn randomly through the month and turn days into sick days(doing random events rn so both this and VN can be ran)
5) change to fixed point arithmetic
6)implement intro

initiation :
if current date if date == null then get daughters birthday & display else pull up date list and display current date
if daughters birthday make start date her birthday else make start day 1 set weekends
*/


public class schedule : MonoBehaviour {
	public int todaysyear, todaysmonth, todaysday, foodtype = 150;
	public int yearType, xpos = 0, ypos = 0, xposition = 0, yposition = 0, ximpos = 0, yimpos = 0, ximposition = 0, yimposition = 0, howmanyitemsselcted = 0;
	public Text todaysdate;
	public GameObject calendarPrefab, imageprefab, canvas, commenceButton;
	
	[SerializeField]private dates Dates;
	[SerializeField]private Stats stats;
	[SerializeField]private statgain Statgain;
	[SerializeField]private jobs Jobs;
	[SerializeField]private randomEvents RandomEvents;
	// Keep track of all instantiated buttons
    private List<GameObject> calendarPrefabs = new List<GameObject>();
	private List<GameObject> jobimageprefabs = new List<GameObject>();
	[SerializeField]private bool firstrun = true;
	
	[SerializeField] private int[] activitytypepicked = { 0, 0, 0};

	public int[,] monthmatrix = new int[,] {
		{31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31},
		//year 3 and 7 are leep years
		{31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
	};
	
	int[,] monthStartDays = new int[,] {
		{2, 5, 5, 1, 3, 6, 1, 4, 7, 2, 5, 7}, // Common Year (Non-Leap)
		{2, 5, 6, 2, 4, 7, 2, 5, 1, 3, 6, 1}  // Leap Year
	};
	
	private bool IsLeapYear(int year) {
		return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
	}
	
	public void MakeCalender() {
		if(todaysyear == 0) {
			todaysmonth = Dates.monthdaughter;
			todaysyear = Dates.daughteryear + 10;
			todaysday = Dates.daydaughter;
		}
	
		todaysdate.text = $"{todaysday}/{todaysmonth}/{todaysyear}";
		yearType = IsLeapYear(todaysyear) ? 1 : 0;
		
		// Instantiate (clone) the prefab
		for(int x = 1; x <= monthmatrix[yearType, todaysmonth - 1]; x++) {
			if(x == 1) {
				xpos = monthStartDays[yearType, todaysmonth - 1];
			}
			
			if(xpos > 7) {
				xpos = 1;
				ypos++;
			}	
			xposition = -600 + (50 * xpos);
			yposition = 150 - (50 * ypos);
				
			Vector3 position = new Vector3(xposition, yposition, 0f);
			GameObject textprefab = Instantiate(calendarPrefab, canvas.transform, false);
            textprefab.transform.localPosition = position;
			
			Text textText = textprefab.GetComponentInChildren<Text>();
			textText.text = $"{x}";
			
			calendarPrefabs.Add(textprefab);

			xpos++;
			
		}
	}
	
	public void destroy() {
		foreach (var calendarprefabInstance  in calendarPrefabs) {
            Destroy(calendarprefabInstance);
        }
        calendarPrefabs.Clear();
		
		xpos = 0;
		ypos = 0;
		
		ximpos = 1;
		yimpos = 0;
		howmanyitemsselcted = 0;
		commenceButton.SetActive(false);
		if(todaysmonth == Dates.monthdaughter && todaysyear == Dates.daughteryear + 10)
			todaysday = Dates.daydaughter;
		else
			todaysday = 1;
		
		removeimgageforcalander();
	}

/*
selection routine:
when job is picked apply add 10 days, if job has been picked 2 times and moth is 31 days add 11 days. if cannot do 10 days make as many as can
if back remove all days
if confirm start routen for jobs
take chash for food
*/


//refactor is complete, i sleep now
	public void addDays() {
		if(howmanyitemsselcted != 3 ) {	
			if(todaysmonth == Dates.monthdaughter && todaysyear == Dates.daughteryear + 10){
				if(todaysday == 1) {
					firstrun = false;
				}
				firstgameplaymonth();
			}
			else if(todaysmonth == Dates.monthdaughter && todaysyear == 1304)
				eighteenthbdaymonth();
			else
				restofthemonths();
			howmanyitemsselcted++;;
		}
		if(howmanyitemsselcted == 3) {
			commenceButton.SetActive(true);
		}
	}
	
	private void firstgameplaymonth() {
		//code for daughters first month with you
		if(todaysday != 1 && firstrun == false) {
			todaysday--;
			ximpos = monthStartDays[yearType, todaysmonth - 1] + todaysday;
			if (ximpos%7 != 0) {
				yimpos = ximpos/7;
			} else {
				yimpos = (ximpos/7) - 1;
			}
			if(ximpos >= 7) {
					ximpos = ximpos%7;
				if (ximpos == 0) {
					ximpos = 7;
				}
			}
			todaysday++;
			firstrun = true;
		}
		
		if(monthmatrix[yearType, todaysmonth - 1] == Dates.daydaughter) {
			intiateimage();	
			howmanyitemsselcted = 2;

		}else if(todaysday <= 20) {
			if(howmanyitemsselcted == 0 && todaysday == 1) {
				ximpos = monthStartDays[yearType, todaysmonth - 1];
			}
			for(int x = 0; x < 10; x++) {
				intiateimage();
			}
		}/*end of month*/ else{
			int daysRemaining = monthmatrix[yearType, todaysmonth - 1] - (todaysday - 1);
			for(int x = 0; x < daysRemaining; x++) {
				intiateimage();
			}
			howmanyitemsselcted = 2;
		}	
	}
	
	private void eighteenthbdaymonth() {
		//code for daughters 18th birthday month
		if(todaysday == 1)
			ximpos = monthStartDays[yearType, todaysmonth - 1];
				
		if(Dates.daydaughter > 10 && Dates.daydaughter - todaysday >= 9) {
			for(int x = 1; x < 10; x++) {
				intiateimage();
			}
		}else{
			for(int x = todaysday; x <= Dates.daydaughter - 1; x++) {
				intiateimage();
			}
			howmanyitemsselcted = 2;
		}
	}
	
	private void restofthemonths() {
		//first choice
		if(howmanyitemsselcted == 0 && !(todaysyear == 1304 && todaysmonth == Dates.monthdaughter)){
			ximpos = monthStartDays[yearType, todaysmonth - 1];
			for(int x = 0; x < 10; x++) {
				intiateimage();
			}
		}/*second choice for most months*/
		else if(howmanyitemsselcted == 1 && !(todaysyear == 1304 && todaysmonth == Dates.monthdaughter)){
			for(int x = 0; x < 10; x++) {
				intiateimage();
			}
		}/*3rd choice for full month, ending*/
		else if(howmanyitemsselcted == 2 && !(todaysyear == 1304 && todaysmonth == Dates.monthdaughter)) {
			int remainingDays = monthmatrix[yearType, todaysmonth - 1] - todaysday;
			todaysday--;
			for(int x = 0; x < monthmatrix[yearType, todaysmonth - 1] - 20; x++) {
				intiateimage();
			}
		}
	}
	
	public void selectactivity(int y) {
		activitytypepicked[howmanyitemsselcted-1] = y;
	}
	
	public void intiateimage() {
		if(ximpos >= 8) {
			ximpos = 1;
			yimpos++;
		}
		ximposition = -600 + (50 * ximpos);
		yimposition = 150 - (50 * yimpos);

		Vector3 position = new Vector3(ximposition, yimposition, 0f);
		GameObject jobimageprefab = Instantiate(imageprefab, canvas.transform, false);
		jobimageprefab.transform.localPosition = position;
		jobimageprefabs.Add(jobimageprefab);
	
		ximpos++;
		todaysday++;		
	}

	public void removeimgageforcalander() {
		for(int x = 0; x <= 2; x++){
			activitytypepicked[x] = -1;
		}
		
		foreach (var jobimageprefabInstance  in jobimageprefabs) {
            Destroy(jobimageprefabInstance);
        }
        jobimageprefabs.Clear();
		
		ximpos = 0;
		yimpos = 0;
		howmanyitemsselcted = 0;
		commenceButton.SetActive(false);
		if(todaysmonth == Dates.monthdaughter && todaysyear == Dates.daughteryear + 10)
			todaysday = Dates.daydaughter;
		else {
			todaysday = 1;
		}
		
		firstrun = false;
	}

/*
routine for day:
check if there is special events if so play special event
play animation for day
check for random event 
repeat until 10 days are finished then display all updated stats
*/
	
	public void commence() {
		RandomEvents.generateEvents();
		//take money out for food update 
		if(todaysmonth != Dates.monthdaughter) {
			for(int x = 1; monthmatrix[yearType, todaysmonth - 1] - x != 0; x++) {
				daterange(x);
			}
		}else {
			for(int x = Dates.daydaughter; monthmatrix[yearType, todaysmonth - 1] - x != 0; x++) {
				daterange(x);
			}
		}
		destroy();
		stats.funds = stats.funds - foodtype;
		todaysmonth++;
		todaysday=1;
		
		if(todaysmonth==13){
			todaysmonth = 1;
			todaysyear++;
		}
	}
	
	public void daterange(int x) {
		int y;
		if(x <= 10) {
			y = 0;
		} else if(x <= 20){
			y = 1;
		} else {
			y = 2;
		}
		Statgain.startroutine(activitytypepicked[y]);
	}
}
