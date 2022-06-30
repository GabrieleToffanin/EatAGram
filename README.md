# EatagramBackend

## Simple ASP .NET Core API for managing Recipes based social

<img src=".github/build.svg" alt="Build and Deploy - EatagramApi" width="100%">

### Storage :

This Api purpouse is to enable mostly `CRUD` operations persistent in the database.

- `MongoDB` :
  > Is used to maintain chats between users, so when a Reconnection happens users can reopen older chats.
- `EntityFramework` :
  > Uses Entity framework for persisting Users, Recipes, Comments etc... it is the main data persister.

### Authentication :

- `JWT`:
  > Actually the authentication is managed by Asp .Net Core Identity and JSON web token Bearer Auth. Possibly in the future the auth will be managed by `Microsoft` service.

### Deployment :

[!IMPORTANT] Still trying to understeand why dotnet test launched in the pipeline is getting stuck.

- `YML Pipeline` :
  > The pipeline [Azure-WebApps-Core-Deployment](.github/workflows/azure-webapps-dotnet-core.yml) starts on `main` branch when push, builds and creates an artifacts of the ZIP compressed application and then publish it on the Azure WebService
