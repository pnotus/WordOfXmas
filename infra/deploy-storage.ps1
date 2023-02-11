az group create --name woxm-data --location westus3
az deployment group create --resource-group woxm-data --template-file .\storage.bicep