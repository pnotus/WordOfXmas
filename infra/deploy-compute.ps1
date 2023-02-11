az group create --name woxm-compute --location westus3
az vm create --name woxmvm --resource-group woxm-compute --image UbuntuLTS --size Standard_F16s_v2 --public-ip-sku Standard --custom-data .\cloud-init.txt
az vm auto-shutdown --name woxmvm --resource-group woxm-compute --time ((Get-Date).AddHours(8).ToUniversalTime().ToString("HHmm"))