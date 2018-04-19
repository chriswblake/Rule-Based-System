		#define MEMORY_ELEMENT_SIZE   80
		#define MAX_MEMORY_ELEMENTS   40
		#define MAX_RULES             40
		#define MAX_TIMERS            10

		typedef struct memoryElementStruct *memPtr;

		typedef struct memoryElementStruct
		{
			int active;
			char element[MEMORY_ELEMENT_SIZE + 1];
			struct memoryElementStruct *next;
		} memoryElementType;
		typedef struct {
			int active;
			char ruleName[MEMORY_ELEMENT_SIZE + 1];
			memoryElementType *antecedent;
			memoryElementType *consequent;
		} ruleType;
		typedef struct {
			int active;
			int expiration;
		} timerType;

		memoryElementType workingMemory[MAX_MEMORY_ELEMENTS];
		ruleType ruleSet[MAX_RULES];
		timerType timers[MAX_TIMERS];

		int endRun = 0;
		int debug = 0;

int main(int argc, char *argv[])
{
	int opt, ret;
	char inpfile[80] = { 0 };

	extern void processTimers(void);
	extern int parseFile(char *);
	extern void interpret(void);

	//Get command line arguments
	while ((opt = getopt(argc, argv, "hdr:")) != -1)
	{
		switch (opt)
		{
			case 'h':
				emitHelp();
				break;

			case 'd':
				debug = 1;
				printf("Debugging enabled\n");
				break;

			case 'r':
				strcpy(inpfile, optarg);
				break;
		}
	}

	//Show help, if requested
	if (inpfile[0] == 0) emitHelp();

	//Clear variables
	bzero((void *)workingMemory, sizeof(workingMemory));
	bzero((void *)ruleSet, sizeof(ruleSet));
	bzero((void *)timers, sizeof(timers));

	//Read in the rules file
	ret = parseFile(inpfile);
	if (ret < 0)
	{
		printf("\nCould not open file, or parse error\n\n");
		exit(0);
	}

	while (1)
	{
		//Process next rule that modifies memory
		interpret();

		//Show current memory
		if (debug)	
			printWorkingMemory();
	
		//Adjust timers
		processTimers();

		//If at end, stop 
		if (endRun) break;

		//Wait one second before continuing
		sleep(1);
	}

	return 0;
}

		int parseFile(char *filename)
		{
			FILE *fp;
			char *file, *cur;
			int fail = 0;

			extern int debug;

			file = (char *)malloc(MAX_FILE_SIZE);

			if (file == NULL) return -1;

			//Open file
			fp = fopen(filename, "r");

			//Check if file point is null
			if (fp == NULL)
			{
				free(file);
				return -1;
			}

			//Feade file contents int "file"
			fread(file, MAX_FILE_SIZE, 1, fp);

			//Start at position 0
			cur = &file[0];

			while (1) {

				//This will parse an entire rule

				//Find the "(defrule" start of a rule
				cur = strstr(cur, "(defrule");

				//break if at end of file
				if (cur == NULL)
				{
					fail = 1;
					break;
				}

				if (!strncmp(cur, "(defrule", 8)) {
					int i = 0;

					cur += 9;

					while (*cur != 0x0a) {
						ruleSet[ruleIndex].ruleName[i++] = *cur++;
					}
					ruleSet[ruleIndex].ruleName[i++] = 0;

					cur = skipWhiteSpace(cur);

					/* Parse the antecedents */
					cur = parseAntecedent(cur, &ruleSet[ruleIndex]);

					if (cur == NULL) {
						fail = 1;
						break;
					}

					/* Should be sitting on the '=>' */
					if (!strncmp(cur, "=>", 2)) {

						cur = skipWhiteSpace(cur + 2);

						/* Parse the consequents */
						cur = parseConsequent(cur, &ruleSet[ruleIndex]);

						if (cur == NULL) {
							fail = 1;
							break;
						}

						/* Ensure we're closing out the current rule */
						if (*cur == ')') {
							cur = skipWhiteSpace(cur + 1);
						}
						else {
							fail = 1;
							break;
						}

					}
					else {
						fail = 1;
						break;
					}

					ruleSet[ruleIndex].active = 1;
					ruleIndex++;

				}
				else {

					break;

				}

			}

			if (debug)
			{
				printf("Found %d rules\n", ruleIndex);
			}

			free((void *)file);

			fclose(fp);

			return 0;
		}
		char *parseAntecedent(char *block, ruleType *rule)
		{
			while (1) {

				block = skipWhiteSpace(block);

				if (*block == '(') {

					block = parseElement(block, &rule->antecedent);

				}
				else break;
			}

			return block;
		}
		char *parseConsequent(char *block, ruleType *rule)
		{
			while (1) {

				block = skipWhiteSpace(block);

				if (*block == '(') {

					block = parseElement(block, &rule->consequent);

				}
				else break;

			}

			return block;
		}
		char *parseElement(char *block, memoryElementType **met)
		{
			memoryElementType *element;
			int i = 0;
			int balance = 1;

			element = (memoryElementType *) malloc(sizeof(memoryElementType));
			element->element[i++] = *block++;

			while (1) {

				if (*block == 0) break;

				if (*block == ')') balance--;
				if (*block == '(') balance++;

				element->element[i++] = *block++;

				if (balance == 0) break;

			}

			element->element[i] = 0;
			element->next = 0;

			if (*met == 0) *met = element;
			else {
				memoryElementType *chain = *met;
				while (chain->next != 0) chain = chain->next;
				chain->next = element;
			}

			return block;
		}
		char *skipWhiteSpace(char *block)
		{
			char ch;

			while (1) {

				ch = *block;
				while ((ch != '(') && (ch != ')') && (ch != '=') &&
					(ch != 0) && (ch != ';')) {
					block++;
					ch = *block;
				}

				if (ch == ';') {

					while (*block++ != 0x0a);

				}
				else break;

			}

			return block;
		}



		void interpret(void)
{
	int rule;
	int fired = 0;

	extern int checkRule(int);
	extern int debug;

	for (rule = 0; rule < MAX_RULES; rule++)
	{
		fired = 0;

		if (ruleSet[rule].active)
		{
			fired = checkRule(rule);

			//If a rule had some effect on working memory, exit, otherwise test another rule.			
			if (fired) break;
		}
	}

	if (debug)
	{
		if (fired) printf("Fired rule %s (%d)\n", ruleSet[rule].ruleName, rule);
	}

	return;
}
		int checkRule(int rule)
{
	int fire = 0;
	char arg[MEMORY_ELEMENT_SIZE] = { 0 };

	extern int fireRule(int, char *);

	fire = checkPattern(rule, arg);

	if (fire == 1)
	{
		fire = fireRule(rule, arg);
	}

	return fire;
}
		int checkPattern(int rule, char *arg)
{
	int ret = 0;
	char term1[MEMORY_ELEMENT_SIZE + 1];
	char term2[MEMORY_ELEMENT_SIZE + 1];
	memoryElementType *antecedent = ruleSet[rule].antecedent;

	while (antecedent)
	{
		//Get terms from antecedent element
		sscanf(antecedent->element, "(%s %s)", term1, term2);
		if (term2[strlen(term2) - 1] == ')') term2[strlen(term2) - 1] = 0;

		//Check if second term is generic
		if (term2[0] == '?')
		{
			int i;
			char wm_term1[MEMORY_ELEMENT_SIZE + 1];
			char wm_term2[MEMORY_ELEMENT_SIZE + 1];

			for (i = 0; i < MAX_MEMORY_ELEMENTS; i++)
			{
				if (workingMemory[i].active)
				{
					//Get terms of working memory element
					sscanf(workingMemory[i].element, "(%s %s)", wm_term1, wm_term2);
					
					//Remove parenthesis?
					if (wm_term2[strlen(wm_term2) - 1] == ')')
						wm_term2[strlen(wm_term2) - 1] = 0;

					//If antecedent and working memory match, add it to the chain
					if (!strncmp(term1, wm_term1, strlen(term1)))
						addToChain(wm_term2);
				}
			}
		}

		//Go to next
		antecedent = antecedent->next;
	}

	//Now that we have the replacement strings, walk through
	//the rules trying the replacement string when necessary.
	do //while (chain)
	{
		memoryElementType *curRulePtr, *temp;

		//Get antecendent of current rule
		curRulePtr = ruleSet[rule].antecedent;

		while (curRulePtr) //while it is active
		{
			//Get terms of the antecedent
			sscanf(curRulePtr->element, "(%s %s)", term1, term2);

			//Replace parenthesis with 0
			if (term2[strlen(term2) - 1] == ')')
				term2[strlen(term2) - 1] = 0;

			//Check terms
			if (!strncmp(term1, "true", strlen(term1)))
			{
				//If term1 is found, end loop
				ret = 1;
				break;
			}
			else
			{
				//If just checking first term (because second term is ?)
				if ((term2[0] == '?') && (chain))
					strcpy(term2, chain->element);
			}

			//Search the memory for this term set
			ret = searchWorkingMemory(term1, term2);

			//If not found, break loop.
			if (!ret) break;

			//Go to next from current antecedent
			curRulePtr = curRulePtr->next;
		}

		//if found
		if (ret)
		{
			//Cleanup the replacement string chain
			while (chain)
			{
				temp = chain;
				chain = chain->next;
				free(temp);
			}

			strcpy(arg, term2);
		}
		else
		{
			if (chain)
			{
				temp = chain;
				chain = chain->next;
				free(temp);
			}
		}

	} while (chain);

	return ret;
}
void addToChain(char *element)
{
	//Variables
	memoryElementType *walker, *newElement;;

	//Create a new mememoryElementType
	newElement = (memoryElementType *) malloc(sizeof(memoryElementType));

	//Copy the element text
	strcpy(newElement->element, element);

	if (chain == NULL)
	{
		chain = newElement;
	}
	else
	{
		walker = chain;
		while (walker->next) walker = walker->next;
		walker->next = newElement;
	}

	newElement->next = NULL;
}

		int searchWorkingMemory(char *term1, char *term2)
{
	int ret = 0;
	int curMem = 0;
	char wm_term1[MEMORY_ELEMENT_SIZE + 1];
	char wm_term2[MEMORY_ELEMENT_SIZE + 1];

	while (1)
	{
		if (workingMemory[curMem].active)
		{
			//extract the memory element
			sscanf(workingMemory[curMem].element, "(%s %s)", wm_term1, wm_term2);
			if (wm_term2[strlen(wm_term2) - 1] == ')')
				wm_term2[strlen(wm_term2) - 1] = 0;

			//If both terms match, set return value to 1 and stop loop.
			if ((!strncmp(term1, wm_term1, strlen(term1))) && (!strncmp(term2, wm_term2, strlen(term2))))
			{
				ret = 1;
				break;
			}
		}

		//If at end of memory, end the loop.
		curMem++;
		if (curMem == MAX_MEMORY_ELEMENTS)
			break;
	}

	return ret;
}
		int fireRule(int rule, const char *arg)
{
	int ret;
	memoryElementType *walker = ruleSet[rule].consequent;
	char newCons[MAX_MEMORY_ELEMENTS + 1];

	while (walker)
	{
		if (!strncmp(walker->element, "(add", 4))
		{
			constructElement(newCons, walker->element, arg);
			ret = performAddCommand(newCons);
		}
		else if (!strncmp(walker->element, "(delete", 7))
		{
			constructElement(newCons, walker->element, arg);
			ret = performDeleteCommand(newCons);
		}
		else if (!strncmp(walker->element, "(disable", 8))
		{
			ruleSet[rule].active = 0;
			ret = 1;
		}
		else if (!strncmp(walker->element, "(print", 6))
		{
			ret = performPrintCommand(walker->element);
		}
		else if (!strncmp(walker->element, "(enable", 7))
		{
			ret = performEnableCommand(walker->element);
		}
		else if (!strncmp(walker->element, "(quit", 5)) 
		{
			extern int endRun;
			endRun = 1;
		}

		walker = walker->next;
	}

	return ret;
}
		void constructElement(char *new, const char *old, const char *arg)
{
	//Find the second parenthesis
	old++;
	while (*old != '(')
		old++;
	while ((*old != 0) && (*old != '?'))
		*new++ = *old++;

	// This was a complete rule (i.e., no ? element)
	if (*old == 0)
	{
		*(--new) = 0;
		return;
	}
	else
	{
		//Copy in the arg
		while (*arg != 0) *new++ = *arg++;
		if (*(new - 1) != ')') *new++ = ')';
		*new = 0;
	}

	return;
}

		int performAddCommand(char *mem)
{
	int slot;

	//Check to ensure that this element isn't already in working memory
	for (slot = 0; slot < MAX_MEMORY_ELEMENTS; slot++)
	{
		if (workingMemory[slot].active)
		{
			if (!strcmp(workingMemory[slot].element, mem))
			{
				//Element is already here, return
				return 0;
			}
		}
	}

	//Add this element to working memory
	slot = findEmptyMemSlot();
	if (slot < MAX_MEMORY_ELEMENTS)
	{
		workingMemory[slot].active = 1;
		strcpy(workingMemory[slot].element, mem);
	}
	else
	{
		assert(0);
	}

	return 1;
}
		int findEmptyMemSlot(void)
{
	int i;

	for (i = 0; i < MAX_MEMORY_ELEMENTS; i++) {
		if (!workingMemory[i].active) break;
	}

	return i;
}
		int performDeleteCommand(char *mem)
{
	int slot;
	int ret = 0;
	char term1[MEMORY_ELEMENT_SIZE + 1];
	char term2[MEMORY_ELEMENT_SIZE + 1];
	char wm_term1[MEMORY_ELEMENT_SIZE + 1];
	char wm_term2[MEMORY_ELEMENT_SIZE + 1];

	sscanf(mem, "(%s %s)", term1, term2);

	for (slot = 0; slot < MAX_MEMORY_ELEMENTS; slot++)
	{
		if (workingMemory[slot].active)
		{
			sscanf(workingMemory[slot].element, "(%s %s)", wm_term1, wm_term2);
			if (!strncmp(term1, wm_term1, strlen(term1)) && !strncmp(term2, wm_term2, strlen(term2)))
			{
				workingMemory[slot].active = 0;
				bzero(workingMemory[slot].element, MEMORY_ELEMENT_SIZE);
				ret = 1;
			}
		}
	}
	return ret;
}
int performPrintCommand(char *element)
{
	char string[MAX_MEMORY_ELEMENTS + 1];
	int i = 0, j = 0;

	/* Find initial '"' */
	while (element[i] != '"') i++;
	i++;

	/* Copy until we reach the end */
	while (element[i] != "") string[j++] = element[i++];
	string[j] = 0;

	printf("%s\n", string);

	return 1;
}

		int performEnableCommand(char *element)
{
	char *string;
	int timer, expiration;

	void startTimer(int, int);

	string = strstr(element, "timer");

	sscanf(string, "timer %d %d", &timer, &expiration);

	startTimer(timer, expiration);

	return 1;
}
		void processTimers(void)
{
	int i;

	for (i = 0; i < MAX_TIMERS; i++)
	{
		if (timers[i].active)
		{
			if (--timers[i].expiration == 0)
			{
				fireTimer(i);
			}
		}
	}
	return;
}
		int fireTimer(int timerIndex)
{
	int ret;
	char element[MEMORY_ELEMENT_SIZE + 1];

	extern int performAddCommand(char *mem);

	sprintf(element, "(timer-triggered %d)", timerIndex);

	ret = performAddCommand(element);

	timers[timerIndex].active = 0;

	return ret;
}
		void startTimer(int index, int expiration)
{
	timers[index].expiration = expiration;
	timers[index].active = 1;

	return;
}