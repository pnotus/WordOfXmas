az vm auto-shutdown --name woxmvm --resource-group woxm-compute --time ((Get-Date).AddHours(8).ToUniversalTime().ToString("HHmm"))
az vm start --name woxmvm --resource-group woxm-compute