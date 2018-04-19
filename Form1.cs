using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Ch8_RuleBasedSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            //Set default rules
            string fileAddress = "FaultTolerance.rdb";
            if (File.Exists(fileAddress))
            {
                tbRulesAddress.Text = fileAddress;
                ruleSet = RuleType.parseFile(fileAddress);
                tbRules.Text = rulesToString();
            }
        }

        //Classes
        class MemoryElementType
        {
            //Fields
            public bool active;
            public string element;
            public string value;
            public MemoryElementType next = null;

            //Constructors
            public MemoryElementType(bool active, string element, string value) : this(active, element, value, null) { }
            public MemoryElementType(bool active, string element, string value, MemoryElementType next)
            {
                this.active = active;
                this.element = element;
                this.value = value;
                this.next = next;
            }

            //Debug
            public override string ToString()
            {
                return element + " " + value + " - " + active;
            }
        }
        class RuleType
        {
            public bool active;
            public string ruleName;
            public List<MemoryElementType> antecedents = new List<MemoryElementType>();
            public List<MemoryElementType> consequents = new List<MemoryElementType>();

            //Constructor
            public RuleType(bool active, string ruleName, string antecedentsText, string consequentsText)
            {
                this.active = active;
                this.ruleName = ruleName;
                addAntecedents(antecedentsText);
                addConsequents(consequentsText);
            }

            //Methods
            public static List<RuleType> parseFile(string fileName)
            {
                //Load file
                string text = "";
                string origText = "";
                try
                {
                    text = File.ReadAllText(fileName);
                    origText = text;
                }
                catch
                {
                    return new List<RuleType>();
                }

                //Working variable
                int cutLength = 0;
                List<RuleType> rules = new List<RuleType>();

                //Remove Notes
                while (true)
                {
                    int startPos = text.IndexOf(";");
                    int endPos = text.IndexOf("\r\n", startPos + 1);
                    if (startPos == -1) break;
                    text = text.Remove(startPos, endPos - startPos);
                }

                //Remove new-line characters
                text = text.Replace("\r", " ").Replace("\n", " ");

                //Find all rules in the file
                while (true)
                {
                    //Check for end of text
                    if (text.Trim().Length == 0)
                        break;

                    //Search for defrule
                    cutLength = text.IndexOf("(defrule");
                    text = text.Remove(0, cutLength);
                    text = text.Remove(0, 9);

                    //Get rule name
                    cutLength = text.IndexOf(" ");
                    string ruleName = text.Substring(0, cutLength);
                    text = text.Remove(0, cutLength);

                    //Get Antecedents text
                    cutLength = text.IndexOf("=>");
                    string antText = text.Substring(0, cutLength);
                    text = text.Remove(0, cutLength);

                    //Get Consequents text
                    int parCounter = 0;
                    cutLength = 0;
                    while (true)
                    {
                        char c = text[cutLength];
                        if (c == '(') parCounter++;
                        if (c == ')') parCounter--;

                        //Check if counter goes above 2. This is an error
                        if (parCounter > 2)
                            throw new FileLoadException("Rule file has an error.");

                        //Check for closing parenthesis
                        if (parCounter == -1)
                            break;

                        //Next character
                        cutLength++;
                    }
                    string conText = text.Substring(0, cutLength);
                    text = text.Remove(0, cutLength + 1);

                    //Add a rule to the list
                    rules.Add(new RuleType(true, ruleName, antText, conText));
                }

                //Return list
                return rules;
            }
            private void addAntecedents(string text)
            {
                while (true)
                {
                    //Check for end of text
                    if (text.Trim().Length == 0)
                        break;

                    //Locate inner text
                    int startPos = text.IndexOf("(")+1;
                    int endPos = text.IndexOf(")");
                    string innerText = text.Substring(startPos, endPos - startPos);

                    //Remove piece
                    text = text.Remove(0, endPos + 1);

                    //Add item
                    int posSpace = innerText.IndexOf(" ");
                    string element = innerText.Substring(0, posSpace);
                    string value = innerText.Substring(posSpace + 1);
                    antecedents.Add(new MemoryElementType(true, element, value));                    
                }
            }
            private void addConsequents(string text)
            {
                int cutLength = 0;

                while (true)
                { 
                    //Check for end of text
                    if (text.Trim().Length == 0)
                        break;

                    //Remove prefix stuff
                    cutLength = text.IndexOf("(");
                    text = text.Remove(0, cutLength+1);

                    //Get element name
                    cutLength = text.IndexOf(" ");
                    string element = text.Substring(0, cutLength);
                    text = text.Remove(0, cutLength+1);

                    //Locate inner text
                    int startPos = text.IndexOf("(") + 1;
                    int endPos = text.IndexOf(")");
                    string innerText = text.Substring(startPos, endPos - startPos);
                    text = text.Remove(0, endPos+1);

                    //Remove closing ")"
                    cutLength = text.IndexOf(")");
                    text = text.Remove(0, cutLength + 1);

                    //Add item
                    consequents.Add(new MemoryElementType(true, element, innerText));
                }
            }

            //Debug
            public override string ToString()
            {
                return ruleName + " (" + antecedents.Count + "," + consequents.Count + ") - " + active;
            }

        }
        class TimerType
        {
            //public bool active;
            public int id;
            public int expiration;

            //Constructor
            public TimerType(int id, int expiration)
            {
                this.id = id;
                this.expiration = expiration;
            }
        }

        //Fields - processing
        List<MemoryElementType> workingMemory = new List<MemoryElementType>();
        List<RuleType> ruleSet = new List<RuleType>();
        List<TimerType> timers = new List<TimerType>();
        bool endRun = false;

        //Methods - Processing
        void start()
        {
            int time = 1;
            int timeLength = 100;

            //Read time interval
            try { timeLength = Convert.ToInt32(tbTimeLength.Text); }
            catch { return; }

            while (true)
            {
                //Check if form is still open
                if (this.Visible == false)
                    Thread.CurrentThread.Abort();;

                //Display Time
                string mText = "Time: " + time.ToString() + Environment.NewLine;

                //Process next rule that modifies memory
                RuleType rule = interpret();
                if (rule != null)
                {
                    //Name of rule that fired
                    if (chbShowRulesFiring.Checked)
                    mText += "Fired: " + rule.ruleName + Environment.NewLine;

                    //Current memory   
                    if (chbMemoryChanges.Checked) 
                    mText += workingMemoryToString() + Environment.NewLine;
                }
                else
                {
                    //No rules fired

                    //Show current memory
                    if (!chbVsTime.Checked)
                        if (chbMemoryChanges.Checked)
                            mText += workingMemoryToString() + Environment.NewLine;
                }
                
                //Edit text box
                if (chbVsTime.Checked)
                    tbWorkingMemory.Text += mText;
                else
                    tbWorkingMemory.Text = mText;

                //Adjust timers
                processTimers();

                //If at end, stop 
                if (endRun) break;

                //Wait one second before continuing
                Thread.Sleep(timeLength); time++;
            }
        }
        RuleType interpret()
        {
            //Filter for only active rules
            List<RuleType> rules = ruleSet.FindAll(r => r.active).ToList();
            /*
            //Search for rules that match antecedents   
            List<RuleType> matchingRules = new List<RuleType>();
            foreach (RuleType rule in rules)
            {
                //Check for "true null" antecedent
                if (rule.antecedents.FindAll(a => a.element.ToLower() == "true").Count > 0)
                {
                    matchingRules.Add(rule);
                    continue;
                }

                //Check if all antecedents are true;
                bool addRule = true;
                foreach(MemoryElementType ant in rule.antecedents)
                {
                    //Count how many elements in memory match the antecedent
                    int count = 0;
                    if (ant.element == "?")
                        count = workingMemory.FindAll(m => m.value == ant.value).Count;
                    else if (ant.value == "?")
                        count = workingMemory.FindAll(m => m.element == ant.element).Count;
                    else
                        count = workingMemory.FindAll(m => m.element == ant.element && m.value == ant.value).Count;

                    //If count is zero, then there were no matches. Continue to next rule
                    if (count == 0)
                    { 
                        addRule = false;
                        break;
                    }
                }

                //If all antecedents are found, then add to possibilty list.
                if (addRule)
                    matchingRules.Add(rule);
            }
            */

            //Find first rule in the match list that actually modifies memory.
            foreach (RuleType rule in rules)
            {
                //Check if the rule will affect memory
                if(checkRule(rule))
                {
                    //Fire the rule
                    fireRule(rule);

                    //Return rule that was fired. (for reference)
                    return rule;
                }
            }

            //No rules were fired.
            return null;
        }
        bool checkRule(RuleType rule)
        {
            //check if inactive
            if (rule.active == false)
                return false;

            //Check if memory will be modified
            return fireRule(rule, false);
        }
        void fireRule(RuleType rule)
        {
            fireRule(rule, true);
        }
        bool fireRule(RuleType rule, bool modifyMemory)
        {
            //Assume that it will not modify memory.
            bool memoryWouldBeModified = false;

            #region Check Antecedents

            //Check Antecedents without "?"
            foreach (MemoryElementType ant in rule.antecedents.FindAll(a => a.value != "?").ToList())
            {
                //Check if it passes simple antecedents. If not, it does not affect memory.
                if (searchWorkingMemory(ant.element, ant.value) == false)
                    return false;
            }

            //Get list of antecedents with "?"
            List<MemoryElementType> questionAntecedents = rule.antecedents.FindAll(a => a.value == "?").ToList();

            //Get possible values for "?"
            HashSet<string> possibleQuestionMarkValues = new HashSet<string>();
            foreach (MemoryElementType ant in questionAntecedents)
            {
                //Get memory items with correct element
                foreach(MemoryElementType m in workingMemory.FindAll(m=> m.element == ant.element))               
                    possibleQuestionMarkValues.Add(m.value);        
            }

            //Find which values works for "?"
            string qmValue = null;
            foreach (string posValue in possibleQuestionMarkValues)
            {
                //Assume that it works
                bool works = true;

                //Check all antecedents to see if it does not work
                foreach (MemoryElementType ant in questionAntecedents)
                {
                    //If not found in memory, it does not work, so skip the rest.
                    if (searchWorkingMemory(ant.element, posValue) == false)
                    { 
                        works = false;
                        break;
                    }
                }

                //If it was found by all antecedents, then it works
                if (works)
                { 
                    qmValue = posValue;
                    break;
                }
            }

            //Check if a possible value is needed
            if (questionAntecedents.Count > 0)
            {
                if (qmValue == null)
                {
                    //no possible question mark values work, so it is does not modify memory.
                    return false;
                }
                
            }

            #endregion

            //Process each consequent
            foreach (MemoryElementType consequent in rule.consequents)
            {
                switch (consequent.element)
                {
                    case "add":
                        {
                            //Get index of space
                            int posSpace = consequent.value.IndexOf(" ");
                            //Get components
                            string element = consequent.value.Substring(0, posSpace);
                            string value = consequent.value.Substring(posSpace + 1);

                            //Check if this is a "?" action
                            if (value == "?")
                            {
                                //Replave original value with questionmark value
                                value = qmValue;
                            }
                                                    
                            //Check if it will modify memory. (if it is already in memory, no need to add again.)
                            if (searchWorkingMemory(element, value))
                                break;

                            //Add to memory
                            memoryWouldBeModified = true;
                            if (modifyMemory)
                                workingMemory.Add(new MemoryElementType(true, element, value));
                            
                            break;
                        }

                    case "delete":
                        {
                            //Get index of space
                            int posSpace = consequent.value.IndexOf(" ");
                            //Get components
                            string element = consequent.value.Substring(0, posSpace);
                            string value = consequent.value.Substring(posSpace + 1);

 
                            //Check if this is a "?" action
                            if (value == "?")
                            {
                                //Replave original value with questionmark value
                                value = qmValue;
                            }
                            
                            //Check if it will modify memory
                            if (!searchWorkingMemory(element, value))
                                break; //Not in memory, nothing will happen.

                            //Remove from memory
                            memoryWouldBeModified = true;
                            if (modifyMemory)
                                workingMemory.RemoveAll(m => m.element == element && m.value == value); //Remove specific
                            
                            break;
                        }

                    case "disable":
                        {
                            //Check if already disabled
                            if (rule.active == false)
                                break;
                            //Change to inactive
                            memoryWouldBeModified = true;
                            if (modifyMemory)
                                rule.active = false;
                        }                        
                        break;

                    case "print":
                        break;

                    case "enable": //Enable a timer
                        {
                            //Get ID and expiration time
                            var parts = consequent.value.Split(' ');
                            int timerID = Convert.ToInt32(parts[1]);
                            int expiration = Convert.ToInt32(parts[2]);

                            //Check if timer exists already
                            if (timers.FindAll(t => t.id == timerID).Count > 0)
                                break;

                            //Set timer to active and set time
                            memoryWouldBeModified = true;
                            if (modifyMemory)
                                timers.Add(new TimerType(timerID, expiration));                         
                            break;
                        }

                    case "quit":
                        { 
                            if (modifyMemory)
                                endRun = true;
                            break;
                        }
                }
            }

            //Return if the memory would be modified or not
            return memoryWouldBeModified;
        }
        void processTimers()
        {
            foreach(TimerType timer in timers.ToList())
            {
                //Reduce time
                timer.expiration--;

                //Check if ready to fire
                if (timer.expiration == 0)
                {
                    //Add event to working memory
                    workingMemory.Add(new MemoryElementType(true, "timer-triggered", timer.id.ToString()));

                    //Remove timer
                    timers.Remove(timer);
                }
            }
        }
        bool searchWorkingMemory(string element, string value)
        {
            //Check for true
            if (element == "true")
                return true;

            //Check the count
            if (workingMemory.FindAll(m => m.element == element && m.value == value).Count > 0)
                return true;
            else
                return false;
                
        }

        //Methods - Display
        string rulesToString()
        {
            string text = "";
            foreach(RuleType rule in ruleSet)
            {
                text += "(defrule "+rule.ruleName + Environment.NewLine;

                //Show antecedents
                foreach(MemoryElementType antecedent in rule.antecedents)
                {
                    text += "     " + "("+antecedent.element+" "+antecedent.value+")" + Environment.NewLine;
                }

                //Show consequents
                text += "=>" + Environment.NewLine;
                foreach (MemoryElementType consequent in rule.consequents)
                {
                    text += "     " + "("+consequent.element + " ("+consequent.value+")"+ ")" + Environment.NewLine;
                }

                //Close
                text += ")" + Environment.NewLine;
                text += Environment.NewLine;
            }

            return text;

            //tbRules.Text = text;
            //tbRules.Select(text.Length-1, 1);
            //tbRules.ScrollToCaret();
        }
        string workingMemoryToString()
        {
            string text = "";
            foreach(MemoryElementType m in workingMemory)
            {
                text += m.element+": "+m.value + Environment.NewLine;
            }
            return text;
        }

        //Controls - Manual
        private void btnLoadRules_Click(object sender, EventArgs e)
        {
            //Get file address
            FileDialog fd = new OpenFileDialog()
            {
                Filter = "Rules Database (*.rdb)|*.rdb",
                RestoreDirectory = true
            };
            DialogResult dialogResult = fd.ShowDialog();

            //Check dialog
            if (dialogResult != DialogResult.OK)
                return;

            //Try to parse file
            string fileAddress = fd.FileName;
            if (File.Exists(fileAddress))
            {
                //Update text box
                tbRulesAddress.Text = fileAddress;
                tbRulesAddress.Select(tbRulesAddress.Text.Length - 1, 1);
                tbRulesAddress.ScrollToCaret();

                //Read rules from file
                ruleSet = RuleType.parseFile(fileAddress);

                //Show on form
                tbRules.Text = rulesToString();
            }
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            Thread tProcessing = new Thread (delegate ()
            {
                //process rules
                start();
            });

            tProcessing.Start();
            
        }

        //Controls - Automatic
        private void tbAutoScrollToEnd_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Select(tb.Text.Length - 1, 1);
            tb.ScrollToCaret();
        }
        private void tbRulesAddress_TextChanged(object sender, EventArgs e)
        {
            if (tbRulesAddress.Text == "")
                btnProcess.Enabled = false;
            else
                btnProcess.Enabled = true;
        } 
    }
}
