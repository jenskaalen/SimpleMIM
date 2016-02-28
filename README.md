# SimpleMIM #

SimpleMIM is a helper framework for defining flow and provision rules without using compiled code. 
The logic can at the time only written using python and at the writing moment only supports one-liners (this will be changed). 

### Why would I use this? ###
You can use this framework if you value configuration over performance, and the problem you are trying to solve is somewhat sane. 
For big organizations with very complex logic in their MIM you might not be able to reach your goals with SimpleMIM. It lies in the name - this is better for simple scenarios.  
Be mindful of that designing your MIM provisioning and flows to be as simple as possible is preferable in any case.  

### Performance ###
I'm not too sure yet. Yes, interpreted code is slow, but MIM is inherently slow so the overhead might not be too noticeable. 
The python runspace and all the script functions are loaded into memory at initialization so when the initial load is done things are fairly fast. 


## How do I get set up? ##

### Building the project ###



### Provisioning rules ###
The MVProvisioningExtension must be set to SimpleMIM.ProvisionExt.
The following file must be created: C:\config\provRules.json. 

```
#!json

[
    {
        "Id":  "AnyUniqueNameWillDo",
        "Agent": "TestAgent MA",
        "RuleType": "Python",
        "Condition": "entry['departmentId'].Value == 'FABRIKAM'",
        "SourceObject": "person",
        "TargetObject": "FabrikanUser",
        "InitialFlows": [ "InitialFlowRuleForFabrikamUser" ]
    }
]
```

### Flow rules ###
In the (advanced) attribute flow, the name of the flow must be equal to the "Name" property and the target attribute of the flow must match the "Target" property.  
Works for both export and import. Also the specified attributes must be available in csentry (import) or mventry (export). 
The following file must be created: C:\config\flowRules.json.

```
#!json

[
    {
        "Name": "LowerCaser",
        "Expression": "entry['FirstName'].Value.lower() + ' ' + entry['LastName'].Value.lower()",
        "ExpressionType": "Python",
        "Target": "displayName"
    } 
]
```

### Plans for the future ###
* Supporting multiple lines of python logic
* Loading entire script files of python to have at your beck and call 
* Simple, non-python, configurable string manipulation for performance reasons 
* Overhaul of term usage

### Contact ###

I can be contacted at ja.kaalen@gmail.com.