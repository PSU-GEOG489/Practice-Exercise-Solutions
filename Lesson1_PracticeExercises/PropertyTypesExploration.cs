﻿
namespace Lesson1_PracticeExercises
{
    public class TestExternalClass
    {
        public TestExternalClass()
        {
            //set a hook to a separate class (which happens to be within the same .cs file container in this case)
            PropertyTypesExploration myPropertyExplorationClass = new PropertyTypesExploration();

            //These are all ways to GET public properties or fields of the external class "PropertyTypesExploration"
            string myString = myPropertyExplorationClass.mySimpleStringProperty;
            double myDouble = myPropertyExplorationClass.mySimpleDoubleProperty;
            string myOtherString = myPropertyExplorationClass.myCustomStringProperty;
            string myLastString = myPropertyExplorationClass.myStringField;

            //These are all ways to SET public properties of fields of the external class
            myPropertyExplorationClass.mySimpleStringProperty = myString;
            myPropertyExplorationClass.mySimpleDoubleProperty = myDouble;
            myPropertyExplorationClass.myCustomStringProperty = myOtherString;
            myPropertyExplorationClass.myStringField = myLastString;
        }
    }

    public class PropertyTypesExploration
    {
        //reference information: 
        // http://net-informations.com/faq/netfaq/field.htm
        // http://stackoverflow.com/questions/4142867/what-is-difference-between-property-and-variable-in-c-sharp


        public string mySimpleStringProperty { get; set; } //string property
        public double mySimpleDoubleProperty { get; set; } //double property

        private string _myCustomStringProperty; //private "backing" field for the "myCustomStringProperty. this won't be seen outside of this class
        public string myCustomStringProperty //string property "wrapper" for the private field with expanded getter and setter functions
        {
            get
            {
                return _myCustomStringProperty;
            }
            set
            {
                _myCustomStringProperty = value;
            }
        }

        public string myStringField; //public class-level string field - This is frowned upon and should be avoided in favor of private fields and public property "wrappers"

        private void MethodToCall()
        {
            //Setting a local string variable to a string literal
            string myLocalStringVariable = "some text here";
            
            //These are all valid ways to GET the values that may be held in a class level field or property
            myLocalStringVariable = myStringField; //public field
            myLocalStringVariable = myCustomStringProperty; //public property
            myLocalStringVariable = _myCustomStringProperty; //private field
            myLocalStringVariable = mySimpleStringProperty; //public property
            myLocalStringVariable = mySimpleDoubleProperty.ToString(); //public property

            //These are all valid ways to SET the class level fields or properties
            myStringField = myLocalStringVariable;
            myCustomStringProperty = myLocalStringVariable;
            _myCustomStringProperty = myLocalStringVariable;
            mySimpleStringProperty = myLocalStringVariable;

            //These are methods of setting the numeric double type properties
            //This will fail because the string value can't be parsed to a double. 
            //This _would_ work if you could guarantee that the string could successfully be parsed as a number...
            //Let's just comment it out unless you want to see for yourself...
            //mySimpleDoubleProperty = double.Parse(myLocalStringVariable); 

            //This will not fail, but will pass a zero value to the simpleDoubleProperty
            double myLocalDoubleVariable;
            double.TryParse(myLocalStringVariable, out myLocalDoubleVariable); //the "out" keyword is required when using the "TryParse" method..
            mySimpleDoubleProperty = myLocalDoubleVariable;
        }
    }
}
