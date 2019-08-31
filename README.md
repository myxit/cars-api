## REST endpoint for api/users/
Sticky and viscous [enterprise] application structure approach :)

## Deploying
DO NOT SUPPORT MIGRATIONS as a part of docker build stage due to problem with passing connection credentials for command `dotnet ef database update`.
So apply DB migrations manually!
