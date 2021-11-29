# azure-function-net6-isolated



az functionapp show --name rhlisolated --resource-group net6

az functionapp config appsettings set --settings FUNCTIONS_EXTENSION_VERSION=~4 -n rhlisolated -g net6

# For Windows function apps only, also enable .NET 6.0 that is needed by the runtime
az functionapp config set --net-framework-version v6.0 -n rhlisolated -g net6


https://stackoverflow.com/questions/60994786/return-immediately-in-httptrigger-on-azure-function
https://philippbauknecht.medium.com/authentication-authorization-in-azure-functions-with-azure-active-directory-using-c-net-aad52c8de925