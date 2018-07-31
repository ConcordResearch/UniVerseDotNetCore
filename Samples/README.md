# Logical Flow

* You first call the ```Filter``` endpoints:
  + ```https://localhost/api/MaintFilter``` for filtering on maintenance accounts, and 
  + ```https://localhost/api/ContractsFilter``` for Loans.

* The ```Filter``` responds with the *listName* (CSS internally stored list of filtered accounts). Use that list name as input to the ChangeXXXCode endpoints, for example "AGHERRERA.MAINT.0TZHOZM4" in the getListName object in the POST when calling ```https://localhost/api/ChangeAccountCode```

```json
{ 
	"file": {
        "fileName": "MAINT"
    },
    "newCode": {
        "code": "X"
    },
    "typeOfChange": "AccountCode",
    "getListName": {
        "accountListName": "AGHERRERA.MAINT.0TZHOZM4"
    },
     "note": {
        "message": "This is MY note."
    },
    "Credentials": {
    "User": "{{Username}}",
    "UserPassword": "{{Password}}",
      "Hostname": "{{Hostname}}",
    "Account": "CONCORD",
    "ServiceType": "uvcs"
   }
}
```

# Variables
You must valid credentials to connect. The model is:
```json
 "Credentials": {
    "User": "{{Username}}",
    "UserPassword": "{{Password}}",
      "Hostname": "{{Hostname}}",
    "Account": "CONCORD",
    "ServiceType": "uvcs"
  }
```
