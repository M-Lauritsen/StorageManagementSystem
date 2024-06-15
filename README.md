Run migrations and update db
dotnet ef migrations add init --startup-project ../StorageManagement.API
dotnet ef database update --startup-project ../StorageManagement.API
