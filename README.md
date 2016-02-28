# SimpleMIM #

SimpleMIM is a helper framework for defining flow and provision rules without using compiled code. 


### How it works ###
Json-files with defined rules will be loaded at runtime. 
Conditions or expressions are written as one-line python commands, and the returned value is used by MIM. 

### Why use this? ###
You can use this framework if you value configuration over performance, and the problem you are trying to solve is somewhat sane. 
For big organizations with very complex logic in their MIM you might not be able to reach your goals with SimpleMIM. It lies in the name - this is better for simple scenarios.  
Be mindful of that designing your MIM provisioning and flows to be as simple as possible is preferable in any case.  

### Performance ###
I'm not too sure yet. Yes, interpreted code is slow, but MIM is inherently slow so the overhead might not be too noticeable. 
The python runspace and all the script functions are loaded into memory at initialization so when the initial load is done things are fairly fast. 


## How do I get set up? ##

### Building the project ###
After you clone the repo and build the solution, nuget packages will be restored. 
Simply copy all the built dlls into your Extensions-folder. 

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

Id: The unique ID. Cannot contain spaces.  
Agent: Management agent name.  
RuleType: Only Python is supported.  
Condition: One-lined conditional in python.  
SourceObject: MVEntry object type.  
TargetObject: Object type in management agent.   
InitialFlows: Upon provisioning, these flowrules (must match existing flow rules) will be applied to the new csentry object before commiting it.  
  

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

Name: The unique name. Can not contain spaces.  
Expression: One-line python expression which sets the target to computed value.  
ExpressionType: Only Python is supported.  
Target: The name of the target-attribute. Csentry-attribute for export and mventry for import.  

## Misc## 


### Plans for the future ###
* Supporting multiple lines of python logic.  
* Loading entire script files of python to have at your beck and call.  
* Simple, non-python, configurable string manipulation for performance reasons.   
* Overhaul of term usage.  
* Setting ReferenceDn property of csentries (for AD MAs).  

### Contact ###

I can be contacted at ja.kaalen@gmail.com.