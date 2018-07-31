# Logical Flow

* You first call the ```Filter``` endpoints:
  + ```https://localhost/api/MaintFilter``` for filtering on maintenance accounts, and 
  + ```https://localhost/api/ContractsFilter``` for Loans.
  
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
