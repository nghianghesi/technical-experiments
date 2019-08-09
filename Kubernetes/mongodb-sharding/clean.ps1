. ./config.ps1
Write-Host "Deleting config nodes"
kubectl delete -f  mongo_config.yaml

Write-Host "`nDeleting shard nodes"
for ($rs=1; $rs -le $SHARD_REPLICA_SET; $rs++){
    kubectl delete -f  mongo_sh_$rs.yaml
}

Write-Host  "`nDeleting router nodes"
kubectl delete -f mongos.yaml
