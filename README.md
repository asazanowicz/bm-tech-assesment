# Bank Millenium Calendar Appointment API

## Description
Build an interview calendar API that manages availability time slots between candidates and interviers in order to provide possible slot times where a interview may occour.

## TechStack required
- .NET Core 3.1 
- C#

## Additional notes
- There's no need to have a sophisticated persisting mechanism implemented
- You can use public nuget packages
- All API are public, no authentication mechanism required

## Endpoints

### Define person availability

> POST /calendar/availability
```
{
    "name": "{person name}",
    "role": "{person role}"
    "slots": [
        {
            "dateStart": "{start date}",
            "dateEnd": "{end date}"
        },
        {
            "dateStart": "{start date}",
            "dateEnd": "{end date}"
        }
    ]
}
```

### Get availability slots

> GET /calendar/availability/{person c}?interviewers={person a}&interviewers={person b}
```
[
    {
        "dateStart": "{start date}",
        "dateEnd": "{end date}"
    },
    {
        "dateStart": "{start date}",
        "dateEnd": "{end date}"
    }
]
```


### Migration
The solution uses SQLite persisting model. Sample data should be seeded while migration process. 
To execute migration in Shell: 
> Add-Migration Initial -Project BM.API



### Description
-The projest uses REST API controller in pesentation layer [BM.API].
-Domain layer coresponds to domain logic and implements repositorium paattern [BM.Domain].
-[BM.DataAccess] contains db context and entities as well as mappings to model be created during migrations.
- Automapper uses to map entities and dto models 
- Serilog as a logging projede to log to json files.
- Dependency injection used to inject domain interfaces
- Swagger Open API specification is available on 'api/swagger/BMAPISpecification/swagger.json' with /swagger/index.html
  
The test project does not implement any authentication model and well as login functionality.

