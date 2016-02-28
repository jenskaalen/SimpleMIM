# SimpleMIM #

SimpleMIM is a helper framework for defining flow and provision rules without using compiled code.  

## How do I get set up? ##


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
In the (advanced) attribute flow, the name of the flow must be equal to the "Name" property and the target attribute of the flow must match the "Target" property. Works for both export and import. Also the specified attributes must be available in csentry (import) or mventry (export). 
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

### Contact ###

I can be contacted at ja.kaalen@gmail.com. 