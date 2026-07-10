#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include <stdint.h>

	/* allllllllllllllll notes here are from other scripts with pertinent info for the code in this box
	//
	//
	//age	money==== increase 1-4 decrease 1-4 stress
	//0000 0000 0000 00 00 00 00  00 00 00 00  0000
	//0000 0000 0000 0000  0000   0000  0000   0000
	//0000"28"00000000"20"00"18"00"16"00"14"00"12"00"10"00"8"00"6"00"4"0000"0"
	//
	//
	//
	//will be an identifier for type of activity
	//0000 = jobs
	//0001 = vacation
	//0010 = education
	//
	//					this is the gold standard
	//
	//	public Activity chapelCleaner = new Activity(
	//		/*age, cost(money made), peity, morality, null, null, sin, null, null, null, stress*/
	//		0xA0150102,
	//		/*proficiency*/ 0.0f,
	//		/*pack stats identify*/ 0x000D009F, 
	//		/*type of activity*/0b0000
	//	);
	//
	//
	//
	//	// each stat increase/ decrease gets 4 bits to define and goes from start which is right
	//////////////////////////////
	//		Social	         	/
	//	elegance = 0000 = 0     /
	//	grace = 0010 = 2	    / 
	//	glamor = 0100	= 4	    / 
	//	negotiation = 0110 = 6  /
	//					   		/
	//		physical	        /
	//	agality = 1000	= 8	    / 
	//	athletics = 1010 =10    /   0000 0000 0000 0000
	//	strength = 1100 = 12    /   0000 0000 0000 0000
	//	craftsmanship = 1110 = 14 / 
	//					    	/
	//		intelligance        /
	//	stategy = 0001 = 1      /  
	//	science = 0011 = 3	    /  
	//	history = 0101 = 5	    /  
	//	math = 0111 = 7	    	/  
	//	morality = 1001 = 9     /
	//	theology = 1011 = 11    /
	//	sin = 1101 = 13	    	/
	//	peity = 1111 = 15	    /
	//					    	/
	//////////////////////////////////////////////////////////////////////////////////////////////////*/
	
	char name[20];
	
	uint8_t age, money;
	uint8_t increase1, increase2, increase3, increase4;
	uint8_t decrease1, decrease2, decrease3, decrease4;
	uint8_t stress;
	uint32_t packedstat;
	
	uint8_t statincrease1, statincrease2, statincrease3, statincrease4;
	uint8_t statdecrease1, statdecrease2, statdecrease3, statdecrease4;
	uint32_t packstatid;
	
	uint8_t activitytype/*0 for job 1 for vacatoin 2 for education*/;
	uint8_t whattodo = 0;
	bool close = 0;
	
	int closeorcontinue(void) {
		printf("make a new activity is 1 close program is 2  \n");
		scanf("%hhu", &whattodo);
		switch(whattodo) {
			case 1: close = 0; break;
			case 2: close = 1; break;
		}
		
		return 0;
	}
	
	int printactivities(void) {
		printf("DEBUG:\n");
		printf("age=%u money=%u stress=%u\n", age, money, stress);
		printf("inc=%u %u %u %u\n", increase1, increase2, increase3, increase4);
		printf("dec=%u %u %u %u\n\n", decrease1, decrease2, decrease3, decrease4);
		
		printf("stat no=%u %u %u %u\n", statincrease1, statincrease2, statincrease3, statincrease4);
		printf("stat no=%u %u %u %u\n",  statdecrease1, statdecrease2, statdecrease3, statdecrease4);
		
		printf("name           %p\n", (void*)name);
		printf("age            %p\n", (void*)&age);
		printf("money          %p\n", (void*)&money);
		printf("increase1      %p\n", (void*)&increase1);
		printf("activitytype   %p\n\n", (void*)&activitytype);
		
		printf("public Activity %s = new Activity(\n\t\t", name);
		printf("0x%08X,\n\t\t", packedstat);
		printf("/*proficiency*/ 0.0f,\n\t\t");
		printf("/*pack stats identify*/ 0x%08X, \n\t\t", packstatid);
		printf("/*type of activity*/ 0x%08X\n\t", activitytype);
		printf(");\n\n");
		
		return 0;
	}
	
	int printmenu(void) {
		system("cls");
		printf(" ELEGANCE      = 0,\n STRATEGY      = 1,\n GRACE         = 2,\n SCIENCE       = 3, \n GLAMOR        = 4,\n HISTORY       = 5,\n NEGOTIATION   = 6,\n MATH          = 7,\n AGILITY       = 8,\n MORALITY      = 9,\n ATHLETICS     = 10,\n THEOLOGY      = 11,\n STRENGTH      = 12,\n SIN           = 13, \n CRAFTSMANSHIP = 14,\n PIETY         = 15\n");
		
		return 0;
	}
	
	int packstatsid(void) {
		
		packstatid |= ((uint32_t)(statincrease1 & 0xF)) << 28;
		packstatid |= ((uint32_t)(statincrease2 & 0xF)) << 24;
		packstatid |= ((uint32_t)(statincrease3 & 0xF)) << 20;
		packstatid |= ((uint32_t)(statincrease4 & 0xF)) << 16;
		
		packstatid |= ((uint32_t)(statdecrease1 & 0xF)) << 12;
		packstatid |= ((uint32_t)(statdecrease2 & 0xF)) << 8;
		packstatid |= ((uint32_t)(statdecrease3 & 0xF)) << 4;
		packstatid |= ((uint32_t)(statdecrease4 & 0xF)) << 0;
		
		return 0;
	}
	
	int statincreasepacker(void) {
		
		packedstat |= ((uint32_t)(age & 0xF)) << 28;
		packedstat |= ((uint32_t)(money & 0xFF)) << 20;
		packedstat |= ((uint32_t)(increase1 & 0x3)) << 18;
		packedstat |= ((uint32_t)(increase2 & 0x3)) << 16;
		packedstat |= ((uint32_t)(increase3 & 0x3)) << 14;
		packedstat |= ((uint32_t)(increase4 & 0x3)) << 12;
		
		packedstat |= ((uint32_t)(decrease1 & 0x3)) << 10;
		packedstat |= ((uint32_t)(decrease2 & 0x3)) << 8;
		packedstat |= ((uint32_t)(decrease3 & 0x3)) << 6;
		packedstat |= ((uint32_t)(decrease4 & 0x3)) << 4;
		packedstat |= ((uint32_t)(stress & 0xF)) << 0;
		
		return 0;
	}
	
	int getdata(void) {
		
		printf("activity name\n");
		scanf(" %19s", name);
		printf("... %s ...", name);
		
		printf("activity type\n 0 for job \n 1 for vacation \n 2 for edecation\n");
		scanf(" %hhu", &activitytype);
		
		printf("age for activity(use 0-7 this represemts 10-17)\n");
		scanf(" %hhu", &age);
		
		if (age != 1)
			printf("FAILED AGE\n");

		printf("money for activity\n");
		scanf(" %hhu", &money);
		
		printf("activity stat increase 1\n");
		scanf(" %hhu", &increase1);
		printf("activity stat increase 2\n");
		scanf(" %hhu", &increase2);
		printf("activity stat increase 3\n");
		scanf(" %hhu", &increase3);
		printf("activity stat increase 4\n");
		scanf(" %hhu", &increase4);
		
		printf("activity stat decrease 1\n");
		scanf(" %hhu", &decrease1);
		printf("activity statv decrease 2\n");
		scanf(" %hhu", &decrease2);
		printf("activity stat decrease 3\n");
		scanf(" %hhu", &decrease3);
		printf("activity stat decrease 4\n");
		scanf(" %hhu", &decrease4);
		
		printf("activity stress gain\n");
		scanf(" %hhu", &stress);
		
		printmenu();
		printf("stat increase 1 id\n");
		scanf(" %hhu", &statincrease1);
		printmenu();
		printf("stat increase 2 id\n");
		scanf(" %hhu", &statincrease2);
		printmenu();
		
		printf("stat increase 3 id\n");
		scanf(" %hhu", &statincrease3);
		printmenu();
		printf("stat increase 4 id\n");
		scanf(" %hhu", &statincrease4);
		printmenu();
		
		printf("stat decrease 1 id\n");
		scanf(" %hhu", &statdecrease1);
		printmenu();
		printf("stat decrease 2 id\n");
		scanf(" %hhu", &statdecrease2);
		printmenu();
		printf("stat decrease 3 id\n");
		scanf(" %hhu", &statdecrease3);
		printmenu();
		printf("stat decrease 4 id\n");
		scanf(" %hhu", &statdecrease4);
		
		system("cls");
		
		return 0;
	}
	
	int runtime(void) {
		packedstat = 0;
		packstatid = 0;
		decrease1 = 0;
		decrease2 = 0;
		decrease3 = 0;
		decrease4 = 0;
		increase1 = 0; 
		increase2 = 0; 
		increase3 = 0; 
		increase4 = 0; 
		
		age = 0;  
		money = 0;
		statincrease1= 0;
		statincrease2= 0; 
		statincrease3= 0;
		statincrease4= 0;
		statdecrease1= 0; 
		statdecrease2= 0;
		statdecrease3= 0; 
		statdecrease4= 0;

		system("cls");
		getdata();
		statincreasepacker();
		packstatsid();
		printactivities();
	
		return 0;
	}
	
	int main(void) {
		
		printf("name           %p\n", (void*)name);
		printf("age            %p\n", (void*)&age);
		printf("money          %p\n", (void*)&money);
		printf("increase1      %p\n", (void*)&increase1);
		printf("activitytype   %p\n", (void*)&activitytype);
		
		while(!close) {
			runtime();
			closeorcontinue();
		}
	
		return 0;
	}