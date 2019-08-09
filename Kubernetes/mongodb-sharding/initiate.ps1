. ./config.ps1

#Creating config nodes
kubectl create -f  mongo_config.yaml | Out-String

#Waiting for containers
Write-Output "Waiting config containers"
$POD_STATUS = kubectl get pods | Select-String "mongocfg" | Select-String "ContainerCreating"
while ( $POD_STATUS.Length -gt 0 ){
  Start-Sleep -s 1
  Write-Output "`n`nWaiting the following containers:"
  $POD_STATUS = kubectl get pods | Select-String "mongocfg" | Select-String "ContainerCreating"
}


#Initializating configuration nodes
$POD_NAME=kubectl get pods | Select-String "mongocfg1" | Select-Object -First 1 
$POD_NAME=$POD_NAME.ToString().Split()[0]
Write-OutPut "Initializating config replica set... connecting to: $POD_NAME"
$CMD='rs.initiate({ _id : `"cfgrs`", configsvr: true, members: [{ _id : 0, host : `"mongocfg1:27019`" },{ _id : 1, host : `"mongocfg2:27019`" },{ _id : 2, host : `"mongocfg3:27019`" }]})'
Write-OutPut $CMD
kubectl exec -it $POD_NAME -- bash -c "mongo --port 27019 --eval '$CMD'" | Out-String



#Creating shard nodes
for ($rs=1; $rs -le $SHARD_REPLICA_SET; $rs++) {
    kubectl create -f  mongo_sh_$rs.yaml | Out-String
}


#Waiting for containers
$POD_STATUS = kubectl get pods | Select-String "mongosh" | Select-String "ContainerCreating"
Write-OutPut  "Waiting shard containers"
while ($POD_STATUS.Length -gt 0)
{
  Start-Sleep -s 1
  Write-OutPut "`n`nWaiting the following containers:"
  $POD_STATUS = kubectl get pods | Select-String "mongosh" | Select-String "ContainerCreating"
  Write-OutPut $POD_STATUS 
}



#Initializating shard nodes
for ($rs=1; $rs -le $SHARD_REPLICA_SET; $rs++){
    Write-OutPut "`n`n---------------------------------------------------"
    Write-OutPut "Initializing mongosh$rs"

    #Retriving pod name
    $POD_NAME=kubectl get pods | Select-String "mongosh$rs-1" | Select-Object -First 1 
    $POD_NAME=$POD_NAME.ToString().Split()[0]
    Write-OutPut "Pod Name: $POD_NAME"
    $CMD = "rs.initiate({ _id : ```"rs$rs```", members: [{ _id : 0, host : ```"mongosh$rs-1:27017```" },{ _id : 1, host : ```"mongosh$rs-2:27017```" },{ _id : 2, host : ```"mongosh$rs-3:27017```" }]})"
    #Executing cmd inside pod
    Write-OutPut $CMD
    kubectl exec -it $POD_NAME -- bash -c "mongo --eval '$CMD'" | Out-String
}


#Initializing routers
kubectl create -f mongos.yaml | Out-String
Write-OutPut  "Waiting router containers"
$POD_STATUS = kubectl get pods | Select-String "mongos[0-9]" | Select-String "ContainerCreating"
while ($POD_STATUS.Length -gt 0){
  Start-Sleep -s 1
  Write-OutPut "`n`nWaiting the following containers:"
  $POD_STATUS = kubectl get pods | Select-String "mongos[0-9]" | Select-String "ContainerCreating"
  Write-OutPut $POD_STATUS 
}


#Adding shard to cluster
#Retriving pod name
$POD_NAME=kubectl get pods | Select-String "mongos1" | Select-Object -First 1 
$POD_NAME=$POD_NAME.ToString().Split()[0]
for ($rs=1; $rs -le $SHARD_REPLICA_SET; $rs++){
    Write-OutPut  "`n`n---------------------------------------------------"
    Write-OutPut  "Adding rs$rs to cluster"
    Write-OutPut  "Pod Name: $POD_NAME"

    $CMD="sh.addShard(```"rs$rs/mongosh$rs-1:27017```")"
    #Executing cmd inside pod
    Write-OutPut  $CMD
    kubectl exec -it $POD_NAME -- bash -c "mongo --eval '$CMD'" | Out-String
}

Write-OutPut  "All done!!!"