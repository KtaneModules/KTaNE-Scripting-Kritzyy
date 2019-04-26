using UnityEngine;
using KModkit;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class KritScript : MonoBehaviour
{
    public KMSelectable RunBtn, Using1Selectable, Using2Selectable, Using3Selectable;
    public KMSelectable VarBtn, MethodBtn;
    public KMSelectable ActionBtn;

    public GameObject VariableObj;
    public GameObject ValueObj;
    public GameObject ScriptLines;

    public GameObject StatusCorrect, StatusIncorrect;

    public TextMesh UsingProgram1, UsingProgram2, UsingProgram3;
    public TextMesh Using1, Using2, Using3;
    public TextMesh Variable, VarName;
    public TextMesh Method, MethodTypeTxtMsh;
    public TextMesh ReturnStatement;
    public TextMesh VariableTxtMsh, VariableValueTxtMsh;
    public TextMesh Condition;
    public TextMesh ActionTxtMsh;

    public KMBombInfo BombInfo;

    GUIStyle style = new GUIStyle();

    List<string> UsingPrograms = new List<string>
    {
        "KTaNE", "KMAPI", "BombGenerator", "ScriptAPI", "System", "UnityEngine", "System.Linq", "EncryptedProgram", "KMMods", "IntGenerator"
    };
    List<string> Variables = new List<string>
    {
        "int", "bool", "float", "char"
    };
    List<string> IntFloatVarNames = new List<string>
    {
        "Fuse", "Timer", "Strikes", "RandomNumber"
    };
    List<string> BoolVarNames = new List<string>
    {
        "Activity", "Encrypted", "AllLit", "Indc"
    };
    List<string> PossibleMethodNames = new List<string>
    {
        "OnSolve()", "Explosion()", "TimerGenerator()", "HandleStrikes()", "OnStrike()", "HandleSolves()", "GenerateEdgework()"
    };
    List<string> PossibleActions = new List<string>
    {
        "HandleSolve();", "HandleStrike();", "Strike();", "Solve();", "OnStrike();", "OnSolve();"
    };
    List<string> Alphabet = new List<string>
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

    string UsingProgram1Value, UsingProgram2Value, UsingProgram3Value;
    string VariableKindValue, VariableNameValue;
    string CharVariableValue;
    string CharLetterGenerated;
    string VariableShouldBe;
    string MethodNameValue;
    string MethodType = "void";
    string MethodTypeShouldBe;
    string BoolConditionString;
    string ActionComponent;
    string Action;
    string ActionShouldBe;
    string SerialNr;

    int UsingProgram1Gen, UsingProgram2Gen, UsingProgram3Gen;
    int VariableGen, VarNameGen;
    int CharGen, CharValGen, CharLetterGenerator;
    int MethodNameGen;
    int MethodNumber = 1;
    int IntVariableValue;
    int BoolConditionValueGen;
    int ActionGen;
    int VariableNumber;
    int ActionNumber;
    int BoolValueGen;
    int IntNumberGenerator;
    int OperatorGen = 0;
    int Batteries, Ports, FirstDigitSerial, LastDigitSerial;
    int SolvedModules;
    int ScriptNr;

    float FloatVariableValue;
    float FloatNumberGenerator;

    static int moduleIdCounter = 1;
    int moduleID;

    bool VennDiagram1, VennDiagram2, VennDiagram3;
    bool IsUsing1Necessary, IsUsing2Necessary, IsUsing3Necessary;
    bool Using1ShouldBeNecessary, Using2ShouldBeNecessary, Using3ShouldBeNecessary;
    bool BoolVariableValue;
    bool BoolConditionValue;
    bool FirstVar = true;
    bool FirstVal = true;
    bool FirstAction = true;

    Vector3 FloatVarPosition = new Vector3(0.2852f, 0.5100001f, -0.08900001f);
    Vector3 BoolVarPosition = new Vector3(0.2992f, 0.5100001f, -0.08900001f);
    Vector3 IntVarPosition = new Vector3(0.3095f, 0.5100001f, -0.08900001f);
    Vector3 CharVarPosition = new Vector3(0.2633f, 0.5100001f, -0.08900001f);
    Vector3 ResetValuePosition = new Vector3(0.205f, 0.5100001f, -0.09799998f);

    private readonly string TwitchHelpMessage = "To set a directive, type !{0} set using1/using2/using3 true/false. To set the variable, type !{0} set var int/float/bool/char (To cycle between variables, type !{0} cyclevar). To set the method, type !{0} set method void/bool. To set the action, type !{0} set action <Action>. Lastly, to run the script, type !{0} run.";

    IEnumerator ProcessTwitchCommand(string Command)
    {
        Command = Command.ToLowerInvariant().Trim();

        if (Command.StartsWith("set"))
        {
            if (Command.Contains("using1"))
            {
                if (Command.EndsWith("true"))
                {
                    //Setting using directive 1 to true
                    if (IsUsing1Necessary)
                    {
                        //Directive 1 is already true. Not setting.
                        yield return "sendtochaterror Directive 1 is already true.";
                    }
                    else
                    {
                        //Directive 1 will be set to true
                        yield return null;
                        yield return new[] { Using1Selectable };
                    }
                }
                else if (Command.EndsWith("false"))
                {
                    //Setting using directive 1 to false
                    if (!IsUsing1Necessary)
                    {
                        //Directive 1 is already false. Not setting.
                        yield return "sendtochaterror Directive 1 is already false.";
                    }
                    else
                    {
                        //Directive 1 will be set to false
                        yield return null;
                        yield return new[] { Using1Selectable };
                    }
                }
                else if (Command.EndsWith("using1"))
                {
                    //Throwing up an error, because the order of the command was incorrect.
                    yield return "sendtochaterror Incorrect order. First place the using directive, then the condition for it.";
                }
                else
                {
                    //Throwing up an error, because no condition was given.
                    yield return "sendtochaterror No condition was given. You need to define what the using directive's condition should be.";
                }
            }
            else if (Command.Contains("using2"))
            {
                if (Command.EndsWith("true"))
                {
                    //Setting using directive 2 to true
                    if (IsUsing2Necessary)
                    {
                        //Directive 2 is already true. Not setting.
                        yield return "sendtochaterror Directive 2 is already true.";
                    }
                    else
                    {
                        //Directive 2 will be set to true.
                        yield return null;
                        yield return new[] { Using2Selectable };
                    }
                }
                else if (Command.EndsWith("false"))
                {
                    //Setting using directive 2 to false
                    if (!IsUsing2Necessary)
                    {
                        //Directive 2 is already false. Not setting.
                        yield return "sendtochaterror Directive 2 is already false.";
                    }
                    else
                    {
                        //Directive 2 will be set to false.
                        yield return null;
                        yield return new[] { Using2Selectable };
                    }
                }
                else if (Command.EndsWith("using2"))
                {
                    //Throwing up an error, because the order of the command was incorrect.
                    yield return "sendtochaterror Incorrect order. First place the using directive, then the condition for it.";
                }
                else
                {
                    //Throwing up an error, because no condition was given.
                    yield return "sendtochaterror No condition was given. You need to define what the using directive's condition should be.";
                }
            }
            else if (Command.Contains("using3"))
            {
                if (Command.EndsWith("true"))
                {
                    //Setting using directive 3 to true
                    if (IsUsing3Necessary)
                    {
                        //Directive 2 is already true. Not setting.
                        yield return "sendtochaterror Directive 3 is already true.";
                    }
                    else
                    {
                        //Directive 3 will be set to true.
                        yield return null;
                        yield return new[] { Using3Selectable };
                    }
                }
                else if (Command.EndsWith("false"))
                {
                    //Setting using directive 3 to false
                    if (!IsUsing3Necessary)
                    {
                        //Directive 3 is already false. Not setting.
                        yield return "sendtochaterror Directive 2 is already true.";
                    }
                    else
                    {
                        //Directive 3 will be set to true.
                        yield return null;
                        yield return new[] { Using3Selectable };
                    }
                }
                else if (Command.EndsWith("using3"))
                {
                    //Throwing up an error, because the order of the command was incorrect.
                    yield return "sendtochaterror Incorrect order. First place the using directive, then the condition for it.";
                }
                else
                {
                    //Throwing up an error, because no condition was given.
                    yield return "sendtochaterror No condition was given. You need to define what the using directive's condition should be.";
                }
            }
            else if (Command.Contains("var"))
            {
                //Changing the variable
                if (Command.EndsWith("int"))
                {
                    //Changing to int
                    VariableNumber = 0;
                    yield return null;
                    yield return new[] { VarBtn };
                }
                else if (Command.EndsWith("float"))
                {
                    //Changing to float
                    VariableNumber = 1;
                    yield return null;
                    yield return new[] { VarBtn };
                }
                else if (Command.EndsWith("bool"))
                {
                    //changing to bool
                    VariableNumber = 2;
                    yield return null;
                    yield return new[] { VarBtn };
                }
                else if (Command.EndsWith("char"))
                {
                    //Changing to char
                    VariableNumber = 3;
                    yield return null;
                    yield return new[] { VarBtn };
                }
                else
                {
                    //Throwing up an error, because no variable was given
                    yield return "sendtochaterror Couldn't change the variable because none were given, or it wasn't valid.";
                }
            }
            else if (Command.Contains("method"))
            {
                if (Command.EndsWith("void"))
                {
                    MethodNumber = 0;
                    yield return null;
                    yield return new[] { MethodBtn };
                }
                if (Command.EndsWith("bool"))
                {
                    MethodNumber = 1;
                    yield return null;
                    yield return new[] { MethodBtn };
                }
                else
                {
                    yield return "sendtochaterror Couldn't change the method because no valid methods were given.";
                    //throwing up an error, because no (valid) method type was given.
                }
            }
            else if (Command.Contains("action"))
            {
                //Changing the action
                if (Command.EndsWith("handlesolve()"))
                {
                    ActionNumber = 0;
                    yield return null;
                    yield return new[] { ActionBtn };
                }
                else if (Command.EndsWith("handlestrike()"))
                {
                    ActionNumber = 1;
                    yield return null;
                    yield return new[] { ActionBtn };
                }
                else if (Command.EndsWith("onsolve()"))
                {
                    ActionNumber = 4;
                    yield return null;
                    yield return new[] { ActionBtn };
                }
                else if (Command.EndsWith("onstrike()"))
                {
                    ActionNumber = 5;
                    yield return null;
                    yield return new[] { ActionBtn };
                }
                else if (Command.EndsWith("solve()"))
                {
                    ActionNumber = 2;
                    yield return null;
                    yield return new[] { ActionBtn };
                }
                else if (Command.EndsWith("strike()"))
                {
                    ActionNumber = 3;
                    yield return null;
                    yield return new[] { ActionBtn };
                }
                else
                {
                    yield return "sendtochaterror Couldn't change the action because none were given, the given action was invalid or the valid action didn't have brackets. Valid options are HandleSolve(), HandleStrike(), Solve(), Strike(), OnSolve() and OnStrike().";
                    //throwing up an error, because no (valid) action was given.
                }
            }
            else
            {
                //Throwing up an error, because no directive, variable, or action was given
                yield return "sendtochaterror No actions were given. You need to define if a directive, variable or action should be changed.";
            }
        }
        else  if (Command.Equals("cyclevar"))
        {
            yield return null;
            yield return new[] { VarBtn };
            yield return new WaitForSecondsRealtime(1.5f);
            yield return null;
            yield return new[] { VarBtn };
            yield return new WaitForSecondsRealtime(1.5f);
            yield return null;
            yield return new[] { VarBtn };
            yield return new WaitForSecondsRealtime(1.5f);
            yield return null;
            yield return new[] { VarBtn };
        }
        else if (Command.StartsWith("run"))
        {
            yield return null;
            yield return new[] { RunBtn };
        }
        else
        {
            yield return "sendtochaterror The command \"" + Command + "\" does not exist in the current context.";
        }
    }

    void Awake()
    {
        style.richText = true;
        moduleID = moduleIdCounter++;
        Using1Selectable.OnInteract = ChangeUsing1;
        Using2Selectable.OnInteract = ChangeUsing2;
        Using3Selectable.OnInteract = ChangeUsing3;
        VarBtn.OnInteract = VariableChange;
        MethodBtn.OnInteract = ChangeMethodType;
        ActionBtn.OnInteract = ChangeAction;
        RunBtn.OnInteract = RunScript;
        ScriptNr = 1;
    }
    void Start()
    {
        UsingPrograms.Clear();
        UsingPrograms.Add("KTaNE");
        UsingPrograms.Add("KMAPI");
        UsingPrograms.Add("BombGenerator");
        UsingPrograms.Add("ScriptAPI");
        UsingPrograms.Add("System");
        UsingPrograms.Add("UnityEngine");
        UsingPrograms.Add("System.Linq");
        UsingPrograms.Add("EncryptedProgram");
        UsingPrograms.Add("KMMods");
        UsingPrograms.Add("IntGenerator");

        IsUsing1Necessary = true;
        IsUsing2Necessary = true;
        IsUsing3Necessary = true;

        UsingProgram1.color = new Color32(173, 173, 173, 255);
        Using1.color = new Color32(34, 123, 156, 255);
        UsingProgram2.color = new Color32(173, 173, 173, 255);
        Using2.color = new Color32(34, 123, 156, 255);
        UsingProgram3.color = new Color32(173, 173, 173, 255);
        Using3.color = new Color32(34, 123, 156, 255);

        VariableObj.transform.localPosition = FloatVarPosition;
        ValueObj.transform.localPosition = ResetValuePosition;

        //Generating the script...
        UsingProgram1Gen = Random.Range(0, 10);
        UsingProgram1Value = UsingPrograms[UsingProgram1Gen];
        UsingPrograms.Remove(UsingProgram1Value);

        UsingProgram2Gen = Random.Range(0, 9);
        UsingProgram2Value = UsingPrograms[UsingProgram2Gen];
        UsingPrograms.Remove(UsingProgram2Value);

        UsingProgram3Gen = Random.Range(0, 8);
        UsingProgram3Value = UsingPrograms[UsingProgram3Gen];
        UsingPrograms.Remove(UsingProgram3Value);

        VariableGen = Random.Range(0, 4);
        VariableKindValue = Variables[VariableGen];


        if (VariableKindValue == "int" || VariableKindValue == "float")
        {
            VarNameGen = Random.Range(0, 4);
            VariableNameValue = IntFloatVarNames[VarNameGen];
            if (VariableKindValue == "int")
                VariableObj.transform.localPosition = new Vector3(0.315f, 0.5100001f, -0.089f);
        }
        else if (VariableKindValue == "bool")
        {
            VarNameGen = Random.Range(0, 4);
            VariableNameValue = BoolVarNames[VarNameGen];
            if (VariableNameValue == "IsLit")
            {
                VariableObj.transform.localPosition = new Vector3(0.2995f, 0.5100001f, -0.0913f);
            }
        }
        else if (VariableKindValue  == "char")
        {
            CharGen = Random.Range(0, 26);
            VariableNameValue = Alphabet[CharGen];
            Alphabet.Remove(VariableNameValue);
        }


        UsingProgram1.text = UsingProgram1Value + ";";
        UsingProgram2.text = UsingProgram2Value + ";";
        UsingProgram3.text = UsingProgram3Value + ";";
        Variable.text = VariableKindValue;
        VarName.text = VariableNameValue + ";";

        UsingNecessityGen();
    }

    void UsingNecessityGen()
    {
        //Checking which using directive is "unnecessary"
        int UsingNecessaryPicker, IsNecessary;
        UsingNecessaryPicker = Random.Range(0, 4);
        IsNecessary = Random.Range(0, 4);

        if (UsingNecessaryPicker == 0)
        {
            switch (IsNecessary)
            {
                case 1:
                    IsUsing1Necessary = false;
                    break;
                default:
                    IsUsing1Necessary = true;
                    break;
            }
        }
        else if (UsingNecessaryPicker == 1)
        {
            switch (IsNecessary)
            {
                case 1:
                    IsUsing2Necessary = false;
                    break;
                default:
                    IsUsing2Necessary = true;
                    break;
            }
        }
        else if (UsingNecessaryPicker == 2)
        {
            switch (IsNecessary)
            {
                case 1:
                    IsUsing3Necessary = false;
                    break;
                default:
                    IsUsing3Necessary = true;
                    break;
            }
        }

        if (!IsUsing1Necessary)
        {
            UsingProgram1.color = new Color32(173, 173, 173, 125);
            Using1.color = new Color32(34, 123, 156, 155);
        }
        else if (!IsUsing2Necessary)
        {
            UsingProgram2.color = new Color32(173, 173, 173, 125);
            Using2.color = new Color32(34, 123, 156, 155);
        }
        else if (!IsUsing3Necessary)
        {
            UsingProgram3.color = new Color32(173, 173, 173, 125);
            Using3.color = new Color32(34, 123, 156, 155);
        }

        MethodNaneGenerator();
    }

    void MethodNaneGenerator()
    {
        MethodNameGen = Random.Range(0, 7);
        MethodNameValue = PossibleMethodNames[MethodNameGen];
        Method.text = MethodNameValue;

        VariableValueGen();
    }

    void VariableValueGen()
    {
        if (VariableKindValue == "int")
        {
            VariableNumber = 1;
            if (VariableNameValue == "Strikes")
            {
                IntVariableValue = Random.Range(1, 6);
            }
            else if (VariableNameValue == "RandomNumber")
            {
                ValueObj.transform.localPosition = new Vector3(0.1685f, 0.5100001f, -0.09799998f);
                IntVariableValue = Random.Range(1, 11);
            }
            else if (VariableNameValue == "Timer")
            {
                ValueObj.transform.localPosition = new Vector3(0.246f, 0.51f, -0.0971f);
                IntVariableValue = Random.Range(1, 11);
            }
            else if (VariableNameValue == "Fuse")
            {
                ValueObj.transform.localPosition = new Vector3(0.246f, 0.51f, -0.0971f);
                IntVariableValue = Random.Range(1, 11);
            }
            else
            {
                IntVariableValue = Random.Range(1, 11);
            }
            VariableTxtMsh.text = VariableNameValue + " = ";
            VariableValueTxtMsh.text = IntVariableValue + ";";

            FloatVariableValue = Random.Range(1f, 50f);
            BoolValueGen = Random.Range(0, 2);
            switch (BoolValueGen)
            {
                case 1:
                    {
                        BoolVariableValue = true;
                        break;
                    }
                default:
                    {
                        BoolVariableValue = false;
                        break;
                    }
            }
            CharValGen = Random.Range(0, 25);
            CharVariableValue = Alphabet[CharValGen];

        }
        else if (VariableKindValue == "float")
        {
            VariableNumber = 2;
            if (VariableNameValue == "Strikes")
            {
                FloatVariableValue = Random.Range(1, 6);
            }
            else if (VariableNameValue == "RandomNumber")
            {
                ValueObj.transform.localPosition = new Vector3(0.1685f, 0.5100001f, -0.09799998f);
                FloatVariableValue = Random.Range(1f, 10f);
            }
            else if (VariableNameValue == "Timer")
            {
                ValueObj.transform.localPosition = new Vector3(0.246f, 0.51f, -0.0971f);
                FloatVariableValue = Random.Range(1f, 10f);
            }
            else if (VariableNameValue == "Fuse")
            {
                ValueObj.transform.localPosition = new Vector3(0.246f, 0.51f, -0.0971f);
                FloatVariableValue = Random.Range(1f, 10f);
            }
            else
            {
                FloatVariableValue = Random.Range(1f, 10f);
            }
            VariableTxtMsh.text = VariableNameValue + " = ";
            VariableValueTxtMsh.text = FloatVariableValue + ";";

            IntVariableValue = Random.Range(1, 51);
            BoolValueGen = Random.Range(0, 2);
            switch (BoolValueGen)
            {
                case 1:
                    {
                        BoolVariableValue = true;
                        break;
                    }
                default:
                    {
                        BoolVariableValue = false;
                        break;
                    }
            }
            CharValGen = Random.Range(0, 25);
            CharVariableValue = Alphabet[CharValGen];
        }
        else if (VariableKindValue == "bool")
        {
            VariableNumber = 3;
            BoolValueGen = Random.Range(0, 2);
            switch (BoolValueGen)
            {
                case 1:
                    {
                        BoolVariableValue = true;
                        break;
                    }
                default:
                    {
                        BoolVariableValue = false;
                        break;
                    }
            }
            if (VariableNameValue == "IsLit")
            {
                ValueObj.transform.localPosition = new Vector3(0.2514f, 0.5100001f, -0.09799998f);
            }
            VariableTxtMsh.text = VariableNameValue + " = ";
            VariableValueTxtMsh.color = new Color32(34, 123, 156, 255);
            VariableValueTxtMsh.text = BoolVariableValue.ToString().ToLowerInvariant() + "<color=#ADADAD>;</color>";

            IntVariableValue = Random.Range(1, 51);
            FloatVariableValue = Random.Range(1f, 50f);
            CharValGen = Random.Range(0, 25);
            CharVariableValue = Alphabet[CharValGen];
        }
        else if (VariableKindValue == "char")
        {
            VariableNumber = 4;
            CharValGen = Random.Range(0, 25);
            CharVariableValue = Alphabet[CharValGen];

            VariableTxtMsh.text = VariableNameValue + " = ";
            ValueObj.transform.localPosition = new Vector3(0.3022f, 0.5100001f, -0.09799998f);
            VariableValueTxtMsh.color = new Color32(205, 147, 118, 255);
            VariableValueTxtMsh.text = "\"" + CharVariableValue.ToString() + "\"" + "<color=#ADADAD>;</color>";

            IntVariableValue = Random.Range(1, 51);
            FloatVariableValue = Random.Range(1f, 50f);
            BoolValueGen = Random.Range(0, 2);
            switch (BoolValueGen)
            {
                case 1:
                    {
                        BoolVariableValue = true;
                        break;
                    }
                default:
                    {
                        BoolVariableValue = false;
                        break;
                    }
            }
        }
        VariableValues();
    }

    void VariableValues()
    {
        if (FirstVar)
        {
            IntNumberGenerator = Random.Range(0, 11);
            FloatNumberGenerator = Random.Range(0f, 10f);
            BoolValueGen = Random.Range(0, 2);
            switch (BoolValueGen)
            {
                case 1:
                    {
                        BoolVariableValue = true;
                        break;
                    }
                default:
                    {
                        BoolVariableValue = false;
                        break;
                    }
            }
            CharLetterGenerator = Random.Range(0, 25);
            CharLetterGenerated = Alphabet[CharLetterGenerator];
            IfStatement();
        }
    }

    void IfStatement()
    {
        string Operator;

        if (VariableKindValue == "bool")
        {
            FirstVar = false;
            switch (BoolConditionValueGen)
            {
                case 0:
                    {
                        BoolConditionValue = false;
                        BoolConditionString = "!";
                        break;
                    }
                default:
                    {
                        BoolConditionValue = true;
                        BoolConditionString = "";
                        break;
                    }

            }
            Condition.text = "(" + BoolConditionString + VariableNameValue + ")";
        }
        else if (VariableKindValue == "int")
        {
            Operator = "==";
            if (FirstVar)
            {
                FirstVar = false;
                OperatorGen = Random.Range(0, 3);
            }
            switch (OperatorGen)
            {
                case 1:
                    {
                        Operator = ">";
                        break;
                    }
                case 2:
                    {
                        Operator = "<";
                        break;
                    }
                default:
                    {
                        Operator = "==";
                        break;
                    }
            }
            Condition.text = "(" + VariableNameValue + " " + Operator + " " + IntNumberGenerator + ")";
        }
        else if (VariableKindValue == "float")
        {
            Operator = "==";
            if (FirstVar)
            {
                FirstVar = false;
                OperatorGen = Random.Range(0, 3);
            }
            switch (OperatorGen)
            {
                case 1:
                    {
                        Operator = ">";
                        break;
                    }
                case 2:
                    {
                        Operator = "<";
                        break;
                    }
                default:
                    {
                        Operator = "==";
                        break;
                    }
            }
            Condition.text = "(" + VariableNameValue + " " + Operator + " " + FloatNumberGenerator + ")";

        }
        else if (VariableKindValue == "char")
        {
            Operator = "==";
            Condition.text = "(" + VariableNameValue + " " + Operator + " " + "<color=#CD9376FF>\"" + CharLetterGenerated + "\"</color>" + ")";
        }

        if (FirstAction)
            ActionGenerator();
    }

    IEnumerator StatusIncorrectShow()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                ScriptLines.SetActive(false);
                StatusIncorrect.SetActive(true);
            }
            if (i == 2)
            {
                StatusIncorrect.SetActive(false);
                ScriptLines.SetActive(true);
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }

    void CalculateAnswers()
    {
        //Calculating the answer...
        Batteries = BombInfo.GetBatteryCount();
        Ports = BombInfo.GetPortCount();
        LastDigitSerial = BombInfo.GetSerialNumberNumbers().Last();

        //First of all: Using directives.
        if (BombInfo.GetOnIndicators().Count() > BombInfo.GetOffIndicators().Count())
        {
            VennDiagram1 = true;
        }
        if (BombInfo.IsIndicatorPresent(Indicator.SND) || BombInfo.IsIndicatorPresent(Indicator.TRN) || BombInfo.IsIndicatorPresent(Indicator.CLR))
        {
            VennDiagram2 = true;
        }
        if (BombInfo.IsIndicatorOn(Indicator.FRQ) || BombInfo.IsIndicatorOn(Indicator.SIG) || BombInfo.IsIndicatorOn(Indicator.BOB))
        {
            VennDiagram3 = true;
        }

        if (VennDiagram1)
        {
            //Blue was true.
            Debug.LogFormat("[Scripting #{0}] Diagram 1 is correct.", moduleID);
            if (VennDiagram2)
            {
                //Red was also true.
                Debug.LogFormat("[Scripting #{0}] Diagram 2 is correct.", moduleID);
                if (VennDiagram3)
                {
                    //All are true.
                    Debug.LogFormat("[Scripting #{0}] Diagram 3 is correct.", moduleID);
                    //All are unnecessary.
                    Using1ShouldBeNecessary = false;
                    Using2ShouldBeNecessary = false;
                    Using3ShouldBeNecessary = false;
                    Debug.LogFormat("[Scripting #{0}] All using directives should be unnecessary.", moduleID);
                }
                else
                {
                    //Green wasn't true, but the others were.
                    Debug.LogFormat("[Scripting #{0}] Diagram 3 is incorrect.", moduleID);
                    //Exception time...
                    if (BombInfo.GetBatteryCount() % 2 == 0)
                    {
                        //The amount of batteries modulo 2 was 0
                        //Both 1 and 2 are unnecessary
                        Using1ShouldBeNecessary = false;
                        Using2ShouldBeNecessary = false;
                        Using3ShouldBeNecessary = true;
                        Debug.LogFormat("[Scripting #{0}] Using directives 1 and 2 should be unnecessary.", moduleID);
                    }
                    else
                    {
                        //The amount of batteries modulo 2 wasn't 0
                        //None are unnecessary.
                        Using1ShouldBeNecessary = true;
                        Using2ShouldBeNecessary = true;
                        Using3ShouldBeNecessary = true;
                        Debug.LogFormat("[Scripting #{0}] None of the using directives are unnecessary.", moduleID);
                    }
                }
            }
            else if (VennDiagram3)
            {
                //Both blue and green were true. Red wasn't.
                Debug.LogFormat("[Scripting #{0}] Diagram 2 is correct.", moduleID);
                Debug.LogFormat("[Scripting #{0}] Diagram 3 is correct.", moduleID);
                //Exception time...
                if (BombInfo.GetSerialNumberNumbers().Last() >= 5)
                {
                    //The last serial number digit was higher than 5
                    //Only 3 is unnecessary
                    Using1ShouldBeNecessary = true;
                    Using2ShouldBeNecessary = true;
                    Using3ShouldBeNecessary = false;
                    Debug.LogFormat("[Scripting #{0}] Using directive 3 should be unnecessary.", moduleID);
                }
                else
                {
                    //The last serial number digit wasn't higher than 5
                    //None are unnecessary
                    Using1ShouldBeNecessary = true;
                    Using2ShouldBeNecessary = true;
                    Using3ShouldBeNecessary = true;
                    Debug.LogFormat("[Scripting #{0}] None of the using directives are unnecessary.", moduleID);
                }
            }
            else
            {
                //Only blue was true 
                Debug.LogFormat("[Scripting #{0}] Only diagram 1 is correct.", moduleID);
                //Only 1 is unnecessary
                Using1ShouldBeNecessary = false;
                Using2ShouldBeNecessary = true;
                Using3ShouldBeNecessary = true;
                Debug.LogFormat("[Scripting #{0}] Using directive 1 should be unnecessary.", moduleID);
            }
        }
        else if (VennDiagram2)
        {
            //Red was correct. Blue wasn't
            Debug.LogFormat("[Scripting #{0}] Diagram 1 is incorrect.", moduleID);
            if (VennDiagram3)
            {
                //Green and red were correct.
                Debug.LogFormat("[Scripting #{0}] Diagram 2 is correct.", moduleID);
                Debug.LogFormat("[Scripting #{0}] Diagram 3 is correct.", moduleID);
                //Exception time...
                if (BombInfo.GetIndicators().Count() > BombInfo.GetSerialNumberNumbers().Last())
                {
                    //The amount of indicators was more than the last digit of the serial
                    //Both 2 and 3 are unnecessary.
                    Using1ShouldBeNecessary = true;
                    Using2ShouldBeNecessary = false;
                    Using3ShouldBeNecessary = false;
                    Debug.LogFormat("[Scripting #{0}] Using directives 2 and 3 should be unnecessary.", moduleID);
                }
                else
                {
                    //The amount of indicators wasn't more than the last digit of the serial
                    //None are unnecessary.
                    Using1ShouldBeNecessary = true;
                    Using2ShouldBeNecessary = true;
                    Using3ShouldBeNecessary = true;
                    Debug.LogFormat("[Scripting #{0}] None of the using directives are unnecessary.", moduleID);
                }
            }
            else
            {
                //Only red was correct.
                Debug.LogFormat("[Scripting #{0}] Only diagram 2 is correct.", moduleID);
                //Only 2 is unnecessary.
                Using1ShouldBeNecessary = true;
                Using2ShouldBeNecessary = false;
                Using3ShouldBeNecessary = true;
                Debug.LogFormat("[Scripting #{0}] Using directive 2 should be unnecessary.", moduleID);
            }
        }
        else if (VennDiagram3)
        {
            //Only green was true
            Debug.LogFormat("[Scripting #{0}] Only diagram 3 is correct.", moduleID);
            //Only 3 is unnecessary.
            Using1ShouldBeNecessary = true;
            Using2ShouldBeNecessary = true;
            Using3ShouldBeNecessary = false;
            Debug.LogFormat("[Scripting #{0}] Using directive 3 should be unnecessary.", moduleID);
        }
        else
        {
            //None were true
            Debug.LogFormat("[Scripting #{0}] None of the diagrams are correct.", moduleID);
            Using1ShouldBeNecessary = true;
            Using2ShouldBeNecessary = true;
            Using3ShouldBeNecessary = true;
            Debug.LogFormat("[Scripting #{0}] No using directives are unnecessary.", moduleID);
        }

        //Second of all: Variables.
        if (IntVariableValue < LastDigitSerial)
        {
            VariableShouldBe = "int";
            Debug.LogFormat("[Scripting #{0}] Int value is lower than the last digit of the serial number.", moduleID);
        }
        else if (FloatVariableValue < Batteries)
        {
            VariableShouldBe = "float";
            Debug.LogFormat("[Scripting #{0}] Float value is lower than the amount of batteries", moduleID);
        }
        else if (BoolConditionValue == BoolVariableValue)
        {
            VariableShouldBe = "bool";
            Debug.LogFormat("[Scripting #{0}] Condition matches the boolean value", moduleID);
        }
        else
        {
            Debug.LogFormat("[Scripting #{0}] Otherwise rule.", moduleID);
            VariableShouldBe = "char";
        }
        Debug.LogFormat("[Scripting #{0}] Variable should be {1}.", moduleID, VariableShouldBe);

        //Third of all: Method types.
        //Check Coroutine CheckSolvedModules.
        

        //Lastly: Actions.
        if (BombInfo.GetSerialNumberLetters().Any("KTANE".Contains))
        {
            if (UsingProgram1Value == "KTaNE" || UsingProgram2Value == "KTaNE" || UsingProgram3Value == "KTaNE")
            {
                ActionShouldBe = "HandleSolve();";
            }
            else
            {
                ActionShouldBe = "HandleStrike();";
            }
        }
        else if (BombInfo.GetSerialNumberLetters().Any("AEIOU".Contains))
        {
            if (UsingProgram1Value == "KTaNE" || UsingProgram2Value == "KTaNE" || UsingProgram3Value == "KTaNE")
            {
                ActionShouldBe = "Solve();";
            }
            else
            {
                ActionShouldBe = "Strike();";
            }
        }
        else
        {
            if (UsingProgram1Value == "KTaNE" || UsingProgram2Value == "KTaNE" || UsingProgram3Value == "KTaNE")
            {
                ActionShouldBe = "OnSolve();";
            }
            else
            {
                ActionShouldBe = "OnStrike();";
            }
        }
        Debug.LogFormat("[Scripting #{0}] Action should be {1}", moduleID, ActionShouldBe);

        ActionGenerator();
    }

    void ActionGenerator()
    {
        ActionGen = Random.Range(0, 6);
        Action = PossibleActions[ActionGen];
        switch (Action)
        {
            case "HandleSolve();":
                {
                    ActionNumber = 1;
                    break;
                }
            case "HandleStrike();":
                {
                    ActionNumber = 2;
                    break;
                }
            case "Solve();":
                {
                    ActionNumber = 3;
                    break;
                }
            case "Strike();":
                {
                    ActionNumber = 4;
                    break;
                }
            case "OnSolve();":
                {
                    ActionNumber = 5;
                    break;
                }
            case "OnStrike();":
                {
                    ActionNumber = 6;
                    break;
                }
        }

        ActionTxtMsh.text = Action;
        if (FirstAction)
        { 
            FirstAction = false;
            CalculateAnswers();
        }
    }

    void CheckScript()
    {
        //Checking the using directives
        Debug.LogFormat("[Scripting #{0}] Submitted: Dir. 1: {1}, dir. 2: {2}, dir. 3: {3}", moduleID, IsUsing1Necessary.ToString().ToLowerInvariant(), IsUsing2Necessary.ToString().ToLowerInvariant(), IsUsing3Necessary.ToString().ToLowerInvariant());
        if (IsUsing1Necessary == Using1ShouldBeNecessary)
        {
            if (IsUsing2Necessary == Using2ShouldBeNecessary)
            {
                if (IsUsing3Necessary == Using3ShouldBeNecessary)
                {
                    Debug.LogFormat("[Scripting #{0}] All directives are correct! Checking variables...", moduleID);
                    CheckVar();
                }
                else
                {
                    Debug.LogFormat("[Scripting #{0}] Using directive 3 is incorrect. Strike handed.", moduleID);
                    GetComponent<KMBombModule>().HandleStrike();
                    StartCoroutine("StatusIncorrectShow");
                }
            }
            else
            {
                Debug.LogFormat("[Scripting #{0}] Using directive 2 is incorrect. Strike handed.", moduleID);
                GetComponent<KMBombModule>().HandleStrike();
                StartCoroutine("StatusIncorrectShow");
            }
        }
        else
        {
            Debug.LogFormat("[Scripting #{0}] Using directive 1 is incorrect. Strike handed.", moduleID);
            GetComponent<KMBombModule>().HandleStrike();
            StartCoroutine("StatusIncorrectShow");
        }
    }

    void CheckVar()
    {
        Debug.LogFormat("[Scripting #{0}] Submitted: Variable {1}", moduleID, VariableKindValue);
        if (VariableKindValue == VariableShouldBe)
        {
            Debug.LogFormat("[Scripting #{0}] Variable is correct! Checking method type...", moduleID);
            CheckMethodType();
        }
        else
        {
            Debug.LogFormat("[Scripting #{0}] Variable is incorrect. Strike handed.", moduleID);
            GetComponent<KMBombModule>().HandleStrike();
            StartCoroutine("StatusIncorrectShow");
        }
    }

    void CheckMethodType()
    {
        Debug.LogFormat("[Scripting #{0}] Submitted: Method {1}", moduleID, MethodType);
        Debug.LogFormat("[Scripting #{0}] Amount of solved modules: {1}", moduleID, SolvedModules);
        SolvedModules = BombInfo.GetSolvedModuleNames().Count() % 2;
        if (SolvedModules % 2 == 0)
        {
            MethodTypeShouldBe = "void";
        }
        else
        {
            MethodTypeShouldBe = "bool";
        }
        if (MethodType == MethodTypeShouldBe)
        {
            Debug.LogFormat("[Scripting #{0}] Method type is correct! Checking action...", moduleID);
            CheckAction();
        }
        else
        {
            Debug.LogFormat("[Scripting #{0}] Method type is incorrect. Strike handed.", moduleID);
            GetComponent<KMBombModule>().HandleStrike();
            StartCoroutine("StatusIncorrectShow");
        }

    }

    void CheckAction()
    {
        Debug.LogFormat("[Scripting #{0}] Submitted: Action {1}", moduleID, Action);
        if (Action == ActionShouldBe)
        {
            Debug.LogFormat("[Scripting #{0}] Action is correct! Module Passed.", moduleID);
            GetComponent<KMBombModule>().HandlePass();
            DeactivateModule();
        }
        else
        {
            Debug.LogFormat("[Scripting #{0}] Action is incorrect. Strike handed.", moduleID);
            GetComponent<KMBombModule>().HandleStrike();
            StartCoroutine("StatusIncorrectShow");
        }
    }

    void DeactivateModule()
    {
        ScriptLines.SetActive(false);
        StatusCorrect.SetActive(true);
        Using1Selectable.OnInteract = DeactivatedScript;
        Using2Selectable.OnInteract = DeactivatedScript;
        Using3Selectable.OnInteract = DeactivatedScript;
        VarBtn.OnInteract = DeactivatedScript;
        ActionBtn.OnInteract = DeactivatedScript;
        RunBtn.OnInteract = DeactivatedButton;
    }

    protected bool ChangeUsing1()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (IsUsing1Necessary)
        {
            IsUsing1Necessary = false;
            UsingProgram1.color = new Color32(173, 173, 173, 125);
            Using1.color = new Color32(34, 123, 156, 155);
        }
        else
        {
            IsUsing1Necessary = true;
            UsingProgram1.color = new Color32(173, 173, 173, 255);
            Using1.color = new Color32(34, 123, 156, 255);
        }
        return false;
    }

    protected bool ChangeUsing2()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (IsUsing2Necessary)
        {

            IsUsing2Necessary = false;
            UsingProgram2.color = new Color32(173, 173, 173, 125);
            Using2.color = new Color32(34, 123, 156, 155);
        }
        else
        {
            IsUsing2Necessary = true;
            UsingProgram2.color = new Color32(173, 173, 173, 255);
            Using2.color = new Color32(34, 123, 156, 255);
        }
        return false;
    }

    protected bool ChangeUsing3()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        if (IsUsing3Necessary)
        {
            IsUsing3Necessary = false;
            UsingProgram3.color = new Color32(173, 173, 173, 125);
            Using3.color = new Color32(34, 123, 156, 155);
        }
        else
        {
            IsUsing3Necessary = true;
            UsingProgram3.color = new Color32(173, 173, 173, 255);
            Using3.color = new Color32(34, 123, 156, 255);
        }
        return false;
    }

    protected bool VariableChange()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        VariableNumber++;
        if (VariableNumber > 4)
            VariableNumber = 1;

        switch (VariableNumber)
        {
            case 1:
                {
                    VariableKindValue = "int";
                    VariableObj.transform.localPosition = IntVarPosition;
                    VariableValueTxtMsh.text = IntVariableValue.ToString();
                    VariableValueTxtMsh.color = new Color32(173, 173, 173, 255);
                    break;
                }
            case 2:
                {
                    VariableKindValue = "float";
                    VariableObj.transform.localPosition = FloatVarPosition;
                    VariableValueTxtMsh.text = FloatVariableValue.ToString();
                    VariableValueTxtMsh.color = new Color32(173, 173, 173, 255);
                    break;
                }
            case 3:
                {
                    VariableKindValue = "bool";
                    VariableObj.transform.localPosition = BoolVarPosition;
                    VariableValueTxtMsh.text = BoolVariableValue.ToString().ToLowerInvariant() + "<color=#ADADAD>;</color>";
                    VariableValueTxtMsh.color = new Color32(34, 123, 156, 255);
                    break;
                }
            case 4:
                {
                    VariableKindValue = "char";
                    VariableObj.transform.localPosition = CharVarPosition;
                    VariableValueTxtMsh.text = "\"" + CharVariableValue.ToString() + "\"" + "<color=#ADADAD>;</color>";
                    VariableValueTxtMsh.color = new Color32(205, 147, 118, 255);
                    break;
                }
        }

        IfStatement();
        Variable.text = VariableKindValue;
        return false;
    }

    protected bool ChangeAction()
    {
        ActionNumber++;
        if (ActionNumber > 6)
            ActionNumber = 1;
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        switch (ActionNumber)
        {
            case 1:
                {
                    Action = "HandleSolve();";
                    break;
                }
            case 2:
                {
                    Action = "HandleStrike();";
                    break;
                }
            case 3:
                {
                    Action = "Solve();";
                    break;
                }
            case 4:
                {
                    Action = "Strike();";
                    break;
                }
            case 5:
                {
                    Action = "OnSolve();";
                    break;
                }
            case 6:
                {
                    Action = "OnStrike();";
                    break;
                }
        }
        ActionTxtMsh.text = Action;
        return false;
    }

    protected bool ChangeMethodType()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        MethodNumber++;
        if (MethodNumber > 2)
            MethodNumber = 1;
        switch (MethodNumber)
        {
            case 1:
                {
                    MethodType = "void";
                    ReturnStatement.text = "return<color=#ADADAD>;</color>";
                    break;
                }
            case 2:
                {
                    MethodType = "bool";
                    ReturnStatement.text = "return false<color=#ADADAD>;</color>";
                    break;
                }
        }
        MethodTypeTxtMsh.text = MethodType;
        return false;
    }

    protected bool RunScript()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        CheckScript();
        return false;
    }

    protected bool DeactivatedScript()
    {
        return false;
    }

    protected bool DeactivatedButton()
    {
        GetComponent<KMSelectable>().AddInteractionPunch();
        return false;
    }
}